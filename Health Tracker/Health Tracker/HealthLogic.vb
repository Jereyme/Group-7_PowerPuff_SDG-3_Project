Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Configuration
Imports System.Data.SqlClient

Public Class HealthLogic

    ' --- Hashing ---
    Private Shared Function HashPassword(password As String, salt As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(password & salt)
            Dim hash = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    ' --- Save Log ---
    Public Shared Sub SaveLog(userId As Integer, action As String)
        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim cmd As New SqlCommand("INSERT INTO logs (user_id, action, log_time) VALUES (@uid, @act, GETDATE())", conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            cmd.Parameters.AddWithValue("@act", action)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' --- Helper: Get UserId ---
    Public Shared Function GetUserId(username As String) As Integer
        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT user_id FROM users WHERE username=@user", conn)
            cmd.Parameters.AddWithValue("@user", username)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function

    ' --- Register User ---
    Public Shared Function RegisterUser(fullName As String, username As String, password As String) As String
        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            Return "Username and password cannot be empty."
        End If

        Dim salt As String = Guid.NewGuid().ToString()
        Dim hashedPassword As String = HashPassword(password, salt)

        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()

            ' Check if username exists
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM users WHERE username=@user", conn)
            checkCmd.Parameters.AddWithValue("@user", username)
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            If exists > 0 Then
                Return "Username already exists."
            End If

            ' Insert new user
            Dim cmd As New SqlCommand("INSERT INTO users (full_name, username, password_hash, salt, role, created_at) 
                                       VALUES (@name, @user, @pass, @salt, 'USER', GETDATE())", conn)
            cmd.Parameters.AddWithValue("@name", fullName)
            cmd.Parameters.AddWithValue("@user", username)
            cmd.Parameters.AddWithValue("@pass", hashedPassword)
            cmd.Parameters.AddWithValue("@salt", salt)
            cmd.ExecuteNonQuery()

            ' Log registration
            Dim userId As Integer = GetUserId(username)
            SaveLog(userId, "Registered")
        End Using

        Return "Success"
    End Function

    ' --- Validate User ---
    Public Shared Function ValidateUser(username As String, password As String) As String
        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()

            Dim cmd As New SqlCommand("SELECT user_id, password_hash, salt FROM users WHERE username=@user", conn)
            cmd.Parameters.AddWithValue("@user", username)
            Dim reader = cmd.ExecuteReader()

            If Not reader.Read() Then
                Return "User not found. Please register."
            End If

            Dim userId As Integer = Convert.ToInt32(reader("user_id"))
            Dim storedHash As String = reader("password_hash").ToString()
            Dim storedSalt As String = reader("salt").ToString()
            reader.Close()

            Dim hashedInput As String = HashPassword(password, storedSalt)

            If storedHash <> hashedInput Then
                Return "Incorrect password."
            End If

            ' Log login
            SaveLog(userId, "Logged In")
        End Using

        Return "Success"
    End Function

    ' --- Daily Assessment ---
    Public Shared Function DailyAssessment(water As Integer, sleep As Integer, activity As Integer, username As String) As String
        Dim score As Integer = 0
        If water >= 8 Then score += 1
        If sleep >= 7 Then score += 1
        If activity >= 30 Then score += 1

        Dim result As String
        Select Case score
            Case 3 : result = "Excellent! You met all health goals today."
            Case 2 : result = "Good job! You met most goals."
            Case 1 : result = "Needs improvement."
            Case Else : result = "Poor health habits today."
        End Select

        SaveHistory(username, "Daily", result)
        SaveLog(GetUserId(username), "Daily Assessment Saved")

        Return result
    End Function

    ' --- BMI Checker ---
    Public Shared Function BMIChecker(weight As Double, heightValue As Double, unit As String, username As String) As String
        Dim heightM As Double
        Select Case unit.ToLower()
            Case "cm" : heightM = heightValue / 100
            Case "m" : heightM = heightValue
            Case "ft" : heightM = heightValue * 0.3048
            Case Else : Return "Invalid unit."
        End Select

        Dim bmi As Double = weight / (heightM * heightM)
        Dim status As String

        If bmi < 18.5 Then
            status = "Underweight"
        ElseIf bmi < 24.9 Then
            status = "Normal"
        ElseIf bmi < 29.9 Then
            status = "Overweight"
        Else
            status = "Obese"
        End If


        Dim result As String = $"BMI: {bmi:F2} ({status})"
        SaveHistory(username, "BMI", result)
        SaveLog(GetUserId(username), "BMI Checked")

        Return result
    End Function

    ' --- Symptoms Checker ---
    Public Shared Function SymptomsChecker(soreThroat As Boolean, headache As Boolean, cough As Boolean, chestPain As Boolean, username As String) As String
        Dim symptomsCount As Integer = 0
        If soreThroat Then symptomsCount += 1
        If headache Then symptomsCount += 1
        If cough Then symptomsCount += 1
        If chestPain Then symptomsCount += 1

        Dim result As String
        Select Case symptomsCount
            Case 0 : result = "No symptoms detected."
            Case 1 : result = "Mild symptom detected."
            Case 2 : result = "Multiple symptoms detected."
            Case Else : result = "Severe symptoms detected."
        End Select

        SaveHistory(username, "Symptoms", result)
        SaveLog(GetUserId(username), "Symptoms Checked")

        Return result
    End Function

    ' --- Save History ---
    Public Shared Sub SaveHistory(username As String, category As String, result As String)
        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim getUserCmd As New SqlCommand("SELECT user_id FROM users WHERE username=@user", conn)
            getUserCmd.Parameters.AddWithValue("@user", username)
            Dim userId As Integer = Convert.ToInt32(getUserCmd.ExecuteScalar())

            Dim cmd As New SqlCommand("INSERT INTO health_records (user_id, category, details, recorded_at) VALUES (@uid, @cat, @det, GETDATE())", conn)
            cmd.Parameters.AddWithValue("@uid", userId)
            cmd.Parameters.AddWithValue("@cat", category)
            cmd.Parameters.AddWithValue("@det", result)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' --- Load History ---
    Public Shared Function LoadHistory(username As String) As List(Of String)
        Dim results As New List(Of String)
        Dim connString As String = ConfigurationManager.ConnectionStrings("HealthTrackerDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()
            Dim cmd As New SqlCommand("
                SELECT h.recorded_at, h.category, h.details
                FROM health_records h
                JOIN users u ON h.user_id = u.user_id
                WHERE u.username=@user", conn)
            cmd.Parameters.AddWithValue("@user", username)

            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                results.Add($"{reader("recorded_at")} | {reader("category")} | {reader("details")}")
            End While
            reader.Close()
        End Using
        Return results
    End Function

End Class
