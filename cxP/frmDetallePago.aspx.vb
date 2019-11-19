Public Class frmDetallePago
    Inherits System.Web.UI.Page
    Public dtDetalleD As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        GridView1.DataSource = dtDetalleD 'dtDetalle
        GridView1.DataBind()
    End Sub

End Class