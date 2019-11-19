Imports System.Web.UI.WebControls
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmMisSolicitudes
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If
        Dim taPagosR As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim taAutorizantes As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesTableAdapter
        Dim dtPagosR As New dsProduccion.CXP_PagosDataTable

        For Each row As GridViewRow In GridView1.Rows
            Dim btn As Button = row.Cells(6).FindControl("btnOpciones")
            AddHandler btn.Click, AddressOf evento
            Session.Item("Leyenda") = "Mis solicitudes con comprobante fiscal"

            taPagosR.ObtUuidParaMisSol_FillBy(dtPagosR, CDec(row.Cells(0).Text), CDec(Session.Item("Empresa")))

            For Each rows As dsProduccion.CXP_PagosRow In dtPagosR

                If taPagosR.ObtUltimoRegistro_ScalarQuery(rows.uuid) = row.Cells(0).Text Then
                    btn.Enabled = True
                Else
                    btn.Enabled = False
                End If

            Next

            If row.Cells(4).Text = "Pagada" Then
                btn.Enabled = False
                btn.Text = "Pagada"
            End If

            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesDataTable
                Dim drow As dsProduccion.Vw_CXP_MisSolicitudesRow
                taAutorizantes.ObtAutorizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CDec(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))

                If dtAutorizaciones.Rows.Count > 0 Then
                    drow = dtAutorizaciones.Rows(0)
                If row.Cells(4).Text.Trim = "Autoriza 1" And drow.st <> "Cancelada" Then
                    row.Cells(4).Text = "Autorizó (1): " & vbCrLf & drow.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Rechazada 1" And drow.st <> "Cancelada" Then
                    row.Cells(4).Text = "Rechazó (1): " & vbCrLf & drow.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Autoriza 2" And drow.st <> "Cancelada" Then
                    row.Cells(4).Text = "Autorizó (2): " & vbCrLf & drow.Autoriza2
                ElseIf row.Cells(4).Text.Trim = "Rechazada 2" And drow.st <> "Cancelada" Then
                    row.Cells(4).Text = "Rechazó (2): " & vbCrLf & drow.Autoriza2
                ElseIf drow.st = "Cancelada" Then
                    row.Cells(4).Text = "Cancelada"
                    btn.Enabled = False
                    btn.Text = "Cancelada"
                ElseIf drow.st = "Pagada" Then
                    row.Cells(4).Text = "Pagada"
                        btn.Enabled = False
                        btn.Text = "Pagada"
                    End If
                End If


            Next
    End Sub

    Private Sub evento(ByVal sender As Object, ByVal e As System.EventArgs)
        'MsgBox("Hola")
    End Sub


    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim contrato As Boolean = False
        Dim fecha As String = ""
        If e.CommandName = "Cancelar" Then

            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud, rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1, (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1, rows.autoriza2, Nothing, Nothing, rows.moneda, Date.Now.ToLongDateString, rows.contrato, Nothing, Nothing, Nothing, Nothing, rows.cCostos, rows.fPago)
                taPagos.ActualizaACancelada_UpdateQuery(rows.folioSolicitud, rows.uuid)
                contrato = rows.contrato
                fecha = rows.fechaSolicitud.ToString("yyyyMMddhhmm")
            Next

            '/////Genera PDF Cancelado
            Dim rptSolPago As New ReportDocument
            Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesAllTableAdapter
            Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
            Dim encripta As readXML_CFDI_class = New readXML_CFDI_class

            Dim dtSolPDF As DataTable
            dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable

            taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text), "Cancelada")

            Dim dtObsSol As DataTable
            dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
            taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))

            Dim var_observaciones As Integer = dtObsSol.Rows.Count

            rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoCopia.rpt"))
            rptSolPago.SetDataSource(dtSolPDF)
            rptSolPago.Subreports(0).SetDataSource(dtObsSol)
            rptSolPago.Refresh()

            rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(fecha & Session.Item("Empresa") & GridView1.Rows(e.CommandArgument).Cells(0).Text))
            rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
            rptSolPago.SetParameterValue("var_contrato", contrato)


            If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
                rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
            Else
                rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
            End If

            Dim rutaPDF As String = "~\TmpFinagil\" & Session.Item("Empresa") & "-" & CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text) & ".pdf"
            rptSolPago.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))
            Response.Write("<script>")
            rutaPDF = rutaPDF.Replace("\", "/")
            rutaPDF = rutaPDF.Replace("~", "..")
            Response.Write("window.open('verPdf.aspx','popup','_blank','width=200,height=200')")
            Response.Write("</script>")
            rptSolPago.Dispose()

            Response.Redirect("~/frmMisSolicitudes.aspx")


        End If

    End Sub

End Class