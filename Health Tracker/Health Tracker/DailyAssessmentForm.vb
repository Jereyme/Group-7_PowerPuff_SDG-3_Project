Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class DailyAssessmentForm

    ' --- Color Palette ---
    Private ReadOnly PrimaryGreen As Color = Color.FromArgb(32, 156, 120)
    Private ReadOnly HoverGreen As Color = Color.FromArgb(20, 125, 90)
    Private ReadOnly TextDark As Color = Color.FromArgb(45, 55, 72)

    ' --- Form Variables ---
    Private pnlLeft As Panel
    Private isDragging As Boolean = False
    Private mouseOffset As Point

    ' --- Input references + result label ---
    Private txtWater As TextBox
    Private txtSleep As TextBox
    Private txtActivity As TextBox
    Private lblResult As Label

    Private Sub DailyAssessmentForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' 1. Form Setup
        Me.Size = New Size(750, 560) ' Increased height
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.White
        Me.Region = New Region(CreateRoundedRectangle(0, 0, Me.Width, Me.Height, 20))

        ' 2. Left Panel
        pnlLeft = New Panel With {
            .Size = New Size(300, Me.Height),
            .Location = New Point(0, 0),
            .BackColor = PrimaryGreen
        }

        ' LOGO
        Dim lblIcon As New Label With {
            .Text = "📋",
            .Font = New Font("Segoe UI Emoji", 60, FontStyle.Regular),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(80, 120),
            .BackColor = Color.Transparent
        }

        Dim lblBrand As New Label With {
            .Text = "Health Tracker",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(55, 230),
            .BackColor = Color.Transparent
        }

        Dim lblSub As New Label With {
            .Text = "Daily Assessment",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(200, 240, 225),
            .AutoSize = True,
            .Location = New Point(90, 270),
            .BackColor = Color.Transparent
        }

        pnlLeft.Controls.Add(lblIcon)
        pnlLeft.Controls.Add(lblBrand)
        pnlLeft.Controls.Add(lblSub)
        Me.Controls.Add(pnlLeft)

        ' Right Side Setup
        Dim rightX As Integer = 350

        Dim lblTitle As New Label With {
            .Text = "Log Your Day",
            .Font = New Font("Segoe UI", 22, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = True,
            .Location = New Point(rightX, 40)
        }
        Me.Controls.Add(lblTitle)

        ' Inputs
        txtWater = CreateInputRow("Cups of Water:", 110, rightX)
        txtSleep = CreateInputRow("Hours of Sleep:", 180, rightX)
        txtActivity = CreateInputRow("Active Minutes:", 250, rightX)

        ' =====================================
        ' FIXED RESULT LABEL
        ' =====================================
        lblResult = New Label With {
            .Text = "",
            .Location = New Point(rightX, 300),
            .Size = New Size(340, 80),
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopLeft,
            .BorderStyle = BorderStyle.None
        }
        Me.Controls.Add(lblResult)

        ' Submit Button
        Dim btnSubmit As New Button With {
            .Text = "Save Assessment",
            .Location = New Point(rightX, 390),
            .Size = New Size(340, 45),
            .BackColor = PrimaryGreen,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnSubmit.FlatAppearance.BorderSize = 0
        AddHandler btnSubmit.Click, AddressOf BtnSubmit_Click
        Me.Controls.Add(btnSubmit)

        ' Back Button
        Dim btnBack As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(rightX, 445),
            .Size = New Size(340, 40),
            .BackColor = Color.White,
            .ForeColor = PrimaryGreen,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnBack.FlatAppearance.BorderColor = PrimaryGreen
        AddHandler btnBack.Click, Sub() Me.Close()
        Me.Controls.Add(btnBack)

        ' Dragging Logic
        AddHandler pnlLeft.MouseDown, AddressOf DragForm_MouseDown
        AddHandler pnlLeft.MouseMove, AddressOf DragForm_MouseMove
        AddHandler pnlLeft.MouseUp, AddressOf DragForm_MouseUp

    End Sub

    ' Helper Function
    Private Function CreateInputRow(labelName As String, y As Integer, x As Integer) As TextBox

        Dim lbl As New Label With {
            .Text = labelName,
            .Location = New Point(x, y),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
        }

        Dim txt As New TextBox With {
            .Location = New Point(x, y + 25),
            .Size = New Size(340, 30),
            .Font = New Font("Segoe UI", 11)
        }

        Me.Controls.Add(lbl)
        Me.Controls.Add(txt)

        Return txt
    End Function

    ' Backend Logic
    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs)

        Dim water As Integer
        Dim sleep As Integer
        Dim activity As Integer

        If Integer.TryParse(txtWater.Text, water) AndAlso
           Integer.TryParse(txtSleep.Text, sleep) AndAlso
           Integer.TryParse(txtActivity.Text, activity) Then

            lblResult.Text =
                HealthLogic.DailyAssessment(
                    water,
                    sleep,
                    activity,
                    Dashboard.CurrentUser
                )

        Else
            MessageBox.Show("Please enter valid numbers for water, sleep, and activity.")
        End If

    End Sub

    ' Rounded Corners
    Private Function CreateRoundedRectangle(
        x As Integer,
        y As Integer,
        width As Integer,
        height As Integer,
        radius As Integer) As GraphicsPath

        Dim path As New GraphicsPath()

        path.AddArc(x, y, radius * 2, radius * 2, 180, 90)
        path.AddArc(width - (radius * 2), y, radius * 2, radius * 2, 270, 90)
        path.AddArc(width - (radius * 2), height - (radius * 2), radius * 2, radius * 2, 0, 90)
        path.AddArc(x, height - (radius * 2), radius * 2, radius * 2, 90, 90)

        path.CloseFigure()

        Return path
    End Function

    ' Drag Form Logic
    Private Sub DragForm_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            isDragging = True
            mouseOffset = New Point(-e.X, -e.Y)
        End If
    End Sub

    Private Sub DragForm_MouseMove(sender As Object, e As MouseEventArgs)
        If isDragging Then
            Dim mousePos As Point = Control.MousePosition
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Me.Location = mousePos
        End If
    End Sub

    Private Sub DragForm_MouseUp(sender As Object, e As MouseEventArgs)
        isDragging = False
    End Sub

End Class