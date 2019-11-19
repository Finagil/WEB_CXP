Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session.Item("Empresa") = "24" Then
            Image1.ImageUrl = "~/imagenes/logoArfin.png"
        End If
    End Sub
End Class