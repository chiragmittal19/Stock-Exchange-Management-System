Imports System.Data.SqlClient

Public Class Form4

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim btn_clicked As Boolean = False

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form3.Show()
        btn_clicked = True
        Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Show()
        Form1.Label5.Hide()
        Form1.Label6.Hide()
        Form1.Label7.Hide()
        Form1.Label8.Show()
        Form1.TextBox2.Text = ""
        Form1.TextBox1.Text = ""
        btn_clicked = True
        Form3.btn_clicked = True
        Form3.Close()
        Me.Close()
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Not btn_clicked) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refresh_user_stock()
    End Sub

    Public Function refresh_user_stock()
        DataGridView1.Rows.Clear()
        con = New SqlConnection
        con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
        con.Open()
        cmd = New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "select DateLastModified,Company,Price,NoOfShares,symbol from user_stocks where userid=(select userid from active where sno=1)"
        dr = cmd.ExecuteReader()
        Try
            While (dr.Read())
                Dim p As New Decimal(dr.GetDecimal(2))
                Dim n As Int32 = New Int32()
                n = dr.GetInt32(3)
                Dim symbol As String = dr.GetString(4)
                Dim arrq() As String = Form3.getQuote(symbol)
                DataGridView1.Rows.Add(New String() {dr.GetDateTime(0), dr.GetString(1), p.ToString, n.ToString, arrq(1)})
            End While
            Label1.Hide()
            Button2.Enabled = True
        Catch exp As InvalidOperationException
            Label1.Show()
            Button2.Enabled = False
        End Try
        Return 1
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form5.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form6.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        refresh_user_stock()
    End Sub
End Class