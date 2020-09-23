Imports System.Web.UI.WebControls
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmMisSolicitudes
    Inherits System.Web.UI.Page
    Dim taAutorizantes As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesTableAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView1.RowStyle.BackColor = System.Drawing.Color.FromArgb(213, 244, 255)
        End If
        Dim taPagosR As New dsProduccionTableAdapters.CXP_PagosTableAdapter

        Dim dtPagosR As New dsProduccion.CXP_PagosDataTable

        For Each row As GridViewRow In GridView1.Rows
            Session.Item("Leyenda") = "Mis solicitudes con comprobante fiscal"

            taPagosR.ObtUuidParaMisSol_FillBy(dtPagosR, CDec(row.Cells(0).Text), CDec(Session.Item("Empresa")))

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
                ElseIf drow.st = "Pagada" Then
                    row.Cells(4).Text = "Pagada"
                End If
            End If
        Next
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim taRegCont As New dsProduccionTableAdapters.CXP_RegContTableAdapter
        Dim dtRegCont As New dsProduccion.CXP_RegContDataTable
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_tipoDeDocumentoTableAdapter
        Dim taPagosTes As New dsProduccionTableAdapters.CXP_PagosTesoreriaTableAdapter

        Dim contrato As Boolean = False
        Dim idCuentas As Integer = 0
        Dim fecha As String = ""
        LabelError.Visible = False

        If e.CommandName = "Select" Then
            HiddenID.Value = e.CommandSource.Text
            HiddenEstatus.Value = e.CommandArgument
        ElseIf HiddenEstatus.Value = "Pagada" Or taAutorizantes.ObtEstatus_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "Pagada" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE PAGADA")
            Exit Sub
        ElseIf HiddenEstatus.Value = "En Proceso de Pago" Or taAutorizantes.ObtEstatus_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "En Proceso de Pago" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " SE ENCUENTRA EN PROCESO DE PAGO")
            Exit Sub
        ElseIf HiddenEstatus.Value = "Cancelada" Or taAutorizantes.ObtEstatus_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "Cancelada" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE CANCELADA")
            Exit Sub
        ElseIf e.CommandName = "Cancelar" And HiddenID.Value > "" Then
            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), HiddenID.Value)
            taPagosTes.CambiaEstatus_UpdateQuery(35, "CXP", HiddenID.Value, CInt(Session.Item("Empresa")))
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud, rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1, (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1, rows.autoriza2, "CANCELADA", "CANCELADA", rows.moneda, Date.Now.ToLongDateString, rows.contrato, Nothing, Nothing, Nothing, Nothing, rows.cCostos, rows.fPago, rows.idCuentas, rows.tipoDeCambio, rows.monedaPago)
                taPagos.ActualizaACanceladaConComp_UpdateQuery("CANCELADA", "CANCELADA", rows.folioSolicitud, rows.uuid)
                contrato = rows.contrato
                fecha = rows.fechaSolicitud.ToString("yyyyMMddhhmm")
                idCuentas = rows.idCuentas
            Next

            '////Genera registro contable de cancelación
            taRegCont.ObtDatosPoliza_FillBy(dtRegCont, CInt(Session.Item("Empresa")), HiddenID.Value)
            If dtRegCont.Rows.Count > 0 Then
                Dim folioPoliza As Integer = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))
                For Each rwRegCont As dsProduccion.CXP_RegContRow In dtRegCont.Rows
                    taRegCont.Insert(CDec(rwRegCont.idCuenta), CDec(rwRegCont.idProveedor), CDec(rwRegCont.abono), CDec(rwRegCont.cargo), rwRegCont.referencia, "CAX-" & rwRegCont.concepto, CDec(rwRegCont.idTipoDocumento), folioPoliza, CDec(rwRegCont.idEmpresa), rwRegCont.uuid, CDec(rwRegCont.folioSolicitud), rwRegCont.fecha, rwRegCont.estatus, CDec(rwRegCont.idConcepto), CDec(rwRegCont.periodoEjercicio))
                Next
                taTipoDocumento.ConsumeFolio(CInt(Session.Item("tipoPoliza")))
            End If

            '/////Genera PDF Cancelado
            Dim rptSolPago As New ReportDocument
                Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesAllTableAdapter
                Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
                Dim encripta As readXML_CFDI_class = New readXML_CFDI_class
                Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter

                Dim dtSolPDF As DataTable
                dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable

                taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), HiddenID.Value, "Cancelada")

                Dim dtObsSol As DataTable
                dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
                taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), HiddenID.Value)

                Dim dtCtasBanco As DataTable
                dtCtasBanco = New dsProduccion.CXP_CuentasBancariasProvDataTable
                taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, idCuentas)

                Dim var_observaciones As Integer = dtObsSol.Rows.Count

                rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoCopia.rpt"))
                rptSolPago.SetDataSource(dtSolPDF)
                rptSolPago.Subreports("rptSubObservaciones").SetDataSource(dtObsSol)
                rptSolPago.Subreports("rptSubCuentas").SetDataSource(dtCtasBanco)
                rptSolPago.Refresh()

                rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(fecha & Session.Item("Empresa") & HiddenID.Value))
                rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
                rptSolPago.SetParameterValue("var_contrato", contrato)
                rptSolPago.SetParameterValue("var_idCuentas", idCuentas)

                If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
                    rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
                Else
                    rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
                End If

                Dim rutaPDF As String = "~\TmpFinagil\" & Session.Item("Empresa") & "-" & HiddenID.Value & ".pdf"
                rptSolPago.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))
                Response.Write("<script>")
                rutaPDF = rutaPDF.Replace("\", "/")
                rutaPDF = rutaPDF.Replace("~", "..")
                Response.Write("window.open('verPdf.aspx','popup','_blank','width=200,height=200')")
                Response.Write("</script>")
                rptSolPago.Dispose()
                Response.Redirect("~/frmMisSolicitudes.aspx")
            Else
                LabelError.Visible = True
            LabelError.Text = UCase("Selecion no válida.")
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class