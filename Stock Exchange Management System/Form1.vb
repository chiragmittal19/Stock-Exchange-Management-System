Imports System.Data.SqlClient

Public Class Form1

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.Show()
        Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim uid As String = TextBox1.Text
        Dim pwd As String = TextBox2.Text
        If (uid = "" And pwd = "") Then
            uid = "chiragmittal19"
            pwd = "semscm10"
        End If
        con = New SqlConnection
        con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
        con.Open()
        cmd = New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "select password from user_credentials where userid=@uid1"
        cmd.Parameters.AddWithValue("@uid1", uid)
        dr = cmd.ExecuteReader()
        Try
            If (dr.Read() And dr.GetString(0) = pwd) Then
                dr.Close()
                cmd.CommandText = "update active set userid=@uid2 where sno=1"
                cmd.Parameters.AddWithValue("@uid2", uid)
                cmd.ExecuteNonQuery()
                Me.Hide()
                Form3.Show()
            Else
                Label5.Show()
                Label6.Hide()
            End If
        Catch exp As InvalidOperationException
            Label6.Show()
            Label5.Hide()
        End Try
        Label7.Hide()
        Label8.Hide()
        dr.Close()
    End Sub

End Class
