Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmComprobarGastos
    Inherits System.Web.UI.Page

    Dim totalfacturas As Decimal = 0
    Dim contadorNoDeduc As Integer = 1
    Dim totalGastoas As Decimal = 0
    Dim importeOtrosIngresos As Decimal


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = 24 Then
            id1.Attributes.Add("style", "background-color: #4BA5FF;")
            id1.Attributes.Add("class", "labelsA")
            id2.Attributes.Add("style", "background-color: #4BA5FF;")
            id2.Attributes.Add("class", "labelsA")
            id3.Attributes.Add("style", "background-color: #4BA5FF;")
            id3.Attributes.Add("class", "labelsA")
            id4.Attributes.Add("style", "background-color: #4BA5FF;")
            id4.Attributes.Add("class", "labelsA")
            id5.Attributes.Add("style", "background-color: #4BA5FF;")
            id5.Attributes.Add("class", "labelsA")
            id6.Attributes.Add("style", "background-color: #4BA5FF;")
            id6.Attributes.Add("class", "labelsA")
            id7.Attributes.Add("style", "background-color: #4BA5FF;")
            id7.Attributes.Add("class", "labelsA")
            id8.Attributes.Add("style", "background-color: #4BA5FF;")
            id8.Attributes.Add("class", "labelsA")
            id9.Attributes.Add("style", "background-color: #4BA5FF;")
            id9.Attributes.Add("class", "labelsA")
            btnAceptar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnAgregar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnAsignar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btncancelarComprobacion.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnComprobar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnNoDeducible.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView2.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView3.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView1.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView2.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView3.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView2.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
            GridView3.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        End If
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Session.Item("Leyenda") = "Comprobación de gastos"
        If Session.Item("rfcEmpresa") = "SAR951230N5A" Then
            Session.Item("rutaCFDI") = "ARFIN/Todos/Procesados"
        Else
            Session.Item("rutaCFDI") = "FINAGIL/Todos/Procesados"
        End If
        If Not IsPostBack Then
            totala = 0
            totalb = 0
            total = 0
            txtFechaLlegada.Text = Date.Now.ToShortDateString
            txtFechaSalida.Text = Date.Now.ToShortDateString
            odbSolicitudes2.FilterExpression = "idConcepto = '" & taTipoDocumento.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' AND estatus like '%Autoriza 2%'"
        Else
        End If
    End Sub

    Protected Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click

        'DataTable para comprobantes no deducibles
        dtDetalleB = New DataTable("ComprobantesComprobacion")
        dtDetalleB.Columns.Add("descripcion", Type.GetType("System.String"))
        dtDetalleB.Columns.Add("importe", Type.GetType("System.Decimal"))

        'DataTable para comprobantes deducibles
        If ddlFolioSolicitud.Items.Count > 0 Then
            Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
            Dim dtPagos As New dsProduccion.CXP_PagosDataTable
            Dim taSolicitudesSC As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter
            Dim taSaldoSolicitud As New dsProduccionTableAdapters.Vw_CXP_SaldoSolicitudTableAdapter
            Dim rows As dsProduccion.CXP_PagosRow

            dtDetalleA = New DataTable("ComprobantesA")

            dtDetalleA.Columns.Add("serie", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("folio", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("uuid", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("concepto", Type.GetType("System.String"))
            dtDetalleA.Columns.Add("total", Type.GetType("System.Decimal"))
            dtDetalleA.Columns.Add("total1", Type.GetType("System.Decimal"))


            hlkPdf.NavigateUrl = "~/TmpFinagil/" & Session.Item("Empresa") & "-" & ddlFolioSolicitud.SelectedItem.Text & ".pdf"

            taPagos.ObtieneDatosPago_FillBy(dtPagos, ddlFolioSolicitud.SelectedValue)
            If dtPagos.Rows.Count >= 1 Then
                rows = dtPagos.Rows(0)
                txtDescripcion.Text = rows.decripcion
                lblFechaSolicitud.Text = rows.fechaSolicitud
                lblImporteSolicitado.Text = rows.totalPagado
                lblSaldo.Text = taSaldoSolicitud.SaldoSolicitud_ScalarQuery(CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa"))) 'taSolicitudesSC.SaldoSolic_ScalarQuery(ddlFolioSolicitud.SelectedValue)
            End If
            dtDetalleA.Dispose()
            GridView2.DataSource = dtDetalleA
            GridView2.DataBind()
            'txtDestinoExtranjero.Enabled = True
            txtDestinoNacional.Enabled = True
            'txtMotivoViaje.Enabled = True
            txtBuscar.Enabled = True
            btnBuscar.Enabled = True
            ddlProveedor.Visible = True
            ddlProveedor.Enabled = True
            btnAsignar.Enabled = True
            ddlAutorizo.Enabled = True
            TabContainer1.Visible = True
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        odsProveedores.FilterExpression = "razonSocial LIKE '%" & txtBuscar.Text.Trim & "%'"
    End Sub

    Protected Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click
        Dim taProveedor As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Session.Item("rfcEmisor") = taProveedor.ObtRfc_ScalarQuery(ddlProveedor.SelectedValue)
        odsCFDI.FilterExpression = "rfcEmisor ='" & taProveedor.ObtRfc_ScalarQuery(ddlProveedor.SelectedValue) & "' AND rfcReceptor ='" & Session.Item("rfcEmpresa") & "'"
        GridView1.Visible = True
        If GridView1.Rows.Count > 0 Then
            btnAgregar.Visible = True
            btnCancelar.Visible = True
            TabContainer1.Visible = True
        End If
        'TabContainer1.Visible = True
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        Dim conta As Integer
        Dim rowA As DataRow
        Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        importeOtrosIngresos = taEmpresas.ObtMontoOGastos_ScalarQuery(Session.Item("Empresa"))


        For Each rows As GridViewRow In GridView1.Rows
            Dim chkg As CheckBox = rows.Cells(0).FindControl("chk")
            Dim txtTot As TextBox = rows.Cells(10).FindControl("txtMontoAPagar")
            Dim txtCon As TextBox = rows.Cells(11).FindControl("txtConceptoFactura")

            If chkg.Checked = True Then

                'If CDec(txtTot.Text) > CDec(lblSaldo.Text) And CDec(txtTot.Text) < (CDec(lblSaldo.Text) + importeOtrosIngresos) Then
                '    'Inserta ND
                '    'MsgBox("H")
                'ElseIf CDec(txtTot.Text) > CDec(lblSaldo.Text) Then
                '    ModalPopupExtender1.Show()
                '    Exit Sub
                'End If

                rowA = dtDetalleA.NewRow
                rowA("serie") = GridView1.Rows(conta).Cells(6).Text.Replace("&nbsp;", "")
                rowA("folio") = GridView1.Rows(conta).Cells(5).Text.Replace("&nbsp;", "")
                rowA("uuid") = GridView1.Rows(conta).Cells(7).Text
                rowA("concepto") = txtCon.Text
                rowA("total1") = GridView1.Rows(conta).Cells(11).Text.Replace("$", "")
                rowA("total") = CDec(txtTot.Text)

                If (totala + CDec(txtTot.Text)) > CDec(lblSaldo.Text) And (totala + CDec(txtTot.Text)) < CDec(lblSaldo.Text) + importeOtrosIngresos Then
                    'Inserta ND
                    'rowA = dtDetalleA.NewRow
                    'rowA("serie") = GridView1.Rows(conta).Cells(6).Text.Replace("&nbsp;", "")
                    'rowA("folio") = GridView1.Rows(conta).Cells(5).Text.Replace("&nbsp;", "")
                    'rowA("uuid") = GridView1.Rows(conta).Cells(7).Text
                    'rowA("concepto") = txtCon.Text
                    'rowA("total1") = GridView1.Rows(conta).Cells(11).Text.Replace("$", "")
                    'rowA("total") = CDec(lblSaldo.Text)

                    dtDetalleA.Rows.Add(rowA)
                    totala += CDec(txtTot.Text)

                    Dim rowComp As DataRow

                    rowComp = dtDetalleB.NewRow
                    rowComp("descripcion") = "Otros Ingresos"
                    rowComp("importe") = (CDec(txtTot.Text) - CDec(lblSaldo.Text)) * -1
                    totalb = totalb + rowComp("importe")

                    dtDetalleB.Rows.Add(rowComp)

                    contadorNoDeduc += 1

                    GridView3.DataSource = dtDetalleB
                    GridView3.DataBind()

                ElseIf (totala + CDec(txtTot.Text)) <= CDec(lblSaldo.Text) Then
                    dtDetalleA.Rows.Add(rowA)
                    totala += CDec(txtTot.Text)
                Else
                    ModalPopupExtender1.Show()
                End If

            End If
            conta += 1
        Next

        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()

        If conta > 0 Then
            GridView2.Visible = True
            btnNoDeducible.Visible = True
            btnComprobar.Visible = True
        End If

        If Not TabContainer1.Visible Then
            TabContainer1.Visible = True
        End If
        lblTotalGastos.Visible = True
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
        lblTotalGastos.Text = "Importe total a comprobar: " + FormatCurrency((totala + totalb).ToString)
    End Sub

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Eliminar" Then
            dtDetalleA.Rows.RemoveAt(e.CommandArgument)
            totala -= CDec(GridView2.Rows(e.CommandArgument).Cells(4).Text)
        End If
        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()

        lblTotalGastos.Text = "Importe total a comprobar: " + FormatCurrency((totala + totalb).ToString)
    End Sub


    Protected Sub btnNoDeducible_Click(sender As Object, e As EventArgs) Handles btnNoDeducible.Click

        totalb = totalb + CDec(txtImporteND.Text)

        If IsNumeric(txtImporteND.Text) Then
            If (totala + totalb) > CDec(lblSaldo.Text) Then
                ModalPopupExtender1.Show()
                Exit Sub
            End If
        Else
            ModalPopupExtender2.Show()
        End If

        Dim rowComp As DataRow

        rowComp = dtDetalleB.NewRow
        rowComp("descripcion") = txtConceptoND.Text
        If IsNumeric(txtImporteND.Text) Then
            rowComp("importe") = CDec(txtImporteND.Text)
        Else
            ModalPopupExtender2.Show()
        End If

        dtDetalleB.Rows.Add(rowComp)

        contadorNoDeduc += 1

        GridView3.DataSource = dtDetalleB
        GridView3.DataBind()
        btnComprobar.Visible = True
    End Sub

    Protected Sub btnComprobar_Click(sender As Object, e As EventArgs) Handles btnComprobar.Click
        Dim taUsuarios As New dsProduccionTableAdapters.UsuariosTableAdapter
        Dim taTipoDocumento As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter

        If Session.Item("Usuario") = "" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Usuario") = "" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If ddlAutorizo.SelectedItem.Text <> "" Then
            Dim taCXPPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
            Dim taUUIDPagos As New dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter
            Dim dtDatosFactura As New dsProduccion.vw_CXP_XmlCfdi2_grpUuidDataTable
            Dim taComprobacionGtos As New dsProduccionTableAdapters.CXP_ComprobGtosTableAdapter
            Dim taGenCorresoFases As New dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter
            Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter


            Dim cont As Integer = 0
            Dim folComprobacionCom As Integer = taEmpresas.ConsultaFolioCom(Session.Item("Empresa"))
            Session.Item("namePDFg") = Session.Item("Empresa") & "-" & folComprobacionCom

            'If taUsuarios.ExisteUsuario_ScalarQuery(taCXPPagos.rfcProveedorSolicitud_ScalarQuery(CDec(Session.Item("Empresa")), CDec(ddlFolioSolicitud.SelectedItem.Text))) = "NE" Then
            '    ModalPopupExtender4.Show()
            '    Exit Sub
            'End If
            Dim mail As String = "#" & taGenCorresoFases.ObtieneCorreo_ScalarQuery(ddlAutorizo.SelectedValue)

                Dim contador As Integer = 0
                Dim totalFacturas As Decimal = 0
                For Each rows As GridViewRow In GridView2.Rows
                    taUUIDPagos.ObtDatosFactura_FillBy(dtDatosFactura, GridView2.Rows(contador).Cells(2).Text)
                    For Each rowsa As dsProduccion.vw_CXP_XmlCfdi2_grpUuidRow In dtDatosFactura.Rows
                        Dim percentPago As Decimal = CDec(GridView2.Rows(contador).Cells(5).Text) / CDec(GridView2.Rows(contador).Cells(4).Text)
                    taCXPPagos.Insert(ddlProveedor.SelectedItem.Value, 0, ddlFolioSolicitud.SelectedItem.Text, Date.Now.ToLongDateString, rowsa.fechaEmision, rowsa.serie, rowsa.folio, rowsa.uuid, Math.Round(rowsa.subTotal * percentPago, 2), CDec(GridView2.Rows(contador).Cells(5).Text), 0, 0, GridView2.Rows(contador).Cells(3).Text, 0, 1, Session.Item("Usuario"), CInt(Session.Item("Empresa")), "CompGtos", "#" & Session.Item("mailJefe"), mail, Nothing, Nothing, rowsa.moneda, Date.Now, False, Nothing, ddlAutorizo.SelectedValue, ddlAutorizo.SelectedItem.Text, Session.Item("Jefe"), Nothing, Nothing)

                    taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), rowsa.uuid, CDec(GridView2.Rows(contador).Cells(5).Text), 0, GridView2.Rows(contador).Cells(3).Text.Replace("&nbsp;", ""), txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, rowsa.folio, rowsa.serie)

                    totalFacturas += CDec(GridView2.Rows(contador).Cells(5).Text)
                    Next
                    contador += 1
                Next

                Dim contadorND As Integer = 0
                For Each rowsND As GridViewRow In GridView3.Rows
                taComprobacionGtos.Insert(CDec(Session.Item("idUsuario")), CDec(ddlFolioSolicitud.SelectedItem.Text), CDec(Session.Item("Empresa")), "ND", CDec(GridView3.Rows(contadorND).Cells(1).Text), 0, GridView3.Rows(contadorND).Cells(0).Text, txtDestinoNacional.Text, "", "", CDate(txtFechaLlegada.Text), CDate(txtFechaSalida.Text), folComprobacionCom, "", "", Session.Item("Jefe"), ddlAutorizo.SelectedItem.Text, "#" & Session.Item("mailJefe"), mail, Date.Now.ToLongDateString, "", "")
                contadorND += 1
                Next

                taEmpresas.ConsumeFolioCom(Session.Item("Empresa"))

                Dim taComprobacion As New dsProduccionTableAdapters.Vw_CXP_ComprobacionGastosTableAdapter

                Dim rptComprobacion As New ReportDocument
                Dim dtSol1 As DataTable
                dtSol1 = New dsProduccion.Vw_CXP_ComprobacionGastosDataTable
                Dim dtSol2 As DataTable
                dtSol2 = New dsProduccion.Vw_CXP_ComprobacionGastosDataTable
                Dim dtSol3 As DataTable
                dtSol3 = New dsProduccion.Vw_CXP_ComprobacionGastosDataTable


                Dim taSol1 As New dsProduccionTableAdapters.Vw_CXP_ComprobacionGastosTableAdapter
            Dim encripta As readXML_CFDI_class = New readXML_CFDI_class
            taSol1.Obt1_FillBy(dtSol1, CDec(Session.Item("Empresa")), folComprobacionCom)
                taSol1.ObtND_FillBy(dtSol2, CDec(Session.Item("Empresa")), folComprobacionCom)
                taSol1.ObtDND_FillBy(dtSol3, CDec(Session.Item("Empresa")), folComprobacionCom)

                rptComprobacion.Load(Server.MapPath("~/rptComprobacionGts.rpt"))
                rptComprobacion.SetDataSource(dtSol1)
                rptComprobacion.Subreports(0).SetDataSource(dtSol2)
                rptComprobacion.Subreports(1).SetDataSource(dtSol3)
                rptComprobacion.Refresh()

                If Session.Item("rfcEmpresa") = "FIN940905AX7" Then
                    rptComprobacion.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/LOGO FINAGIL.JPG"))
                Else
                    rptComprobacion.SetParameterValue("var_pathImagen", Server.MapPath("~/imagenes/logoArfin.JPG"))
                End If
            rptComprobacion.SetParameterValue("var_genero", encripta.Encriptar(Date.Now.ToString("yyyyMMddhhmm") & Session.Item("Empresa") & folComprobacionCom))
            rptComprobacion.SetParameterValue("var_totalConFactura", FormatCurrency(taComprobacion.importeSifacturas_ScalarQuery(CDec(Session.Item("Empresa")), folComprobacionCom)))
                rptComprobacion.SetParameterValue("var_totalSinFactura", FormatCurrency(taComprobacion.importeNoFacturas_ScalarQuery(CDec(Session.Item("Empresa")), folComprobacionCom)))
                rptComprobacion.SetParameterValue("var_total", FormatCurrency(taComprobacion.Total_ScalarQuery(CDec(Session.Item("Empresa")), folComprobacionCom)))

            Dim rutaPDF As String = "~\GTS\" & Session.Item("namePDFg") & ".pdf"
            rptComprobacion.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(rutaPDF))

                Response.Write("<script>")
                rutaPDF = rutaPDF.Replace("\", "/")
                rutaPDF = rutaPDF.Replace("~", "..")
                Response.Write("window.open('verPdfGts.aspx','popup','_blank','width=200,height=200')")
                Response.Write("</script>")
            Else
                ModalPopupExtender3.Show()
            Exit Sub
        End If
        lblTotalGastos.Visible = False
        btnComprobar.Visible = False

        GridView1.DataSource = Nothing
        GridView1.DataBind()
        GridView2.DataSource = Nothing
        GridView2.DataBind()
        GridView3.DataSource = Nothing
        GridView3.DataBind()
        GridView1.Visible = False
        GridView2.Visible = False
        GridView3.Visible = False
        btnAgregar.Visible = False
        btnCancelar.Visible = False
        btnComprobar.Visible = False
        btncancelarComprobacion.Visible = False
        txtDescripcion.Enabled = False
        ddlAutorizo.Enabled = False
        'ddlFolioSolicitud.Enabled = False
        ddlProveedor.Enabled = False
        'txtDestinoExtranjero.Enabled = False
        txtDestinoNacional.Enabled = False
        btnBuscar.Enabled = False
        btnAsignar.Enabled = False
        txtBuscar.Enabled = False
        txtImporteND.Text = ""
        txtConceptoND.Text = ""
        TabContainer1.Visible = False
        odbSolicitudes2.FilterExpression = "idConcepto = '" & taTipoDocumento.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "' AND estatus like '%Autoriza 2%'"
    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged

    End Sub

    Private Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound

        Dim con_b As Integer = 0
        If e.Row.RowType = DataControlRowType.DataRow Then
            txtConceptoND.Text = ""
            txtImporteND.Text = "0"
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "MONTO NO DEDUCIBLE: "
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Text = totalb.ToString("C")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If
        For Each rows2 As GridViewRow In GridView3.Rows
            total_b += total_b + CDec(GridView3.Rows(con_b).Cells(1).Text)
            con_b += 1
        Next
        lblTotalGastos.Text = "Importe total a comprobar: " + FormatCurrency((totala + totalb).ToString)

    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        GridView1.Visible = False
        'odbSolicitudes2.FilterExpression = "Vw_CXP_MisSolicitudesSC.usuario = '" & Session.Item("Usuario") & "' AND Vw_CXP_MisSolicitudesSC.idEmpresas = " & Session.Item("Empresa") & " AND Vw_CXP_MisSolicitudesSC.totalPagado - SUM(ISNULL(CXP_ComprobGtos.importe, 0)) > 0"
        'ddlAutorizo.Text = ""
        lblFechaSolicitud.Text = ""
        txtDescripcion.Text = ""
        txtFechaLlegada.Text = Date.Now.ToShortDateString
        txtFechaSalida.Text = Date.Now.ToShortDateString
        lblImporteSolicitado.Text = ""
        lblSaldo.Text = ""
        'txtDestinoExtranjero.Text = ""
        txtDestinoNacional.Text = ""
        'txtMotivoViaje.Text = ""
        txtBuscar.Text = ""
        odsProveedores.FilterExpression = "(idSucursal = '" & Session.Item("idSucursal") & "') AND (empresa = '" & Session.Item("Empresa") & "') OR ((idSucursal IS NULL) AND (empresa IS NULL))"

        ddlAutorizo.Enabled = False
        lblFechaSolicitud.Enabled = False
        txtDescripcion.Enabled = False
        txtFechaLlegada.Enabled = False
        txtFechaSalida.Enabled = False
        'txtDestinoExtranjero.Enabled = False
        txtDestinoNacional.Enabled = False
        'txtMotivoViaje.Enabled = False
        txtBuscar.Enabled = False
        btnBuscar.Enabled = False
        btnAsignar.Enabled = False
        btnCancelar.Visible = False
        ddlAutorizo.Enabled = False
        TabContainer1.Visible = False
        btnAgregar.Visible = False
    End Sub

    Protected Sub btncancelarComprobacion_Click(sender As Object, e As EventArgs) Handles btncancelarComprobacion.Click
        TabContainer1.Visible = False
        dtDetalleA.Clear()
        dtDetalleB.Clear()
        GridView2.DataSource = dtDetalleA
        GridView2.DataBind()
        GridView3.DataSource = dtDetalleB
        GridView3.DataBind()
        totalFactSald = 0
        lblTotalGastos.Text = "Importe total a comprobar: $ 0.00"
        btnComprobar.Visible = False
        btncancelarComprobacion.Visible = False
        btnComprobar.Visible = False
        lblTotalGastos.Visible = False
        btnAgregar.Visible = False
    End Sub


End Class
