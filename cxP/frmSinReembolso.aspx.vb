Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class frmSinReembolso
    Inherits System.Web.UI.Page
    Dim taTiposCambio As New dsProduccionTableAdapters.CONT_TiposDeCambioTableAdapter
    Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim taProveedor As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
    Dim totalfacturas As Decimal = 0
    Dim totalfacturasB As Decimal = 0
    Dim contadorNoDeduc As Integer = 1
    Dim totalGastoas As Decimal = 0
    Dim dtDetalleAg As DataTable
    Dim dtDetalleBg As DataTable
    Dim totalga As Decimal = 0
    Dim totalbg As Decimal = 0
    Dim taSucursales As New dsProduccionTableAdapters.CXP_SucursalesTableAdapter
    Dim taFormaPago As New dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter
    Dim idCuentas As Integer = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If

        If Session.Item("Empresa") = "24" Then
            formato()
        End If
        Dim rows As dsProduccion.CXP_ProveedoresRow
        Dim dtDeudor As New dsProduccion.CXP_ProveedoresDataTable
        Dim taCXProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter


        If Not IsPostBack Then

            'Variables de sessión
            Session.Item("totala") = 0
            Session.Item("totalb") = 0
            Session.Item("dtDetalleA") = Nothing
            Session.Item("dtDetalleB") = Nothing
            Session.Item("totalFacturas") = 0
            Session.Item("totalFacturasB") = 0


            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            odsCuentasBancarias.FilterExpression = "idProveedor = 0"
            txtFechaPago.Text = Date.Now.ToShortDateString
            lblFechaSolicitud.Text = Date.Now.ToShortDateString
            ddlMoneda.SelectedValue = "MXN"
            Session.Item("Leyenda") = "Solicitud de reembolso de gastos"
            If Session.Item("rfcEmpresa") = "SAR951230N5A" Then
                Session.Item("rutaCFDI") = "ARFIN/Todos/Procesados"
            Else
                Session.Item("rutaCFDI") = "FINAGIL/Todos/Procesados"
            End If

            taCXProveedores.ObtDeudor_FillBy(dtDeudor, Session.Item("rfcUsuario"))

            If dtDeudor.Rows.Count > 0 Then
                rows = dtDeudor.Rows(0)
                txtDeudor.Text = rows.razonSocial
                Session.Item("idDeudor") = rows.idProveedor
                validaEstatusProveedor(rows.idProveedor)
            Else
                lblErrorGeneral.Text = "El usuario no existe en el catálogo de proveedores"
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            odsContratos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
            cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
            cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))

            dtDetalleBg = New DataTable("ComprobantesComprobacion")
            dtDetallebg.Columns.Add("descripcion", Type.GetType("System.String"))
            dtDetallebg.Columns.Add("importe", Type.GetType("System.Decimal"))

            Session.Item("dtDetalleB") = dtDetalleBg

            dtDetalleAg = New DataTable("ComprobantesA")

            dtDetalleAg.Columns.Add("serie", Type.GetType("System.String"))
            dtDetalleAg.Columns.Add("folio", Type.GetType("System.String"))
            dtDetalleAg.Columns.Add("uuid", Type.GetType("System.String"))
            dtDetalleAg.Columns.Add("concepto", Type.GetType("System.String"))
            dtDetalleAg.Columns.Add("total", Type.GetType("System.Decimal"))
            dtDetalleAg.Columns.Add("total1", Type.GetType("System.Decimal"))

            Session.Item("dtDetalleA") = dtDetalleAg

            txtBuscar.Visible = False
            btnBuscar0.Visible = False
            ddlProveedor0.Visible = False
            btnAsignar.Visible = False
            TabContainer1.Visible = False
            'contenedorID.Attributes.Add("style", "overflow-y: auto; height: 500px;")
            'contenedorID.Attributes.Add("class", "alturaCorta")
            'contenedor2ID1.Visible = False


            txtBuscar.Visible = True
            btnBuscar0.Visible = True
            ddlProveedor0.Visible = True
            btnAsignar.Visible = True
            TabContainer1.Visible = True
            'contenedorID.Attributes.Add("style", "overflow-y: auto; height: 700px;")
            'contenedorID.Attributes.Add("class", "alturaLarga")
            'contenedor2ID1.Visible = True
            chkContrato.Enabled = False
            fupCarteNeteo.Enabled = False

        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        odsProveedores.FilterExpression = "razonSocial LIKE '%" & txtBuscarProveedor.Text.Trim & "%'"

        valida_Proveedor()
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        btnSolicitar.Visible = False
        btnSolicitar.Enabled = False
        Dim guuid As String = Guid.NewGuid.ToString
        Dim guuidCN As String = Guid.NewGuid.ToString

        Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter

        Dim taRegContable As New dsProduccionTableAdapters.CXP_RegContTableAdapter
        Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_tipoDeDocumentoTableAdapter
        Dim taGenFasesCorreo As New dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter
        Dim taCFDI As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
        Dim dtDatosFactura As New dsProduccion.vw_CXP_XmlCfdi2_grpUuidDataTable
        Dim taUUIDPagos As New dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter
        Dim taComprobacionGtos As New dsProduccionTableAdapters.CXP_ComprobGtosTableAdapter

        Dim rptSolPago As New ReportDocument
        Dim folSolPagoFinagil As Integer = 0
        Dim folioPolizaDiario As Integer = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))

        If CDec(txtImporteCartaNeteto.Text) > 0 And fupCarteNeteo.HasFile = False Then
            lblErrorGeneral.Text = "No se ha seleccionado ninguna Carta Neteo como archivo adjunto"
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        'Nuevo para cuentas bancarias
        'Dim lblTipar As Label = CType(FormView3.FindControl("tipoContrato"), Label)
        'MsgBox(lblTipar.Text)
        'If Not IsNothing(lblTipar) Then


        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
            idCuentas = cmbCuentasBancarias.SelectedValue
        Else
            idCuentas = 0
        End If

        'End If
        '/*****

        Dim mail As String = "#" & taGenFasesCorreo.ObtieneCorreo_ScalarQuery(ddlAutorizo.SelectedValue)
        Dim nombreAutorizante2 As String = taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("OPERACIONES_CXP") 'taGenFasesCorreo.ObtienNombre_ScalarQuery(ddlAutorizo.SelectedValue)
        If ddlAutorizo.SelectedItem.Text <> "" Then
            If IsNumeric(txtMontoSolicitado.Text) Then
                folSolPagoFinagil = taEmpresas.ConsultaFolio(Session.Item("Empresa"))
                Session.Item("namePDF") = Session.Item("Empresa") & "-" & folSolPagoFinagil
                taEmpresas.ConsumeFolio(Session.Item("Empresa"))
                If ddlMismoDeudor.SelectedValue = "Elegir proveedor" Then
                    If chkContrato.Checked = True Then
                        taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", CDec(txtMontoSolicitado.Text), 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                        If CDec(txtImporteCartaNeteto.Text) > 0 And fupCarteNeteo.HasFile = True Then
                            taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "CN", "CARTA NETEO", guuidCN, "0", "-" & txtImporteCartaNeteto.Text, 0, 0, "CARTA NETEO", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                            Dim archivoPDF As HttpPostedFile = fupCarteNeteo.PostedFile
                            If Session.Item("Empresa") = "23" Then
                                archivoPDF.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))
                                taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                            Else
                                archivoPDF.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                                taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                            End If
                        End If
                    Else
                        If ddlConcepto.SelectedValue <> taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) Then
                            taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", txtMontoSolicitado.Text, 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                        Else
                            '//Inserta documentos digitales CFDI y ND
                            taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", CDec(txtMontoSolicitado.Text), 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                            Dim contador As Integer = 0
                            For Each rows As GridViewRow In GridView2.Rows
                                taUUIDPagos.ObtDatosFactura_FillBy(dtDatosFactura, GridView2.Rows(contador).Cells(2).Text)
                                For Each rowsa As dsProduccion.vw_CXP_XmlCfdi2_grpUuidRow In dtDatosFactura.Rows
                                    Dim percentPago As Decimal = CDec(GridView2.Rows(contador).Cells(5).Text) / CDec(GridView2.Rows(contador).Cells(4).Text)
                                    taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rowsa.fechaEmision, rowsa.serie, rowsa.folio, rowsa.uuid, Math.Round(rowsa.subTotal * percentPago, 2), CDec(GridView2.Rows(contador).Cells(5).Text), 0, 0, GridView2.Rows(contador).Cells(3).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, rowsa.moneda, Date.Now, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                Next
                                contador += 1
                            Next
                            Dim contadorND As Integer = 0
                            For Each rowsND As GridViewRow In GridView3.Rows
                                Dim guidND As String = Guid.NewGuid.ToString
                                'taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), "ND", CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, GridView3.Rows(contadorND).Cells(0).Text, txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, "", "")
                                taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now.ToLongDateString, "", "ND", guidND, 0, CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, 0, GridView3.Rows(contadorND).Cells(0).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, "MXN", Date.Now.ToLongDateString, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                taCFDI.Insert("", "", 0, "", 0, guidND, "", "", "", "", 0, "I", "", "ND", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                                contadorND += 1
                            Next

                        End If
                    End If
                Else
                    If chkContrato.Checked = True Then
                        taCXPPagos.Insert(Session.Item("idDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", CDec(txtMontoSolicitado.Text), 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                        If CDec(txtImporteCartaNeteto.Text) > 0 And fupCarteNeteo.HasFile = True Then
                            taCXPPagos.Insert(Session.Item("idDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "CN", "CARTA NETEO", guuidCN, "0", "-" & txtImporteCartaNeteto.Text, 0, 0, "CARTA NETEO", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                            Dim archivoPDF As HttpPostedFile = fupCarteNeteo.PostedFile
                            If Session.Item("Empresa") = "23" Then
                                archivoPDF.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))

                            Else
                                archivoPDF.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                            End If
                        End If
                    Else
                        If ddlConcepto.SelectedValue <> taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) Then
                            taCXPPagos.Insert(Session.Item("idDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", txtMontoSolicitado.Text, 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                        Else
                            '**************************************
                            taCXPPagos.Insert(Session.Item("idDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", CDec(txtMontoSolicitado.Text), 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                            Dim contador As Integer = 0
                            For Each rows As GridViewRow In GridView2.Rows
                                taUUIDPagos.ObtDatosFactura_FillBy(dtDatosFactura, GridView2.Rows(contador).Cells(2).Text)
                                For Each rowsa As dsProduccion.vw_CXP_XmlCfdi2_grpUuidRow In dtDatosFactura.Rows
                                    Dim percentPago As Decimal = CDec(GridView2.Rows(contador).Cells(5).Text) / CDec(GridView2.Rows(contador).Cells(4).Text)
                                    taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rowsa.fechaEmision, rowsa.serie, rowsa.folio, rowsa.uuid, Math.Round(rowsa.subTotal * percentPago, 2), CDec(GridView2.Rows(contador).Cells(5).Text), 0, 0, GridView2.Rows(contador).Cells(3).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, rowsa.moneda, Date.Now, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                Next
                                contador += 1
                            Next
                            Dim contadorND As Integer = 0

                            For Each rowsND As GridViewRow In GridView3.Rows
                                Dim guidND As String = Guid.NewGuid.ToString
                                'taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), "ND", CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, GridView3.Rows(contadorND).Cells(0).Text, txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, "", "")
                                taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now.ToLongDateString, "", "ND", guidND, 0, CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, 0, GridView3.Rows(contadorND).Cells(0).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, "MXN", Date.Now.ToLongDateString, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                taCFDI.Insert("", "", 0, "", 0, guidND, "", "", "", "", 0, "I", "", "ND", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                                contadorND += 1
                            Next

                        End If
                    End If
                End If

                subirArchivosAdjuntos(folSolPagoFinagil, Session.Item("idCDeudor"))

                Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesTableAdapter
                Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
                Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter

                Dim dtSolPDF As DataTable
                dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesDataTable
                Dim dtSolPDFSD As DataTable
                dtSolPDFSD = New dsProduccion.Vw_CXP_AutorizacionesDataTable
                Dim dtSolPDFND As DataTable
                dtSolPDFND = New dsProduccion.Vw_CXP_AutorizacionesDataTable

                Dim dtObsSol As DataTable
                dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
                taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), CDec(folSolPagoFinagil))
                taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), folSolPagoFinagil, "No Pagada")
                taSolicitudPDF.DetalleSD_FillBy(dtSolPDFSD, CDec(Session.Item("Empresa")), CDec(folSolPagoFinagil))
                taSolicitudPDF.DetalleND_FillBy(dtSolPDFND, CDec(Session.Item("Empresa")), CDec(folSolPagoFinagil))

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
                rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folSolPagoFinagil))
                rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
                rptSolPago.SetParameterValue("var_contrato", chkContrato.Checked)
                rptSolPago.SetParameterValue("var_idCuentas", idCuentas)

                If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
                    rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
                Else
                    rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
                End If


                Dim rutaPDF As String = "~\TmpFinagil\" & Session.Item("namePDF") & ".pdf"
                rptSolPago.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))
                Response.Write("<script>")
                rutaPDF = rutaPDF.Replace("\", "/")
                rutaPDF = rutaPDF.Replace("~", "..")
                Response.Write("window.open('verPdf.aspx','popup','_blank','width=200,height=200')")
                Response.Write("</script>")
                rptSolPago.Dispose()

                cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
                cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
                divRevisar.Visible = True
            Else
                lblErrorGeneral.Text = "El importe solicitado no es numérico"
                ModalPopupExtender1.Show()
                bloquea()
            End If
        Else
            lblErrorGeneral.Text = "No se ha seleccionado un autorizante"
            ModalPopupExtender1.Show()
            bloquea()
        End If
        limpiar()
    End Sub

    Private Sub subirArchivosAdjuntos(ByVal foliosSolicitud As Decimal, ByVal deudor As Decimal)
        Dim taCFDI As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
        Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        If fup1.HasFiles Then
            For Each files As HttpPostedFile In fup1.PostedFiles

                'MsgBox(Right(fup1.PostedFile.ContentType.Trim, 3).ToString)

                If fup1.FileBytes.Length > 500000 Then
                    lblErrorGeneral.Text = "El tamaño del archivo no puede ser mayor a 5 MB"
                    Exit Sub
                ElseIf Right(fup1.PostedFile.ContentType.Trim, 3).ToString <> "PDF" And Right(fup1.PostedFile.ContentType.Trim, 3).ToString <> "pdf" Then
                    lblErrorGeneral.Text = "El tipo de archivo no puede ser distinto a PDF"
                    Exit Sub
                End If
            Next
            For Each files As HttpPostedFile In fup1.PostedFiles

                Dim guuidCN As String = Guid.NewGuid.ToString
                If Session.Item("Empresa") = "23" Then
                    files.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, cmbCuentasBancarias.SelectedValue)
                Else
                    files.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, cmbCuentasBancarias.SelectedValue)
                End If
            Next
        End If
    End Sub

    Public Sub limpiar()
        btnRevisar.Visible = True
        txtRevision.Text = ""
        txtRevision.Visible = False
        btnCancelar.Visible = False
        btnSolicitar.Visible = False
        txtBuscarProveedor.Text = ""
        txtMontoSolicitado.Text = ""
        txtDescripcionPago.Text = ""
        txtFechaPago.Text = Date.Now.ToShortDateString
        txtBuscarProveedor.Enabled = False
        btnBuscar.Enabled = False
        ddlProveedor.Enabled = False
        ddlClientes.Enabled = False
        ddlContratos.Enabled = False
        txtImporteCartaNeteto.Enabled = False
        txtImporteCartaNeteto.Text = ""
        lblError.Visible = False
        fupCarteNeteo.Visible = False
        fupCarteNeteo.Dispose()
        chkContrato.Checked = False
        odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
        'odsConceptos.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "'" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        ddlMismoDeudor.SelectedIndex = 1
        'dtDetalleAg.Dispose()
        'dtDetallebg.Dispose()
        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
        'cmbCentroDeCostos.Enabled = True
        'cmbFormaPago.Enabled = True
        lblAdjuntos.Visible = False
        fup1.Visible = False
        idCuentas = 0

        odsClientes.DataBind()
        GridView1.DataBind()
        GridView2.DataBind()
        GridView3.DataBind()
        'chkContrato.Checked = False

        'tablaDatosSol.Visible = False
        'tablaReferenciaBancaria.Visible = False
        tablaContratos.Visible = False
        tablaAutorizante.Visible = False

        'tablaReferenciaBancaria.Visible = False
        divRevisar.Visible = False

        ddlAutorizo.Enabled = True

        cexFechaPago.Enabled = True
        txtFechaPago.Enabled = True

        cmbCuentasBancarias.Enabled = True
        ddlMoneda.Enabled = True
        txtMontoSolicitado.Enabled = True
        txtDescripcionPago.Enabled = True

        validaEstatusProveedor(Session.Item("idDeudor"))

        GridView1.Visible = True

        btnAgregar.Visible = True
        TabContainer1.Visible = True
        'btnRevisar.Visible = False
        'btnCancelarRev.Visible = False

    End Sub


    Protected Sub btnRevisar_Click(sender As Object, e As EventArgs) Handles btnRevisar.Click
        Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
        If ddlAutorizo.SelectedItem.Text <> "" Then
            If IsNumeric(txtMontoSolicitado.Text) Then

                Dim proveedor As String

                If ddlMismoDeudor.Text = "Mismo Deudor" Then
                    proveedor = "Deudor: " & txtDeudor.Text
                Else
                    proveedor = "Proveedor: " & ddlProveedor.SelectedItem.Text
                End If

                If CDec(lblDeducibles.Text) + CDec(lblNDeducibles.Text) <> CDec(txtMontoSolicitado.Text) Then
                    lblErrorGeneral.Text = "La suma de los importes deducibles y no deducibles es diferente al importe solicitado."
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                If txtDescripcionPago.Text.Trim = String.Empty Then
                    lblErrorGeneral.Text = "No se ha agregado una descripción del pago"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                'Valida cuenta
                Dim datosBancarios As String
                If ddlMismoDeudor.SelectedIndex <> 2 Then
                    If ddlConcepto.SelectedValue <> "" Then
                        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) And taConceptos.ObtExigirCtaBancaria__ScalarQuery(ddlConcepto.SelectedValue) = "SI" Then

                            If cmbCuentasBancarias.SelectedIndex = -1 Then
                                lblErrorGeneral.Text = "Cuando la forma de pago es por Tranferencia Elctrónica se debe seleccionar una cuenta bancaria de pago."
                                ModalPopupExtender1.Show()
                                Exit Sub
                            Else
                                idCuentas = cmbCuentasBancarias.SelectedValue
                                datosBancarios = cmbCuentasBancarias.SelectedItem.Text
                            End If
                        Else
                            datosBancarios = "SIN DATOS BANCARIOS"
                            cmbCuentasBancarias.Enabled = False
                            idCuentas = 0
                        End If
                    Else
                        lblErrorGeneral.Text = "No existe un concepto asignado a este usuario"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                Else
                    cmbCuentasBancarias.Enabled = False
                    idCuentas = 0
                End If
                '******

                If chkContrato.Checked = True And CDec(txtImporteCartaNeteto.Text) = 0 Then
                    Dim taAnexos As New dsProduccionTableAdapters.AnexosTableAdapter
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & txtMontoSolicitado.Text & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Contrato: " & ddlContratos.SelectedValue & vbNewLine &
                           "Cliente: " & ddlClientes.SelectedItem.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                    'ModalPopupExtender2.Show()
                ElseIf chkContrato.Checked = True And CDec(txtImporteCartaNeteto.Text) > 0 Then
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & CDec(txtMontoSolicitado.Text & vbNewLine) - CDec(txtImporteCartaNeteto.Text) & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Contrato: " & ddlContratos.SelectedValue & vbNewLine &
                           "Cliente: " & ddlClientes.SelectedItem.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                    "Datos bancarios: " & datosBancarios
                    'ModalPopupExtender2.Show()
                ElseIf ddlConcepto.SelectedValue = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) Then
                    If CDec(txtMontoSolicitado.Text) <> (Session.Item("totalb") + Session.Item("totala")) Then
                        lblErrorGeneral.Text = "El importe a reembolsar es distinto al monto solicitado"
                        ModalPopupExtender1.Show()
                        bloquea()
                        Exit Sub
                    End If
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & CDec(txtMontoSolicitado.Text & vbNewLine) & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Importe deducible: " & FormatCurrency(Session.Item("totala")) & vbNewLine &
                           "Importe no deducible: " & FormatCurrency(Session.Item("totalb")) & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                    "Datos bancarios: " & datosBancarios
                    'ModalPopupExtender2.Show()
                Else
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & txtMontoSolicitado.Text & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                    ' ModalPopupExtender2.Show()
                End If
                btnRevisar.Visible = False
            Else
                lblErrorGeneral.Text = "El importe solicitado no es numérico"
                ModalPopupExtender1.Show()
                bloquea()
            End If

            If IsNumeric(txtImporteCartaNeteto.Text) > 0 Then
                lblErrorGeneral.Text = "El importe solicitado para la carta neteo no es numérico"
                ModalPopupExtender1.Show()
                bloquea()
            End If
            If IsNumeric(txtImporteCartaNeteto.Text) Then
                If CDec(txtImporteCartaNeteto.Text) > 0 Then
                    fupCarteNeteo.Visible = True
                Else
                    fupCarteNeteo.Visible = False
                End If
            End If
        Else
            bloquea()
            lblErrorGeneral.Text = "No se ha seleccionado un autorizante"
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        'cmbCentroDeCostos.Enabled = False
        'cmbFormaPago.Enabled = False
        'cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"), CDec(Session.Item("Empresa")))
        'cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
        txtMontoSolicitado.Enabled = False
        txtDescripcionPago.Enabled = False
        txtFechaPago.Enabled = False
        divRevisar.Visible = True
        desbloquea()
    End Sub

    Public Sub bloquea()
        lblAdjuntos.Visible = False
        fup1.Visible = False
        btnSolicitar.Visible = False
        txtRevision.Visible = False
        btnCancelar.Visible = False
    End Sub

    Public Sub desbloquea()
        lblAdjuntos.Visible = True
        fup1.Visible = True
        btnSolicitar.Visible = True
        txtRevision.Visible = True
        btnCancelar.Visible = True
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiar()
        bloquea()
        txtMontoSolicitado.Enabled = True
        txtDescripcionPago.Enabled = True
        txtFechaPago.Enabled = True
        btnCancelar.Visible = False
        btnSolicitar.Visible = False
        GridView1.DataSource = Session.Item("")
        GridView2.DataSource = Session.Item("")
        txtImporteCartaNeteto.Text = "0"
        divRevisar.Visible = False
    End Sub

    Protected Sub chkContrato_CheckedChanged(sender As Object, e As EventArgs) Handles chkContrato.CheckedChanged

        Dim dtDatosEmpresa As New dsProduccion.CXP_EmpresasDataTable
        Dim drDatosEmpresa As dsProduccion.CXP_EmpresasRow


        taTipoDocumento.DatosEmpresa_FillBy(dtDatosEmpresa, Session.Item("Empresa"))

        If dtDatosEmpresa.Rows.Count > 0 Then
            drDatosEmpresa = dtDatosEmpresa.Rows(0)
        End If

        If chkContrato.Checked = True Then
            ddlClientes.Enabled = True
            ddlContratos.Enabled = True
            odsContratos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
            fupCarteNeteo.Enabled = True
            txtImporteCartaNeteto.Enabled = True
            odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"
            'odsConceptos.FilterExpression = "idConcepto = '" & drDatosEmpresa.idConceptoPagoCtos & "'"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        Else
            ddlClientes.Enabled = False
            ddlContratos.Enabled = False
            fupCarteNeteo.Enabled = False
            txtImporteCartaNeteto.Enabled = False
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
            'odsConceptos.FilterExpression = "idConcepto = '" & drDatosEmpresa.idConceptoGastos & "'"
            'odsConceptos.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "'" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        End If
    End Sub

    Protected Sub ddlClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClientes.SelectedIndexChanged
        odsContratos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
    End Sub

    Protected Sub ddlMismoDeudor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMismoDeudor.SelectedIndexChanged
        'If ddlMismoDeudor.SelectedIndex = 1 Then
        '    txtBuscarProveedor.Enabled = False
        '    btnBuscar.Enabled = False
        '    ddlProveedor.Enabled = False
        'Else
        '    txtBuscarProveedor.Enabled = True
        '    btnBuscar.Enabled = True
        '    ddlProveedor.Enabled = True
        'End If
        tablaAutorizante.Visible = False
        tablaContratos.Visible = False
        tablaDatos.Visible = False
        If ddlMismoDeudor.SelectedIndex = 1 Or ddlMismoDeudor.SelectedIndex = 2 Then
            txtBuscarProveedor.Enabled = False
            btnBuscar.Enabled = False
            ddlProveedor.Enabled = False
            btnSeleccionarProv.Enabled = False
            'tablaBuscar.Visible = False
        Else
            txtBuscarProveedor.Enabled = True
            btnBuscar.Enabled = True
            ddlProveedor.Enabled = True
            btnSeleccionarProv.Enabled = True
            tablaAutorizante.Visible = False
            tablaContratos.Visible = False
            tablaDatos.Visible = False
            tablaAgregarCFDI.Visible = False
            tablaCFDI.Visible = False
        End If
        If ddlMismoDeudor.SelectedIndex <> 2 Then
            cmbCuentasBancarias.Enabled = True
            If ddlMismoDeudor.SelectedIndex = 1 Then
                validaEstatusProveedor(Session.Item("idDeudor"))
            End If
        Else
            tablaAutorizante.Visible = True
            tablaContratos.Visible = True
            tablaDatos.Visible = True
            cmbCuentasBancarias.Enabled = False
            idCuentas = 0
        End If
        If ddlMismoDeudor.SelectedIndex = 1 Then
            chkContrato.Enabled = False
        Else
            chkContrato.Enabled = True
        End If
    End Sub


    Protected Sub btnBuscar0_Click(sender As Object, e As EventArgs) Handles btnBuscar0.Click
        odsProveedores0.FilterExpression = "razonSocial LIKE '%" & txtBuscar.Text.Trim & "%'"
    End Sub

    Protected Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click
        Session.Item("rfcEmisor") = taProveedor.ObtRfc_ScalarQuery(ddlProveedor0.SelectedValue)
        odsCFDI.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedor0.SelectedValue) & "' AND rfcReceptor ='" & Session.Item("rfcEmpresa") & "'"

        If valida_Proveedor2() = "ERR" Then
            lblEFOSDesc.Text = "Pago no Procedente a Proveedor"
            lblEFOSEnc.Text = "¡ Alerta !"
            ModalPopupExtender3.Show()
            Exit Sub
        End If

        GridView1.Visible = True

        'If GridView1.Rows.Count > 0 Then
        btnAgregar.Visible = True
        TabContainer1.Visible = True
        'End If

    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim conta As Integer

        Dim rowA As DataRow

        dtDetalleAg = Session.Item("dtDetalleA")

        For Each rows As GridViewRow In GridView1.Rows
            Dim chkg As CheckBox = rows.Cells(0).FindControl("chk")
            Dim txtTot As TextBox = rows.Cells(10).FindControl("txtMontoAPagar")
            Dim txtCon As TextBox = rows.Cells(11).FindControl("txtConceptoFactura")

            If chkg.Checked = True Then

                If txtCon.Text.Length = 0 Then
                    lblErrorGeneral.Text = "Falta descripción del pago"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                Dim contb As Integer = 0
                For Each rows1 As GridViewRow In GridView2.Rows
                    If GridView1.Rows(conta).Cells(7).Text = GridView2.Rows(contb).Cells(2).Text Then
                        lblErrorGeneral.Text = "Ya existe el UUID: " & GridView1.Rows(conta).Cells(7).Text
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    contb += 1
                Next

                rowA = dtDetalleAg.NewRow
                rowA("serie") = GridView1.Rows(conta).Cells(6).Text.Replace("&nbsp;", "")
                rowA("folio") = GridView1.Rows(conta).Cells(5).Text.Replace("&nbsp;", "")
                rowA("uuid") = GridView1.Rows(conta).Cells(7).Text
                rowA("concepto") = txtCon.Text
                rowA("total1") = GridView1.Rows(conta).Cells(11).Text.Replace("$", "")
                rowA("total") = CDec(txtTot.Text)
                dtDetalleAg.Rows.Add(rowA)

                Session.Item("totala") = Session.Item("totala") + CDec(txtTot.Text)

                Session.Item("dtDetalleA") = dtDetalleAg

            End If
            conta += 1
        Next

        GridView2.DataSource = Session.Item("dtDetalleA") 'dtDetalleAg
        GridView2.DataBind()

        If conta > 0 Then
            GridView2.Visible = True
            btnNoDeducible.Visible = True
        End If

        If Not TabContainer1.Visible Then
            TabContainer1.Visible = True
        End If
    End Sub

    Protected Sub btnNoDeducible_Click(sender As Object, e As EventArgs) Handles btnNoDeducible.Click

        Session.Item("totalb") = Session.Item("totalb") + CDec(txtImporteND.Text)
        Dim rowComp As DataRow

        dtDetalleBg = Session.Item("dtDetalleB")

        rowComp = dtDetallebg.NewRow
        Session.Item("dtDetalleB") = dtDetallebg
        rowComp("descripcion") = txtConceptoND.Text
        If IsNumeric(txtImporteND.Text) Then
            rowComp("importe") = CDec(txtImporteND.Text)
        Else
            lblErrorGeneral.Text = "El importe solicitado no es numérico"
            ModalPopupExtender1.Show()
        End If

        dtDetallebg.Rows.Add(rowComp)

        Session.Item("dtDetalleB") = dtDetalleBg

        GridView3.DataSource = Session.Item("dtDetalleB") 'dtDetalleB
        GridView3.DataBind()
        'btnComprobar.Visible = True
    End Sub


    Private Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound
        Dim con_b As Integer = 0
        Dim con_c As Integer = 0

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "MONTO NO DEDUCIBLE: "
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Text = FormatCurrency(Session.Item("totalb"))
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True

            txtConceptoND.Text = ""
            txtImporteND.Text = "0"

            actualizaTablaTotales()
        End If

    End Sub

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand

        dtDetalleAg = Session.Item("dtDetalleA")

        If e.CommandName = "Eliminar" Then
            dtDetalleAg.Rows.RemoveAt(e.CommandArgument)
            Session.Item("totala") -= CDec(GridView2.Rows(e.CommandArgument).Cells(5).Text)

            actualizaTablaTotales()
        End If

        Session.Item("dtDetalleA") = dtDetalleAg

        GridView2.DataSource = Session.Item("dtDetalleA")
        GridView2.DataBind()


    End Sub

    Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        Dim con_a As Integer = 0

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = "Importe Facturas: "
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Text = FormatCurrency(Session.Item("totala")) 'totalfacturas.ToString("C")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True

            actualizaTablaTotales()
        End If
    End Sub

    Private Sub actualizaTablaTotales()
        lblDeducibles.Text = FormatCurrency(Session.Item("totala"), 2)
        lblNDeducibles.Text = FormatCurrency(Session.Item("totalb"), 2)

        If CDec(Session.Item("totala")) > 0 Or CDec(Session.Item("totalb")) > 0 Then
            tablaTotales.Visible = True
        Else
            tablaTotales.Visible = False
        End If



        'If CDec(Session.Item("totalb")) > 0 Then
        '    tablaTotales.Visible = True
        'Else
        '    tablaTotales.Visible = False
        'End If
    End Sub



    Private Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        dtDetalleBg = Session.Item("dtDetalleB")

        If e.CommandName = "Eliminar" Then
            dtDetalleBg.Rows.RemoveAt(e.CommandArgument)
            Session.Item("totalb") -= CDec(GridView3.Rows(e.CommandArgument).Cells(1).Text)
            actualizaTablaTotales()
        End If

        Session.Item("dtDetalleB") = dtDetalleBg

        GridView3.DataSource = Session.Item("dtDetalleB")
        GridView3.DataBind()
    End Sub

    Private Sub valida_Proveedor()
        lbl69.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69B.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69.ForeColor = Color.Green
        lbl69B.ForeColor = Color.Green

        Session.Item("rfcEmisor") = taProveedor.ObtRfc_ScalarQuery(ddlProveedor.SelectedValue)
        'comprobantesFiscales.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedores.SelectedValue) & "' AND rfcReceptor ='" & Session.Item("rfcEmpresa") & "'"
        'GridView1.Visible = True

        Dim ta69 As New dsProduccionTableAdapters.CRED_Lista_Art69TableAdapter
        Dim ta69B As New dsProduccionTableAdapters.CRED_Lista_Art69BTableAdapter
        Dim dt69 As New dsProduccion.CRED_Lista_Art69DataTable
        Dim dt69B As New dsProduccion.CRED_Lista_Art69BDataTable

        ta69.ObtEst_FillBy(dt69, Session.Item("rfcEmisor"))

        For Each rows69 As dsProduccion.CRED_Lista_Art69Row In dt69
            lbl69.ForeColor = Color.Yellow
            Select Case rows69.supuesto
                Case "FIRMES"
                    lbl69.Text = "1. DE CONTRIBUYENTE QUE TIENE CRÉDITOS FISCALES FIRMES"
                Case "EXIGIBLES"
                    lbl69.Text = "2. CRÉDITOS EXIGIBLES, NO PAGADOS O GARANTIZADOS"
                Case "CANCELADOS"
                    lbl69.Text = "3. CRÉDITOS CANCELADOS"
                Case "CONDONADOS"
                    lbl69.Text = "4. CRÉDITOS CONDONADOS"
                Case "SENTENCIA"
                    lbl69.Text = "5. DE CONTRIBUYENTE QUE TIENE SENTENCIA CONDENATORIA EJECUTORIA POR LA COMISIÓN DE UN DELITO FISCAL"
                Case "NO LOCALIZADO"
                    lbl69.Text = "NO LOCALIZADO"
            End Select
        Next

        ta69B.ObtEst_FillBy(dt69B, Session.Item("rfcEmisor"))

        For Each rows69B As dsProduccion.CRED_Lista_Art69BRow In dt69B
            lbl69B.ForeColor = Color.Red
            If rows69B.status_cont <> "Desvirtuado" Or rows69B.status_cont <> "" Then
                lbl69B.Text = "NO PROCEDE EL PAGO A PROVEEDOR, SOLICITAR ACLARACION"
                GridView1.Enabled = False
            Else
                ddlAutorizo.Enabled = False
                txtMontoSolicitado.Enabled = False
                txtDescripcionPago.Enabled = False
                txtFechaPago.Enabled = False
                chkContrato.Enabled = False
            End If
        Next
    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged
        'odsCuentasBancarias.FilterExpression = "idProveedor =" & ddlProveedor.SelectedValue
        'GridView1.Visible = True
        'valida_Proveedor()
    End Sub

    Private Function valida_Proveedor2()
        lbl69.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69B.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69.ForeColor = Color.Green
        lbl69B.ForeColor = Color.Green

        Session.Item("rfcEmisor2") = taProveedor.ObtRfc_ScalarQuery(ddlProveedor0.SelectedValue)
        'comprobantesFiscales.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedores.SelectedValue) & "' AND rfcReceptor ='" & Session.Item("rfcEmpresa") & "'"
        'GridView1.Visible = True

        Dim ta69 As New dsProduccionTableAdapters.CRED_Lista_Art69TableAdapter
        Dim ta69B As New dsProduccionTableAdapters.CRED_Lista_Art69BTableAdapter
        Dim dt69 As New dsProduccion.CRED_Lista_Art69DataTable
        Dim dt69B As New dsProduccion.CRED_Lista_Art69BDataTable

        ta69.ObtEst_FillBy(dt69, Session.Item("rfcEmisor2"))

        For Each rows69 As dsProduccion.CRED_Lista_Art69Row In dt69
            lbl69.ForeColor = Color.Yellow
            Select Case rows69.supuesto
                Case "FIRMES"
                    lbl69.Text = "1. DE CONTRIBUYENTE QUE TIENE CRÉDITOS FISCALES FIRMES"
                Case "EXIGIBLES"
                    lbl69.Text = "2. CRÉDITOS EXIGIBLES, NO PAGADOS O GARANTIZADOS"
                Case "CANCELADOS"
                    lbl69.Text = "3. CRÉDITOS CANCELADOS"
                Case "CONDONADOS"
                    lbl69.Text = "4. CRÉDITOS CONDONADOS"
                Case "SENTENCIA"
                    lbl69.Text = "5. DE CONTRIBUYENTE QUE TIENE SENTENCIA CONDENATORIA EJECUTORIA POR LA COMISIÓN DE UN DELITO FISCAL"
                Case "NO LOCALIZADO"
                    lbl69.Text = "NO LOCALIZADO"
            End Select
            Return lbl69.Text
        Next

        ta69B.ObtEst_FillBy(dt69B, Session.Item("rfcEmisor2"))

        For Each rows69B As dsProduccion.CRED_Lista_Art69BRow In dt69B
            lbl69B.ForeColor = Color.Red
            If rows69B.status_cont <> "Desvirtuado" Or rows69B.status_cont <> "" Then
                lbl69B.Text = "NO PROCEDE EL PAGO A PROVEEDOR, SOLICITAR ACLARACION"
                Return "ERR"
            End If
        Next
    End Function

    Protected Sub btnSeleccionarProv_Click(sender As Object, e As EventArgs) Handles btnSeleccionarProv.Click
        'If taProveedor.EsActivo_ScalarQuery(ddlProveedor.SelectedValue) = "NO" Then
        '    lblErrorGeneral.Text = "El proveedor no está activo o autorizado"
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'Else
        '    odsCuentasBancarias.FilterExpression = "idProveedor = '" & ddlProveedor.SelectedValue & "' AND vigente = true"
        '    valida_Proveedor()
        'End If


        validaEstatusProveedor(ddlProveedor.SelectedValue)
        'limpiar()

        odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
        'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1)" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
    End Sub

    Protected Sub validaEstatusProveedor(idProveedor As String)
        If idProveedor <> String.Empty Then
            If taProveedor.EsActivo_ScalarQuery(idProveedor) = "NO" Then
                lblErrorGeneral2.Text = "El proveedor no está activo o autorizado"
                ModalPopupExtender4.Show()
                tablaAutorizante.Visible = False
                tablaContratos.Visible = False
                tablaDatos.Visible = False
                tablaAgregarCFDI.Visible = False
                Session.Item("solicitud") = "OK"
                Session.Item("noProveedor") = idProveedor
                Exit Sub
            Else
                'odsCuentasBancarias.FilterExpression = "idProveedor = '" & idProveedor & "' AND estatus = 11"
                tablaAutorizante.Visible = True
                tablaContratos.Visible = True
                tablaDatos.Visible = True
                tablaAgregarCFDI.Visible = True
                If btnSeleccionarProv.Enabled = True Then
                    valida_Proveedor()
                    odsCuentasBancarias.FilterExpression = "idProveedor = '" & idProveedor & "' AND estatus = 11"
                Else
                    odsCuentasBancarias.FilterExpression = "idProveedor = '" & idProveedor & "' AND estatus = 11"
                End If
            End If
        End If
        tablaCFDI.Visible = True
    End Sub
    Protected Sub formato()
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnRevisar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSolicitar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnBuscar0.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAsignar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAgregar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSolicitar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnNoDeducible.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSeleccionarProv.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelarRev.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)

        GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView3.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView3.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)

        tablaSelecciona.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaSelecciona.Attributes.Add("class", "labelsA")

        tablaBuscar.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaBuscar.Attributes.Add("class", "labelsA")

        tablaAutorizante.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaAutorizante.Attributes.Add("class", "labelsA")

        tablaContratos.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaContratos.Attributes.Add("class", "labelsA")

        tablaCFDI.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaCFDI.Attributes.Add("class", "labelsA")

        tablaAgregarCFDI.Attributes.Add("style", "position:relative; margin-top:20px; width:60%; border-radius:5px; border-style: groove; border-width: 3px;font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF; margin-left: auto; margin-right: auto;")
        tablaAgregarCFDI.Attributes.Add("class", "labelsA")

        tablaDatos.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaDatos.Attributes.Add("class", "labelsA")

        divRevisar.Attributes.Add("style", "position:fixed;top:30%;left:20%; width:60%; padding:5px; border-radius:5px; border-style: groove; border-width: 5px; border-color: navy; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(255,255,255,1) 9%, rgba(252,252,252,1) 10%, rgba(246,246,246,1) 12%, rgba(187,218,249,1) 32%, rgba(75,165,255,1) 70%, rgba(75,165,255,1) 87%);")
        divRevisar.Attributes.Add("class", "labelsA")
    End Sub

    Protected Sub cmbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
            cmbCuentasBancarias.Enabled = True
        Else
            cmbCuentasBancarias.Enabled = False
        End If
    End Sub

    Protected Sub btnCancelarRev_Click(sender As Object, e As EventArgs) Handles btnCancelarRev.Click
        Response.Redirect("~/frmSinReembolso.aspx")
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
        Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim rptSolPago As New ReportDocument
        Dim folio As String = "1134"
        Dim idPago As String = "0"

        Dim dtSolPDF As DataTable
        dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesDataTable
        Dim dtSolPDFSD As DataTable
        dtSolPDFSD = New dsProduccion.Vw_CXP_AutorizacionesDataTable
        Dim dtSolPDFND As DataTable
        dtSolPDFND = New dsProduccion.Vw_CXP_AutorizacionesDataTable

        Dim dtObsSol As DataTable
        dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
        taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), CDec(folio))
        taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), folio, "No Pagada")
        taSolicitudPDF.DetalleSD_FillBy(dtSolPDFSD, CDec(Session.Item("Empresa")), CDec(folio))
        taSolicitudPDF.DetalleND_FillBy(dtSolPDFND, CDec(Session.Item("Empresa")), CDec(folio))

        Dim dtCtasBanco As DataTable
        dtCtasBanco = New dsProduccion.CXP_CuentasBancariasProvDataTable
        taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, idPago)

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
        rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folio))
        rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
        rptSolPago.SetParameterValue("var_contrato", chkContrato.Checked)
        rptSolPago.SetParameterValue("var_idCuentas", idPago)

        If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
            rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
        Else
            rptSolPago.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
        End If


        Dim rutaPDF As String = "~\TmpFinagil\" & Session.Item("Empresa") & "-" & folio & ".pdf"
        rptSolPago.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))
        Response.Write("<script>")
        rutaPDF = rutaPDF.Replace("\", "/")
        rutaPDF = rutaPDF.Replace("~", "..")
        Response.Write("window.open('verPdf.aspx','popup','_blank','width=200,height=200')")
        Response.Write("</script>")
        rptSolPago.Dispose()
    End Sub

End Class