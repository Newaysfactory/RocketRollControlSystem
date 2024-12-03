Public Class About

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LBVersione.Text = Principale.VersioneSoftware
        FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow 'Non visualizza i botoni in alto a destra
        MaximizeBox = False            'Evita che si possa massimizzare la finestra

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class