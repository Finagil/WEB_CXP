Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmMisSolicitudesSCR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Session.Item("idConcepto") = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa"))

        'odsMisSolicitudesSC.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "'"

        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView1.RowStyle.BackColor = System.Drawing.Color.FromArgb(213, 244, 255)
        End If
        For Each row As GridViewRow In GridView1.Rows
            Session.Item("Leyenda") = "Mis solicitudes de reembolso"
            Dim taAutorizaciones As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter
            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesSCDataTable
            Dim drAutorizaciones As dsProduccion.Vw_CXP_MisSolicitudesSCRow

            taAutorizaciones.ObtAuroizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))
            If dtAutorizaciones.Rows.Count > 0 Then
                drAutorizaciones = dtAutorizaciones.Rows(0)
                If row.Cells(4).Text.Trim = "Autoriza 1" Then
                    row.Cells(4).Text = "Autorizó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Rechazada 1" Then
                    row.Cells(4).Text = "Rechazó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Autoriza 2" Then
                    row.Cells(4).Text = "Autorizó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                ElseIf row.Cells(4).Text.Trim = "Rechazada 2" Then
                    row.Cells(4).Text = "Rechazó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                End If
            End If
        Next
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim taPagosTes As New dsProduccionTableAdapters.CXP_PagosTesoreriaTableAdapter
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim contrato As Boolean = False
        Dim fecha As String = ""
        Dim idCuentas As Integer = 0

        LabelError.Visible = False
        If e.CommandName = "Select" Then
            HiddenID.Value = e.CommandSource.Text
            HiddenEstatus.Value = e.CommandArgument
        ElseIf InStr(HiddenEstatus.Value, "Pagada") > 0 Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE PAGADA")
            Exit Sub
        ElseIf InStr(HiddenEstatus.Value, "Cancelada") > 0 Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE CANCELADA")
            Exit Sub
        ElseIf e.CommandName = "Cancelar" Then
            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), HiddenID.Value)
            taPagosTes.CambiaEstatus_UpdateQuery(35, "CXP", HiddenID.Value, CInt(Session.Item("Empresa")))
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud,
                               rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1,
                               (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1,
                               rows.autoriza2, "CANCELADA", "CANCELADA", rows.moneda, Date.Now.ToLongDateString, False, rows.noContrato, rows.idAutoriza2, rows.naAutoriza2,
                               rows.naAutoriza1, rows.cCostos, rows.fPago, rows.idCuentas)
                taPagos.ActualizaACanceladaReemb_UpdateQuery("CANCELADA", "CANCELADA", rows.folioSolicitud, rows.uuid)

                contrato = rows.contrato
                fecha = rows.fechaSolicitud.ToString("yyyyMMddhhmm")
                idCuentas = rows.idCuentas
            Next


            '/////Genera PDF Cancelado
            Dim rptSolPago As New ReportDocument
            Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesAllTableAdapter
            Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
            Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter

            Dim dtSolPDF As DataTable
            dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable
            Dim dtSolPDFSD As DataTable
            dtSolPDFSD = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable
            Dim dtSolPDFND As DataTable
            dtSolPDFND = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable

            Dim dtObsSol As DataTable
            dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
            taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), HiddenID.Value)
            taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), HiddenID.Value, "Cancelada")
            taSolicitudPDF.DetalleSD_FillBy(dtSolPDFSD, CDec(Session.Item("Empresa")), HiddenID.Value)
            taSolicitudPDF.DetalleND_FillBy(dtSolPDFND, CDec(Session.Item("Empresa")), HiddenID.Value)

            Dim dtCtasBanco As DataTable
            dtCtasBanco = New dsProduccion.CXP_CuentasBancariasProvDataTable
            taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, idCuentas)

            Dim var_observaciones As Integer = dtObsSol.Rows.Count
            Dim encripta As readXML_CFDI_class = New readXML_CFDI_class
            rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoSCCopia.rpt"))
            rptSolPago.SetDataSource(dtSolPDF)
            rptSolPago.Subreports("rptSubObservaciones").SetDataSource(dtObsSol)
            rptSolPago.Subreports("rptSubSolicitudSCND").SetDataSource(dtSolPDFND)
            rptSolPago.Subreports("rptSubSolicitudSCSD").SetDataSource(dtSolPDFSD)
            rptSolPago.Subreports("rptSubCtasBancarias").SetDataSource(dtCtasBanco)
            rptSolPago.Refresh()

            rptSolPago.SetParameterValue("var_SD", dtSolPDFSD.Rows.Count)
            rptSolPago.SetParameterValue("var_ND", dtSolPDFND.Rows.Count)
            rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(fecha & Session.Item("Empresa") & HiddenID.Value.ToString))
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

            Response.Redirect("~/frmMisSolicitudesSCR.aspx")

        Else
            LabelError.Visible = True
            LabelError.Text = UCase("Selecion no válida.")
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class