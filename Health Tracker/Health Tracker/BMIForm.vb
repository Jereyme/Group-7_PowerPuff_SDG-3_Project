Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class BMIForm
    ' --- Colors ---
    Private ReadOnly PrimaryGreen As Color = Color.FromArgb(32, 156, 120)
    Private ReadOnly TextDark As Color = Color.FromArgb(45, 55, 72)

    ' --- Variables ---
    Private isDragging As Boolean = False
    Private mouseOffset As Point

    ' Input fields + result label
    Private txtWeight As TextBox
    Private txtHeight As TextBox
    Private cmbUnit As ComboBox
    Private lblResult As Label

    Private Sub BMIForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Form Setup
        Me.Size = New Size(750, 520)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White
        Me.Region = New Region(CreateRoundedRectangle(0, 0, Me.Width, Me.Height, 20))

        ' 2. Left Side Panel (Sidebar)
        Dim pnlLeft As New Panel With {
            .Size = New Size(300, Me.Height),
            .BackColor = PrimaryGreen,
            .Location = New Point(0, 0)
        }
        Me.Controls.Add(pnlLeft)

        ' Sidebar Branding
        Dim lblIcon As New Label With {.Text = "⚖️", .Font = New Font("Segoe UI Emoji", 55), .ForeColor = Color.White, .Location = New Point(80, 80), .AutoSize = True}
        Dim lblBrand As New Label With {.Text = "Health Tracker", .Font = New Font("Segoe UI", 18, FontStyle.Bold), .ForeColor = Color.White, .Location = New Point(55, 180), .AutoSize = True}
        Dim lblSub As New Label With {.Text = "BMI Calculator", .Font = New Font("Segoe UI", 11), .ForeColor = Color.FromArgb(200, 240, 225), .Location = New Point(95, 215), .AutoSize = True}
        pnlLeft.Controls.AddRange({lblIcon, lblBrand, lblSub})

        ' 3. Right Side (Inputs & Buttons)
        Dim rightX As Integer = 370
        Dim lblHeader As New Label With {.Text = "Body Mass Index", .Font = New Font("Segoe UI", 22, FontStyle.Bold), .ForeColor = TextDark, .Location = New Point(rightX, 50), .AutoSize = True}
        Me.Controls.Add(lblHeader)

        ' Input Rows
        txtWeight = CreateInputRow("Weight (kg)", 150, rightX)
        txtHeight = CreateInputRow("Height Value", 230, rightX)

        ' Unit ComboBox
        Dim lblUnit As New Label With {.Text = "Height Unit", .Location = New Point(rightX, 270), .AutoSize = True, .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = Color.Gray}
        cmbUnit = New ComboBox With {.Location = New Point(rightX, 290), .Size = New Size(300, 30), .Font = New Font("Segoe UI", 12)}
        cmbUnit.Items.AddRange(New String() {"cm", "m", "ft"})
        cmbUnit.SelectedIndex = 0
        Me.Controls.Add(lblUnit)
        Me.Controls.Add(cmbUnit)

        ' Result label
        lblResult = New Label With {.Text = "", .Location = New Point(rightX, 330), .AutoSize = True, .Font = New Font("Segoe UI", 12, FontStyle.Bold), .ForeColor = TextDark}
        Me.Controls.Add(lblResult)

        ' MAIN CALCULATE BUTTON
        Dim btnCalc As New Button With {.Text = "Calculate BMI", .Size = New Size(300, 50), .Location = New Point(rightX, 370), .BackColor = PrimaryGreen, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        btnCalc.FlatAppearance.BorderSize = 0
        AddHandler btnCalc.Click, AddressOf BtnCalc_Click
        Me.Controls.Add(btnCalc)

        ' Back Button
        Dim btnBackToDash As New Button With {.Text = "Back to Dashboard", .Size = New Size(300, 45), .Location = New Point(rightX, 430), .FlatStyle = FlatStyle.Flat, .ForeColor = PrimaryGreen, .BackColor = Color.White, .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Cursor = Cursors.Hand}
        btnBackToDash.FlatAppearance.BorderColor = PrimaryGreen
        btnBackToDash.FlatAppearance.BorderSize = 1
        AddHandler btnBackToDash.Click, Sub() Me.Close()
        Me.Controls.Add(btnBackToDash)

        ' Dragging Support
        AddHandler pnlLeft.MouseDown, AddressOf DragForm_MouseDown
        AddHandler pnlLeft.MouseMove, AddressOf DragForm_MouseMove
        AddHandler pnlLeft.MouseUp, AddressOf DragForm_MouseUp
    End Sub

    ' --- Utility Functions ---
    Private Function CreateInputRow(labelName As String, y As Integer, x As Integer) As TextBox
        Dim lbl As New Label With {.Text = labelName, .Location = New Point(x, y), .AutoSize = True, .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = Color.Gray}
        Dim txt As New TextBox With {.Location = New Point(x, y + 25), .Size = New Size(300, 30), .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(lbl)
        Me.Controls.Add(txt)
        Return txt
    End Function

    Private Function CreateRoundedRectangle(x As Integer, y As Integer, w As Integer, h As Integer, r As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        path.AddArc(x, y, r * 2, r * 2, 180, 90)
        path.AddArc(w - (r * 2), y, r * 2, r * 2, 270, 90)
        path.AddArc(w - (r * 2), h - (r * 2), r * 2, r * 2, 0, 90)
        path.AddArc(x, h - (r * 2), r * 2, r * 2, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    ' --- Backend Logic Hook ---
    Private Sub BtnCalc_Click(sender As Object, e As EventArgs)
        Dim weight As Double
        Dim height As Double
        Dim unit As String = cmbUnit.SelectedItem.ToString()

        If Double.TryParse(txtWeight.Text, weight) AndAlso Double.TryParse(txtHeight.Text, height) Then
            lblResult.Text = HealthLogic.BMIChecker(weight, height, unit, Dashboard.CurrentUser)
        Else
            MessageBox.Show("Please enter valid numbers for weight and height.")
        End If
    End Sub

    Private Sub DragForm_MouseDown(sender As Object, e As MouseEventArgs)
        isDragging = True : mouseOffset = New Point(-e.X, -e.Y)
    End Sub

    Private Sub DragForm_MouseMove(sender As Object, e As MouseEventArgs)
        If isDragging Then
            Dim mPos = Control.MousePosition : mPos.Offset(mouseOffset.X, mouseOffset.Y)
            Me.Location = mPos
        End If
    End Sub

    Private Sub DragForm_MouseUp(sender As Object, e As MouseEventArgs)
        isDragging = False
    End Sub
End Class