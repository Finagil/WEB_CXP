Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports AjaxControlToolkit

Public Class frmSinComprobante
    Inherits System.Web.UI.Page
    Dim taTiposCambio As New dsProduccionTableAdapters.CONT_TiposDeCambioTableAdapter
    Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim taProveedor As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
    Dim totalfacturas As Decimal = 0
    Dim contadorNoDeduc As Integer = 1
    Dim totalGastoas As Decimal = 0
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

            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            odsCuentasBancarias.FilterExpression = "idProveedor = 0"

            If ddlConcepto.Items.Count = -1 Then
                lblErrorGeneral.Text = "El usuario: " & Session.Item("Usuario") & " no tiene conceptos relacionados. Favor de verificarlo con el área contable."
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            txtFechaPago.Text = Date.Now.ToShortDateString
            lblFechaSolicitud.Text = Date.Now.ToShortDateString
            ddlMoneda.SelectedValue = "MXN"
            Session.Item("Leyenda") = "Solicitud de pagos sin comprobante fiscal"
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

            odsContratos.FilterExpression = "cliente = '" & ddlClientes.SelectedValue & "'"
            cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
            cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))


            dtDetalleB = New DataTable("ComprobantesComprobacion")
            dtDetalleB.Columns.Add("descripcion", Type.GetType("System.String"))
            dtDetalleB.Columns.Add("importe", Type.GetType("System.Decimal"))


            dtDetalleA = New DataTable("ComprobantesA")

            dtDetalleA.Columns.Add("serie", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("folio", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("uuid", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("concepto", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("total", Type.GetType("System.Decimal"))
            dtDetalleA.Columns.Add("total1", Type.GetType("System.Decimal"))

            txtBuscar.Visible = False
            btnBuscar0.Visible = False
            ddlProveedor0.Visible = False
            btnAsignar.Visible = False
            TabContainer1.Visible = False
            'contenedorID.Attributes.Add("style", "overflow-y: auto; height: 500px;")
            'contenedorID.Attributes.Add("class", "alturaCorta")
            contenedor2ID.Visible = False

            If ddlMismoDeudor.SelectedIndex = 1 Then
                chkContrato.Enabled = False
            Else
                chkContrato.Enabled = True
            End If

        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        odsProveedores.FilterExpression = "razonSocial LIKE '%" & txtBuscarProveedor.Text.Trim & "%'"

        valida_Proveedor()

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

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
        Dim guuidAdjuntoCtas As String = Guid.NewGuid.ToString
        Try

            If CDec(txtImporteCartaNeteto.Text) > 0 And fupCarteNeteo.HasFile = False Then
                lblErrorGeneral.Text = "No se ha seleccionado ninguna Carta Neteo como archivo adjunto"
                ModalPopupExtender1.Show()
                Exit Sub
            End If


            'Insertar registro de datos bancarios para crédito simple y liquidez inmediata
            Dim lblTipar As Label = CType(FormView3.FindControl("tipoContrato"), Label)
            Dim taCuentasProv As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
            If Not IsNothing(lblTipar) Then
                If chkContrato.Checked = True Then
                    If lblTipar.Text.Trim = "L" Or lblTipar.Text.Trim = "S" Then
                        idCuentas = taCuentasProv.NuevaCuenta_ScalarQuery(0, ddlBancos.SelectedValue, txtCuenta.Text, txtClabe.Text, "PAGO CTOS " & ddlContratos.SelectedItem.Text, ddlMoneda.SelectedValue, Session.Item("guuidArchivoCtas"), True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 11, txtReferencia.Text.Trim, txtConvenio.Text.Trim)
                    Else
                        idCuentas = 0
                    End If
                Else
                    If taConceptos.ObtExigirCtaBancaria__ScalarQuery(ddlConcepto.SelectedValue) = "SI" Then
                        If Session.Item("ref") = "CRE" Then
                            idCuentas = taCuentasProv.NuevaCuenta_ScalarQuery(0, ddlBancos.SelectedValue, txtCuenta.Text, txtClabe.Text, "PAGO CTOS " & ddlContratos.SelectedItem.Text, ddlMoneda.SelectedValue, Session.Item("guuidArchivoCtas"), True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 11, txtReferencia.Text.Trim, txtConvenio.Text.Trim)
                        ElseIf Session.Item("ref") = "TRE" Then
                            idCuentas = cmbCuentasBancarias.SelectedValue
                        ElseIf Session.Item("ref") = "CHE" Then
                            idCuentas = 0
                        End If
                    Else
                        idCuentas = 0
                    End If
                End If
            Else
                If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) And taConceptos.ObtExigirCtaBancaria__ScalarQuery(ddlConcepto.SelectedValue) = "SI" Then
                    idCuentas = cmbCuentasBancarias.SelectedValue
                Else
                    If Session.Item("ref") = "CRE" Then
                        idCuentas = taCuentasProv.NuevaCuenta_ScalarQuery(0, ddlBancos.SelectedValue, txtCuenta.Text, txtClabe.Text, "PAGO CTOS " & ddlContratos.SelectedItem.Text, ddlMoneda.SelectedValue, Session.Item("guuidArchivoCtas"), True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 11, txtReferencia.Text.Trim, txtConvenio.Text.Trim)
                    Else
                        idCuentas = 0
                    End If
                End If
            End If
        Catch ex As Exception
            lblErrorGeneral.Text = ex.ToString
            ModalPopupExtender1.Show()
        End Try

        Try
            btnSolicitar.Enabled = False
            Dim guuid As String = Guid.NewGuid.ToString
            Dim guuidCN As String = Guid.NewGuid.ToString

            Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
            Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter

            Dim taRegContable As New dsProduccionTableAdapters.CXP_RegContTableAdapter
            'Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
            Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_tipoDeDocumentoTableAdapter
            Dim taGenFasesCorreo As New dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter
            Dim taCFDI As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
            Dim dtDatosFactura As New dsProduccion.vw_CXP_XmlCfdi2_grpUuidDataTable
            Dim taUUIDPagos As New dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter
            Dim taComprobacionGtos As New dsProduccionTableAdapters.CXP_ComprobGtosTableAdapter

            Dim rptSolPago As New ReportDocument
            Dim folSolPagoFinagil As Integer = 0
            Dim folioPolizaDiario As Integer = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))



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
                            'subirArchivosAdjuntos(folSolPagoFinagil, ddlProveedor.SelectedItem.Value)
                            Session.Item("idCDeudor") = ddlProveedor.SelectedItem.Value
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
                                    Dim contadorND As Integer = 0
                                    For Each rowsND As GridViewRow In GridView3.Rows
                                        'taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), "ND", CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, GridView3.Rows(contadorND).Cells(0).Text, txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, "", "")
                                        taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now.ToLongDateString, "", "ND", Guid.NewGuid.ToString, 0, CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, 0, GridView3.Rows(contadorND).Cells(0).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, "MXN", Date.Now.ToLongDateString, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                        contadorND += 1
                                    Next
                                Next
                            End If
                        End If
                    Else
                        If chkContrato.Checked = True Then
                            '*****************
                            Dim taCXProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
                            Dim dtCDeudor As New dsProduccion.CXP_ProveedoresDataTable
                            Dim rowsD As dsProduccion.CXP_ProveedoresRow
                            taCXProveedores.ObtDeudor_FillBy(dtCDeudor, Session.Item("rfcCliente"))

                            If dtCDeudor.Rows.Count > 0 Then
                                rowsD = dtCDeudor.Rows(0)
                                txtDeudor.Text = rowsD.razonSocial
                                Session.Item("idCDeudor") = rowsD.idProveedor
                            End If
                            '********************
                            taCXPPagos.Insert(Session.Item("idCDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "PSC", "PROVEEDOR", guuid, "0", CDec(txtMontoSolicitado.Text), 0, 0, txtDescripcionPago.Text, ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                            If CDec(txtImporteCartaNeteto.Text) > 0 And fupCarteNeteo.HasFile = True Then
                                taCXPPagos.Insert(Session.Item("idCDeudor"), 0, folSolPagoFinagil, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "CN", "CARTA NETEO", guuidCN, "0", "-" & txtImporteCartaNeteto.Text, 0, 0, "CARTA NETEO ( " & txtDesccartaNeteo.Text & " )", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), taGenFasesCorreo.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenFasesCorreo.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
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
                                    Dim contadorND As Integer = 0
                                    For Each rowsND As GridViewRow In GridView3.Rows
                                        'taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), "ND", CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, GridView3.Rows(contadorND).Cells(0).Text, txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, "", "")
                                        taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now.ToLongDateString, "", "ND", Guid.NewGuid.ToString, 0, CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, 0, GridView3.Rows(contadorND).Cells(0).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", Session.Item("mailJefe"), mail.Replace("#", ""), Nothing, Nothing, "MXN", Date.Now.ToLongDateString, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                                        contadorND += 1
                                    Next
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


                    Session("afuArchivoCtas") = Nothing
                    Session("nameAfuArchivoCtas") = Nothing
                    Session.Item("guuidArchivoCtas") = Nothing
                    'Evento contable
                    'Valida evento contable en concepto y contrato
                    'If taConceptos.GeneraEventoCont_ScalarQuery(ddlConcepto.SelectedValue) = False And chkContrato.Checked = False Then
                    '    taTipoDocumento.ConsumeFolio(CInt(Session.Item("tipoPoliza")))
                    'End If
#Region "generaEvenetoContable"
                    '***************
                    Try
                        If chkContrato.Checked = False And taConceptos.GeneraEventoCont_ScalarQuery(ddlConcepto.SelectedValue) = 1 Then
                            'If CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConcepto.SelectedValue)) <> 0 And CDec(taConceptos.ObtCtaIngreso_ScalarQuery(ddlConcepto.SelectedValue)) <> 0 Then
                            taRegContable.Insert(CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConcepto.SelectedValue)), CDec(ddlProveedor.SelectedValue), CDec(txtMontoSolicitado.Text) - CDec(txtImporteCartaNeteto.Text), 0, ddlProveedor.SelectedItem.Text, ddlConcepto.SelectedItem.Text & " - " & txtDescripcionPago.Text, CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), guuid, folSolPagoFinagil, Date.Now, 29, ddlConcepto.SelectedValue, CInt(Session.Item("idPeriodo")))
                            taRegContable.Insert(CDec(taConceptos.ObtCtaIngreso_ScalarQuery(ddlConcepto.SelectedValue)), CDec(ddlProveedor.SelectedValue), 0, CDec(txtMontoSolicitado.Text) - CDec(txtImporteCartaNeteto.Text), ddlProveedor.SelectedValue, ddlConcepto.SelectedItem.Text & " - " & txtDescripcionPago.Text, CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), guuid, folSolPagoFinagil, Date.Now, 29, ddlConcepto.SelectedValue, CInt(Session.Item("idPeriodo")))
                            taTipoDocumento.ConsumeFolio(CInt(Session.Item("tipoPoliza")))
                            'End If
                        End If
                    Catch ex As Exception
                        lblErrorGeneral.Text = ex.ToString.Substring(1, 100)
                        ModalPopupExtender1.Show()
                    End Try
