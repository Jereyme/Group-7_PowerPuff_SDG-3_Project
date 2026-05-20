Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class SymptomsForm

    ' --- Color Palette ---
    Private ReadOnly PrimaryGreen As Color = Color.FromArgb(32, 156, 120)
    Private ReadOnly HoverGreen As Color = Color.FromArgb(20, 125, 90)
    Private ReadOnly TextDark As Color = Color.FromArgb(45, 55, 72)

    ' --- Form Variables ---
    Private pnlLeft As Panel
    Private isDragging As Boolean = False
    Private mouseOffset As Point

    ' --- Symptom ComboBoxes + result label ---
    Private cmbHeadache As ComboBox
    Private cmbCough As ComboBox
    Private cmbFever As ComboBox
    Private cmbChestPain As ComboBox
    Private lblResult As Label

    Private Sub SymptomsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' 1. Form Setup
        Me.Size = New Size(750, 520)
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
            .Text = "🩺",
            .Font = New Font("Segoe UI Emoji", 60, FontStyle.Regular),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(80, 130),
            .BackColor = Color.Transparent
        }

        Dim lblBrand As New Label With {
            .Text = "Health Tracker",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(55, 240),
            .BackColor = Color.Transparent
        }

        Dim lblSub As New Label With {
            .Text = "Symptoms Checker",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(200, 240, 225),
            .AutoSize = True,
            .Location = New Point(85, 280),
            .BackColor = Color.Transparent
        }

        pnlLeft.Controls.Add(lblIcon)
        pnlLeft.Controls.Add(lblBrand)
        pnlLeft.Controls.Add(lblSub)
        Me.Controls.Add(pnlLeft)

        ' 3. Right Side Setup
        Dim rightX As Integer = 340

        Dim lblTitle As New Label With {
            .Text = "Check Symptoms",
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = True,
            .Location = New Point(rightX, 40)
        }
        Me.Controls.Add(lblTitle)

        ' Symptom Rows
        cmbHeadache = CreateSymptomRow("Headache:", 100, rightX)
        cmbCough = CreateSymptomRow("Cough:", 160, rightX)
        cmbFever = CreateSymptomRow("Fever:", 220, rightX)
        cmbChestPain = CreateSymptomRow("Chest Pain:", 280, rightX)

        ' RESULT LABEL (FIXED VERSION)
        lblResult = New Label With {
            .Text = "",
            .Location = New Point(rightX, 320),
            .Size = New Size(350, 60),
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopLeft
        }
        Me.Controls.Add(lblResult)

        ' Check Button
        Dim btnCheck As New Button With {
            .Text = "Check Results",
            .Location = New Point(rightX, 390),
            .Size = New Size(350, 45),
            .BackColor = PrimaryGreen,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnCheck.FlatAppearance.BorderSize = 0
        AddHandler btnCheck.Click, AddressOf BtnCheck_Click
        Me.Controls.Add(btnCheck)

        ' Back Button
        Dim btnBack As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(rightX, 445),
            .Size = New Size(350, 40),
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

    ' Create Symptom Row
    Private Function CreateSymptomRow(name As String, y As Integer, x As Integer) As ComboBox

        Dim lbl As New Label With {
            .Text = name,
            .Location = New Point(x, y),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 10)
        }

        Dim cmb As New ComboBox With {
            .Location = New Point(x + 100, y - 3),
            .Size = New Size(250, 25),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }

        cmb.Items.AddRange({"No", "Yes"})
        cmb.SelectedIndex = 0

        Me.Controls.Add(lbl)
        Me.Controls.Add(cmb)

        Return cmb

    End Function

    ' Check Results Logic
    Private Sub BtnCheck_Click(sender As Object, e As EventArgs)

        Dim headache As Boolean = (cmbHeadache.SelectedItem.ToString() = "Yes")
        Dim cough As Boolean = (cmbCough.SelectedItem.ToString() = "Yes")
        Dim fever As Boolean = (cmbFever.SelectedItem.ToString() = "Yes")
        Dim chestPain As Boolean = (cmbChestPain.SelectedItem.ToString() = "Yes")

        lblResult.Text = HealthLogic.SymptomsChecker(
            headache,
            cough,
            fever,
            chestPain,
            Dashboard.CurrentUser
        )

    End Sub

    ' Rounded Form
    Private Function CreateRoundedRectangle(
        x As Integer,
        y As Integer,
        width As Integer,
        height As Integer,
        radius As Integer
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        path.AddArc(x, y, radius * 2, radius * 2, 180, 90)
        path.AddArc(width - (radius * 2), y, radius * 2, radius * 2, 270, 90)
        path.AddArc(width - (radius * 2), height - (radius * 2), radius * 2, radius * 2, 0, 90)
        path.AddArc(x, height - (radius * 2), radius * 2, radius * 2, 90, 90)

        path.CloseFigure()

        Return path

    End Function

    ' Drag Form
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