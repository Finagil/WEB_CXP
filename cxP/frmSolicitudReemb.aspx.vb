Public Class frmSolicitudReemb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        Session.Item("Leyenda") = "Solicitud de pagos con comprobante fiscal"
    End Sub

End Class