#End Region

                    cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
                    cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))

                Else
                    lblErrorGeneral.Text = "El importe solicitado no es numérico"
                    ModalPopupExtender1.Show()
                End If
            Else
                lblErrorGeneral.Text = "No se ha seleccionado un autorizante"
                ModalPopupExtender1.Show()
            End If
            limpiar()

        Catch ex As Exception
            lblErrorGeneral.Text = ex.ToString
            ModalPopupExtender1.Show()
        End Try
    End Sub

    Private Sub subirArchivosAdjuntos(ByVal foliosSolicitud As Decimal, ByVal deudor As Decimal)
        Dim taCFDI As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
        Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        If fup1.HasFiles Then
            For Each files As HttpPostedFile In fup1.PostedFiles
                Dim guuidCN As String = Guid.NewGuid.ToString
                If Session.Item("Empresa") = "23" Then
                    files.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                Else
                    files.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
                End If
            Next
        End If

        If IsNothing(Session("afuArchivoCtas")) = False Then
            If Session.Item("Empresa") = "23" Then
                taCFDI.Insert("", "", 0, "", 0, Session.Item("guuidArchivoCtas"), "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", Session.Item("guuidArchivoCtas"), 0, 0, 0, 0, "Cta Pago", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
            Else
                taCFDI.Insert("", "", 0, "", 0, Session.Item("guuidArchivoCtas"), "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                taCXPPagos.Insert(deudor, 0, foliosSolicitud, lblFechaSolicitud.Text, lblFechaSolicitud.Text, "AD", "ADJUNTO", Session.Item("guuidArchivoCtas"), 0, 0, 0, 0, "Cta Pago", ddlConcepto.SelectedValue, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, ddlMoneda.SelectedValue, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas)
            End If
            Session("afuArchivoCtas") = Nothing
            'afuAdjuntoCta.SaveAs(MapPath("~/TmpFinagil/FilesProv/" & Convert.ToString(Session.Item("guuidArchivoCtas")) & ".pdf"))
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
        'odsConceptos.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1)" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        ddlMismoDeudor.SelectedIndex = 1
        TabContainer1.Visible = False
        GridView1.Visible = False
        btnAgregar.Visible = False
        txtBuscar.Visible = False
        btnBuscar0.Visible = False
        btnAsignar.Visible = False
        ddlProveedor0.Visible = False
        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
        cmbCentroDeCostos.Enabled = True
        cmbFormaPago.Enabled = True
        txtImporteCartaNeteto.Text = "0"
        lblAdjuntos.Visible = False
        fup1.Visible = False
        idCuentas = 0

        odsClientes.DataBind()
        chkContrato.Checked = False
        odsBancos.DataBind()
        txtCuenta.Text = ""
        txtClabe.Text = ""
        txtReferencia.Text = ""

        tablaDatosSol.Visible = False
        tablaReferenciaBancaria.Visible = False
        tablaContratos.Visible = False
        'tablaAutorizante.Visible = False

        tablaReferenciaBancaria.Visible = False
        divRevisar.Visible = False

        ddlAutorizo.Enabled = True

        cexFechaPago.Enabled = True
        txtFechaPago.Enabled = True

        cmbCuentasBancarias.Enabled = True
        ddlMoneda.Enabled = True
        txtMontoSolicitado.Enabled = True
        txtDescripcionPago.Enabled = True
        btnSolicitar.Enabled = True

        validaEstatusProveedor(Session.Item("idDeudor"))

        Session("afuArchivoCtas") = Nothing
        Session("nameAfuArchivoCtas") = Nothing
        Session.Item("guuidArchivoCtas") = Nothing
        Session.Item("ref") = Nothing

    End Sub

    Public Sub validaExisteProveedor(ByVal cliente As String)
        Dim taClientes As New dsProduccionTableAdapters.ClientesTableAdapter
        Dim dtClientes As New dsProduccion.ClientesDataTable
        Dim rowsc As dsProduccion.ClientesRow
        Dim taProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Dim dtProveedores As New dsProduccion.CXP_ProveedoresDataTable
        Dim rowsp As dsProduccion.CXP_ProveedoresRow

        taClientes.ObtDatosCliente_FillBy(dtClientes, cliente)

        If dtClientes.Rows.Count > 0 Then
            rowsc = dtClientes.Rows(0)
            Session.Item("rfcCliente") = rowsc.RFC
            If taProveedores.ExisteRFC_ScalarQuery(rowsc.RFC) = "NE" Then
                'inserta datos del cliente, pero ahora como proveedor, se difereniará de otro tipo de proveedor, por el campo activo = true
                taProveedores.Insert(rowsc.RFC, Nothing, rowsc.CURP, rowsc.Descr, Nothing, False, Nothing, Nothing, Nothing, Nothing, Date.Now, rowsc.EMail1, Nothing, Nothing, Nothing, Nothing, True, False, rowsc.Calle, rowsc.Colonia, rowsc.Colonia, rowsc.Delegacion, rowsc.Estado, "MEX", rowsc.Copos, "NO ACTIVO", "NO AUTORIZADO", Nothing, Nothing, Nothing, Nothing)
            End If

        End If
    End Sub

    Protected Sub btnRevisar_Click(sender As Object, e As EventArgs) Handles btnRevisar.Click
        Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
        Session.Item("ref") = taFormaPago.ObtRef_ScalarQuery(cmbFormaPago.SelectedValue)
        If ddlAutorizo.SelectedItem.Text <> "" Then
            If IsNumeric(txtMontoSolicitado.Text) Then


                If txtDescripcionPago.Text.Trim.Length > 199 Then
                    lblErrorGeneral.Text = "La descripción del pago supera la longitud permitida"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                Dim proveedor As String

                If ddlMismoDeudor.Text = "Mismo Deudor" Then
                    proveedor = "Deudor: " & txtDeudor.Text
                    If chkContrato.Checked = True Then
                        lblErrorGeneral.Text = "No es posible generar un solicitud de pago de contrato al mismo solicitante"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                ElseIf ddlMismoDeudor.Text = "Mismo Cliente" Then

                    validaExisteProveedor(ddlClientes.SelectedValue)

                    proveedor = "Cliente: " & ddlClientes.SelectedItem.Text
                Else
                    proveedor = "Proveedor: " & ddlProveedor.SelectedItem.Text
                End If

                'Valida cuenta
                Dim datosBancarios As String
                'If ddlMismoDeudor.SelectedIndex <> 2 Then
                If ddlConcepto.SelectedValue <> "" Then

                    If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) And taConceptos.ObtExigirCtaBancaria__ScalarQuery(ddlConcepto.SelectedValue) = "SI" Then
                        Dim lblTipar As Label = CType(FormView3.FindControl("tipoContrato"), Label)

                        If chkContrato.Checked = False Then
                            If cmbCuentasBancarias.SelectedIndex = -1 Then
                                lblErrorGeneral.Text = "Cuando la forma de pago es por Tranferencia Electrónica se debe seleccionar una cuenta bancaria de pago."
                                ModalPopupExtender1.Show()
                                Exit Sub
                            Else
                                idCuentas = cmbCuentasBancarias.SelectedValue
                                datosBancarios = cmbCuentasBancarias.SelectedItem.Text
                            End If
                        Else
                            If Not IsNothing(lblTipar) Then
                                If lblTipar.Text = "L" Or lblTipar.Text = "S" Then
                                    datosBancarios = "Banco: " & ddlBancos.SelectedItem.Text & " Cuenta: " & txtCuenta.Text.Trim & " CLABE: " & txtClabe.Text.Trim & " Referencia: " & txtReferencia.Text.Trim
                                    If IsNothing(Session("afuArchivoCtas")) Then
                                        lblErrorGeneral.Text = "No se ha ingresado el archivo de soporte de los datos bancarios"
                                        ModalPopupExtender1.Show()
                                        Exit Sub
                                    Else
                                        If txtClabe.Text.Trim = String.Empty And txtCuenta.Text.Trim = String.Empty And txtReferencia.Text.Trim = String.Empty And txtConvenio.Text.Trim = String.Empty Then
                                            lblErrorGeneral.Text = "No se ha ingresado al menos uno de los siguientes datos: CLABE, Cuenta o Referencia"
                                            ModalPopupExtender1.Show()
                                            Exit Sub
                                        Else
                                            If txtClabe.Text.Trim <> String.Empty And txtClabe.Text.Trim.Length <> 18 Then
                                                lblErrorGeneral.Text = "La CLABE debe de ser de 18 digitos"
                                                ModalPopupExtender1.Show()
                                                Exit Sub
                                            End If
                                            If txtCuenta.Text.Trim <> String.Empty And txtCuenta.Text.Trim.Length <> 10 Then
                                                lblErrorGeneral.Text = "La cuenta debe de ser de 10 digitos"
                                                ModalPopupExtender1.Show()
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Else
                                    datosBancarios = "SIN DATOS BANCARIOS"
                                    cmbCuentasBancarias.Enabled = False
                                    idCuentas = 0
                                End If
                            End If

                            '****Pago con referencia

                        End If
                    Else
                        If Session.Item("ref") = "CRE" Then
                            datosBancarios = "Banco: " & ddlBancos.SelectedItem.Text & " Cuenta: " & txtCuenta.Text.Trim & " CLABE: " & txtClabe.Text.Trim & " Convenio: " & txtConvenio.Text.Trim & " Referencia: " & txtReferencia.Text.Trim
                            If IsNothing(Session("afuArchivoCtas")) Then
                                lblErrorGeneral.Text = "No se ha ingresado el archivo de soporte de los datos bancarios"
                                ModalPopupExtender1.Show()
                                Exit Sub
                            Else
                                If txtClabe.Text.Trim = String.Empty And txtCuenta.Text.Trim = String.Empty And txtReferencia.Text.Trim = String.Empty And txtConvenio.Text.Trim = String.Empty Then
                                    lblErrorGeneral.Text = "No se ha ingresado al menos uno de los siguientes datos: CLABE, Cuenta o Referencia"
                                    ModalPopupExtender1.Show()
                                    Exit Sub
                                Else
                                    If txtClabe.Text.Trim <> String.Empty And txtClabe.Text.Trim.Length <> 18 Then
                                        lblErrorGeneral.Text = "La CLABE debe de ser de 18 digitos"
                                        ModalPopupExtender1.Show()
                                        Exit Sub
                                    End If
                                    If txtCuenta.Text.Trim <> String.Empty And txtCuenta.Text.Trim.Length <> 10 Then
                                        lblErrorGeneral.Text = "La cuenta debe de ser de 10 digitos"
                                        ModalPopupExtender1.Show()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Else
                            datosBancarios = "SIN DATOS BANCARIOS"
                            cmbCuentasBancarias.Enabled = False
                            idCuentas = 0
                        End If
                    End If
                Else
                    lblErrorGeneral.Text = "No existe un concepto asignado a este usuario"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If


                If chkContrato.Checked = True And CDec(txtImporteCartaNeteto.Text) = 0 Then
                    Dim lbl As Label = CType(FormView1.FindControl("saldoContrato"), Label)
                    If CDec(txtMontoSolicitado.Text.Trim) > CDec(lbl.Text.Trim) Then
                        lblErrorGeneral.Text = "El monto solicitado no puede ser mayor al Saldo del Contrato"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    Dim taAnexos As New dsProduccionTableAdapters.AnexosTableAdapter
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & txtMontoSolicitado.Text & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Contrato: " & ddlContratos.SelectedValue & vbNewLine &
                           "Cliente: " & ddlClientes.SelectedItem.Text & vbNewLine &
                           "Descripción carta neteo: " & txtDesccartaNeteo.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                ElseIf chkContrato.Checked = True And CDec(txtImporteCartaNeteto.Text) > 0 Then
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & CDec(txtMontoSolicitado.Text) - CDec(txtImporteCartaNeteto.Text) & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Contrato: " & ddlContratos.SelectedValue & vbNewLine &
                           "Cliente: " & ddlClientes.SelectedItem.Text & vbNewLine &
                           "Descripción carta neteo: " & txtDesccartaNeteo.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                ElseIf ddlConcepto.SelectedValue = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) Then
                    If CDec(txtMontoSolicitado.Text & vbNewLine) <> (totala + totalb) Then
                        lblErrorGeneral.Text = "El importe a reembolsar es distinto al monto solicitado"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & CDec(txtMontoSolicitado.Text) & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Importe deducible: " & totala.ToString("c") & vbNewLine &
                           "Importe no deducible: " & totalb.ToString("c") & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                Else
                    txtRevision.Text = "Descripción del pago: " & txtDescripcionPago.Text & vbNewLine &
                           "Importe solicitado: $ " & txtMontoSolicitado.Text & vbNewLine &
                           "Moneda: " & ddlMoneda.SelectedValue & vbNewLine &
                           proveedor & vbNewLine &
                           "Fecha de solicitud: " & lblFechaSolicitud.Text & vbNewLine &
                           "Sucursal (CC): " & cmbCentroDeCostos.SelectedItem.Text & vbNewLine &
                           "Forma de Pago: " & cmbFormaPago.SelectedItem.Text & vbNewLine &
                           "Datos bancarios: " & datosBancarios
                End If



                btnRevisar.Visible = False
                btnSolicitar.Visible = True
                txtRevision.Visible = True
                btnCancelar.Visible = True
                divRevisar.Visible = True
            Else
                lblErrorGeneral.Text = "El importe solicitado no es numérico"
                ModalPopupExtender1.Show()
                limpiar()
            End If

            If IsNumeric(txtImporteCartaNeteto.Text) > 0 Then
                lblErrorGeneral.Text = "El importe solicitado para la carta neteo no es numérico"
                ModalPopupExtender1.Show()
                limpiar()
            End If
            If IsNumeric(txtImporteCartaNeteto.Text) Then
                If CDec(txtImporteCartaNeteto.Text) > 0 Then
                    fupCarteNeteo.Visible = True
                Else
                    fupCarteNeteo.Visible = False
                End If
            End If
        Else
            lblErrorGeneral.Text = "No se ha seleccionado un autorizante"
            ModalPopupExtender1.Show()
        End If
        lblAdjuntos.Visible = True
        fup1.Visible = True
        cmbCentroDeCostos.Enabled = False
        cmbFormaPago.Enabled = False
        ddlAutorizo.Enabled = False
        cexFechaPago.Enabled = False
        txtFechaPago.Enabled = False
        cmbCuentasBancarias.Enabled = False
        ddlMoneda.Enabled = False
        txtMontoSolicitado.Enabled = False
        txtDescripcionPago.Enabled = False
        txtDesccartaNeteo.Enabled = False
        'cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"), CDec(Session.Item("Empresa")))
        'cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiar()
        divRevisar.Visible = False
        'tablaAutorizante.Visible = False
        tablaContratos.Visible = False
        tablaDatosSol.Visible = False
        btnSeleccionarProv.Enabled = False
        validaEstatusProveedor(Session.Item("idDeudor"))
        cmbCuentasBancarias.Enabled = True

        cmbCentroDeCostos.Enabled = True
        cmbFormaPago.Enabled = True
        ddlAutorizo.Enabled = True
        cexFechaPago.Enabled = True
        txtFechaPago.Enabled = True

        ddlMoneda.Enabled = True
        txtMontoSolicitado.Enabled = True
        txtDescripcionPago.Enabled = True

        tablaReferenciaBancaria.Visible = False


        odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"


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
            fupCarteNeteo.Visible = True
            txtImporteCartaNeteto.Enabled = True
            txtDesccartaNeteo.Enabled = True
            odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"
            'odsConceptos.FilterExpression = "idConcepto = '" & drDatosEmpresa.idConceptoPagoCtos & "'"
            'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1)" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND idConcepto = '" & drDatosEmpresa.idConceptoPagoCtos & "'"
            'tablaReferenciaBancaria.Visible = True
            idCuentas = 0
            cmbCuentasBancarias.Enabled = False
        Else
            ddlClientes.Enabled = False
            ddlContratos.Enabled = False
            fupCarteNeteo.Enabled = False
            txtImporteCartaNeteto.Enabled = False
            txtDesccartaNeteo.Enabled = False
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
            'odsConceptos.FilterExpression = "idConcepto = '" & drDatosEmpresa.idConceptoGastos & "'"
            'odsConceptos.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1)" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
        End If
    End Sub

    Protected Sub ddlClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClientes.SelectedIndexChanged
        odsContratos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
    End Sub

    Protected Sub ddlMismoDeudor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMismoDeudor.SelectedIndexChanged
        Dim lblCta As Label = CType(FormView4.FindControl("CuentaCLABELabel"), Label)

        tablaContratos.Visible = False

        Select Case ddlMismoDeudor.SelectedIndex
            Case 0 'elegir proveedor
                chkContrato.Enabled = True
                txtBuscarProveedor.Enabled = True
                btnBuscar.Enabled = True
                ddlProveedor.Enabled = True
                btnSeleccionarProv.Enabled = True
                'cmbCuentasBancarias.Enabled = True
                validaEstatusProveedor(ddlProveedor.SelectedValue)
                tablaDatosSol.Visible = False
                txtClabe.Text = ""
                txtClabe.Enabled = True
                chkContrato.Checked = False
                tablaReferenciaBancaria.Visible = False
                tablaContratos.Visible = True
                cmbCuentasBancarias.Enabled = False

                odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
                odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
                cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
                cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))

                'odsCuentasBancarias.FilterExpression = "idProveedor = '0'"
            Case 1 'mismo deudor
                chkContrato.Enabled = False
                txtBuscarProveedor.Enabled = False
                btnBuscar.Enabled = False
                ddlProveedor.Enabled = False
                btnSeleccionarProv.Enabled = False
                cmbCuentasBancarias.Enabled = True
                validaEstatusProveedor(Session.Item("idDeudor"))
                'lblCta.Visible = False
                txtClabe.Text = ""
                txtClabe.Enabled = True
                chkContrato.Checked = False
                tablaReferenciaBancaria.Visible = False
                ddlClientes.Enabled = False
                ddlContratos.Enabled = False

                odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
                odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
                cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
                cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
            Case 2 'mismo cliente
                Dim dtDatosEmpresa As New dsProduccion.CXP_EmpresasDataTable
                Dim drDatosEmpresa As dsProduccion.CXP_EmpresasRow

                taTipoDocumento.DatosEmpresa_FillBy(dtDatosEmpresa, Session.Item("Empresa"))
                If dtDatosEmpresa.Rows.Count > 0 Then
                    drDatosEmpresa = dtDatosEmpresa.Rows(0)
                End If

                chkContrato.Enabled = False
                txtBuscarProveedor.Enabled = False
                btnBuscar.Enabled = False
                ddlProveedor.Enabled = False
                btnSeleccionarProv.Enabled = False
                tablaContratos.Visible = True
                tablaDatosSol.Visible = True
                cmbCuentasBancarias.Enabled = False
                chkContrato.Checked = True
                tablaReferenciaBancaria.Visible = False
                ddlClientes.Enabled = True
                ddlContratos.Enabled = True
                txtImporteCartaNeteto.Enabled = True
                txtDesccartaNeteo.Enabled = True
                fupCarteNeteo.Enabled = True

                odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"
                odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND idConcepto = '" & drDatosEmpresa.idConceptoPagoCtos & "'"
                ddlMoneda.SelectedValue = "MXN"
                'odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
                'odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' OR idConcepto ='" & taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "' OR eventoContable = 1 AND idConcepto <>'" & taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "')" '"idConcepto IN (" & Session.Item("Conceptos") & ") AND conComprobante = false"
                cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
                cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
        End Select
    End Sub

    Protected Sub ddlConcepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto.SelectedIndexChanged
        If ddlConcepto.SelectedValue = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) Then
            txtBuscar.Visible = True
            btnBuscar0.Visible = True
            ddlProveedor0.Visible = True
            btnAsignar.Visible = True
            TabContainer1.Visible = True
            'contenedorID.Attributes.Add("style", "overflow-y: auto; height: 700px;")
            'contenedorID.Attributes.Add("class", "alturaLarga")
            contenedor2ID.Visible = True
            chkContrato.Enabled = False
            fupCarteNeteo.Enabled = False
        Else
            txtBuscar.Visible = False
            btnBuscar0.Visible = False
            ddlProveedor0.Visible = False
            btnAsignar.Visible = False
            TabContainer1.Visible = False
            'contenedorID.Attributes.Add("style", "overflow-y: auto; height: 500px;")
            'contenedorID.Attributes.Add("class", "alturaCorta")
            contenedor2ID.Visible = False
            chkContrato.Enabled = True
            fupCarteNeteo.Enabled = True
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
        Else
            lblEFOSDesc.Text = valida_Proveedor2()
            lblEFOSEnc.Text = "¡ Validación de proveedor !"
            ModalPopupExtender3.Show()
        End If

        GridView1.Visible = True

        If GridView1.Rows.Count > 0 Then
            btnAgregar.Visible = True
            TabContainer1.Visible = True
        End If
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim conta As Integer
        Dim rowA As DataRow

        For Each rows As GridViewRow In GridView1.Rows
            Dim chkg As CheckBox = rows.Cells(0).FindControl("chk")
            Dim txtTot As TextBox = rows.Cells(10).FindControl("txtMontoAPagar")
            Dim txtCon As TextBox = rows.Cells(11).FindControl("txtConceptoFactura")

            If chkg.Checked = True Then


                If txtCon.Text.Length = 0 Then
                    lblErrorGeneral.Text = "Falta incluir la descripción de pago"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                rowA = dtDetalleA.NewRow
                rowA("serie") = GridView1.Rows(conta).Cells(6).Text.Replace("&nbsp;", "")
                rowA("folio") = GridView1.Rows(conta).Cells(5).Text.Replace("&nbsp;", "")
                rowA("uuid") = GridView1.Rows(conta).Cells(7).Text
                rowA("concepto") = txtCon.Text
                rowA("total1") = GridView1.Rows(conta).Cells(11).Text.Replace("$", "")
                rowA("total") = CDec(txtTot.Text)
                dtDetalleA.Rows.Add(rowA)

            End If
            conta += 1
        Next

        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()

        If conta > 0 Then
            GridView2.Visible = True
            btnNoDeducible.Visible = True
            'btnComprobar.Visible = True
        End If

        If Not TabContainer1.Visible Then
            TabContainer1.Visible = True
        End If
        'lblTotalGastos.Visible = True
    End Sub

    Protected Sub btnNoDeducible_Click(sender As Object, e As EventArgs) Handles btnNoDeducible.Click

        totalb = totalb + CDec(txtImporteND.Text)
        Dim rowComp As DataRow

        If txtConceptoND.Text.Length = 0 Then
            lblErrorGeneral.Text = "Falta incluir la descripción de pago"
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        rowComp = dtDetalleB.NewRow
        rowComp("descripcion") = txtConceptoND.Text
        If IsNumeric(txtImporteND.Text) Then
            rowComp("importe") = CDec(txtImporteND.Text)
        Else
            'ModalPopupExtender2.Show()
        End If



        dtDetalleB.Rows.Add(rowComp)

        GridView3.DataSource = dtDetalleB
        GridView3.DataBind()
        'btnComprobar.Visible = True
    End Sub


    Private Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound
        Dim con_b As Integer = 0
        Dim con_c As Integer = 0
        If e.Row.RowType = DataControlRowType.DataRow Then
            total_b += CDec(txtImporteND.Text) 'CDec(DataBinder.Eval(e.Row.DataItem, "total"))
            txtConceptoND.Text = ""
            txtImporteND.Text = "0"
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "MONTO NO DEDUCIBLE: "
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Text = totalb.ToString("C")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
            'con_b += 1
        End If

        If GridView3.Rows.Count > 0 Then
            For Each rows2 As GridViewRow In GridView3.Rows
                total_b += total_b + CDec(GridView3.Rows(con_c).Cells(1).Text)
                con_c += 1
            Next
        End If
    End Sub


    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Eliminar" Then
            dtDetalleA.Rows.RemoveAt(e.CommandArgument)
            totala -= CDec(GridView2.Rows(e.CommandArgument).Cells(4).Text)
        End If
        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()


    End Sub

    Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        Dim con_a As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then
            totalfacturas += CDec(DataBinder.Eval(e.Row.DataItem, "total"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "Importe Facturas: "
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).Text = totalfacturas.ToString("C")
            totala = totalfacturas
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'subirArchivosAdjuntos(1, 0)
        Dim rptSolPago As New ReportDocument
        Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
        Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim folio As String = "2498"
        Dim cuenta As String = "370"

        Dim dtSolPDF As DataTable
        dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesDataTable
        Dim dtSolPDFSD As DataTable
        dtSolPDFSD = New dsProduccion.Vw_CXP_AutorizacionesDataTable
        Dim dtSolPDFND As DataTable
        dtSolPDFND = New dsProduccion.Vw_CXP_AutorizacionesDataTable

        Dim dtObsSol As DataTable
        dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
        taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), folio)
        taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), folio, "No Pagada")
        taSolicitudPDF.DetalleSD_FillBy(dtSolPDFSD, CDec(Session.Item("Empresa")), folio)
        taSolicitudPDF.DetalleND_FillBy(dtSolPDFND, CDec(Session.Item("Empresa")), folio)

        Dim dtCtasBanco As DataTable
        dtCtasBanco = New dsProduccion.CXP_CuentasBancariasProvDataTable
        taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, cuenta)

        Dim var_observaciones As Integer = dtObsSol.Rows.Count
        Dim encripta As readXML_CFDI_class = New readXML_CFDI_class
        rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoSCCopia.rpt"))
        rptSolPago.SetDataSource(dtSolPDF)
        rptSolPago.Subreports("rptSubObservaciones").SetDataSource(dtObsSol)
        rptSolPago.Subreports("rptSubSolicitudSCND").SetDataSource(dtSolPDFND)
        rptSolPago.Subreports("rptSubSolicitudSCSD").SetDataSource(dtSolPDFSD)
        rptSolPago.Subreports("rptSubCtasBancarias").SetDataSource(dtCtasBanco)
        dtCtasBanco.WriteXml("E:\VS_Proj\WEB_cxp\cxP\TmpFinagil\ctas.xml")
        rptSolPago.Refresh()

        rptSolPago.SetParameterValue("var_SD", dtSolPDFSD.Rows.Count)
        rptSolPago.SetParameterValue("var_ND", dtSolPDFND.Rows.Count)
        rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folio))
        rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
        rptSolPago.SetParameterValue("var_contrato", True)
        rptSolPago.SetParameterValue("var_idCuentas", cuenta)

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
    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged

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

    Private Sub cmbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        Session.Item("ref") = taFormaPago.ObtRef_ScalarQuery(cmbFormaPago.SelectedValue)
        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
            cmbCuentasBancarias.Enabled = True
            tablaReferenciaBancaria.Visible = False
            txtCuenta.Enabled = True
            txtCuenta.ReadOnly = False
            txtClabe.Enabled = True
            txtClabe.ReadOnly = False
        ElseIf Session.Item("ref") = "CHE" Then
            cmbCuentasBancarias.Enabled = False
            tablaReferenciaBancaria.Visible = False
            txtCuenta.Enabled = True
            txtCuenta.ReadOnly = False
            txtClabe.Enabled = True
            txtClabe.ReadOnly = False
        ElseIf Session.Item("ref") = "CRE" Then
            tablaReferenciaBancaria.Visible = True
            cmbCuentasBancarias.Enabled = False
            txtCuenta.Enabled = False
            txtCuenta.ReadOnly = True
            txtClabe.Enabled = False
            txtClabe.ReadOnly = True
        End If
    End Sub

    Protected Sub btnSeleccionarProv_Click(sender As Object, e As EventArgs) Handles btnSeleccionarProv.Click
        Dim dtDatosEmpresa As New dsProduccion.CXP_EmpresasDataTable
        Dim drDatosEmpresa As dsProduccion.CXP_EmpresasRow
        If chkContrato.Checked = True Then
            If ddlMismoDeudor.SelectedIndex = 0 Then
                Dim lblTipar As Label = CType(FormView3.FindControl("tipoContrato"), Label)
                If lblTipar.Text.Trim <> "L" And lblTipar.Text.Trim <> "S" Then

                    validaEstatusProveedor(ddlProveedor.SelectedValue)
                Else
                    odsCuentasBancarias.FilterExpression = "idProveedor = '" & ddlProveedor.SelectedValue & "' AND estatus = 11"
                    tablaContratos.Visible = True
                    tablaDatosSol.Visible = True
                    If btnSeleccionarProv.Enabled = True Then
                        valida_Proveedor()
                    End If
                End If
            Else
                validaEstatusProveedor(ddlProveedor.SelectedValue)
            End If
            odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"
            ddlMoneda.SelectedValue = "MXN"
        Else
            validaEstatusProveedor(ddlProveedor.SelectedValue)
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV')"
            ddlMoneda.SelectedValue = "MXN"
        End If

        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))

    End Sub

    Protected Sub validaEstatusProveedor(idProveedor As String)
        If idProveedor <> String.Empty Then
            If taProveedor.EsActivo_ScalarQuery(idProveedor) = "NO" Then
                lblErrorGeneral2.Text = "El proveedor no está activo o autorizado"
                ModalPopupExtender4.Show()
                tablaContratos.Visible = False
                tablaDatosSol.Visible = False
                Session.Item("solicitud") = "OK"
                Session.Item("noProveedor") = idProveedor
                Exit Sub
            Else
                odsCuentasBancarias.FilterExpression = "idProveedor = '" & idProveedor & "' AND estatus = 11"
                tablaContratos.Visible = True
                tablaDatosSol.Visible = True

                If btnSeleccionarProv.Enabled = True Then
                    valida_Proveedor()
                End If
                If chkContrato.Checked = False Then
                    cmbCuentasBancarias.Enabled = True
                Else
                    cmbCuentasBancarias.Enabled = False
                End If
            End If
        End If
    End Sub

    Protected Sub obtieneSaldos(anexo As String)
        Dim tableAdapterSaldosContratos As New dsProduccionTableAdapters.AnexosTableAdapter
        Dim datatableSaldosContratos As New dsProduccion.AnexosDataTable
        Dim rowSaldoContratos As dsProduccion.AnexosRow

        tableAdapterSaldosContratos.SaldosAnexos_FillBy(datatableSaldosContratos, anexo)

        If datatableSaldosContratos.Rows.Count = 1 Then
            rowSaldoContratos = datatableSaldosContratos.Rows(0)

            If txtMontoSolicitado.Text <> String.Empty Then
                If CDec(txtMontoSolicitado.Text) > rowSaldoContratos.SaldoContrato Then
                    lblErrorGeneral.Text = "El saldo del contrato " & ddlContratos.SelectedValue & " es menor al monto solicitado."
                    ModalPopupExtender1.Show()
                    tablaContratos.Visible = True
                    tablaDatosSol.Visible = True
                    divRevisar.Visible = False
                End If
            End If
        End If

    End Sub


    Private Sub FormView3_DataBound(sender As Object, e As EventArgs) Handles FormView3.DataBound
        Dim lblTipar As Label = CType(FormView3.FindControl("tipoContrato"), Label)
        Dim lblCta As Label = CType(FormView4.FindControl("CuentaCLABELabel"), Label)
        Dim lblBco As Label = CType(FormView5.FindControl("Banco"), Label)
        If Not IsNothing(lblTipar) Then
            If cmbFormaPago.SelectedValue <> String.Empty Then
                If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
                    If Not IsNothing(lblTipar) Then
                        If chkContrato.Checked = True Then
                            If lblTipar.Text.Trim = "L" Or lblTipar.Text.Trim = "S" Then
                                tablaReferenciaBancaria.Visible = True
                                If Not IsNothing(lblCta) And Not IsNothing(lblBco) Then
                                    If ddlMismoDeudor.SelectedIndex = 2 Then
                                        txtClabe.Text = lblCta.Text.Trim
                                        odsBancos.FilterExpression = "nombreCorto = '" & lblBco.Text.Trim & "'"
                                        txtClabe.Enabled = False
                                        lblCta.Visible = False
                                        ddlBancos.Enabled = False
                                    Else
                                        lblCta.Visible = False
                                    End If
                                End If
                            Else
                                tablaReferenciaBancaria.Visible = False
                            End If
                        Else
                            tablaReferenciaBancaria.Visible = False
                            If ddlMismoDeudor.SelectedIndex = 2 Then
                                cmbCuentasBancarias.Enabled = True
                                'obtieneDatosBancariosClienteProveedor()
                            ElseIf ddlMismoDeudor.SelectedIndex = 0 Then
                                'cmbCuentasBancarias.Enabled = True
                                'obtieneDatosBancariosClienteProveedor()
                            ElseIf ddlMismoDeudor.SelectedIndex = 1 Then
                                cmbCuentasBancarias.Enabled = False
                            End If
                        End If
                    End If
                End If
            Else
                If Not IsNothing(lblTipar) Then
                    If chkContrato.Checked = True Then
                        If lblTipar.Text.Trim = "L" Or lblTipar.Text.Trim = "S" Then
                            tablaReferenciaBancaria.Visible = True
                            If Not IsNothing(lblCta) And Not IsNothing(lblBco) Then
                                If ddlMismoDeudor.SelectedIndex = 2 Then
                                    txtClabe.Text = lblCta.Text.Trim
                                    odsBancos.FilterExpression = "nombreCorto = '" & lblBco.Text.Trim & "'"
                                    txtClabe.Enabled = False
                                    lblCta.Visible = False
                                    ddlBancos.Enabled = False
                                Else
                                    lblCta.Visible = False
                                End If
                            End If
                        Else
                            tablaReferenciaBancaria.Visible = False
                        End If
                    Else
                        tablaReferenciaBancaria.Visible = False
                        If ddlMismoDeudor.SelectedIndex = 2 Then
                            cmbCuentasBancarias.Enabled = True
                            'obtieneDatosBancariosClienteProveedor()
                        ElseIf ddlMismoDeudor.SelectedIndex = 0 Then
                            'cmbCuentasBancarias.Enabled = True

                        ElseIf ddlMismoDeudor.SelectedIndex = 1 Then
                            cmbCuentasBancarias.Enabled = False
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub obtieneDatosBancariosClienteProveedor()
        Dim taClientes As New dsProduccionTableAdapters.ClientesTableAdapter
        Dim dtClientes As New dsProduccion.ClientesDataTable
        Dim rwClientes As dsProduccion.ClientesRow

        Dim taProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Dim dtProveedores As New dsProduccion.CXP_ProveedoresDataTable
        Dim rwProveedores As dsProduccion.CXP_ProveedoresRow

        taClientes.ObtDatosCliente_FillBy(dtClientes, ddlClientes.SelectedValue)
        If dtClientes.Rows.Count > 0 Then
            rwClientes = dtClientes.Rows(0)
            taProveedores.ObtDeudor_FillBy(dtProveedores, rwClientes.RFC)
            If dtProveedores.Rows.Count > 0 Then
                rwProveedores = dtProveedores.Rows(0)
                If taProveedor.EsActivo_ScalarQuery(rwProveedores.idProveedor) = "NO" Then
                    lblErrorGeneral2.Text = "El cliente no está activo o autorizado"
                    ModalPopupExtender4.Show()
                    Session.Item("solicitud") = "OK"
                    Session.Item("noProveedor") = rwProveedores.idProveedor
                    Exit Sub
                Else
                    odsCuentasBancarias.FilterExpression = "idProveedor = '" & rwProveedores.idProveedor & "' AND estatus = 11"
                End If
            End If
        End If
    End Sub

    Private Sub formato()
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnRevisar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSolicitar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnBuscar0.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAsignar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAgregar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSolicitar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSeleccionarProv.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)

        GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView3.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)

        tablaSelecciona.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaSelecciona.Attributes.Add("class", "labelsA")

        tablaBuscar.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaBuscar.Attributes.Add("class", "labelsA")

        tablaContratos.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaContratos.Attributes.Add("class", "labelsA")

        tablaReferenciaBancaria.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaReferenciaBancaria.Attributes.Add("class", "labelsA")

        tablaDatosSol.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaDatosSol.Attributes.Add("class", "labelsA")

        tablaRevisar.Attributes.Add("style", "position:fixed;top:30%;left:20%; width:60%; padding:5px; border-radius:5px; border-style: groove; border-width: 5px; border-color: navy; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(255,255,255,1) 9%, rgba(252,252,252,1) 10%, rgba(246,246,246,1) 12%, rgba(187,218,249,1) 32%, rgba(75,165,255,1) 70%, rgba(75,165,255,1) 87%);")
        tablaRevisar.Attributes.Add("class", "labelsA")
    End Sub

    Private Sub afuAdjuntoCta_UploadedComplete(sender As Object, e As AsyncFileUploadEventArgs) Handles afuAdjuntoCta.UploadedComplete
        Dim guuidAdjuntoCtas As String = Guid.NewGuid.ToString

        If afuAdjuntoCta.HasFile Then
            If Session.Item("Empresa") = "23" Then
                afuAdjuntoCta.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidAdjuntoCtas & ".pdf"))
            Else
                afuAdjuntoCta.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidAdjuntoCtas & ".pdf"))
            End If
            afuAdjuntoCta.SaveAs(MapPath("~/TmpFinagil/FilesProv/" & Convert.ToString(guuidAdjuntoCtas) & ".pdf"))
            End If
            Session("afuArchivoCtas") = afuAdjuntoCta
            Session("nameAfuArchivoCtas") = afuAdjuntoCta.FileName
            Session.Item("guuidArchivoCtas") = guuidAdjuntoCtas

    End Sub

    Protected Sub btnCancelar1_Click(sender As Object, e As EventArgs) Handles btnCancelar1.Click
        Response.Redirect("~/frmSinComprobante.aspx")
    End Sub

    Protected Sub FormView5_PageIndexChanging(sender As Object, e As FormViewPageEventArgs) Handles FormView5.PageIndexChanging

    End Sub
End Class

