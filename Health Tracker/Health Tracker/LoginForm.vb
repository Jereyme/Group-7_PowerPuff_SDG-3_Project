Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class LoginForm

    Private ReadOnly PrimaryGreen As Color = Color.FromArgb(32, 156, 120)
    Private ReadOnly HoverGreen As Color = Color.FromArgb(20, 125, 90)
    Private ReadOnly TextDark As Color = Color.FromArgb(45, 55, 72)
    Private ReadOnly TextLight As Color = Color.FromArgb(150, 160, 165)
    Private ReadOnly LineInactive As Color = Color.FromArgb(220, 225, 230)

    Private pnlLeft As Panel
    Private pnlUserLine As Panel
    Private pnlPassLine As Panel

    Private isDragging As Boolean = False
    Private mouseOffset As Point

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Health Tracker - Login"
        Me.Size = New Size(750, 480)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.White

        Me.Region = New Region(CreateRoundedRectangle(0, 0, Me.Width, Me.Height, 20))

        pnlLeft = New Panel With {
            .Size = New Size(300, Me.Height),
            .Location = New Point(0, 0),
            .BackColor = PrimaryGreen
        }

        AddHandler pnlLeft.MouseDown, AddressOf DragForm_MouseDown
        AddHandler pnlLeft.MouseMove, AddressOf DragForm_MouseMove
        AddHandler pnlLeft.MouseUp, AddressOf DragForm_MouseUp

        Dim lblIcon As New Label With {
            .Text = "🏃",
            .Font = New Font("Segoe UI", 60, FontStyle.Bold),
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
            .Location = New Point(55, 220),
            .BackColor = Color.Transparent
        }

        Dim lblSub As New Label With {
            .Text = "Secure Patient Management",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(200, 240, 225),
            .AutoSize = True,
            .Location = New Point(57, 260),
            .BackColor = Color.Transparent
        }

        pnlLeft.Controls.Add(lblIcon)
        pnlLeft.Controls.Add(lblBrand)
        pnlLeft.Controls.Add(lblSub)
        Me.Controls.Add(pnlLeft)

        Dim rightOffsetX As Integer = 380

        Dim lblLoginHead As New Label With {
            .Text = "Sign In",
            .Font = New Font("Segoe UI", 22, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = True,
            .Location = New Point(rightOffsetX, 60)
        }
        Me.Controls.Add(lblLoginHead)

        Dim lblUser As New Label With {
            .Text = "USERNAME",
            .Font = New Font("Segoe UI", 8, FontStyle.Bold),
            .ForeColor = TextLight,
            .AutoSize = True,
            .Location = New Point(rightOffsetX, 150)
        }
        Me.Controls.Add(lblUser)

        Dim lblPass As New Label With {
            .Text = "PASSWORD",
            .Font = New Font("Segoe UI", 8, FontStyle.Bold),
            .ForeColor = TextLight,
            .AutoSize = True,
            .Location = New Point(rightOffsetX, 240)
        }
        Me.Controls.Add(lblPass)

        txtUsername.Location = New Point(rightOffsetX, 175)
        txtUsername.Size = New Size(300, 30)
        txtUsername.BorderStyle = BorderStyle.None
        txtUsername.BackColor = Color.White
        txtUsername.ForeColor = TextDark
        txtUsername.Font = New Font("Segoe UI", 12)

        txtPassword.Location = New Point(rightOffsetX, 265)
        txtPassword.Size = New Size(300, 30)
        txtPassword.BorderStyle = BorderStyle.None
        txtPassword.BackColor = Color.White
        txtPassword.ForeColor = TextDark
        txtPassword.Font = New Font("Segoe UI", 12)
        txtPassword.PasswordChar = "●"c

        pnlUserLine = New Panel With {
            .BackColor = LineInactive,
            .Size = New Size(300, 2),
            .Location = New Point(rightOffsetX, txtUsername.Bottom + 2)
        }

        pnlPassLine = New Panel With {
            .BackColor = LineInactive,
            .Size = New Size(300, 2),
            .Location = New Point(rightOffsetX, txtPassword.Bottom + 2)
        }

        Me.Controls.Add(pnlUserLine)
        Me.Controls.Add(pnlPassLine)

        AddHandler txtUsername.Enter, Sub() pnlUserLine.BackColor = PrimaryGreen
        AddHandler txtUsername.Leave, Sub() pnlUserLine.BackColor = LineInactive
        AddHandler txtPassword.Enter, Sub() pnlPassLine.BackColor = PrimaryGreen
        AddHandler txtPassword.Leave, Sub() pnlPassLine.BackColor = LineInactive

        With btnLogin
            .Location = New Point(rightOffsetX, 330)
            .Size = New Size(300, 45)
            .BackColor = PrimaryGreen
            .ForeColor = Color.White
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .Font = New Font("Segoe UI", 12, FontStyle.Bold)
            .Cursor = Cursors.Hand
            .Region = New Region(CreateRoundedRectangle(0, 0, .Width, .Height, 10))
        End With

        With btnBack
            .Text = "Create an Account"
            .Location = New Point(rightOffsetX, 390)
            .Size = New Size(300, 40)
            .BackColor = Color.White
            .ForeColor = PrimaryGreen
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 1
            .FlatAppearance.BorderColor = PrimaryGreen
            .Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .Cursor = Cursors.Hand
            .Region = New Region(CreateRoundedRectangle(0, 0, .Width, .Height, 10))
        End With

        AddHandler btnLogin.MouseEnter, Sub() btnLogin.BackColor = HoverGreen
        AddHandler btnLogin.MouseLeave, Sub() btnLogin.BackColor = PrimaryGreen

        AddHandler btnBack.MouseEnter, Sub()
                                           btnBack.BackColor = PrimaryGreen
                                           btnBack.ForeColor = Color.White
                                       End Sub

        AddHandler btnBack.MouseLeave, Sub()
                                           btnBack.BackColor = Color.White
                                           btnBack.ForeColor = PrimaryGreen
                                       End Sub

        Dim btnClose As New Button With {
            .Text = "✕",
            .Size = New Size(40, 40),
            .Location = New Point(Me.Width - 45, 10),
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.FromArgb(170, 170, 170),
            .BackColor = Color.Transparent,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 240, 240)
        btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 150, 150)

        AddHandler btnClose.Click, Sub() Application.Exit()

        Me.Controls.Add(btnClose)
        btnClose.BringToFront()

        AddHandler Me.MouseDown, AddressOf DragForm_MouseDown
        AddHandler Me.MouseMove, AddressOf DragForm_MouseMove
        AddHandler Me.MouseUp, AddressOf DragForm_MouseUp

    End Sub

    Private Function CreateRoundedRectangle(x As Integer, y As Integer, width As Integer, height As Integer, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()

        path.AddArc(x, y, radius * 2, radius * 2, 180, 90)
        path.AddArc(width - (radius * 2), y, radius * 2, radius * 2, 270, 90)
        path.AddArc(width - (radius * 2), height - (radius * 2), radius * 2, radius * 2, 0, 90)
        path.AddArc(x, height - (radius * 2), radius * 2, radius * 2, 90, 90)

        path.CloseFigure()

        Return path
    End Function

    ' LOGIN BUTTON
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) Then

            MessageBox.Show("Please enter both username and password.",
                            "Authentication Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        Dim result As String = HealthLogic.ValidateUser(username, password)

        If result = "Success" Then

            Dashboard.CurrentUser = username

            Dim dash As New Dashboard()
            dash.Show()

            Me.Hide()

        Else

            MessageBox.Show(result,
                            "Login Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim reg As New RegisterForm()
        reg.Show()
        Me.Hide()
    End Sub

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