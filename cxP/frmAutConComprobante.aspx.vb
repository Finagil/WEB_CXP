Public Class frmAutConComprobante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If

        Session.Item("Leyenda") = "Mis Autorizaciones Con Comprobante"

        Dim taReporte As New dsProduccionTableAdapters.Vw_CXP_AutConComprobanteTableAdapter
        Dim dtReporte As New dsProduccion.Vw_CXP_AutConComprobanteDataTable
        Dim rowReporte As dsProduccion.Vw_CXP_AutConComprobanteRow

        For Each row As GridViewRow In GridView1.Rows
            taReporte.ObtAutorizanta1y2_FillBy(dtReporte, row.Cells(0).Text, CDec(Session.Item("Empresa")))

            If dtReporte.Rows.Count >= 1 Then
                rowReporte = dtReporte.Rows(0)
                If rowReporte.A1.Trim = Session.Item("Usuario").ToString.Trim And (row.Cells(5).Text.Trim = "Autoriza 1" Or row.Cells(5).Text.Trim = "Autoriza 2") Then
                    row.Cells(6).BackColor = System.Drawing.Color.FromArgb(117, 255, 125)
                End If
                If rowReporte.A1.Trim = Session.Item("Usuario").ToString.Trim And (row.Cells(5).Text.Trim = "Rechazada 1" Or row.Cells(5).Text.Trim = "Rechazada 2") Then
                    row.Cells(6).BackColor = System.Drawing.Color.FromArgb(255, 121, 117)
                End If

                If rowReporte.A2.Trim = Session.Item("Usuario").ToString.Trim And (row.Cells(5).Text.Trim = "Autoriza 1" Or row.Cells(5).Text.Trim = "Autoriza 2") Then
                    row.Cells(7).BackColor = System.Drawing.Color.FromArgb(117, 255, 125)
                End If
                If rowReporte.A2.Trim = Session.Item("Usuario").ToString.Trim And (row.Cells(5).Text.Trim = "Rechazada 1" Or row.Cells(5).Text.Trim = "Rechazada 2") Then
                    row.Cells(7).BackColor = System.Drawing.Color.FromArgb(255, 121, 117)
                End If

            End If
        Next
    End Sub

End Class