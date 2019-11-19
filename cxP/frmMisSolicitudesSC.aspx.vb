Imports System.Web.UI.WebControls
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class frmMisSolicitudesSC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Session.Item("idConcepto") = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa"))

        'If Session.Item("Usuario") = "lmercado" Or Session.Item("Usuario") = "maria.montes" Then
        '    odsMisSolicitudesSC.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "'"
        'End If
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If
        For Each row As GridViewRow In GridView1.Rows
            Dim btn As Button = row.Cells(5).FindControl("btnOpciones")
            Session.Item("Leyenda") = "Mis solicitudes sin comprobante fiscal"


            Dim taAutorizaciones As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter
            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesSCDataTable
            Dim drAutorizaciones As dsProduccion.Vw_CXP_MisSolicitudesSCRow

            taAutorizaciones.ObtAuroizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))
            If dtAutorizaciones.Rows.Count > 0 Then
                drAutorizaciones = dtAutorizaciones.Rows(0)
                'If row.Cells(0).Text = "50" Then
                '    MsgBox("OK")
                'End If
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
                    btn.Enabled = False
                    btn.Text = "Cancelada"
                ElseIf drAutorizaciones.st = "Pagada" Then
                    row.Cells(4).Text = "Pagada"
                    btn.Enabled = False
                    btn.Text = "Pagada"
                End If
            End If
        Next
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim dsProd As New dsProduccion
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim contrato As Boolean = False
        Dim fecha As String = ""

        If e.CommandName = "Cancelar" Then
            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))
            Dim nRowCXPPagos As dsProduccion.CXP_PagosRow
            For Each rows As dsProduccion.CXP_PagosRow In td

                nRowCXPPagos = dsProd.CXP_Pagos.NewCXP_PagosRow

                'taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud, rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1, (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1, rows.autoriza2, rows.ok1, rows.ok2, rows.moneda, Date.Now.ToLongDateString, False, rows.noContrato, rows.idAutoriza2, rows.naAutoriza2, rows.naAutoriza1, rows.cCostos, rows.fPago)

                nRowCXPPagos.idProveedor = rows.idProveedor
                nRowCXPPagos.idUsuario = rows.idUsuario
                nRowCXPPagos.folioSolicitud = rows.folioSolicitud
                nRowCXPPagos.fechaSolicitud = Date.Now.ToLongDateString()
                nRowCXPPagos.fechaFactura = rows.fechaSolicitud
                nRowCXPPagos.serie = rows.serie
                nRowCXPPagos.folio = rows.folio
                nRowCXPPagos.uuid = rows.uuid
                nRowCXPPagos.subtotalPagado = (rows.subtotalPagado) * -1
                nRowCXPPagos.totalPagado = (rows.totalPagado) * -1
                nRowCXPPagos.trasladosPagados = (rows.trasladosPagados) * -1
                nRowCXPPagos.retencionesPagadas = (rows.retencionesPagadas) * -1
                nRowCXPPagos.decripcion = rows.decripcion
                nRowCXPPagos.idConcepto = rows.idConcepto
                nRowCXPPagos.parcialidad = -1
                nRowCXPPagos.usuario = rows.usuario
                nRowCXPPagos.idEmpresas = rows.idEmpresas
                nRowCXPPagos.estatus = "Cancelacion"
                nRowCXPPagos.autoriza1 = rows.autoriza1
                nRowCXPPagos.autoriza2 = rows.autoriza2
                nRowCXPPagos.ok1 = rows.ok1
                nRowCXPPagos.ok2 = rows.ok2
                nRowCXPPagos.moneda = rows.moneda
                nRowCXPPagos.fechaPago = Date.Now.ToLongDateString()
                nRowCXPPagos.contrato = False
                nRowCXPPagos.noContrato = rows.noContrato
                nRowCXPPagos.idAutoriza2 = rows.idAutoriza2
                nRowCXPPagos.naAutoriza2 = rows.naAutoriza2
                nRowCXPPagos.naAutoriza1 = rows.naAutoriza1
                nRowCXPPagos.cCostos = rows.cCostos
                nRowCXPPagos.fPago = rows.fPago

                contrato = nRowCXPPagos.contrato
                fecha = nRowCXPPagos.fechaSolicitud.ToString("yyyyMMddhhmm")

                dsProd.CXP_Pagos.AddCXP_PagosRow(nRowCXPPagos)
                dsProd.CXP_Pagos.GetChanges()
                taPagos.Update(dsProd.CXP_Pagos)

                taPagos.ActualizaACancelada_UpdateQuery(rows.folioSolicitud, rows.uuid)
            Next


            '/////Genera PDF Cancelado
            Dim rptSolPago As New ReportDocument
            Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesAllTableAdapter
            Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter

            Dim dtSolPDF As DataTable
            dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable
            Dim dtSolPDFSD As DataTable
            dtSolPDFSD = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable
            Dim dtSolPDFND As DataTable
            dtSolPDFND = New dsProduccion.Vw_CXP_AutorizacionesAllDataTable

            Dim dtObsSol As DataTable
            dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
            taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))
            taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text), "Cancelada")
            taSolicitudPDF.DetalleSD_FillBy(dtSolPDFSD, CDec(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))
            taSolicitudPDF.DetalleND_FillBy(dtSolPDFND, CDec(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))

            Dim var_observaciones As Integer = dtObsSol.Rows.Count
            Dim encripta As readXML_CFDI_class = New readXML_CFDI_class
            rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoSCCopia.rpt"))
            rptSolPago.SetDataSource(dtSolPDF)
            rptSolPago.Subreports(0).SetDataSource(dtObsSol)
            rptSolPago.Subreports(1).SetDataSource(dtSolPDFND)
            rptSolPago.Subreports(2).SetDataSource(dtSolPDFSD)
            rptSolPago.Refresh()

            rptSolPago.SetParameterValue("var_SD", dtSolPDFSD.Rows.Count)
            rptSolPago.SetParameterValue("var_ND", dtSolPDFND.Rows.Count)
            rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(fecha & Session.Item("Empresa") & GridView1.Rows(e.CommandArgument).Cells(0).Text))
            rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
            rptSolPago.SetParameterValue("var_contrato", contrato)

            If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
                rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
            Else
                rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
            End If


            Dim rutaPDF As String = "~\TmpFinagil\" & Session.Item("Empresa") & "-" & GridView1.Rows(e.CommandArgument).Cells(0).Text & ".pdf"
            rptSolPago.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))
            Response.Write("<script>")
            rutaPDF = rutaPDF.Replace("\", "/")
            rutaPDF = rutaPDF.Replace("~", "..")
            Response.Write("window.open('verPdf.aspx','popup','_blank','width=200,height=200')")
            Response.Write("</script>")
            rptSolPago.Dispose()

            Response.Redirect("~/frmMisSolicitudesSC.aspx")
        End If

    End Sub
End Class