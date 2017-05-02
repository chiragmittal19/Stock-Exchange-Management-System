Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class Form3

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Public btn_clicked As Boolean = False

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim uid As String = "User"
        con = New SqlConnection
        con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
        con.Open()
        cmd = New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "select userid from active where sno=1"
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            uid = dr.GetString(0)
        End If
        Label3.Text = uid
        refreshQuotes()
    End Sub

    Public Function refreshQuotes()
        DataGridView1.Rows.Clear()

        Dim strSymbol() As String = {"googl", "aapl", "fb", "msft", "amzn"}
        For i As Integer = 0 To strSymbol.Count - 1
            DataGridView1.Rows.Add(getQuote(strSymbol(i)))
        Next
        Return 1
    End Function

    Public Function getQuote(ByVal symbol As String) As String()
        Dim strURL As String = " http://quote.yahoo.com/d/quotes.csv?s=" & symbol & "&d=t&f=sl1d1t1c1ohgvj1pp2wern"
        Dim quote As String = RequestWebData(strURL)
        Dim arrq() As String = quote.Split(",")
        Return {arrq(15).Replace("""", "").Trim, arrq(1), arrq(11).Replace("""", "").Replace("%", "").Trim}
    End Function

    Public Function RequestWebData(ByVal pstrURL As String) As String
        Dim wreq As WebRequest
        Dim wresp As WebResponse
        Dim strBuffer As String
        wreq = HttpWebRequest.Create(pstrURL)
        wresp = wreq.GetResponse()
        Dim sr As StreamReader
        sr = New StreamReader(wresp.GetResponseStream)
        strBuffer = sr.ReadToEnd
        sr.Close()

        wresp.Close()

        Return strBuffer
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Form1.Label5.Hide()
        Form1.Label6.Hide()
        Form1.Label7.Hide()
        Form1.Label8.Show()
        Form1.TextBox2.Text = ""
        Form1.TextBox1.Text = ""
        btn_clicked = True
        Me.Close()
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Not btn_clicked) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Show()
        Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        refreshQuotes()
    End Sub
End Class