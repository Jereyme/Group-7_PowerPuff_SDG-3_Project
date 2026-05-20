Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class HistoryForm

    ' --- Color Palette ---
    Private ReadOnly PrimaryGreen As Color = Color.FromArgb(32, 156, 120)
    Private ReadOnly HoverGreen As Color = Color.FromArgb(20, 125, 90)
    Private ReadOnly TextDark As Color = Color.FromArgb(45, 55, 72)
    Private ReadOnly TextLight As Color = Color.FromArgb(150, 160, 165)

    ' --- Form Variables ---
    Private pnlLeft As Panel
    Private isDragging As Boolean = False
    Private mouseOffset As Point

    ' Store history records
    Private historyData As List(Of String)

    Private Sub HistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' 1. Form Setup
        Me.Text = "Health Tracker - History"
        Me.Size = New Size(750, 480)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.White

        ' Apply Rounded Corners
        Me.Region = New Region(CreateRoundedRectangle(0, 0, Me.Width, Me.Height, 20))

        ' 2. Left Panel Setup
        pnlLeft = New Panel With {
            .Size = New Size(300, Me.Height),
            .Location = New Point(0, 0),
            .BackColor = PrimaryGreen
        }

        AddHandler pnlLeft.MouseDown, AddressOf DragForm_MouseDown
        AddHandler pnlLeft.MouseMove, AddressOf DragForm_MouseMove
        AddHandler pnlLeft.MouseUp, AddressOf DragForm_MouseUp

        ' ==========================================
        ' HISTORY ICON DESIGN 🕒
        ' ==========================================
        Dim lblIcon As New Label With {
            .Text = "🕒",
            .Font = New Font("Segoe UI Emoji", 55, FontStyle.Regular),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(80, 110),
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
            .Text = "Patient History",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(200, 240, 225),
            .AutoSize = True,
            .Location = New Point(100, 260),
            .BackColor = Color.Transparent
        }

        pnlLeft.Controls.Add(lblIcon)
        pnlLeft.Controls.Add(lblBrand)
        pnlLeft.Controls.Add(lblSub)
        Me.Controls.Add(pnlLeft)

        ' ==========================================
        ' RIGHT SIDE SETUP
        ' ==========================================
        Dim rightOffsetX As Integer = 350

        Dim lblMainHead As New Label With {
            .Text = "Activity History",
            .Font = New Font("Segoe UI", 22, FontStyle.Bold),
            .ForeColor = TextDark,
            .AutoSize = True,
            .Location = New Point(rightOffsetX, 40)
        }

        Me.Controls.Add(lblMainHead)

        ' ==========================================
        ' FILTER COMBOBOX
        ' ==========================================
        Dim cmbFilter As New ComboBox With {
            .Location = New Point(rightOffsetX, 95),
            .Size = New Size(350, 30),
            .Font = New Font("Segoe UI", 11),
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .ForeColor = TextDark
        }

        cmbFilter.Items.AddRange({
            "Items: All",
            "Daily",
            "Symptoms",
            "BMI"
        })

        cmbFilter.SelectedIndex = 0

        Me.Controls.Add(cmbFilter)

        ' ==========================================
        ' SEARCH BOX
        ' ==========================================
        Dim txtSearch As New TextBox With {
            .Location = New Point(rightOffsetX, 140),
            .Size = New Size(240, 30),
            .Font = New Font("Segoe UI", 11),
            .BorderStyle = BorderStyle.FixedSingle,
            .ForeColor = TextDark
        }

        Me.Controls.Add(txtSearch)

        ' ==========================================
        ' SEARCH BUTTON
        ' ==========================================
        Dim btnSearch As New Button With {
            .Text = "Search",
            .Location = New Point(rightOffsetX + 250, 139),
            .Size = New Size(100, 28),
            .BackColor = PrimaryGreen,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnSearch.FlatAppearance.BorderSize = 0

        AddHandler btnSearch.MouseEnter,
            Sub()
                btnSearch.BackColor = HoverGreen
            End Sub

        AddHandler btnSearch.MouseLeave,
            Sub()
                btnSearch.BackColor = PrimaryGreen
            End Sub

        Me.Controls.Add(btnSearch)

        ' ==========================================
        ' HISTORY LISTBOX
        ' ==========================================
        Dim lstHistory As New ListBox With {
            .Location = New Point(rightOffsetX, 180),
            .Size = New Size(350, 140),
            .Font = New Font("Segoe UI", 10),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.FromArgb(245, 245, 245),
            .ForeColor = TextDark
        }

        ' ==========================================
        ' LOAD ONLY CURRENT USER HISTORY
        ' ==========================================
        historyData =
            HealthLogic.LoadHistory(Dashboard.CurrentUser)

        lstHistory.Items.Clear()

        For Each record In historyData
            lstHistory.Items.Add(record)
        Next

        If lstHistory.Items.Count = 0 Then
            lstHistory.Items.Add("No history records found.")
        End If

        Me.Controls.Add(lstHistory)

        ' ==========================================
        ' REFRESH BUTTON
        ' ==========================================
        Dim btnRefresh As New Button With {
            .Text = "Refresh",
            .Location = New Point(rightOffsetX, 340),
            .Size = New Size(350, 35),
            .BackColor = Color.White,
            .ForeColor = PrimaryGreen,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnRefresh.FlatAppearance.BorderColor = PrimaryGreen

        AddHandler btnRefresh.MouseEnter,
            Sub()
                btnRefresh.BackColor = PrimaryGreen
                btnRefresh.ForeColor = Color.White
            End Sub

        AddHandler btnRefresh.MouseLeave,
            Sub()
                btnRefresh.BackColor = Color.White
                btnRefresh.ForeColor = PrimaryGreen
            End Sub

        Me.Controls.Add(btnRefresh)

        ' ==========================================
        ' BACK BUTTON
        ' ==========================================
        Dim btnBack As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(rightOffsetX, 390),
            .Size = New Size(350, 45),
            .BackColor = Color.White,
            .ForeColor = PrimaryGreen,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnBack.FlatAppearance.BorderSize = 2
        btnBack.FlatAppearance.BorderColor = PrimaryGreen

        btnBack.Region =
            New Region(
                CreateRoundedRectangle(
                    0,
                    0,
                    btnBack.Width,
                    btnBack.Height,
                    10))

        AddHandler btnBack.MouseEnter,
            Sub()
                btnBack.BackColor = PrimaryGreen
                btnBack.ForeColor = Color.White
            End Sub

        AddHandler btnBack.MouseLeave,
            Sub()
                btnBack.BackColor = Color.White
                btnBack.ForeColor = PrimaryGreen
            End Sub

        AddHandler btnBack.Click, AddressOf btnBack_Click

        Me.Controls.Add(btnBack)

        ' ==========================================
        ' CLOSE BUTTON
        ' ==========================================
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
        btnClose.FlatAppearance.MouseOverBackColor =
            Color.FromArgb(255, 240, 240)

        btnClose.FlatAppearance.MouseDownBackColor =
            Color.FromArgb(255, 150, 150)

        AddHandler btnClose.Click,
            Sub()
                Me.Close()
            End Sub

        Me.Controls.Add(btnClose)

        btnClose.BringToFront()

        ' ==========================================
        ' ENABLE FORM DRAGGING
        ' ==========================================
        AddHandler Me.MouseDown, AddressOf DragForm_MouseDown
        AddHandler Me.MouseMove, AddressOf DragForm_MouseMove
        AddHandler Me.MouseUp, AddressOf DragForm_MouseUp

        ' ==========================================
        ' SEARCH BUTTON LOGIC
        ' ==========================================
        AddHandler btnSearch.Click,
            Sub()

                FilterHistory(
                    lstHistory,
                    cmbFilter.SelectedItem.ToString(),
                    txtSearch.Text)

            End Sub

        ' ==========================================
        ' REFRESH BUTTON LOGIC
        ' ==========================================
        AddHandler btnRefresh.Click,
            Sub()

                historyData =
                    HealthLogic.LoadHistory(
  Dashboard.CurrentUser)

                FilterHistory(
                    lstHistory,
                    cmbFilter.SelectedItem.ToString(),
                    "")

            End Sub

    End Sub

    ' ==========================================
    ' BACK BUTTON CLICK
    ' ==========================================
    Private Sub btnBack_Click(
        sender As Object,
        e As EventArgs)

        Me.Close()

    End Sub

    ' ==========================================
    ' FILTER HISTORY
    ' ==========================================
    Private Sub FilterHistory(
        lst As ListBox,
        filter As String,
        searchText As String)

        lst.Items.Clear()

        If historyData Is Nothing Then

            historyData =
                HealthLogic.LoadHistory(
                    Dashboard.CurrentUser)

        End If

        For Each item In historyData

            Dim matchFilter As Boolean =
                (filter = "Items: All") OrElse
                (filter = "BMI" AndAlso item.ToLower().Contains("bmi")) OrElse
                (filter = "Daily" AndAlso item.ToLower().Contains("daily")) OrElse
                (filter = "Symptoms" AndAlso item.ToLower().Contains("symptom"))

            Dim matchSearch As Boolean =
                String.IsNullOrWhiteSpace(searchText) OrElse
                item.ToLower().Contains(searchText.ToLower())

            If matchFilter AndAlso matchSearch Then
                lst.Items.Add(item)
            End If

        Next

        If lst.Items.Count = 0 Then
            lst.Items.Add("No matching history records found.")
        End If

    End Sub

    ' ==========================================
    ' CREATE ROUNDED RECTANGLE
    ' ==========================================
    Private Function CreateRoundedRectangle(
        x As Integer,
        y As Integer,
        width As Integer,
        height As Integer,
        radius As Integer) As GraphicsPath

        Dim path As New GraphicsPath()

        path.AddArc(
            x,
            y,
            radius * 2,
            radius * 2,
            180,
            90)

        path.AddArc(
            width - (radius * 2),
            y,
            radius * 2,
            radius * 2,
            270,
            90)

        path.AddArc(
            width - (radius * 2),
            height - (radius * 2),
            radius * 2,
            radius * 2,
            0,
            90)

        path.AddArc(
            x,
            height - (radius * 2),
            radius * 2,
            radius * 2,
            90,
            90)

        path.CloseFigure()

        Return path

    End Function

    ' ==========================================
    ' DRAG FORM LOGIC
    ' ==========================================
    Private Sub DragForm_MouseDown(
        sender As Object,
        e As MouseEventArgs)

        If e.Button = MouseButtons.Left Then
            isDragging = True
            mouseOffset = New Point(-e.X, -e.Y)
        End If

    End Sub

    Private Sub DragForm_MouseMove(
        sender As Object,
        e As MouseEventArgs)

        If isDragging Then

            Dim mousePos As Point =
                Control.MousePosition

            mousePos.Offset(
                mouseOffset.X,
                mouseOffset.Y)

            Me.Location = mousePos

        End If

    End Sub

    Private Sub DragForm_MouseUp(
        sender As Object,
        e As MouseEventArgs)

        isDragging = False

    End Sub

End Class