Imports System.Drawing
Imports System.Windows.Forms

Public Class Dashboard

    ' GLOBAL logged-in user (shared across all forms)
    Public Shared CurrentUser As String = ""

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Health Tracker - Main Menu"
        Me.Size = New Size(750, 520) ' Made the form slightly taller to fit the new button
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White

        ' Dynamic Welcome Message
        Dim lblTitle As New Label With {
            .Text = "Welcome " & CurrentUser & "!",
            .Font = New Font("Segoe UI", 22, FontStyle.Bold),
            .ForeColor = Color.FromArgb(55, 57, 61),
            .AutoSize = True
        }

        Me.Controls.Add(lblTitle)

        ' Centers the dynamic label perfectly
        lblTitle.Location = New Point((Me.ClientSize.Width - lblTitle.PreferredWidth) / 2, 40)

        ' Menu Buttons
        CreateMenuButton("BMI Calculator", 110, Sub()
                                                    Dim bmi As New BMIForm()
                                                    bmi.ShowDialog()
                                                End Sub)

        CreateMenuButton("Daily Assessment", 180, Sub()
                                                      Dim assessment As New DailyAssessmentForm()
                                                      assessment.ShowDialog()
                                                  End Sub)

        CreateMenuButton("Check Symptoms", 250, Sub()
                                                    Dim symptoms As New SymptomsForm()
                                                    symptoms.ShowDialog()
                                                End Sub)

        ' History Button
        CreateMenuButton("History", 320, Sub()
                                             Dim history As New HistoryForm()
                                             history.ShowDialog()
                                         End Sub)

        ' Log Out Button
        Dim btnLogout As New Button With {
            .Text = "Log Out",
            .Size = New Size(300, 45),
            .BackColor = Color.White,
            .ForeColor = Color.Crimson,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btnLogout.FlatAppearance.BorderColor = Color.Crimson
        btnLogout.Location = New Point((Me.ClientSize.Width - btnLogout.Width) / 2, 410)

        AddHandler btnLogout.Click, Sub()
                                        ' Clear current session user
                                        CurrentUser = ""

                                        ' Close dashboard
                                        Me.Close()
                                    End Sub

        Me.Controls.Add(btnLogout)
    End Sub

    Private Sub CreateMenuButton(text As String, yPos As Integer, action As MethodInvoker)
        Dim btn As New Button With {
            .Text = text,
            .Size = New Size(300, 55),
            .BackColor = Color.FromArgb(32, 156, 120),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }

        btn.Location = New Point((Me.ClientSize.Width - btn.Width) / 2, yPos)
        btn.FlatAppearance.BorderSize = 0

        AddHandler btn.Click, Sub() action.Invoke()

        Me.Controls.Add(btn)
    End Sub

End Class