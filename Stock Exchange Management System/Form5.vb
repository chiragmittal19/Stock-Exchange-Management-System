Imports System.Data.SqlClient

Public Class Form5

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim symbol As String
    Dim arrq() As String

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label10.Hide()
        Select Case ComboBox1.SelectedIndex
            Case 0
                symbol = "googl"
            Case 1
                symbol = "aapl"
            Case 2
                symbol = "fb"
            Case 3
                symbol = "msft"
            Case 4
                symbol = "amzn"
            Case Else
                symbol = "googl"
        End Select
        arrq = Form3.getQuote(symbol)
        Label6.Text = arrq(1)
        Label7.Text = arrq(2).Replace("""", "").Replace("%", "").Trim
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If (TextBox1.Text <> "") Then
            Dim total As Decimal = TextBox1.Text * arrq(1)
            Label9.Text = total.ToString
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As Integer = MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            con = New SqlConnection
            con.ConnectionString = "server=(localdb)\MSSQLLocalDB; initial catalog=userdb; Integrated Security=True"
            con.Open()
            cmd = New SqlCommand
            cmd.Connection = con
            cmd.CommandText = "insert into user_stocks values(@uid,getdate(),@company,@price,@n,@symbol)"
            cmd.Parameters.AddWithValue("@uid", Form3.Label3.Text)
            cmd.Parameters.AddWithValue("@company", ComboBox1.SelectedItem)
            cmd.Parameters.AddWithValue("@price", arrq(1))
            cmd.Parameters.AddWithValue("@n", TextBox1.Text)
            cmd.Parameters.AddWithValue("@symbol", symbol)
            cmd.ExecuteNonQuery()
            Label10.Show()
            Form4.refresh_user_stock()
        End If
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class