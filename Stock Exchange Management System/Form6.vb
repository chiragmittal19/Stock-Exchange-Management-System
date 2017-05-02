Imports System.Data.SqlClient

Public Class Form6

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        show_available()
    End Sub

    Public Function show_available()
        DataGridView1.Rows.Clear()
        con = New SqlConnection
        con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
        con.Open()
        cmd = New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "select DateLastModified,Company,Price,NoOfShares,symbol from user_stocks where userid=(select userid from active where sno=1)"
        dr = cmd.ExecuteReader()

        While (dr.Read())
            Dim p As New Decimal(dr.GetDecimal(2))
            Dim n As Int32 = New Int32()
            n = dr.GetInt32(3)
            Dim symbol As String = dr.GetString(4)
            Dim arrq() As String = Form3.getQuote(symbol)
            DataGridView1.Rows.Add(False, dr.GetDateTime(0).ToString, dr.GetString(1))
        End While
        Button1.Enabled = False
        dr.Close()
        Return 1
    End Function

    'Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    '    If () Then

    '    End If
    '    Button1.Enabled = True
    '    Dim arrq() As String = Form3.getQuote(dr.GetString(4))
    '    Dim n As New Int32
    '    n = dr.GetInt32(3)
    '    Label2.Text = n * (arrq(1) - dr.GetDecimal(2))
    'End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Button1.Enabled = True
        Dim company As String = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim symbol As String = getSymbol(company)
        Dim p As New Decimal(dr.GetDecimal(2))
        Dim n As Int32 = New Int32()
        n = dr.GetInt32(3)
    End Sub

    Public Function getSymbol(ByVal company As String) As String
        Select Case company
            Case "Alphabet Inc."
                Return "googl"
            Case "Apple Inc."
                Return "aapl"
            Case "Facebook"
                Return "fb"
            Case "Microsoft Corporation"
                Return "msft"
            Case "Amazon.com"
                Return "amzn"
            Case Else
                Return "googl"
        End Select
    End Function
End Class