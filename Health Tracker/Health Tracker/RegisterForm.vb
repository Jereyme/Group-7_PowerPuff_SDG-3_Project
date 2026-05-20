Public Class RegisterForm


    Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Form properties
        Me.Text = "Health Tracker - Register"
        Me.Size = New Size(420, 550)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.FromArgb(245, 250, 245)


        Dim leftPanel As New Panel()
        leftPanel.Size = New Size(8, Me.Height)
        leftPanel.Location = New Point(0, 0)
        leftPanel.BackColor = Color.FromArgb(46, 139, 87)
        Me.Controls.Add(leftPanel)


        Dim lblIcon As New Label()
        lblIcon.Text = "+"
        lblIcon.Font = New Font("Segoe UI", 36, FontStyle.Bold)
        lblIcon.ForeColor = Color.FromArgb(46, 139, 87)
        lblIcon.AutoSize = True
        lblIcon.Location = New Point(180, 25)
        Me.Controls.Add(lblIcon)


        Dim lblTitle As New Label()
        lblTitle.Text = "Create Account"
        lblTitle.Font = New Font("Segoe UI", 20, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(33, 37, 41)
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(100, 80)
        Me.Controls.Add(lblTitle)


        Dim lblSubtitle As New Label()
        lblSubtitle.Text = "Fill in your details to register"
        lblSubtitle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblSubtitle.ForeColor = Color.FromArgb(108, 117, 125)
        lblSubtitle.AutoSize = True
        lblSubtitle.Location = New Point(115, 115)
        Me.Controls.Add(lblSubtitle)

        '
        Dim lblName As New Label()
        lblName.Text = "FULL NAME"
        lblName.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblName.ForeColor = Color.FromArgb(108, 117, 125)
        lblName.AutoSize = True
        lblName.Location = New Point(60, 155)
        Me.Controls.Add(lblName)

        Dim txtName As New TextBox()
        txtName.Size = New Size(298, 35)
        txtName.Location = New Point(61, 175)
        txtName.BackColor = Color.White
        txtName.ForeColor = Color.FromArgb(33, 37, 41)
        txtName.BorderStyle = BorderStyle.None
        txtName.Font = New Font("Segoe UI", 11)
        txtName.Name = "txtName"
        Me.Controls.Add(txtName)

        Dim pnlNameBorder As New Panel()
        pnlNameBorder.Size = New Size(300, 37)
        pnlNameBorder.Location = New Point(60, 174)
        pnlNameBorder.BackColor = Color.FromArgb(222, 226, 230)
        Me.Controls.Add(pnlNameBorder)


        Dim lblUser As New Label()
        lblUser.Text = "USERNAME"
        lblUser.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblUser.ForeColor = Color.FromArgb(108, 117, 125)
        lblUser.AutoSize = True
        lblUser.Location = New Point(60, 225)
        Me.Controls.Add(lblUser)

        Dim txtUser As New TextBox()
        txtUser.Size = New Size(298, 35)
        txtUser.Location = New Point(61, 245)
        txtUser.BackColor = Color.White
        txtUser.ForeColor = Color.FromArgb(33, 37, 41)
        txtUser.BorderStyle = BorderStyle.None
        txtUser.Font = New Font("Segoe UI", 11)
        txtUser.Name = "txtUser"
        Me.Controls.Add(txtUser)

        Dim pnlUserBorder As New Panel()
        pnlUserBorder.Size = New Size(300, 37)
        pnlUserBorder.Location = New Point(60, 244)
        pnlUserBorder.BackColor = Color.FromArgb(222, 226, 230)
        Me.Controls.Add(pnlUserBorder)


        Dim lblPass As New Label()
        lblPass.Text = "PASSWORD"
        lblPass.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblPass.ForeColor = Color.FromArgb(108, 117, 125)
        lblPass.AutoSize = True
        lblPass.Location = New Point(60, 295)
        Me.Controls.Add(lblPass)

        Dim txtPass As New TextBox()
        txtPass.Size = New Size(298, 35)
        txtPass.Location = New Point(61, 315)
        txtPass.BackColor = Color.White
        txtPass.ForeColor = Color.FromArgb(33, 37, 41)
        txtPass.BorderStyle = BorderStyle.None
        txtPass.Font = New Font("Segoe UI", 11)
        txtPass.PasswordChar = "●"c
        txtPass.Name = "txtPass"
        Me.Controls.Add(txtPass)

        Dim pnlPassBorder As New Panel()
        pnlPassBorder.Size = New Size(300, 37)
        pnlPassBorder.Location = New Point(60, 314)
        pnlPassBorder.BackColor = Color.FromArgb(222, 226, 230)
        Me.Controls.Add(pnlPassBorder)


        Dim lblConfirm As New Label()
        lblConfirm.Text = "CONFIRM PASSWORD"
        lblConfirm.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblConfirm.ForeColor = Color.FromArgb(108, 117, 125)
        lblConfirm.AutoSize = True
        lblConfirm.Location = New Point(60, 365)
        Me.Controls.Add(lblConfirm)

        Dim txtConfirm As New TextBox()
        txtConfirm.Size = New Size(298, 35)
        txtConfirm.Location = New Point(61, 385)
        txtConfirm.BackColor = Color.White
        txtConfirm.ForeColor = Color.FromArgb(33, 37, 41)
        txtConfirm.BorderStyle = BorderStyle.None
        txtConfirm.Font = New Font("Segoe UI", 11)
        txtConfirm.PasswordChar = "●"c
        txtConfirm.Name = "txtConfirm"
        Me.Controls.Add(txtConfirm)

        Dim pnlConfirmBorder As New Panel()
        pnlConfirmBorder.Size = New Size(300, 37)
        pnlConfirmBorder.Location = New Point(60, 384)
        pnlConfirmBorder.BackColor = Color.FromArgb(222, 226, 230)
        Me.Controls.Add(pnlConfirmBorder)

        Dim btnRegister As New Button()
        btnRegister.Text = "Create Account"
        btnRegister.Size = New Size(300, 45)
        btnRegister.Location = New Point(60, 445)
        btnRegister.BackColor = Color.FromArgb(46, 139, 87)
        btnRegister.ForeColor = Color.White
        btnRegister.FlatStyle = FlatStyle.Flat
        btnRegister.FlatAppearance.BorderSize = 0
        btnRegister.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnRegister.Cursor = Cursors.Hand
        btnRegister.Name = "btnRegister"
        Me.Controls.Add(btnRegister)


        Dim lblBack As New Label()
        lblBack.Text = "Already have an account? Sign In"
        lblBack.Font = New Font("Segoe UI", 9, FontStyle.Underline)
        lblBack.ForeColor = Color.FromArgb(46, 139, 87)
        lblBack.AutoSize = True
        lblBack.Location = New Point(120, 505)
        lblBack.Cursor = Cursors.Hand
        Me.Controls.Add(lblBack)


        Dim btnClose As New Button()
        btnClose.Text = "✕"
        btnClose.Size = New Size(35, 30)
        btnClose.Location = New Point(375, 5)
        btnClose.BackColor = Color.Transparent
        btnClose.ForeColor = Color.FromArgb(108, 117, 125)
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnClose.Cursor = Cursors.Hand
        AddHandler btnClose.Click, Sub() Application.Exit()
        Me.Controls.Add(btnClose)


        AddHandler btnRegister.Click, AddressOf BtnRegister_Click
        AddHandler lblBack.Click, AddressOf LblBack_Click
        AddHandler btnRegister.MouseEnter, Sub() btnRegister.BackColor = Color.FromArgb(39, 119, 74)
        AddHandler btnRegister.MouseLeave, Sub() btnRegister.BackColor = Color.FromArgb(46, 139, 87)
    End Sub


    Private Sub BtnRegister_Click(sender As Object, e As EventArgs)
        ' Grab values
        Dim name As String = Me.Controls("txtName").Text
        Dim username As String = Me.Controls("txtUser").Text
        Dim password As String = Me.Controls("txtPass").Text
        Dim confirm As String = Me.Controls("txtConfirm").Text

        ' Basic validation
        If String.IsNullOrWhiteSpace(name) OrElse
           String.IsNullOrWhiteSpace(username) OrElse
           String.IsNullOrWhiteSpace(password) OrElse
           String.IsNullOrWhiteSpace(confirm) Then
            MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If password <> confirm Then
            MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Store user in HealthLogic
        Dim result As String = HealthLogic.RegisterUser(Me.Controls("txtName").Text.Trim(),
                                                Me.Controls("txtUser").Text.Trim(),
                                                Me.Controls("txtPass").Text.Trim())

        If result = "Success" Then
            MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
        Else
            MessageBox.Show(result, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub LblBack_Click(sender As Object, e As EventArgs)
        Dim loginForm As New LoginForm()
        loginForm.Show()
        Me.Close()
    End Sub


    Private isDragging As Boolean = False
    Private mouseOffset As Point

    Private Sub RegisterForm_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            isDragging = True
            mouseOffset = New Point(-e.X, -e.Y)
        End If
    End Sub

    Private Sub RegisterForm_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isDragging Then
            Dim mousePos As Point = Control.MousePosition
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Me.Location = mousePos
        End If
    End Sub

    Private Sub RegisterForm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        isDragging = False
    End Sub

End Class