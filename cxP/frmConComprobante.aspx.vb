﻿Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports AjaxControlToolkit

Public Class frmConComprobante
    Inherits System.Web.UI.Page
    Dim taCFDI As New dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter
    Dim taCFDI2 As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
    Dim taProveedor As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
    Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim total2 As Decimal = 0
    Dim dtDetalle As New DataTable("Comprobantes")
    Dim tableCFDI2 As New dsProduccion.CXP_XmlCfdi2DataTable
    Dim tableCFDI2Cont As New dsProduccion.CXP_XmlCfdi2DataTable
    Dim ds As New dsProduccion
    Dim rptSolPago As New ReportDocument
    Dim taCFDIImpuestos As New dsProduccionTableAdapters.Vw_CXP_ImpuestosCFDITableAdapter
    Dim dtCFDIImpuestos As New dsProduccion.Vw_CXP_ImpuestosCFDIDataTable
    Dim taSucursales As New dsProduccionTableAdapters.CXP_SucursalesTableAdapter
    Dim taFormaPago As New dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter
    Dim taTipoDeCambio As New dsProduccionTableAdapters.CONT_TiposDeCambioTableAdapter
    Dim taCuentasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
    Dim idCuentas As Integer = 0
    Dim tipoDeCambio As Decimal = 1
    Dim lblMoneda As Label
    Dim utilerias As New utilerias

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            formato()
        End If

        If Not IsPostBack Then
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV' AND Fase <> 'TES_CXP')"
            odsCuentasBancarias.FilterExpression = "idProveedor = 0"
            Session.Item("Leyenda") = "Solicitud de pagos con comprobante fiscal"
            txtFechaPago.Text = Date.Now.ToShortDateString
            If Session.Item("rfcEmpresa") = "SAR951230N5A" Then
                Session.Item("rutaCFDI") = "ARFIN/Todos/Procesados"
            Else
                Session.Item("rutaCFDI") = "FINAGIL/Todos/Procesados"
            End If

            If Request.QueryString("id") IsNot Nothing Then
                Dim ITCode As Integer = Integer.Parse(Request.QueryString("id").ToString())
                GetDocuments(ITCode)
            End If
            odsAnexosActivos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
            cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
            cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
            GridView1.Visible = False
        End If
    End Sub

    Private Sub formato()

        tablaBuscar.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        tablaBuscar.Attributes.Add("class", "labelsA")

        estatusEfos.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        estatusEfos.Attributes.Add("class", "labelsA")

        otrosDatos.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        otrosDatos.Attributes.Add("class", "labelsA")

        ctasBancarias.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        ctasBancarias.Attributes.Add("class", "labelsA")

        ctasBancarias.Attributes.Add("style", "border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #00BFFF;")
        ctasBancarias.Attributes.Add("class", "labelsA")

        'revision.Attributes.Add("style", "position:fixed;border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(255,255,255,1) 9%, rgba(252,252,252,1) 10%, rgba(246,246,246,1) 12%, rgba(187,218,249,1) 32%, rgba(75,165,255,1) 70%, rgba(75,165,255,1) 87%);")
        revision.Attributes.Add("style", "position:fixed;top:30%;left:20%; width:60%; padding:5px; border-radius:5px; border-style: groove; border-width: 5px; border-color: navy; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(255,255,255,1) 9%, rgba(252,252,252,1) 10%, rgba(246,246,246,1) 12%, rgba(187,218,249,1) 32%, rgba(75,165,255,1) 70%, rgba(75,165,255,1) 87%);")
        revision.Attributes.Add("class", "labelsA")


        GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView1.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelarBusqueda.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnProcesar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnVistaPrevia.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
    End Sub

    Protected Sub GetDocuments(ByVal ITCode As Integer)
        Dim filePaths As String() = Directory.GetFiles(("~/Procesados/" & ITCode))
        Dim files As List(Of ListItem) = New List(Of ListItem)()
        For Each filePath As String In filePaths
            files.Add(New ListItem(Path.GetFileName(filePath), filePath))
        Next
        GridView1.DataSource = files
        GridView1.DataBind()
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        proveedores.FilterExpression = "razonSocial LIKE '%" & txtBuscar.Text.Trim & "%' OR rfc LIKE '%" & txtBuscar.Text.Trim & "%'" '"razonSocial LIKE '%" & txtBuscar.Text.Trim & "%'"

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        'valida estatus del proveedor, si no está activo o vigente, solicita la autorización
        If taProveedor.EsActivo_ScalarQuery(ddlProveedores.SelectedValue) = "NO" Then
            lblErrorGeneral2.Text = "El proveedor no está activo o autorizado"
            ModalPopupExtender2.Show()
            Session.Item("solicitud") = "OK"
            Session.Item("noProveedor") = ddlProveedores.SelectedValue
            'Response.Redirect("~/frmConComprobante.aspx")
            'Exit Sub
            lbl69.Visible = False
            lbl69B.Visible = False
            ddlClientes.Visible = False
            ddlContratos.Visible = False
            ddlAutorizo.Visible = False
            cmbCentroDeCostos.Visible = False
            cmbFormaPago.Visible = False
            txtFechaPago.Visible = False
            cmbCuentasBancarias.Visible = False
            chkContrato.Visible = False
            divCtaBancaria.Visible = False
            divEfos.Visible = False
            divOtros.Visible = False
            'divFacturas.Visible = False
            Exit Sub
        Else
            lbl69.Visible = True
            lbl69B.Visible = True
            ddlClientes.Visible = True
            ddlContratos.Visible = True
            ddlAutorizo.Visible = True
            cmbCentroDeCostos.Visible = True
            cmbFormaPago.Visible = True
            txtFechaPago.Visible = True
            cmbCuentasBancarias.Visible = True
            chkContrato.Visible = True
            divCtaBancaria.Visible = True
            divEfos.Visible = True
            divOtros.Visible = True
            'divFacturas.Visible = True
        End If

        lbl69.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69B.Text = "PAGO PROCEDENTE A PROVEEDOR"
        lbl69.ForeColor = Color.Green
        lbl69B.ForeColor = Color.Green

        Session.Item("rfcEmisor") = taProveedor.ObtRfc_ScalarQuery(ddlProveedores.SelectedValue)

        'MsgBox(Session.Item("rfcEmisor") & Session.Item("mesesFacturas"))
        comprobantesFiscales.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedores.SelectedValue) & "' AND rfcReceptor ='" & Session.Item("rfcEmpresa") & "'"



        'comprobantesFiscales.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedores.SelectedValue) & "'"
        Session.Item("noProveedor") = ddlProveedores.SelectedValue
        odsCuentasBancarias.FilterExpression = "idProveedor = '" & ddlProveedores.SelectedValue & "' AND estatus = 11"
        GridView1.Visible = True



        Dim ta69 As New dsProduccionTableAdapters.CRED_Lista_Art69TableAdapter
        Dim ta69B As New dsProduccionTableAdapters.CRED_Lista_Art69BTableAdapter
        Dim dt69 As New dsProduccion.CRED_Lista_Art69DataTable
        Dim dt69B As New dsProduccion.CRED_Lista_Art69BDataTable

        ta69.ObtEst_FillBy(dt69, Session.Item("rfcEmisor"))

        For Each rows69 As dsProduccion.CRED_Lista_Art69Row In dt69
            lbl69.ForeColor = Color.GreenYellow
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
                GridView1.Enabled = True
                btnVistaPrevia.Visible = True
                btnCancelarBusqueda.Visible = True
            End If
        Next

        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))

        Dim taTipoDocumento1 As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Dim dtDatosEmpresa1 As New dsProduccion.CXP_EmpresasDataTable
        Dim drDatosEmpresa1 As dsProduccion.CXP_EmpresasRow
        taTipoDocumento1.DatosEmpresa_FillBy(dtDatosEmpresa1, Session.Item("Empresa"))
        If dtDatosEmpresa1.Rows.Count > 0 Then
            drDatosEmpresa1 = dtDatosEmpresa1.Rows(0)
        End If
        If chkContrato.Checked = True Then
            'ddlContratos.Enabled = True
            fupCartaNeteo.Visible = True
            txtDescCartaNeteo.Visible = True
            lblCarteNeteo.Visible = True
            lblImporte.Visible = True
            txtImporteCartaNeteto.Visible = True

            'odsConceptos.FilterExpression = "idConcepto = '" & drDatosEmpresa1.idConceptoPagoCtos & "'"
            odsAnexosActivos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
            'odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"

            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV' AND Fase <> 'TES_CXP')"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "')"

        Else
            ddlContratos.Enabled = False
            ddlClientes.Enabled = False
            fupCartaNeteo.Visible = False
            txtDescCartaNeteo.Visible = False
            lblImporte.Visible = False
            txtImporteCartaNeteto.Visible = False

            'lblCarteNeteo.Visible = False
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV' AND Fase <> 'TES_CXP')"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento1.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "')"
        End If



    End Sub



    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Dim taCuentasProv As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim taRegContable As New dsProduccionTableAdapters.CXP_RegContTableAdapter
        Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_tipoDeDocumentoTableAdapter
        Dim taImpuesto As New dsProduccionTableAdapters.CXP_ImpuestoTableAdapter
        Dim taGenCorresoFases As New dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter
        Dim taPeriodos As New dsProduccionTableAdapters.CXP_PeriodosTableAdapter
        Dim taCuentasBanco As New dsProduccionTableAdapters.CXP_CuentaBancoTableAdapter
        Dim monedaPago As String = ""
        Dim importeOtrosGastosIngresos As Decimal = 0
        Dim banderaOtrosIngresos As String = ""




        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
            If cmbCuentasBancarias.SelectedIndex = -1 Then
                lblErrorGeneral.Text = "Cuando la forma de pago es por Tranferencia Electrónica se debe seleccionar una cuenta bancaria de pago."
                ModalPopupExtender1.Show()
                Exit Sub
            Else
                idCuentas = cmbCuentasBancarias.SelectedValue
            End If
        Else
            If cmbFormaPago.SelectedIndex = 2 Then
                idCuentas = taCuentasProv.NuevaCuenta_ScalarQuery(0, ddlBancos.SelectedValue, txtCuenta.Text, txtClabe.Text, "PAGO CON REFERENCIA ", ddlMonedas.SelectedValue, Session.Item("guuidArchivoCtas"), True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 11, txtReferencia.Text.Trim, txtConvenio.Text.Trim, txtConcepto.Text.Trim)
                If txtReferencia.Text <> String.Empty And txtConvenio.Text = String.Empty Then
                    'inserta solo valor de referencia
                    If taCuentasbanco.existeCuenta_ScalarQuery(utilerias.obtNumCadena(txtReferencia.Text.Trim), "TCR") = "NE" Then
                        taCuentasBanco.Insert(txtReferencia.Text.Trim, ddlMonedas.SelectedValue, ddlProveedores.SelectedItem.Text, "TCR", False)
                    End If
                ElseIf txtConvenio.Text <> String.Empty Then
                    'inserta valor del convenio
                    If taCuentasbanco.existeCuenta_ScalarQuery(utilerias.obtNumCadena(txtConvenio.Text.Trim), "CIE") = "NE" Then
                        taCuentasBanco.Insert(txtConvenio.Text.Trim, ddlMonedas.SelectedValue, ddlProveedores.SelectedItem.Text, "CIE", False)
                    End If
                End If
            Else
                cmbCuentasBancarias.Enabled = False
                idCuentas = 0
            End If
        End If


        'Identifica moneda de pago
        monedaPago = taCuentasProv.ObtMoneda_ScalarQuery(idCuentas)

        Try

            If Session.Item("Usuario") = "" Then
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If

            Dim fechaRegistroCont As Date '= Date.Now


            If ddlAutorizo.SelectedItem.Text <> "" Then
                Dim mail As String = "#" & taGenCorresoFases.ObtieneCorreo_ScalarQuery(ddlAutorizo.SelectedValue)
                Dim nombreAutorizante2 As String = taGenCorresoFases.ObtieneNombreXFase_ScalarQuery("OPERACIONES_CXP")


                Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter

                dtDetalle.Columns.Add("rfcEmisor", Type.GetType("System.String"))
                dtDetalle.Columns.Add("rfcReceptor", Type.GetType("System.String"))
                dtDetalle.Columns.Add("serie", Type.GetType("System.String"))
                dtDetalle.Columns.Add("folio", Type.GetType("System.String"))
                dtDetalle.Columns.Add("uuid", Type.GetType("System.String"))
                dtDetalle.Columns.Add("impuesto", Type.GetType("System.String"))
                dtDetalle.Columns.Add("mImpuestoT", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("mImpuestoR", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("mImpLocalT", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("mImpLocalR", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("fechaEmision", Type.GetType("System.String"))
                dtDetalle.Columns.Add("factor", Type.GetType("System.String"))
                dtDetalle.Columns.Add("SubTotal", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("total", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("totalOrg", Type.GetType("System.Decimal"))
                dtDetalle.Columns.Add("observaciones", Type.GetType("System.String"))
                dtDetalle.Columns.Add("concepto", Type.GetType("System.String"))

                Dim row As DataRow
                Dim cont As Integer = 0
                Dim cont2 As Integer = 0
                Dim folSolPagoFinagil As Integer = 0
                Dim folSolPagoArfin As Integer = 0
                Dim conCT As Integer = 0
                Dim totalSolicitud As Decimal = 0
                Dim folioPolizaDiario As Integer = 0
                Dim banderaRegistroContable As String = "NO"

                Dim dtPeriodos As New dsProduccion.CXP_PeriodosDataTable
                Dim rwPeriodos As dsProduccion.CXP_PeriodosRow
                Dim fechaRegistro As String = "True"

                taPeriodos.Estatus_FillBy(dtPeriodos, CInt(Session.Item("Empresa")))
                If dtPeriodos.Rows.Count > 0 Then
                    rwPeriodos = dtPeriodos.Rows(0)
                End If

                folSolPagoFinagil = taEmpresas.ConsultaFolio(Session.Item("Empresa"))

                Session.Item("namePDF") = Session.Item("Empresa") & "-" & folSolPagoFinagil
                'validaTamanoArchiAdjunto(folSolPagoFinagil, Session.Item("idCDeudor"))
                taEmpresas.ConsumeFolio(Session.Item("Empresa"))

                'revisa filas del grid para validar fecha de facturas
                For Each rows As GridViewRow In GridView1.Rows
                    Dim ddlConc As DropDownList = rows.Cells(12).FindControl("ddlConceptos")
                    taCFDI2.SumaImp_FillBy(tableCFDI2Cont, GridView1.Rows(cont).Cells(7).Text)
                    For Each rows2 As dsProduccion.CXP_XmlCfdi2Row In tableCFDI2Cont
                        If chkContrato.Checked = False And taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue) = 1 Then
                            If CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConc.SelectedValue)) <> 0 And CDec(taConceptos.ObtCtaIngreso_ScalarQuery(ddlConc.SelectedValue)) <> 0 Then
                                fechaRegistroCont = CDate(rows2.fechaEmision)
                                fechaRegistro = "True"

                                If fechaRegistroCont.Month >= Date.Now.Month Then
                                    folioPolizaDiario = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))
                                    fechaRegistroCont = Date.Now
                                Else

                                    If rwPeriodos.estatus = 31 Then
                                        fechaRegistroCont = rwPeriodos.finPeriodo
                                        folioPolizaDiario = taPeriodos.ConsultaFolio_ScalarQuery(fechaRegistroCont.Year, fechaRegistroCont.Month, CInt(Session.Item("Empresa")))
                                        fechaRegistro = "False"
                                    Else
                                        folioPolizaDiario = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))
                                        fechaRegistroCont = Date.Now
                                        fechaRegistro = "True"
                                    End If

                                End If
                            End If
                        End If
                    Next
                Next

                For Each rows As GridViewRow In GridView1.Rows
                    Dim chkg As CheckBox = rows.Cells(0).FindControl("chk")
                    Dim txtTot As TextBox = rows.Cells(10).FindControl("txtMontoAPagar")
                    Dim txtObs As TextBox = rows.Cells(11).FindControl("txtObservaciones")
                    Dim ddlConc As DropDownList = rows.Cells(12).FindControl("ddlConceptos")
                    Dim txtPorcentaje As TextBox = rows.Cells(11).FindControl("txtPorcentaje")
                    'Dim generaEvento As Boolean = taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue)

                    If ddlConc.SelectedValue = 144 And Session.Item("jAlterno") = "False" Then
                        If Session.Item("Usuario") = "atorres" Then
                            Session.Item("mailJefe") = "atorres@finagil.com.mx"
                            Session.Item("Jefe") = "CP ABRAHAM TORRES"
                        End If
                    End If

                    Dim GeneraEventoContable As Boolean = False
                    If chkg.Checked = True Then
                        'If taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue) = True Then
                        '    GeneraEventoContable = True
                        'End If
                        taCFDI2.SumaImp_FillBy(tableCFDI2, GridView1.Rows(cont).Cells(7).Text)

                        For Each rows2 As dsProduccion.CXP_XmlCfdi2Row In tableCFDI2


                            Dim existeUUIDReg As String = taRegContable.ExisteUUID_ScalarQuery(rows2.uuid)

                            row = dtDetalle.NewRow

                            Dim percentPago As Decimal = CDec(txtTot.Text) / CDec(GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", ""))



                            If CDec(txtTot.Text) < rows2.total Then
                                If (rows2.rfcEmisor = "CSS160330CP7" Or rows2.rfcEmisor = "TNO8105076Q8" Or rows2.rfcEmisor = "CES671208JE5") And CDec(txtTot.Text) - 2 < rows2.total Then
                                    importeOtrosGastosIngresos = rows2.total - CDec(txtTot.Text)
                                    banderaOtrosIngresos = "OI"

                                    row("rfcEmisor") = rows2.rfcEmisor
                                    row("rfcReceptor") = rows2.rfcReceptor
                                    row("serie") = rows2.serie.ToString
                                    row("folio") = rows2.folio.ToString
                                    row("uuid") = rows2.uuid.ToString
                                    row("impuesto") = ""
                                    row("mImpuestoT") = rows2.montoImpuesto.ToString
                                    row("mImpuestoR") = rows2.montoImpuestoR.ToString
                                    row("mImpLocalT") = rows2.impLocTra.ToString
                                    row("mImpLocalR") = rows2.impLocRet.ToString
                                    row("fechaEmision") = rows2.fechaEmision.ToString
                                    row("factor") = ""
                                    row("subTotal") = rows2.subTotal
                                    row("total") = rows2.total
                                    row("totalOrg") = GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "")
                                    row("observaciones") = txtObs.Text
                                    row("concepto") = ddlConc.SelectedItem.Text
                                Else
                                    row("rfcEmisor") = rows2.rfcEmisor
                                    row("rfcReceptor") = rows2.rfcReceptor
                                    row("serie") = rows2.serie.ToString
                                    row("folio") = rows2.folio.ToString
                                    row("uuid") = rows2.uuid.ToString
                                    row("impuesto") = ""
                                    row("mImpuestoT") = Math.Round(CDec(rows2.montoImpuesto.ToString) * percentPago, 2)
                                    row("mImpuestoR") = Math.Round(CDec(rows2.montoImpuestoR.ToString) * percentPago, 2)
                                    row("mImpLocalT") = Math.Round(CDec(rows2.impLocTra.ToString) * percentPago, 2)
                                    row("mImpLocalR") = Math.Round(CDec(rows2.impLocRet.ToString) * percentPago, 2)
                                    row("fechaEmision") = rows2.fechaEmision.ToString
                                    row("factor") = ""
                                    row("subTotal") = Math.Round(CDec(rows2.subTotal) * percentPago, 2)
                                    row("total") = txtTot.Text
                                    row("totalOrg") = GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "")
                                    row("observaciones") = txtObs.Text
                                    row("concepto") = ddlConc.SelectedItem.Text
                                End If
                            ElseIf CDec(txtTot.Text) > rows2.total Then
                                'Permite ingresar importes menores o superiores según
                                If (rows2.rfcEmisor = "CSS160330CP7" Or rows2.rfcEmisor = "TNO8105076Q8" Or rows2.rfcEmisor = "CES671208JE5") And CDec(txtTot.Text) + 2 > rows2.total Then
                                    importeOtrosGastosIngresos = CDec(txtTot.Text) - rows2.total
                                    banderaOtrosIngresos = "OG"

                                    row("rfcEmisor") = rows2.rfcEmisor
                                    row("rfcReceptor") = rows2.rfcReceptor
                                    row("serie") = rows2.serie.ToString
                                    row("folio") = rows2.folio.ToString
                                    row("uuid") = rows2.uuid.ToString
                                    row("impuesto") = ""
                                    row("mImpuestoT") = rows2.montoImpuesto.ToString
                                    row("mImpuestoR") = rows2.montoImpuestoR.ToString
                                    row("mImpLocalT") = rows2.impLocTra.ToString
                                    row("mImpLocalR") = rows2.impLocRet.ToString
                                    row("fechaEmision") = rows2.fechaEmision.ToString
                                    row("factor") = ""
                                    row("subTotal") = rows2.subTotal
                                    row("total") = txtTot.Text
                                    row("totalOrg") = GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "")
                                    row("observaciones") = txtObs.Text
                                    row("concepto") = ddlConc.SelectedItem.Text
                                Else
                                    lblErrorGeneral.Text = "El importe a pagar no debe ser mayor al saldo de la factura"
                                    ModalPopupExtender1.Show()
                                    Exit Sub
                                End If
                            ElseIf IsNumeric(txtPorcentaje.Text) Then
                                row("rfcEmisor") = rows2.rfcEmisor
                                row("rfcReceptor") = rows2.rfcReceptor
                                row("serie") = rows2.serie.ToString
                                row("folio") = rows2.folio.ToString
                                row("uuid") = rows2.uuid.ToString
                                row("impuesto") = ""
                                row("mImpuestoT") = (CDec(rows2.montoImpuesto.ToString) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("mImpuestoR") = (CDec(rows2.montoImpuestoR.ToString) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("mImpLocalT") = (CDec(rows2.impLocTra.ToString) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("mImpLocalR") = (CDec(rows2.impLocRet.ToString) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("fechaEmision") = rows2.fechaEmision.ToString
                                row("factor") = ""
                                row("subTotal") = (CDec(rows2.subTotal) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("total") = (CDec(txtTot.Text) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("totalOrg") = (CDec(GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "")) * (CDec(txtPorcentaje.Text) / 100)).ToString
                                row("observaciones") = txtObs.Text
                                row("concepto") = ddlConc.SelectedItem.Text
                            Else
                                row("rfcEmisor") = rows2.rfcEmisor
                                row("rfcReceptor") = rows2.rfcReceptor
                                row("serie") = rows2.serie.ToString
                                row("folio") = rows2.folio.ToString
                                row("uuid") = rows2.uuid.ToString
                                row("impuesto") = ""
                                row("mImpuestoT") = rows2.montoImpuesto.ToString
                                row("mImpuestoR") = rows2.montoImpuestoR.ToString
                                row("mImpLocalT") = rows2.impLocTra.ToString
                                row("mImpLocalR") = rows2.impLocRet.ToString
                                row("fechaEmision") = rows2.fechaEmision.ToString
                                row("factor") = ""
                                row("subTotal") = rows2.subTotal
                                row("total") = txtTot.Text
                                row("totalOrg") = GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "")
                                row("observaciones") = txtObs.Text
                                row("concepto") = ddlConc.SelectedItem.Text
                            End If

                            totalSolicitud += CDec(row("total"))
                            'MsgBox(taProveedor.ObtCuentaProv_ScalarQuery(ddlProveedores.SelectedValue) + ddlProveedores.SelectedValue + GridView1.Rows(cont).Cells(11).Text.Replace(",", "").Replace("$", "") + "0" + ddlProveedores.SelectedItem.Text & " " & row("serie") & " " & row("folio") & row("observaciones") + ddlConc.SelectedValue.ToString, taConceptos.ObtTipoPoliza_ScalarQuery(ddlConc.SelectedValue) + taTipoDocumento.ConsultaFolio(taConceptos.ObtTipoPoliza_ScalarQuery(ddlConc.SelectedValue)) + Session.Item("Empresa"))

                            taCFDIImpuestos.CFDIImpuestos_Fill(dtCFDIImpuestos, rows2.uuid.ToString)

                            Dim taPagoImpuestos As New dsProduccionTableAdapters.CXP_PagosImpuestosTableAdapter
                            Dim errores As String = ""

                            If chkContrato.Checked = False Then
                                For Each rowsCfdi As dsProduccion.Vw_CXP_ImpuestosCFDIRow In dtCFDIImpuestos
                                    Dim efecto As String = ""
                                    Dim tipo As String = ""

                                    Dim mPago As Decimal = 0

                                    'Prueba de omisión
                                    If rowsCfdi.mTras <> "X" Then
                                        efecto = "TRA"
                                        mPago = Math.Round(CDec(Val(rowsCfdi.mTras) * percentPago), 2)
                                        tipo = "Federal"
                                        If taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) = "0" Then
                                            lblError2.Text = "Concepto: " & ddlConc.SelectedItem.Text & " Impuesto: " & rowsCfdi.Impuesto & " Efecto: " & efecto & " Factor: " & rowsCfdi.tipoFactor & " Tipo: " & tipo & " Sin impuesto configurado  " & vbNewLine
                                            errores = "OK"
                                        End If
                                    End If

                                    'Prueba de omision
                                    If rowsCfdi.mRet <> "X" Then
                                        efecto = "RET"
                                        mPago = Math.Round(CDec(Val(rowsCfdi.mRet) * percentPago), 2)
                                        tipo = "Federal"
                                        If taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) = "0" Then
                                            lblError2.Text = "Concepto: " & ddlConc.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                            errores = "OK"
                                        End If
                                    End If

                                    If rowsCfdi.mLocTra <> "X" And rowsCfdi.mLocTra <> 0 Then
                                        efecto = "LOC"
                                        mPago = Math.Round(CDec(Val(rowsCfdi.mLocTra) * percentPago), 2)
                                        tipo = "Local"
                                        If taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "TRA") = "0" Then
                                            lblError2.Text = "Concepto: " & ddlConc.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                            errores = "OK"
                                        End If
                                    End If

                                    If rowsCfdi.mLocRet <> "X" And rowsCfdi.mLocRet <> 0 Then
                                        efecto = "LOC"
                                        mPago = Math.Round(CDec(Val(rowsCfdi.mLocRet) * percentPago), 2)
                                        tipo = "Local"
                                        If taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "RET") = "0" Then
                                            lblError2.Text = "Concepto: " & ddlConc.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                            errores = "OK"
                                        End If
                                    End If


                                Next
                            End If
                            If errores = "OK" Then
                                lblError2.Visible = True
                                Exit Sub
                            End If
                            'Pago Parcial
                            If chkContrato.Checked = True Then
                                If cont = 0 Then
                                    taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rows2.fechaEmision, rows2.serie, rows2.folio, rows2.uuid, CDec(row("subTotal")), CDec(row("total")), CDec(row("mImpuestoT")) + CDec(row("mImpLocalT")), CDec(row("mImpuestoR")) + CDec(row("mImpLocalR")), row("observaciones"), ddlConc.SelectedItem.Value, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, rows2.moneda, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenCorresoFases.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                                Else
                                    taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rows2.fechaEmision, rows2.serie, rows2.folio, rows2.uuid, CDec(row("subTotal")), CDec(row("total")), CDec(row("mImpuestoT")) + CDec(row("mImpLocalT")), CDec(row("mImpuestoR")) + CDec(row("mImpLocalR")), row("observaciones"), ddlConc.SelectedItem.Value, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, rows2.moneda, CDate(txtFechaPago.Text), True, ddlContratos.SelectedValue, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenCorresoFases.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                                End If
                            Else
                                taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rows2.fechaEmision, rows2.serie, rows2.folio, rows2.uuid, CDec(row("subTotal")), CDec(row("total")), CDec(row("mImpuestoT")) + CDec(row("mImpLocalT")), CDec(row("mImpuestoR")) + CDec(row("mImpLocalR")), row("observaciones"), ddlConc.SelectedItem.Value, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, rows2.moneda, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)


