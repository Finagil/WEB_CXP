Public Class verPdfGts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim filePath As String = "~/GTS/" & Session.Item("namePDFg") & ".pdf" '"~/Procesados/" & Request.QueryString("fileName") & ".pdf"
            Response.ContentType = "Application/pdf"
            Response.WriteFile(filePath)
            Response.[End]()
        End If
    End Sub

End Class