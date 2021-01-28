Imports System.Web.UI.WebControls
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class frmMisSolicitudesSC
    Inherits System.Web.UI.Page
    Dim taComprobaciones As New dsProduccionTableAdapters.Vw_CXP_MisComprobacionesTableAdapter
    Dim taAutorizaciones As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Session.Item("idConcepto") = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa"))

        'If Session.Item("Usuario") = "lmercado" Or Session.Item("Usuario") = "maria.montes" Then
        '    odsMisSolicitudesSC.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "'"
        'End If
        If Not IsPostBack Then
            txtFechaFinal.Text = Date.Now.ToShortDateString
            txtFechaInicial.Text = Date.Now.AddDays(-10).ToShortDateString
            Session("fechaInicialSC") = CDate(txtFechaInicial.Text)
            Session("fechaFinalSC") = CDate(txtFechaFinal.Text).AddHours(11).AddMinutes(59)
            If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If
            If Session.Item("Empresa") = "24" Then
                GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
                GridView1.RowStyle.BackColor = System.Drawing.Color.FromArgb(213, 244, 255)
            End If
            For Each row As GridViewRow In GridView1.Rows
                Session.Item("Leyenda") = "Mis solicitudes sin comprobante fiscal"


                Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesSCDataTable
                Dim drAutorizaciones As dsProduccion.Vw_CXP_MisSolicitudesSCRow


                taAutorizaciones.ObtAuroizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))
                If dtAutorizaciones.Rows.Count > 0 Then
                    drAutorizaciones = dtAutorizaciones.Rows(0)
                    Dim contComp As Integer = taComprobaciones.ObtNoComprobaciones_ScalarQuery(CDec(row.Cells(0).Text.Trim), CInt(Session.Item("Empresa")))
                    If row.Cells(4).Text.Trim = "Autoriza 1" And drAutorizaciones.st <> "Cancelada" Then
                        row.Cells(4).Text = "Autorizó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                    ElseIf row.Cells(4).Text.Trim = "Rechazada 1" And drAutorizaciones.st <> "Cancelada" Then
                        row.Cells(4).Text = "Rechazó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                    ElseIf row.Cells(4).Text.Trim = "Autoriza 2" And drAutorizaciones.st <> "Cancelada" Then
                        row.Cells(4).Text = "Autorizó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                    ElseIf row.Cells(4).Text.Trim = "Rechazada 2" And drAutorizaciones.st <> "Cancelada" Then
                        row.Cells(4).Text = "Rechazó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                    ElseIf drAutorizaciones.st = "Cancelada" Then
                        row.Cells(4).Text = "Cancelada"
                    ElseIf drAutorizaciones.st = "Pagada" Then
                        row.Cells(4).Text = "Pagada"
                    End If
                    If contComp > 0 Then
                        'row.Cells(4).Text = "Comprobaciones"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim taPagosTes As New dsProduccionTableAdapters.CXP_PagosTesoreriaTableAdapter
        Dim dsProd As New dsProduccion
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim contrato As Boolean = False
        Dim fecha As String = ""
        Dim idCuentas As Integer = 0
        LabelError.Visible = False



        If e.CommandName = "Select" Then
            HiddenID.Value = e.CommandSource.Text
            HiddenEstatus.Value = e.CommandArgument
        ElseIf HiddenEstatus.Value = "Pagada" Or taAutorizaciones.ObtieneEstatusSol_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "Pagada" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE PAGADA")
            Exit Sub
        ElseIf HiddenEstatus.Value = "En Proceso de Pago" Or taAutorizaciones.ObtieneEstatusSol_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "En Proceso de Pago" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " SE ENCUENTRA EN PROCESO DE PAGO")
            Exit Sub
        ElseIf HiddenEstatus.Value = "Cancelada" Or taAutorizaciones.ObtieneEstatusSol_ScalarQuery(HiddenID.Value, CInt(Session.Item("Empresa"))) = "Cancelada" Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE CANCELADA")
            Exit Sub
        ElseIf e.CommandName = "Cancelar" Then
            GridView1.Enabled = False
            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), HiddenID.Value)
            taPagosTes.CambiaEstatus_UpdateQuery(35, "CXP", HiddenID.Value, CInt(Session.Item("Empresa")))
            Dim nRowCXPPagos As dsProduccion.CXP_PagosRow
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.ActualizaACancelada_UpdateQuery("CANCELADA", "CANCELADA", CDec(Math.Abs(rows.totalPagado) * -1), rows.folioSolicitud, rows.uuid)
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

            Response.Redirect("~/frmMisSolicitudesSC.aspx")
        Else
            LabelError.Visible = True
            LabelError.Text = UCase("Selecion no válida.")
        End If
        GridView1.Enabled = True
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Session("fechaInicialSC") = CDate(txtFechaInicial.Text)
        Session("fechaFinalSC") = CDate(txtFechaFinal.Text).AddHours(11).AddMinutes(59)
        odsMisSolicitudesSC.DataBind()

        For Each row As GridViewRow In GridView1.Rows
            Session.Item("Leyenda") = "Mis solicitudes sin comprobante fiscal"


            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesSCDataTable
            Dim drAutorizaciones As dsProduccion.Vw_CXP_MisSolicitudesSCRow


            taAutorizaciones.ObtAuroizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))
            If dtAutorizaciones.Rows.Count > 0 Then
                drAutorizaciones = dtAutorizaciones.Rows(0)
                Dim contComp As Integer = taComprobaciones.ObtNoComprobaciones_ScalarQuery(CDec(row.Cells(0).Text.Trim), CInt(Session.Item("Empresa")))
                If row.Cells(4).Text.Trim = "Autoriza 1" And drAutorizaciones.st <> "Cancelada" Then
                    row.Cells(4).Text = "Autorizó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Rechazada 1" And drAutorizaciones.st <> "Cancelada" Then
                    row.Cells(4).Text = "Rechazó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Autoriza 2" And drAutorizaciones.st <> "Cancelada" Then
                    row.Cells(4).Text = "Autorizó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                ElseIf row.Cells(4).Text.Trim = "Rechazada 2" And drAutorizaciones.st <> "Cancelada" Then
                    row.Cells(4).Text = "Rechazó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                ElseIf drAutorizaciones.st = "Cancelada" Then
                    row.Cells(4).Text = "Cancelada"
                ElseIf drAutorizaciones.st = "Pagada" Then
                    row.Cells(4).Text = "Pagada"
                End If
                If contComp > 0 Then
                    'row.Cells(4).Text = "Comprobaciones"
                End If
            End If
        Next

    End Sub
End Class