#Region "ProvisionDiario"


                                'Provisión de diario
                                'If chkContrato.Checked = False And taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue) = True Then
                                If chkContrato.Checked = False And taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue) = 1 Then
                                    If CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConc.SelectedValue)) <> 0 And CDec(taConceptos.ObtCtaIngreso_ScalarQuery(ddlConc.SelectedValue)) <> 0 Then
                                        If existeUUIDReg = "NE" Then
                                            banderaRegistroContable = "SI"
                                            '***** se comenta para dividir importe en cada concepto
                                            'taRegContable.Insert(CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConc.SelectedValue)), CDec(ddlProveedores.SelectedValue), CDec(rows2.subTotal), 0, rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))

                                        Else
                                            taRegContable.ActualizaFolioSol_UpdateQuery(folSolPagoFinagil, rows2.uuid, CInt(Session.Item("Empresa")))
                                        End If
                                        Dim contador As Integer = 0
                                        For Each rowsCfdi As dsProduccion.Vw_CXP_ImpuestosCFDIRow In dtCFDIImpuestos
                                            Dim efecto As String = ""
                                            Dim tipo As String = ""
                                            Dim retecionL As String = ""
                                            Dim mPago As Decimal = 0

                                            If rowsCfdi.mTras <> "X" Then
                                                efecto = "TRA"
                                                mPago = Math.Round(CDec(Val(rowsCfdi.mTras) * percentPago), 2)
                                                tipo = "Federal"
                                                If existeUUIDReg = "NE" Then
                                                    'taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), rowsCfdi.mTras, 0, taConceptos.ObtCtaImpDesc_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) & " " & rows2.rfcEmisor, "F-" & row("serie") & " " & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    If rowsCfdi.tipoFactor = "Exento" Then
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.base), 2), 0, rows2.rfcEmisor & " Exento", "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    ElseIf rowsCfdi.tipoFactor = "No Objeto" Then
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.importeCon), 2), 0, rows2.rfcEmisor & " No Obj", "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    ElseIf rowsCfdi.tipoFactor = "Tasa" And CDec(rowsCfdi.mTras) = 0 And rowsCfdi.total <> rowsCfdi.subTotal Then
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.base), 2), 0, rows2.rfcEmisor & " Tasa 0%", "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    ElseIf rowsCfdi.tipoFactor = "Tasa" And CDec(rowsCfdi.mTras) > 0 Then

                                                        ''***** se quita comentario para dividir importe en cada concepto
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaEgreso_ScalarQuery(ddlConc.SelectedValue)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.importeCon), 2), 0, rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))

                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.mTras), 2), 0, rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    End If
                                                End If
                                            Else
                                                If rowsCfdi.tipoFactor = "No Objeto" Then
                                                    taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), Math.Round(CDec(rowsCfdi.importeCon), 2), 0, rows2.rfcEmisor & " No Obj", "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                End If
                                            End If
                                            If rowsCfdi.mRet <> "X" Then
                                                efecto = "RET"
                                                mPago = Math.Round(CDec(Val(rowsCfdi.mRet) * percentPago), 2)
                                                tipo = "Federal"
                                                'taPagoImpuestos.Insert(rowsCfdi.Impuesto, mPago, folSolPagoFinagil, CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), rowsCfdi.uuid)
                                                If existeUUIDReg = "NE" Then
                                                    taRegContable.Insert(CDec(taConceptos.ObtCtaImp_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo)), CDec(ddlProveedores.SelectedValue), 0, Math.Round(CDec(rowsCfdi.mRet), 2), taConceptos.ObtCtaImpDesc_ScalarQuery(ddlConc.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) & " " & rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                End If
                                            End If

                                            If contador = 0 Then
                                                If rowsCfdi.mLocTra <> "X" And rowsCfdi.mLocTra <> 0 Then
                                                    efecto = "LOC"
                                                    mPago = Math.Round(CDec(Val(rowsCfdi.mLocTra) * percentPago), 2)
                                                    tipo = "Local"
                                                    'taPagoImpuestos.Insert(rowsCfdi.Impuesto & "L", mPago, folSolPagoFinagil, CDec(taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "TRA")), rowsCfdi.uuid)
                                                    If existeUUIDReg = "NE" Then
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "TRA")), CDec(ddlProveedores.SelectedValue), 0, Math.Round(CDec(rowsCfdi.mLocTra), 2), taConceptos.ObtctaImpLocDesc_ScalarQuery(ddlConc.SelectedValue, tipo, "TRA") & " " & rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    End If
                                                End If

                                                If rowsCfdi.mLocRet <> "X" And rowsCfdi.mLocRet <> 0 Then
                                                    efecto = "LOC"
                                                    mPago = Math.Round(CDec(Val(rowsCfdi.mLocRet) * percentPago), 2)
                                                    tipo = "Local"
                                                    'taPagoImpuestos.Insert(rowsCfdi.Impuesto & "L", mPago, folSolPagoFinagil, CDec(taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "RET")), rowsCfdi.uuid)
                                                    If existeUUIDReg = "NE" Then
                                                        taRegContable.Insert(CDec(taConceptos.ObtCtaImpLoc_ScalarQuery(ddlConc.SelectedValue, tipo, "RET")), CDec(ddlProveedores.SelectedValue), 0, Math.Round(CDec(rowsCfdi.mLocRet), 2), taConceptos.ObtctaImpLocDesc_ScalarQuery(ddlConc.SelectedValue, tipo, "RET") & " " & rows2.rfcEmisor, "F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                                    End If
                                                End If
                                            End If
                                            contador += 1
                                        Next
                                        If existeUUIDReg = "NE" Then
                                            taRegContable.Insert(CDec(taConceptos.ObtCtaIngreso_ScalarQuery(ddlConc.SelectedValue)), CDec(ddlProveedores.SelectedValue), 0, rows2.total, rows2.rfcEmisor, ddlProveedores.SelectedItem.Text & " F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                        End If
                                    End If

                                    'Valida otros ingresos u otros gatos para pago de servicios con convenio CIE
                                    If banderaOtrosIngresos = "OI" Then
                                        If CInt(Session.Item("Empresa")) = 23 Then
                                            taRegContable.Insert(93340, CDec(ddlProveedores.SelectedValue), 0, importeOtrosGastosIngresos, rows2.rfcEmisor, ddlProveedores.SelectedItem.Text & " F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                        Else
                                            taRegContable.Insert(985, CDec(ddlProveedores.SelectedValue), 0, importeOtrosGastosIngresos, rows2.rfcEmisor, ddlProveedores.SelectedItem.Text & " F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                        End If
                                    End If
                                    If banderaOtrosIngresos = "OG" Then
                                        If CInt(Session.Item("Empresa")) = 23 Then
                                            taRegContable.Insert(93467, CDec(ddlProveedores.SelectedValue), importeOtrosGastosIngresos, 0, rows2.rfcEmisor, ddlProveedores.SelectedItem.Text & " F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                        Else
                                            taRegContable.Insert(1070, CDec(ddlProveedores.SelectedValue), importeOtrosGastosIngresos, 0, rows2.rfcEmisor, ddlProveedores.SelectedItem.Text & " F-" & row("folio") & " " & row("observaciones"), CInt(Session.Item("tipoPoliza")), folioPolizaDiario, CInt(Session.Item("Empresa")), rows2.uuid, folSolPagoFinagil, fechaRegistroCont, 29, ddlConc.SelectedItem.Value, CInt(Session.Item("idPeriodo")))
                                        End If
                                    End If
                                End If
#End Region

                            End If

                            dtDetalle.Rows.Add(row)
                        Next
                        'If taConceptos.GeneraEventoCont_ScalarQuery(ddlConc.SelectedValue) = False And chkContrato.Checked = False Then
                        '    banderaRegistroContable = "SI"
                        'End If
                        cont2 += 1
                    End If
                    cont += 1
                Next

                'inserta otros ingresos
                'importeOtrosGastosIngresos = rows2.total - CDec(txtTot.Text)
                'banderaOtrosIngresos = "OI"
                If banderaOtrosIngresos = "OI" Then
                    Dim guuidCN As String = Guid.NewGuid.ToString
                    taCFDI2.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text) * -1, 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now, "", "", guuidCN, 0, CDec(importeOtrosGastosIngresos) * -1, 0, 0, "CARTA NETEO ( " & txtDescCartaNeteo.Text & " )", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                    'taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, rows2.fechaEmision, rows2.serie, rows2.folio, rows2.uuid, CDec(row("subTotal")), CDec(row("total")), CDec(row("mImpuestoT")) + CDec(row("mImpLocalT")), CDec(row("mImpuestoR")) + CDec(row("mImpLocalR")), row("observaciones"), ddlConc.SelectedItem.Value, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, rows2.moneda, CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                End If

                'inserta carta neteo

                If CDec(txtImporteCartaNeteto.Text) > 0 And IsNumeric(txtImporteCartaNeteto.Text) Then
                    If Not IsNumeric(txtImporteCartaNeteto.Text) Then
                        'lblError.Visible = True
                        lblErrorGeneral.Text = "El importe de la Carta Neteo no es numérico"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    If fupCartaNeteo.HasFile = False Then
                        'lblError.Visible = True
                        lblErrorGeneral.Text = "No se ha seleccionado ninguna Carta Neteo como archivo adjunto"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    Dim guuidCN As String = Guid.NewGuid.ToString
                    Dim archivoPDF As HttpPostedFile = fupCartaNeteo.PostedFile
                    If Session.Item("Empresa") = "23" Then
                        archivoPDF.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))
                        taCFDI2.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text) * -1, 1, "", 0, 0, 0, 0)
                    Else
                        archivoPDF.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                        taCFDI2.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text) * -1, 1, "", 0, 0, 0, 0)
                    End If

                    taCXPPagos.Insert(ddlProveedores.SelectedItem.Value, 0, folSolPagoFinagil, Date.Now.ToLongDateString, Date.Now, "", "", guuidCN, 0, CDec(txtImporteCartaNeteto.Text) * -1, 0, 0, "CARTA NETEO ( " & txtDescCartaNeteo.Text & " )", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "No Pagada", "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("MCONTROL_CXP"), "#" & taGenCorresoFases.ObtieneCorreoXFase_ScalarQuery("OPERACIONES_CXP"), Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, nombreAutorizante2, taGenCorresoFases.ObtieneNombreXFase_ScalarQuery("MCONTROL_CXP"), cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                    conCT = +1

                End If
                Try
                    subirArchivosAdjuntos(folSolPagoFinagil, Session.Item("idCDeudor"), monedaPago)
                Catch ex As Exception
                    lblErrorGeneral.Text = "Erro: 1 " & ex.ToString.Substring(1, 100)
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try
                If cont2 > 0 Then
                    If banderaRegistroContable = "SI" Then
                        If fechaRegistroCont.Month >= Date.Now.Month Then
                            'folioPolizaDiario = CInt(taTipoDocumento.ConsultaFolio(CInt(Session.Item("tipoPoliza"))))
                            taTipoDocumento.ConsumeFolio(CInt(Session.Item("tipoPoliza")))
                        Else
                            'fechaRegistroCont = Date.Now.AddDays(-Now.Day + 1).AddDays(-1)
                            'folioPolizaDiario = taPeriodos.ConsultaFolio_ScalarQuery(fechaRegistroCont.Year, fechaRegistroCont.Month, CInt(Session.Item("Empresa")))
                            If rwPeriodos.estatus = 31 Then
                                taPeriodos.ConsumeFolio_UpdateQuery(fechaRegistroCont.Year, fechaRegistroCont.Month, CInt(Session.Item("Empresa")))
                            Else
                                taTipoDocumento.ConsumeFolio(CInt(Session.Item("tipoPoliza")))
                            End If

                        End If
                    End If
                    'Genera PDF
                    generaPDF(folSolPagoFinagil, idCuentas, cmbFormaPago.SelectedValue, CDate(txtFechaPago.Text))

                Else
                    lblErrorGeneral.Text = "No se ha seleccionado ningún comprobante"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                terminaProceso()
            Else
                lblErrorGeneral.Text = "No se ha seleccionado un autorizante"
                ModalPopupExtender1.Show()
                btnProcesar.Enabled = True
                Exit Sub
            End If
        Catch ex As Exception
            lblErrorGeneral.Text = "Err: 2 " & ex.ToString
            ModalPopupExtender1.Show()
            Exit Sub
        End Try

        GridView1.Dispose()

        divCtaBancaria.Visible = False
        divEfos.Visible = False
        divOtros.Visible = False
        divRevision.Visible = False
        divCtaBancaria.Visible = False
        Session.Item("monedaCtas") = Nothing
        Session("afuArchivoCtas") = Nothing
        Session("nameAfuArchivoCtas") = Nothing
        Session.Item("guuidArchivoCtas") = Nothing
        total2 = 0
    End Sub

    Public Sub terminaProceso()
        GridView1.Visible = False
        btnProcesar.Visible = False
        GridView1.Enabled = True
        btnCancelar.Visible = False
        btnVistaPrevia.Visible = False
        lblError.Text = ""
        lblError.Text = ""
        GridView2.Visible = False
        fupCartaNeteo.Visible = False
        txtDescCartaNeteo.Visible = False
        btnSeleccionar.Enabled = True
        ddlProveedores.Enabled = True
        txtBuscar.Enabled = True
        btnBuscar.Enabled = True
        cmbCentroDeCostos.SelectedValue = taSucursales.ObtSucursalXUsuario_ScalarQuery(Session.Item("Usuario"))
        cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("Empresa")))
    End Sub

    Public Sub generaPDF(ByVal folSol As Integer, ByVal idCtas As Integer, ByVal idFPago As Integer, ByVal fechaProgPago As Date)
        Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
        Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim taPagosTesoreria As New dsProduccionTableAdapters.CXP_PagosTesoreriaTableAdapter

        'Inserta datos del pago para tesorería
        taPagosTesoreria.Insert("CXP", folSol, Nothing, idCuentas, 33, CInt(Session.Item("Empresa")), taSolicitudPDF.TotalSolicitud_ScalarQuery(CInt(Session.Item("Empresa")), folSol), System.Data.SqlTypes.SqlDateTime.Null, idFPago, fechaProgPago)

        Dim dtSolPDF As DataTable
        dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesDataTable

        taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), folSol, "No Pagada")

        Dim dtObsSol As DataTable
        dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
        taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), folSol)

        Dim dtCtasBanco As DataTable = New dsProduccion.CXP_CuentasBancariasProvDataTable
        taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, idCuentas)
        'dtCtasBanco.WriteXml("C:\Files\dtCtasDetalle.xml", XmlWriteMode.WriteSchema)

        Dim var_observaciones As Integer = dtObsSol.Rows.Count
        Dim encripta As readXML_CFDI_class = New readXML_CFDI_class

        'dtDetalle.WriteXml("C:\Files\dtDetalle.xml", XmlWriteMode.WriteSchema)
        rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoCopia.rpt"))
        rptSolPago.SetDataSource(dtSolPDF)
        rptSolPago.Subreports("rptSubObservaciones").SetDataSource(dtObsSol)
        rptSolPago.Subreports("rptSubCuentas").SetDataSource(dtCtasBanco)
        rptSolPago.Refresh()


        rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folSol.ToString))
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
    End Sub

    Private Sub validaTamanoArchiAdjunto(ByVal foliosSolicitud As Decimal, ByVal deudor As Decimal)
        If fup1.HasFiles Then
            For Each files As HttpPostedFile In fup1.PostedFiles
                If files.ContentLength > 500000 Then
                    lblErrorGeneral.Text = "El tamaño del archivo no puede ser mayor a 5 MB"
                    ModalPopupExtender1.Show()
                    Exit Sub
                ElseIf Right(fup1.PostedFile.ContentType.Trim, 3).ToString <> "PDF" And Right(fup1.PostedFile.ContentType.Trim, 3).ToString <> "pdf" Then
                    lblErrorGeneral.Text = "El tipo de archivo no puede ser distinto a PDF"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub subirArchivosAdjuntos(ByVal foliosSolicitud As Decimal, ByVal deudor As Decimal, ByVal monedaPago As String)
        Dim taCFDI As New dsProduccionTableAdapters.CXP_XmlCfdi2TableAdapter
        Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        If fup1.HasFiles Then
            For Each files As HttpPostedFile In fup1.PostedFiles
                Dim guuidCN As String = Guid.NewGuid.ToString
                If Session.Item("Empresa") = "23" Then
                    files.SaveAs(Path.Combine(Server.MapPath("Finagil") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, Date.Now, Date.Now, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                Else
                    files.SaveAs(Path.Combine(Server.MapPath("Arfin") & "\Procesados\", guuidCN & ".pdf"))
                    taCFDI.Insert("", "", 0, "", 0, guuidCN, "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                    taCXPPagos.Insert(deudor, 0, foliosSolicitud, Date.Now, Date.Now, "AD", "ADJUNTO", guuidCN, 0, 0, 0, 0, "adjunto", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
                End If
            Next
        End If

        If IsNothing(Session("afuArchivoCtas")) = False Then
            If Session.Item("Empresa") = "23" Then
                taCFDI.Insert("", "", 0, "", 0, Session.Item("guuidArchivoCtas"), "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                taCXPPagos.Insert(deudor, 0, foliosSolicitud, Date.Now.ToShortDateString, Date.Now.ToShortDateString, "AD", "ADJUNTO", Session.Item("guuidArchivoCtas"), 0, 0, 0, 0, "Cta Pago", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
            Else
                taCFDI.Insert("", "", 0, "", 0, Session.Item("guuidArchivoCtas"), "", "", "", "", 0, "I", "", "", "", "", Date.Now, "PENDIENTE", CDec(txtImporteCartaNeteto.Text), 1, "", 0, 0, 0, 0)
                taCXPPagos.Insert(deudor, 0, foliosSolicitud, Date.Now.ToShortDateString, Date.Now.ToShortDateString, "AD", "ADJUNTO", Session.Item("guuidArchivoCtas"), 0, 0, 0, 0, "Cta Pago", 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "Reemb", "", "", Nothing, Nothing, "MXN", CDate(txtFechaPago.Text), False, Nothing, ddlAutorizo.SelectedValue, "", "", cmbCentroDeCostos.SelectedValue, cmbFormaPago.SelectedValue, idCuentas, CDec(txtTipoDeCambio.Text), monedaPago)
            End If
            Session("afuArchivoCtas") = Nothing
            'afuAdjuntoCta.SaveAs(MapPath("~/TmpFinagil/FilesProv/" & Convert.ToString(Session.Item("guuidArchivoCtas")) & ".pdf"))
        End If
    End Sub

    Private Sub Abrir_Click(sender As Object, e As EventArgs)
        Dim cadena As String = "window.open('verPdf.aspx','Dates','scrollbars=yes,resizable=yes','height=300', 'width=300')"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "popup", cadena, True)
    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        btnVistaPrevia.Visible = True
    End Sub

    Protected Sub btnVistaPrevia_Click(sender As Object, e As EventArgs) Handles btnVistaPrevia.Click
        If Session.Item("Usuario") = "" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        lblError.Visible = False
        lblError2.Visible = False
        Dim dtDetalleA As New DataTable("ComprobantesA")
        Dim rowA As DataRow
        dtDetalleA.Columns.Add("serie", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("folio", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("uuid", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("concepto", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("total", Type.GetType("System.Decimal"))
        Dim conta As Integer = 0


        Dim contaTrue As Integer = 0
        Dim fileCtaNeteo As HttpPostedFile = fupCartaNeteo.PostedFile

        For Each rows As GridViewRow In GridView1.Rows
            Dim chkg As CheckBox = rows.Cells(0).FindControl("chk")
            Dim txtTot As TextBox = rows.Cells(10).FindControl("txtMontoAPagar")
            Dim txtPorcentaje As TextBox = rows.Cells(11).FindControl("txtPorcentaje")
            Dim ddlCon As DropDownList = rows.Cells(13).FindControl("ddlConceptos")
            Dim txtObs As TextBox = rows.Cells(11).FindControl("txtObservaciones")

            If ddlCon.SelectedValue = 144 Then
                odsAutorizantes.FilterExpression = "Fase = 'TES_CXP'"
            End If

            If chkg.Checked = True Then
                rowA = dtDetalleA.NewRow

                rowA("serie") = GridView1.Rows(conta).Cells(6).Text.Replace("&nbsp;", "")
                rowA("folio") = GridView1.Rows(conta).Cells(5).Text.Replace("&nbsp;", "")
                rowA("uuid") = GridView1.Rows(conta).Cells(7).Text
                Dim contV As Integer = 0

                For Each rowsV As GridViewRow In GridView1.Rows

                    Dim chkgV As CheckBox = rowsV.Cells(0).FindControl("chk")
                    If chkgV.Checked = True Then
                        If GridView1.Rows(conta).Cells(4).Text <> GridView1.Rows(contV).Cells(4).Text Then
                            lblErrorGeneral.Text = "No es posible generar una solicitud con dos monedas distintas"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        Else
                            Session.Item("monedaCtas") = GridView1.Rows(conta).Cells(4).Text
                        End If

                        If CDate(GridView1.Rows(conta).Cells(1).Text).Month <> CDate(GridView1.Rows(contV).Cells(1).Text).Month Then
                            lblErrorGeneral.Text = "No es posible generar una solicitud con facturas de meses distintos"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    End If

                    contV += 1
                Next

                Try
                    rowA("concepto") = ddlCon.SelectedItem.Text
                Catch ex As Exception
                    lblErrorGeneral.Text = "No existe un concepto asignado a este usuario"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try

                If txtObs.Text.Length > 199 Then
                    lblErrorGeneral.Text = "La descripción del pago no puede ser mayor a 200 caracteres"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                If txtConvenio.Text.Trim.Length > 0 Then
                    Dim validaCaracteres As New utilerias

                    If IsNumeric(txtConvenio.Text) Then
                        If txtConvenio.Text.Trim.Length > 7 Then
                            lblErrorGeneral.Text = "El convenio CIE no puedes ser mayor a 7 dígitos"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Else
                        lblErrorGeneral.Text = "El convenio CIE debe ser numérico"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If

                    If txtConcepto.Text.Trim.Length > 30 Then
                        lblErrorGeneral.Text = "El concepto CIE no puedes ser mayor a 30 dígitos"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If

                    If txtReferencia.Text.Trim.Length > 20 Then
                        lblErrorGeneral.Text = "La referencia CIE no puedes ser mayor a 20 dígitos"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If

                    If validaCaracteres.validaCaracteresEspeciales(txtConvenio.Text.Trim) = False Then
                        lblErrorGeneral.Text = "El convenio CIE no puede contener los caracteres especiales"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    If validaCaracteres.validaCaracteresEspeciales(txtReferencia.Text.Trim) = False Then
                        lblErrorGeneral.Text = "La referencia CIE no puede contener los caracteres especiales"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    If validaCaracteres.validaCaracteresEspeciales(txtConcepto.Text.Trim) = False Then
                        lblErrorGeneral.Text = "El concepto CIE no puede contener los caracteres especiales"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtObs.Text.Trim.Length = 0 Then
                        lblErrorGeneral.Text = "Falta incluir una descripción de pago"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If

                    'Valida igualdad de monedas, cuenta VS factura
                    'lblMoneda = CType(FormView6.FindControl("MonedaTipoCambio"), Label)
                    If Session.Item("monedaCtas") <> taCuentasBancarias.ObtMoneda_ScalarQuery(cmbCuentasBancarias.SelectedValue) Then
                        txtTipoDeCambio.Enabled = True
                        txtTipoDeCambio.Text = taTipoDeCambio.ObtTipoCambio_ScalarQuery(taCuentasBancarias.ObtMoneda_ScalarQuery(cmbCuentasBancarias.SelectedValue), Date.Now.ToShortDateString)
                        If txtTipoDeCambio.Text = "1.0000" Then
                            lblErrorGeneral.Text = "No existe el tipo de cambio del día de hoy."
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Else
                        txtTipoDeCambio.Text = "1.0000"
                        txtTipoDeCambio.Enabled = False
                    End If

                    If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
                        If cmbCuentasBancarias.SelectedIndex = -1 Then
                            lblErrorGeneral.Text = "Cuando la forma de pago es por Tranferencia Electrónica se debe seleccionar una cuenta bancaria de pago."
                            ModalPopupExtender1.Show()
                            Exit Sub
                        Else
                            idCuentas = cmbCuentasBancarias.SelectedValue
                        End If
                    Else
                        If cmbFormaPago.SelectedIndex = 2 Then
                            'datosBancarios = "Banco: " & ddlBancos.SelectedItem.Text & " Cuenta: " & txtCuenta.Text.Trim & " CLABE: " & txtClabe.Text.Trim & " Referencia: " & txtReferencia.Text.Trim
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
                            cmbCuentasBancarias.Enabled = False
                            idCuentas = 0
                        End If
                    End If

                    If IsNumeric(txtPorcentaje.Text) Then
                        If CDec(txtPorcentaje.Text) > 100 Then
                            lblErrorGeneral.Text = "El % de pago solicitado no debe de exceder el 100 % del importe restante"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf CDec(txtPorcentaje.Text) <= 0 Then
                            lblErrorGeneral.Text = "El % de pago solicitado no puede ser 0 o menor a este"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        rowA("total") = ((CDec(txtTot.Text) * CDec(txtPorcentaje.Text)) / 100)
                    ElseIf IsNumeric(txtTot.Text) Then
                    If CDec(txtTot.Text) > CDec(GridView1.Rows(conta).Cells(11).Text) Then
                        If (Session.Item("rfcEmisor") <> "CSS160330CP7" And Session.Item("rfcEmisor") <> "TNO8105076Q8" And Session("rfcEmisor") <> "CES671208JE5") Then
                            lblErrorGeneral.Text = "El importe solicitado para pago no debe ser mayor al importe de la factura"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    ElseIf CDec(txtTot.Text) <= 0 Then
                            lblErrorGeneral.Text = "El importe solicitado no puede ser 0 o menor a este"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        rowA("total") = CDec(txtTot.Text)
                    Else
                        lblErrorGeneral.Text = "Los datos ingresados solo deben de ser númericos"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If

                    dtDetalleA.Rows.Add(rowA)
                    contaTrue += 1

                    '**************************

                    Dim taConceptos As New dsProduccionTableAdapters.CXP_ConceptosTableAdapter

                    If chkContrato.Checked = False Then
                        taCFDI2.SumaImp_FillBy(tableCFDI2, GridView1.Rows(conta).Cells(7).Text)
                        For Each rows2 As dsProduccion.CXP_XmlCfdi2Row In tableCFDI2
                            taCFDIImpuestos.CFDIImpuestos_Fill(dtCFDIImpuestos, rows2.uuid.ToString)

                            Dim taPagoImpuestos As New dsProduccionTableAdapters.CXP_PagosImpuestosTableAdapter
                            Dim errores As String = ""

                            For Each rowsCfdi As dsProduccion.Vw_CXP_ImpuestosCFDIRow In dtCFDIImpuestos
                                Dim efecto As String = ""
                                Dim tipo As String = ""

                                Dim mPago As Decimal = 0

                                'Prueba de omisión
                                If rowsCfdi.mTras <> "X" Then
                                    efecto = "TRA"
                                    tipo = "Federal"
                                    If taConceptos.ObtCtaImp_ScalarQuery(ddlCon.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) = "0" Then
                                        lblError2.Text = "Concepto: " & ddlCon.SelectedItem.Text & " Impuesto: " & rowsCfdi.Impuesto & " Efecto: " & efecto & " Factor: " & rowsCfdi.tipoFactor & " Tipo: " & tipo & " Sin impuesto configurado  " & vbNewLine
                                        errores = "OK"
                                    End If
                                End If

                                'Prueba de omisión
                                If rowsCfdi.mRet <> "X" Then
                                    efecto = "RET"
                                    tipo = "Federal"
                                    If taConceptos.ObtCtaImp_ScalarQuery(ddlCon.SelectedValue, rowsCfdi.Impuesto, efecto, rowsCfdi.tipoFactor, tipo) = "0" Then
                                        lblError2.Text = "Concepto: " & ddlCon.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                        errores = "OK"
                                    End If
                                End If

                                If rowsCfdi.mLocTra <> "X" And rowsCfdi.mLocTra <> 0 Then
                                    efecto = "LOC"
                                    ' mPago = Math.Round(CDec(Val(rowsCfdi.mLocTra) * percentPago), 2)
                                    tipo = "Local"
                                    If taConceptos.ObtCtaImpLoc_ScalarQuery(ddlCon.SelectedValue, tipo, "TRA") = "0" Then
                                        lblError2.Text = "Concepto: " & ddlCon.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                        errores = "OK"
                                    End If
                                End If

                                If rowsCfdi.mLocRet <> "X" And rowsCfdi.mLocRet <> 0 Then
                                    efecto = "LOC"
                                    'mPago = Math.Round(CDec(Val(rowsCfdi.mLocRet) * percentPago), 2)
                                    tipo = "Local"
                                    If taConceptos.ObtCtaImpLoc_ScalarQuery(ddlCon.SelectedValue, tipo, "RET") = "0" Then
                                        lblError2.Text = "Concepto: " & ddlCon.SelectedItem.Text & vbNewLine & "Impuesto: " & rowsCfdi.Impuesto & vbNewLine & "Efecto: " & efecto & vbNewLine & "Factor: " & rowsCfdi.tipoFactor & vbNewLine & "Tipo: " & tipo & " Sin impuesto configurado  "
                                        errores = "OK"
                                    End If
                                End If


                            Next
                            If errores = "OK" Then
                                lblError2.Visible = True
                                Exit Sub
                            End If
                        Next
                    End If
                End If
                '**************************
                conta += 1
        Next


        If IsNumeric(txtImporteCartaNeteto.Text) And CDec(txtImporteCartaNeteto.Text) > 0 And conta > 0 Then
            rowA = dtDetalleA.NewRow
            Dim rows0 As DataRow = dtDetalleA.Rows(0)
            rowA("serie") = ""
            rowA("folio") = ""
            rowA("uuid") = rows0.Item("uuid") 'GridView2.Rows(0).Cells(2).Text
            rowA("concepto") = "CARTA NETEO ( " & txtDescCartaNeteo.Text & " )"
            If IsNumeric(txtImporteCartaNeteto.Text) Then
                rowA("total") = CDec(txtImporteCartaNeteto.Text) * -1
                fupCartaNeteo.Visible = True
                lblCarteNeteo.Visible = True
                txtDescCartaNeteo.Visible = True
                lblDescrCartaNeteo.Visible = True
            Else
                'lblError.Visible = True
                lblErrorGeneral.Text = "Los valores son incorrectos"
                ModalPopupExtender1.Show()
            End If
            dtDetalleA.Rows.Add(rowA)
        End If

        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()
        If conta > 0 And contaTrue > 0 Then
            GridView2.Visible = True
            btnProcesar.Visible = True
            btnCancelar.Visible = True
            btnVistaPrevia.Visible = False
            GridView1.Enabled = False
            btnCancelar.Text = "Cancelar"
            btnCancelarBusqueda.Visible = False
            divRevision.Visible = True
            divCtaBancaria.Visible = True
            '----------------------------
            txtImporteCartaNeteto.Visible = False
            'lblCarteNeteo.Visible = False
            lblImporte.Visible = False
            chkContrato.Enabled = False
            ddlContratos.Enabled = False
            ddlClientes.Enabled = False
            'cmbCentroDeCostos.Enabled = False
            'cmbFormaPago.Enabled = False
            'fupCartaNeteo.Visible = False
            lblAdjuntos.Visible = True
            fup1.Visible = True
            ddlAutorizo.Enabled = False
            cmbCentroDeCostos.Enabled = False
            cmbFormaPago.Enabled = False
            txtFechaPago.Enabled = False
            cmbCuentasBancarias.Enabled = False
            'divCtaBancaria.Visible = False
        Else
            'lblError.Visible = True
            lblErrorGeneral.Text = "No se ha seleccionado ningún registro"
            ModalPopupExtender1.Show()
            divCtaBancaria.Visible = True
            btnCancelarBusqueda.Visible = True
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        GridView2.Visible = False

        btnProcesar.Visible = False
        btnCancelar.Visible = False
        btnVistaPrevia.Visible = True
        GridView1.Enabled = True
        lblError.Visible = False
        lblError.Text = ""
        lblError2.Visible = False
        lblError2.Text = ""
        btnCancelarBusqueda.Visible = True
        fupCartaNeteo.Visible = False
        lblCarteNeteo.Visible = False
        txtDescCartaNeteo.Visible = False
        lblDescrCartaNeteo.Visible = False
        txtImporteCartaNeteto.Visible = False


        If chkContrato.Checked = True Then
            'ddlContratos.Enabled = True
            fupCartaNeteo.Visible = True
            txtDescCartaNeteo.Visible = True
            lblDescrCartaNeteo.Visible = True
            lblCarteNeteo.Visible = True
            lblImporte.Visible = True
            txtImporteCartaNeteto.Visible = True
            txtImporteCartaNeteto.Text = "0"
            fupCartaNeteo.Visible = False
            lblCarteNeteo.Visible = False
        Else
            ddlContratos.Enabled = False
            fupCartaNeteo.Visible = False
            txtDescCartaNeteo.Visible = False
            lblDescrCartaNeteo.Visible = False
            lblImporte.Visible = False
            txtImporteCartaNeteto.Visible = False
            chkContrato.Enabled = True
            ddlClientes.Enabled = False
            fupCartaNeteo.Visible = False
            'lblCarteNeteo.Visible = False
            'cmbCentroDeCostos.Enabled = True
            'cmbFormaPago.Enabled = True
        End If
        lblAdjuntos.Visible = False
        fup1.Visible = False
        ddlAutorizo.Enabled = True
        cmbCentroDeCostos.Enabled = True
        cmbFormaPago.Enabled = True
        txtFechaPago.Enabled = True
        cmbCuentasBancarias.Enabled = True
        divRevision.Visible = False
        divCtaBancaria.Visible = False
        'divFacturas.Visible = False

        divRevision.Visible = False
        divCtaBancaria.Visible = True
    End Sub



    Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then

            If IsNumeric(DataBinder.Eval(e.Row.DataItem, "total")) Then
                total2 += CDec(DataBinder.Eval(e.Row.DataItem, "total"))
            Else
                total2 += 0
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "MONTO TOTAL A PAGAR A SOLICITUD: "
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Text = total2.ToString("C")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If
    End Sub

    Protected Sub btnCancelarBusqueda_Click(sender As Object, e As EventArgs) Handles btnCancelarBusqueda.Click
        btnSeleccionar.Enabled = True
        ddlProveedores.Enabled = True
        txtBuscar.Enabled = True
        btnBuscar.Enabled = True
        GridView1.Visible = False
        btnVistaPrevia.Visible = False
        btnCancelarBusqueda.Visible = False
        lblError.Visible = False
        lblError2.Visible = False
        fupCartaNeteo.Visible = False
        lblCarteNeteo.Visible = False
        lblImporte.Visible = False
        txtImporteCartaNeteto.Visible = False
        chkContrato.Checked = False
        chkContrato.Enabled = True
        ddlClientes.Enabled = False
        ddlContratos.Enabled = False
        fupCartaNeteo.Visible = False
        txtDescCartaNeteo.Visible = False
        lblDescrCartaNeteo.Visible = False
        'cmbCentroDeCostos.Enabled = True
        'cmbFormaPago.Enabled = True
        lblAdjuntos.Visible = False
        fup1.Visible = False

        lbl69.Visible = False
        lbl69B.Visible = False
        ddlClientes.Visible = False
        ddlContratos.Visible = False
        ddlAutorizo.Visible = False
        cmbCentroDeCostos.Visible = False
        cmbFormaPago.Visible = False
        txtFechaPago.Visible = False
        cmbCuentasBancarias.Visible = False
        chkContrato.Visible = False

        divRevision.Visible = False
        divOtros.Visible = False
        divEfos.Visible = False
        divCtaBancaria.Visible = False
        'divFacturas.Visible = False
    End Sub



    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If Session.Item("Empresa") = "23" Then
                e.Row.Attributes("onmouseover") = "this.style.backgroundColor='DarkOrange';"
                e.Row.Attributes("onmouseout") = "this.style.backgroundColor='white';"
                'e.Row.Cells(0).Attributes.Add("onclick", ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString()))
                e.Row.Cells(0).Attributes.Add("onclick", "this.style.backgroundColor='DarkOrange';")
                e.Row.Cells(0).Style.Add("cursor", "pointer")
            Else
                e.Row.Attributes("onmouseover") = "this.style.backgroundColor='#4BA5FF';"
                e.Row.Attributes("onmouseout") = "this.style.backgroundColor='white';"
                e.Row.Cells(0).Attributes.Add("onclick", "this.style.backgroundColor='#4BA5FF';")
                e.Row.Cells(0).Style.Add("cursor", "pointer")
            End If
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            If GridView1.Rows.Count > 0 Then
                btnSeleccionar.Enabled = False
                ddlProveedores.Enabled = False
                txtBuscar.Enabled = False
                btnBuscar.Enabled = False
                'btnCancelarBusqueda.Visible = False
            Else
                btnSeleccionar.Enabled = True
                ddlProveedores.Enabled = True
                txtBuscar.Enabled = True
                btnBuscar.Enabled = True
                btnCancelarBusqueda.Visible = True
            End If
        End If
    End Sub

    Protected Sub chkContrato_CheckedChanged(sender As Object, e As EventArgs) Handles chkContrato.CheckedChanged
        Dim dtDatosEmpresa As New dsProduccion.CXP_EmpresasDataTable
        Dim drDatosEmpresa As dsProduccion.CXP_EmpresasRow
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter

        taTipoDocumento.DatosEmpresa_FillBy(dtDatosEmpresa, Session.Item("Empresa"))

        If dtDatosEmpresa.Rows.Count > 0 Then
            drDatosEmpresa = dtDatosEmpresa.Rows(0)
        End If
        If chkContrato.Checked = True Then
            ddlContratos.Enabled = True
            'fupCartaNeteo.Visible = True
            'lblCarteNeteo.Visible = True
            lblImporte.Visible = True
            txtImporteCartaNeteto.Visible = True
            ddlClientes.Enabled = True
            odsAnexosActivos.FilterExpression = "cliente ='" & ddlClientes.SelectedValue & "'"
            odsAutorizantes.FilterExpression = "Fase = 'MCONTROL_CXP'"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND idConcepto = '" & drDatosEmpresa.idConceptoPagoCtos & "'"
        Else
            ddlContratos.Enabled = False
            'fupCartaNeteo.Visible = False
            'lblCarteNeteo.Visible = False
            lblImporte.Visible = False
            txtImporteCartaNeteto.Visible = False
            ddlClientes.Enabled = False
            odsAutorizantes.FilterExpression = "Descripcion = 'CXP_AUTORIZACIONES' AND (Fase <> 'MCONTROL_CXP' AND Fase <> 'MCONTROL_AV' AND Fase <> 'TES_CXP')"
            odsConceptos.FilterExpression = "idConcepto IN (" & Session.Item("Conceptos") & ") AND (" & "idConcepto <>'" & taTipoDocumento.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa")) & "' AND idConcepto <>'" & taTipoDocumento.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa")) & "')"
        End If
    End Sub

    Protected Sub ddlClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClientes.SelectedIndexChanged
        odsAnexosActivos.FilterExpression = "Cliente = '" & ddlClientes.SelectedValue & "'"
    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles Button1.Click


        MsgBox(cmbCuentasBancarias.SelectedValue.ToString)
        Dim taSolicitudPDF As New dsProduccionTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim taObsSolic As New dsProduccionTableAdapters.CXP_ObservacionesSolicitudTableAdapter
        Dim taCtasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim folio As String = "4260"
        Dim idPago As String = "152"
        Dim estatus As String = "No Pagada"
        Dim dtSolPDF As DataTable
        dtSolPDF = New dsProduccion.Vw_CXP_AutorizacionesDataTable

        taSolicitudPDF.Fill(dtSolPDF, Session.Item("Empresa"), folio, estatus)

        Dim dtObsSol As DataTable
        dtObsSol = New dsProduccion.CXP_ObservacionesSolicitudDataTable
        taObsSolic.Fill(dtObsSol, CDec(Session.Item("Empresa")), folio)

        Dim dtCtasBanco As DataTable
        dtCtasBanco = New dsProduccion.CXP_CuentasBancariasProvDataTable
        taCtasBancarias.ObtCtaPago_FillBy(dtCtasBanco, idPago)
        dtCtasBanco.WriteXml("C:\Files\dtCtasDetalle.xml", XmlWriteMode.WriteSchema)

        Dim var_observaciones As Integer = dtObsSol.Rows.Count
        Dim encripta As readXML_CFDI_class = New readXML_CFDI_class

        'dtDetalle.WriteXml("C:\Files\dtDetalle.xml", XmlWriteMode.WriteSchema)
        rptSolPago.Load(Server.MapPath("~/rptSolicitudDePagoCopia.rpt"))
        rptSolPago.SetDataSource(dtSolPDF)
        rptSolPago.Subreports("rptSubObservaciones").SetDataSource(dtObsSol)
        rptSolPago.Subreports("rptSubCuentas").SetDataSource(dtCtasBanco)
        rptSolPago.Refresh()


        rptSolPago.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folio))
        rptSolPago.SetParameterValue("var_observaciones", var_observaciones.ToString)
        rptSolPago.SetParameterValue("var_contrato", chkContrato.Checked)
        rptSolPago.SetParameterValue("var_idCuentas", idPago)


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


    Protected Sub hypFacturas_Click(sender As Object, e As EventArgs)
        MsgBox("GHola")
    End Sub

    Protected Sub cmbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If cmbFormaPago.SelectedValue = taFormaPago.ObtFormaPago_ScalarQuery(CDec(Session.Item("empresa"))) Then
            cmbCuentasBancarias.Enabled = True
            tablaReferenciaBancaria.Visible = False
        ElseIf cmbFormaPago.SelectedIndex = 0 Then
            cmbCuentasBancarias.Enabled = False
            tablaReferenciaBancaria.Visible = False
        ElseIf cmbFormaPago.SelectedIndex = 2 Then
            tablaReferenciaBancaria.Visible = True
            cmbCuentasBancarias.Enabled = False
        End If
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

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class