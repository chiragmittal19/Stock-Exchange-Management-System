Imports System.Data.SqlClient

Public Class Form2

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim btn_clicked As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim uid As String = TextBox1.Text
        Dim pwd1 As String = TextBox2.Text
        Dim pwd2 As String = TextBox3.Text
        If (uid = "" Or pwd1 = "" Or pwd2 = "") Then
            Label7.Show()
            Label5.Hide()
            Label6.Hide()
            Return
        End If

        If (pwd1 = pwd2) Then
            con = New SqlConnection
            con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
            con.Open()
            cmd = New SqlCommand
            cmd.Connection = con
            cmd.CommandText = "insert into user_credentials values (@uid,@pwd1)"
            cmd.Parameters.AddWithValue("@uid", uid)
            cmd.Parameters.AddWithValue("@pwd1", pwd1)
            Try
                If (cmd.ExecuteNonQuery()) Then
                    Form1.Show()
                    Form1.Label7.Show()
                    Form1.Label5.Hide()
                    Form1.Label6.Hide()
                    Form1.Label8.Hide()
                    Form1.TextBox1.Text = ""
                    Form1.TextBox2.Text = ""
                    btn_clicked = True
                    Me.Close()
                End If
            Catch exp As SqlException
                Label6.Show()
                Label5.Hide()
                Label7.Hide()
            End Try
        Else
            Label5.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Form1.Label5.Hide()
        Form1.Label6.Hide()
        Form1.Label7.Hide()
        Form1.Label8.Hide()
        Form1.TextBox2.Text = ""
        Form1.TextBox1.Text = ""
        btn_clicked = True
        Me.Close()
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Not btn_clicked) Then
            Application.Exit()
        End If
    End Sub
End Class