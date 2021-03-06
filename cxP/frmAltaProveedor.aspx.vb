﻿Imports AjaxControlToolkit
Imports System.Data.SqlClient.SqlCommand
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Drawing

Public Class frmAltaProveedor
    Inherits System.Web.UI.Page
    Dim taProveedor As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            formato()
        End If

        'ddlMoneda.SelectedValue = "MXN"
        ddlPais.SelectedValue = "MEX"

        If Not IsPostBack Then
            Session.Item("Leyenda") = "Alta de proveedores"
            If Session.Item("solicitud") = "OK" Then
                odsProveedores.FilterExpression = "idProveedor = '" & Session.Item("noProveedor") & "'"
            End If
            Session.Item("noProveedor") = 0
            GridView1.DataBind()
            GridView2.DataBind()
            desactivarNuevosDatos()


        End If
    End Sub

    Private Sub formato()


        GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView1.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        GridView2.FooterStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAceptar2.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnActualizar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnActualizarArch.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAgregarCta.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnAutorizar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnGuardar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnNuevo.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)


    End Sub

    'Protected Sub fluAsistencias_UploadComplete(sender As Object, e As AjaxFileUploadEventArgs)
    '    Dim tableAdapterProveedoresArch As New dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter
    '    Dim guuidArch As String = Guid.NewGuid.ToString
    '    Dim filePath As String = "~/TmpFinagil/FilesProv/" & Session.Item("empresa") & "-" & guuidArch & ".pdf" 'Convert.ToString(e.FileName)
    '    fluAsistencias.SaveAs(MapPath(filePath))
    '    Dim txt As String = Session.Item("noProveedor")
    '    tableAdapterProveedoresArch.Insert(CDec(Session.Item("noProveedor")), Convert.ToString(e.FileName), guuidArch, 1, 1)
    '    GridView2.DataBind()
    'End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        odsProveedores.FilterExpression = "razonSocial LIKE '%" & txtBuscar.Text.Trim & "%' OR rfc LIKE '%" & txtBuscar.Text.Trim & "%'"
        limpiarNuevo()
        divDetalles.Visible = False
        txtRfc.Enabled = False
        txtRazonSocial.Enabled = False
        txtActivo.Enabled = False
        txtAutorizado.Enabled = False
    End Sub

    Protected Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        txtRfc.Enabled = False
        txtRazonSocial.Enabled = False
        txtActivo.Enabled = False
        txtAutorizado.Enabled = False
        Session.Item("tipoPersona") = ""

        Dim tableProveedores2 As New dsProduccion.CXP_Proveedores2DataTable
        Dim rowsProveedores2 As dsProduccion.CXP_Proveedores2Row
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        Dim taUsuarios As New dsProduccionTableAdapters.UsuariosTableAdapter
        Dim taProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Try
            tableAdapterProveedores2.Fill(tableProveedores2, ddlBuscar.SelectedValue)
        Catch ex As Exception
            lblErrorGeneral.Text = ex.ToString
            ModalPopupExtender1.Show()
            Exit Sub
        End Try
        If tableProveedores2.Rows.Count = 1 Then
            tablaCuentas.Visible = True
            rowsProveedores2 = tableProveedores2.Rows(0)
            txtRfc.Text = rowsProveedores2.rfc
            txtRazonSocial.Text = rowsProveedores2.razonSocial
            txtColonia.Text = rowsProveedores2.colonia
            txtCalle.Text = rowsProveedores2.calle
            txtLocalidad.Text = rowsProveedores2.localidad
            txtDelegacion.Text = rowsProveedores2.delegacion
            txtEstado.Text = rowsProveedores2.estado


            If IsNothing(rowsProveedores2.pais) = False Then
                ddlPais.SelectedValue = rowsProveedores2.pais.Trim
            Else
                ddlPais.SelectedValue = Nothing
            End If
            txtCp.Text = rowsProveedores2.cp
            txtActivo.Text = rowsProveedores2.activoS
            txtAutorizado.Text = rowsProveedores2.autorizadoS
            txtNoProveedor.Text = rowsProveedores2.idProveedor
            txtNit.Text = rowsProveedores2.nit
            txtCurp.Text = rowsProveedores2.curp
            txtMail.Text = rowsProveedores2.mail

            chkClientProv.Checked = tableAdapterProveedores2.EsCliente_ScalarQuery(rowsProveedores2.idProveedor)

            'If IsNothing(rowsProveedores2.activo) Then
            '    chkClientProv.Checked = False
            'Else
            '    chkClientProv.Checked = rowsProveedores2.activo
            'End If

            If taUsuarios.ExisteUsuario_ScalarQuery(rowsProveedores2.rfc) <> "NE" Then
                Session.Item("rfcEmpleado") = rowsProveedores2.rfc
            Else
                Session.Item("rfcEmpleado") = "NE"
            End If

        End If

        If tableProveedores2.Rows.Count = 0 Then
            tablaCuentas.Visible = False
        End If
        If txtActivo.Text = "NO ACTIVO" And txtAutorizado.Text = "NO AUTORIZADO" Then
            btnActualizar.Enabled = True
            activarNuevosDatos()
        ElseIf txtAutorizado.Text = "EN PROCESO" Then
            btnActualizar.Enabled = False
            desactivarNuevosDatos()
        ElseIf txtAutorizado.Text = "AUTORIZADO" And txtActivo.Text = "ACTIVO" Then
            btnActualizar.Enabled = False
            desactivarNuevosDatos()
        ElseIf txtAutorizado.Text = "PENDIENTE" Then
            btnAutorizar.Enabled = False
            desactivarProceso()
            'ElseIf txtAutorizado.Text = "RECHAZADO" Then
            '    btnAutorizar.Enabled = False
            'activarNuevosDatos()
        ElseIf txtAutorizado.Text = "" And txtActivo.Text = "" Then
            btnActualizar.Enabled = True
            activarNuevosDatos()
        Else
            btnActualizar.Enabled = True
            activarNuevosDatos()
        End If
        Session.Item("noProveedor") = ddlBuscar.SelectedValue
        GridView1.DataBind()
        GridView2.DataBind()

        If Session.Item("rfcEmpleado") = "NE" Or IsNothing(Session.Item("rfcEmpleado")) Then
            If taProveedores.ObtClientProv_ScalarQuery(txtRfc.Text.Trim) = False Then

                If txtRfc.Text.Length = 12 Then
                    Session.Item("tipoPersona") = "M"
                ElseIf txtRfc.Text.Length = 13 Then
                    Session.Item("tipoPersona") = "F"
                End If
            Else
                Session.Item("tipoPersona") = "C"
            End If
        Else
            Session.Item("tipoPersona") = "E"
        End If
        divDetalles.Visible = True
    End Sub

    Protected Sub btnNuevo1_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        btnGuardar.Enabled = True
        GridView1.DataBind()
        GridView2.DataBind()
        divDetalles.Visible = False
        limpiarNuevo()
        ddlPais.SelectedValue = "MEX"
    End Sub

    Public Sub limpiarNuevo()
        txtActivo.Text = ""
        txtAutorizado.Text = ""
        txtCalle.Text = ""
        txtColonia.Text = ""
        txtCp.Text = ""
        txtDelegacion.Text = ""
        txtDescipcion.Text = ""
        txtEstado.Text = ""
        txtLocalidad.Text = ""
        txtRazonSocial.Text = ""
        txtRfc.Text = ""
        txtCurp.Text = ""
        ddlPais.SelectedIndex = 0
        txtNit.Text = ""
        txtRfc.Text = ""
        txtNoProveedor.Text = ""
        txtMail.Text = ""

        chkCheque.Enabled = True
        txtRfc.Enabled = True
        txtRazonSocial.Enabled = True
        txtNit.Enabled = True
        txtCurp.Enabled = True
        txtMail.Enabled = True
        txtCalle.Enabled = True
        txtColonia.Enabled = True
        txtLocalidad.Enabled = True
        txtDelegacion.Enabled = True
        txtEstado.Enabled = True
        ddlPais.Enabled = True
        txtCp.Enabled = True
        chkClientProv.Enabled = True
    End Sub

    Public Sub activarNuevosDatos()
        ddlBanco.Enabled = True
        ddlMoneda.Enabled = True
        txtDescipcion.Enabled = True
        txtCuentaBancaria.Enabled = True
        txtClabe.Enabled = True
        afuArcCta.Enabled = True
        afuArcCta.Visible = True
        ddlDocumentacionProv.Enabled = True
        afuDocumentacionProv.Enabled = True
        afuDocumentacionProv.Visible = True
        btnAgregarCta.Enabled = True
        btnActualizarArch.Enabled = True
        btnActualizar.Enabled = True
        btnAutorizar.Enabled = True
        txtRfc.Enabled = True
        txtRazonSocial.Enabled = True
        txtNit.Enabled = True
        txtCurp.Enabled = True
        txtMail.Enabled = True
        txtCalle.Enabled = True
        txtColonia.Enabled = True
        txtLocalidad.Enabled = True
        txtDelegacion.Enabled = True
        txtEstado.Enabled = True
        ddlPais.Enabled = True
        txtCp.Enabled = True
        chkClientProv.Enabled = True
    End Sub

    Public Sub desactivarNuevosDatos()
        'ddlBanco.Enabled = False
        'ddlMoneda.Enabled = False
        'txtDescipcion.Enabled = False
        'txtCuentaBancaria.Enabled = False
        'txtClabe.Enabled = False
        'afuArcCta.Enabled = False
        'afuArcCta.Visible = False
        'ddlDocumentacionProv.Enabled = False
        'afuDocumentacionProv.Enabled = False
        'afuDocumentacionProv.Visible = False
        'btnAgregarCta.Enabled = False
        'btnActualizarArch.Enabled = False
        chkCheque.Enabled = False
        btnActualizar.Enabled = False
        btnAutorizar.Enabled = False
        txtRfc.Enabled = False
        txtRazonSocial.Enabled = False
        txtNit.Enabled = False
        txtCurp.Enabled = False
        txtMail.Enabled = False
        txtCalle.Enabled = False
        txtColonia.Enabled = False
        txtLocalidad.Enabled = False
        txtDelegacion.Enabled = False
        txtEstado.Enabled = False
        ddlPais.Enabled = False
        txtCp.Enabled = False
        chkClientProv.Enabled = False
    End Sub

    Public Sub desactivarProceso()
        ddlBanco.Enabled = True
        ddlMoneda.Enabled = True
        txtDescipcion.Enabled = True
        txtCuentaBancaria.Enabled = True
        txtClabe.Enabled = True
        afuArcCta.Enabled = True
        afuArcCta.Visible = True
        ddlDocumentacionProv.Enabled = True
        afuDocumentacionProv.Enabled = True
        afuDocumentacionProv.Visible = True
        btnAgregarCta.Enabled = True
        btnActualizarArch.Enabled = True
        btnActualizar.Enabled = True
        'btnAutorizar.Enabled = True
        txtRfc.Enabled = False
        txtRazonSocial.Enabled = False
        txtNit.Enabled = False
        txtCurp.Enabled = False
        txtMail.Enabled = False
        txtCalle.Enabled = False
        txtColonia.Enabled = False
        txtLocalidad.Enabled = False
        txtDelegacion.Enabled = False
        txtEstado.Enabled = False
        ddlPais.Enabled = False
        txtCp.Enabled = False
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        Dim tableAdapterProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter

        Dim validacion As String = "SI"
        'valida RFC
        If txtRfc.Text.Trim.Length = 12 Or txtRfc.Text.Trim.Length = 13 Then
            If txtRfc.Text.Trim <> String.Empty Then
                If Regex.IsMatch(txtRfc.Text.Trim, "^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$") = False Then
                    lblErrorGeneral.Text = "Estructura del RFC incorrecta."
                    ModalPopupExtender1.Show()
                    validacion = "NO"
                    Exit Sub
                End If
            End If
        Else
            lblErrorGeneral.Text = "Longitud del RFC incorrecta."
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        'valida CURP
        If txtCurp.Text.Trim <> String.Empty Then
            If Regex.IsMatch(txtCurp.Text.Trim, "^([a-zA-Z]{4,4}[0-9]{6}[a-zA-Z]{6,6}[0-9-a-zA-Z]{2})$") = False Then
                lblErrorGeneral.Text = "Estructura de la clave CURP incorrecta."
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If
        End If

        If txtRazonSocial.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "No se ha incluido la razón social."
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtRfc.Text.Trim = "XEXX010101000" Then
            If txtNit.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Cuando un RFC es para extranjeros, se debe incluir el NIT correspondiente al país de origen"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If
        End If

        If txtRfc.Text.Trim.Length = 13 Then
            If txtCurp.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Cuando un RFC es de una persona física, se debe incluir la CURP correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If
        End If

        If txtMail.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir una dirección de correo electrónico"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtCalle.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir la calle y número correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtColonia.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir la colonia correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtLocalidad.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir la localidad correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtDelegacion.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir la delegación o municipio correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtEstado.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir la estado correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If txtCp.Text.Trim = String.Empty Then
            lblErrorGeneral.Text = "Se debe de incluir el CP correspondiente"
            ModalPopupExtender1.Show()
            validacion = "NO"
            Exit Sub
        End If

        If validacion = "SI" Then
            If txtRfc.Text.Trim = "XAXX010101000" Or txtRfc.Text = "XEXX010101000" Then
                Dim idProv As Decimal = tableAdapterProveedores2.NuevoProveedorScalarQuery(txtRfc.Text, txtNit.Text, txtCurp.Text, txtRazonSocial.Text, Nothing, 0, Nothing, 0, 0, Nothing, Date.Now.ToLongDateString, txtMail.Text, Nothing, "", "", "", 0, 0, txtCalle.Text, txtColonia.Text, txtLocalidad.Text, txtDelegacion.Text, txtEstado.Text, ddlPais.SelectedValue, txtCp.Text, "NO ACTIVO", "NO AUTORIZADO")
                lblErrorGeneral.Text = "El número de proveedor es: " & idProv.ToString
                ModalPopupExtender1.Show()
                txtNoProveedor.Text = idProv.ToString

                divDetalles.Visible = True

                If txtActivo.Text = "NO ACTIVO" And txtAutorizado.Text = "NO AUTORIZADO" Then
                    btnActualizar.Enabled = True
                    activarNuevosDatos()
                ElseIf txtAutorizado.Text = "EN PROCESO" Then
                    btnActualizar.Enabled = False
                    desactivarNuevosDatos()
                ElseIf txtAutorizado.Text = "AUTORIZADO" And txtActivo.Text = "ACTIVO" Then
                    btnActualizar.Enabled = False
                    'activarNuevosDatos()
                ElseIf txtAutorizado.Text = "" And txtActivo.Text = "" Then
                    btnActualizar.Enabled = True
                    activarNuevosDatos()
                Else
                    btnActualizar.Enabled = True
                    activarNuevosDatos()
                End If
            Else
                If tableAdapterProveedores.ExisteRFC_ScalarQuery(txtRfc.Text.Trim) = "NE" Then
                    Dim idProv As Decimal = tableAdapterProveedores2.NuevoProveedorScalarQuery(txtRfc.Text, txtNit.Text, txtCurp.Text, txtRazonSocial.Text, Nothing, 0, Nothing, 0, 0, Nothing, Date.Now.ToLongDateString, txtMail.Text, Nothing, "", "", "", chkClientProv.Checked, 0, txtCalle.Text, txtColonia.Text, txtLocalidad.Text, txtDelegacion.Text, txtEstado.Text, ddlPais.SelectedValue, txtCp.Text, "NO ACTIVO", "NO AUTORIZADO")
                    lblErrorGeneral.Text = "El número de proveedor es: " & idProv.ToString
                    ModalPopupExtender1.Show()
                    txtNoProveedor.Text = idProv.ToString
                    Session.Item("noProveedor") = idProv.ToString
                    divDetalles.Visible = True

                    If txtActivo.Text = "NO ACTIVO" And txtAutorizado.Text = "NO AUTORIZADO" Then
                        btnActualizar.Enabled = True
                        activarNuevosDatos()
                    ElseIf txtAutorizado.Text = "EN PROCESO" Then
                        btnActualizar.Enabled = False
                        desactivarNuevosDatos()
                    ElseIf txtAutorizado.Text = "AUTORIZADO" And txtActivo.Text = "ACTIVO" Then
                        btnActualizar.Enabled = False
                        'activarNuevosDatos()
                    ElseIf txtAutorizado.Text = "" And txtActivo.Text = "" Then
                        btnActualizar.Enabled = True
                        activarNuevosDatos()
                    Else
                        btnActualizar.Enabled = True
                        activarNuevosDatos()
                    End If
                Else
                    lblErrorGeneral.Text = "El RFC ya existe"
                    ModalPopupExtender1.Show()
                    divDetalles.Visible = False
                    Exit Sub
                End If
            End If
        End If
        btnGuardar.Enabled = False
        If Session.Item("rfcEmpleado") = "NE" Or IsNothing(Session.Item("rfcEmpleado")) Then
            If tableAdapterProveedores.ObtClientProv_ScalarQuery(txtRfc.Text.Trim) = False Then
                If txtRfc.Text.Length = 12 Then
                    Session.Item("tipoPersona") = "M"
                ElseIf txtRfc.Text.Length = 13 Then
                    Session.Item("tipoPersona") = "F"
                End If
            Else
                Session.Item("tipoPersona") = "C"
            End If
        Else
            Session.Item("tipoPersona") = "E"
        End If
        If chkClientProv.Checked = True Or txtRfc.Text.Trim = "XAXX010101000" Then
            Session.Item("tipoPersona") = "C"
        End If
        GridView3.DataBind()
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        Dim taProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        If txtNoProveedor.Text <> String.Empty Then
            Dim validacion As String = "SI"
            'valida RFC
            If txtRfc.Text <> String.Empty Then
                If Regex.IsMatch(txtRfc.Text.Trim, "^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$") = False Then
                    lblErrorGeneral.Text = "Estructura del RFC incorrecta."
                    ModalPopupExtender1.Show()
                    validacion = "NO"
                    Exit Sub
                End If
            End If

            'valida CURP
            If txtCurp.Text <> String.Empty Then
                If Regex.IsMatch(txtCurp.Text.Trim, "^([a-zA-Z]{4,4}[0-9]{6}[a-zA-Z]{6,6}[0-9]{2})$") = False Then
                    lblErrorGeneral.Text = "Estructura de la clave CURP incorrecta."
                    ModalPopupExtender1.Show()
                    validacion = "NO"
                    Exit Sub
                End If
            End If

            If txtRfc.Text.Trim = "XEXX010101000" Then
                If txtNit.Text.Trim = String.Empty Then
                    lblErrorGeneral.Text = "Cuando un RFC es para extranjeros, se debe incluir el NIT correspondiente al país de origen"
                    ModalPopupExtender1.Show()
                    validacion = "NO"
                    Exit Sub
                End If
            End If

            If txtRfc.Text.Trim.Length = 13 Then
                If txtCurp.Text.Trim = String.Empty Then
                    lblErrorGeneral.Text = "Cuando un RFC es de una persona física, se debe incluir la CURP correspondiente"
                    ModalPopupExtender1.Show()
                    validacion = "NO"
                    Exit Sub
                End If
            End If

            If txtMail.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir una dirección de correo electrónico"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtCalle.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir la calle y número correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtColonia.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir la colonia correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtLocalidad.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir la localidad correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtDelegacion.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir la delegación o municipio correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtEstado.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir la estado correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If txtCp.Text.Trim = String.Empty Then
                lblErrorGeneral.Text = "Se debe de incluir el CP correspondiente"
                ModalPopupExtender1.Show()
                validacion = "NO"
                Exit Sub
            End If

            If validacion = "SI" Then
                tableAdapterProveedores2.ActualizaProveedor_UpdateQuery(txtRfc.Text.Trim, txtNit.Text.Trim, txtCurp.Text.Trim, txtRazonSocial.Text.Trim, txtMail.Text.Trim, txtCalle.Text.Trim, txtColonia.Text.Trim, txtLocalidad.Text.Trim, txtDelegacion.Text.Trim, txtEstado.Text.Trim, ddlPais.SelectedValue, txtCp.Text.Trim, chkClientProv.Checked, txtNoProveedor.Text.Trim)
            End If
            'btnActualizar.Enabled = False
        End If
        If Session.Item("rfcEmpleado") = "NE" Or IsNothing(Session.Item("rfcEmpleado")) Then
            If taProveedores.ObtClientProv_ScalarQuery(txtRfc.Text.Trim) = False Then
                If txtRfc.Text.Length = 12 Then
                    Session.Item("tipoPersona") = "M"
                ElseIf txtRfc.Text.Length = 13 Then
                    Session.Item("tipoPersona") = "F"
                End If
            Else
                Session.Item("tipoPersona") = "C"
            End If
        Else
            Session.Item("tipoPersona") = "E"
        End If
        If chkClientProv.Checked = True Or txtRfc.Text.Trim = "XAXX010101000" Then
            Session.Item("tipoPersona") = "C"
        End If
    End Sub

    Protected Sub btnAgregarCta_Click(sender As Object, e As EventArgs) Handles btnAgregarCta.Click
        Dim tableAdapterCuentasBancariasProv As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        If txtDescipcion.Text = String.Empty Then
            lblErrorGeneral.Text = "No se ha ingresado una descripción para esta cuenta."
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        'If ddlBanco.SelectedItem.Text = "SANTANDER" Then
        '    If txtCuentaBancaria.Text.Trim <> String.Empty Then
        '        If txtCuentaBancaria.Text.Trim.Length = 11 Then
        '            If IsNumeric(txtCuentaBancaria.Text.Trim) = False Then
        '                lblErrorGeneral.Text = "El valor ingresado como cuenta bancaria debe de ser numérico."
        '                ModalPopupExtender1.Show()
        '                Exit Sub
        '            End If
        '        Else
        '            lblErrorGeneral.Text = "La cuenta bancaria no tiene la longitud correcta."
        '            ModalPopupExtender1.Show()
        '            Exit Sub
        '        End If
        '    End If
        'Else
        '    If txtCuentaBancaria.Text.Trim <> String.Empty Then
        '        If txtCuentaBancaria.Text.Trim.Length = 10 Then
        '            If IsNumeric(txtCuentaBancaria.Text.Trim) = False Then
        '                lblErrorGeneral.Text = "El valor ingresado como cuenta bancaria debe de ser numérico."
        '                ModalPopupExtender1.Show()
        '                Exit Sub
        '            End If
        '        Else
        '            lblErrorGeneral.Text = "La cuenta bancaria no tiene la longitud correcta."
        '            ModalPopupExtender1.Show()
        '            Exit Sub
        '        End If
        '    End If
        'End If
        If txtClabe.Text.Trim <> String.Empty Then
            If txtClabe.Text.Trim.Length = 18 Then
                If IsNumeric(txtClabe.Text.Trim) = False Then
                    lblErrorGeneral.Text = "El valor ingresado como cuenta CLABE debe de ser numérico."
                    ModalPopupExtender1.Show()
                    Exit Sub
                Else
                    txtCuentaBancaria.Text = txtClabe.Text.Substring(6, 11)
                End If
            Else
                lblErrorGeneral.Text = "La cuenta CLABE no tiene la longitud correcta."
                ModalPopupExtender1.Show()
                Exit Sub
            End If
        End If
        If afuArcCta.HasFile Then
            Dim guuidCta As String = Guid.NewGuid.ToString
            Dim filePath As String = "~/TmpFinagil/FilesProv/" & Convert.ToString(guuidCta) & ".pdf"
            afuArcCta.SaveAs(MapPath(filePath))

            If txtAutorizado.Text.Trim = "AUTORIZADO" Then
                tableAdapterProveedores2.CambiaEstatus_UpdateQuery("PENDIENTE", txtNoProveedor.Text.Trim)
                txtAutorizado.Text = "PENDIENTE"
            End If

            Select Case txtAutorizado.Text.Trim
                Case "AUTORIZADO"
                    tableAdapterCuentasBancariasProv.Insert(txtNoProveedor.Text.Trim, ddlBanco.SelectedValue, txtCuentaBancaria.Text.Trim, txtClabe.Text.Trim, txtDescipcion.Text.Trim, ddlMoneda.SelectedValue, guuidCta, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 12, txtReferencia.Text.Trim, txtConvenio.Text.Trim, Nothing, chkCuentaNueva.Checked)
                    enviaCorreoCuentasBancariasActiv(ddlBanco.SelectedItem.Text, ddlMoneda.SelectedItem.Text, txtCuentaBancaria.Text, txtClabe.Text, txtConvenio.Text, txtReferencia.Text, "CXP\FilesProv\" & guuidCta & ".pdf")
                Case "PENDIENTE"
                    tableAdapterCuentasBancariasProv.Insert(txtNoProveedor.Text.Trim, ddlBanco.SelectedValue, txtCuentaBancaria.Text.Trim, txtClabe.Text.Trim, txtDescipcion.Text.Trim, ddlMoneda.SelectedValue, guuidCta, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 12, txtReferencia.Text.Trim, txtConvenio.Text.Trim, Nothing, chkCuentaNueva.Checked)
                    enviaCorreoCuentasBancariasActiv(ddlBanco.SelectedItem.Text, ddlMoneda.SelectedItem.Text, txtCuentaBancaria.Text, txtClabe.Text, txtConvenio.Text, txtReferencia.Text, "CXP\FilesProv\" & guuidCta & ".pdf")
                Case "NO AUTORIZADO"
                    tableAdapterCuentasBancariasProv.Insert(txtNoProveedor.Text.Trim, ddlBanco.SelectedValue, txtCuentaBancaria.Text.Trim, txtClabe.Text.Trim, txtDescipcion.Text.Trim, ddlMoneda.SelectedValue, guuidCta, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 22, txtReferencia.Text.Trim, txtConvenio.Text.Trim, Nothing, chkCuentaNueva.Checked)
                Case "RECHAZADO"
                    tableAdapterCuentasBancariasProv.Insert(txtNoProveedor.Text.Trim, ddlBanco.SelectedValue, txtCuentaBancaria.Text.Trim, txtClabe.Text.Trim, txtDescipcion.Text.Trim, ddlMoneda.SelectedValue, guuidCta, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 22, txtReferencia.Text.Trim, txtConvenio.Text.Trim, Nothing, chkCuentaNueva.Checked)
                Case ""
                    txtAutorizado.Text = "NO AUTORIZADO"
                    tableAdapterCuentasBancariasProv.Insert(txtNoProveedor.Text.Trim, ddlBanco.SelectedValue, txtCuentaBancaria.Text.Trim, txtClabe.Text.Trim, txtDescipcion.Text.Trim, ddlMoneda.SelectedValue, guuidCta, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 22, txtReferencia.Text.Trim, txtConvenio.Text.Trim, Nothing, chkCuentaNueva.Checked)
            End Select

            GridView1.DataBind()
        Else
            lblErrorGeneral.Text = "Es necesario adjuntar el archivo correspondiente al estado de cuenta bancario en formato PDF."
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        ddlBanco.SelectedIndex = 0
        ddlMoneda.SelectedIndex = 0
        txtDescipcion.Text = ""
        txtCuentaBancaria.Text = ""
        txtClabe.Text = ""
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim tableAdapterCuentasBancariasProv As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter

        Select Case e.CommandName

            Case "Eliminar"
                tableAdapterCuentasBancariasProv.EliminRegistro_DeleteQuery(e.CommandArgument)
                GridView1.DataBind()
                validaYCambiaEstatus(txtNoProveedor.Text.Trim, txtRfc.Text.Trim)
            Case "verPdf"
                Dim rutaPDF As String = "~\TmpFinagil\FilesProv\" & e.CommandArgument & ".pdf"
                Session.Item("namePDF") = e.CommandArgument
                Response.Write("<script>")
                rutaPDF = rutaPDF.Replace("\", "/")
                rutaPDF = rutaPDF.Replace("~", "..")
                Response.Write("window.open('verPdfProv.aspx#toolbar=0','popup','_blank','width=200,height=200')")
                Response.Write("</script>")
            Case "DesactivarCuenta"
                Dim tableCuentas As New dsProduccion.Vw_CXP_CuentasBancariasProvDataTable
                Dim rowCuentas As dsProduccion.Vw_CXP_CuentasBancariasProvRow
                Dim taCuentas As New dsProduccionTableAdapters.Vw_CXP_CuentasBancariasProvTableAdapter
                Dim banco = "", descripcion = "", moneda = "", cuenta = "", clabe = "", convenio = "", referencia = "", archivo = ""

                taCuentas.Fill(tableCuentas, e.CommandArgument)
                If tableCuentas.Rows.Count = 1 Then
                    rowCuentas = tableCuentas.Rows(0)
                    banco = rowCuentas.nombreCorto
                    descripcion = rowCuentas.descripcion
                    moneda = rowCuentas.c_NombreMoneda
                    cuenta = rowCuentas.cuenta
                    clabe = rowCuentas.clabe
                    convenio = rowCuentas.convenio
                    referencia = rowCuentas.referencia
                    archivo = rowCuentas.archivo1
                End If

                tableAdapterCuentasBancariasProv.CambiaEstatus_UpdateQuery(14, Date.Now, Session.Item("usuario"), e.CommandArgument)
                enviaCorreoCuentasBancarias(banco & " - " & descripcion, moneda, cuenta, clabe, convenio, referencia, archivo)
                If txtAutorizado.Text.Trim = "AUTORIZADO" Then
                    tableAdapterProveedores2.CambiaEstatus_UpdateQuery("PENDIENTE", txtNoProveedor.Text.Trim)
                    txtAutorizado.Text = "PENDIENTE"
                End If
                GridView1.DataBind()
        End Select
    End Sub

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        Dim tableAdapterDocumentacionProv As New dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter

        Select Case e.CommandName
            Case "Eliminar"
                tableAdapterDocumentacionProv.EliminaReg_DeleteQuery(e.CommandArgument)
                GridView2.DataBind()
                validaYCambiaEstatus(txtNoProveedor.Text.Trim, txtRfc.Text.Trim)
            Case "verPdf"
                Dim rutaPDF As String = "~\TmpFinagil\FilesProv\" & e.CommandArgument & ".pdf"
                Session.Item("namePDF") = e.CommandArgument
                Response.Write("<script>")
                rutaPDF = rutaPDF.Replace("\", "/")
                rutaPDF = rutaPDF.Replace("~", "..")
                Response.Write("window.open('verPdfProv.aspx#toolbar=0','popup','_blank','width=200,height=200')")
                Response.Write("</script>")
            Case "DesactivarDocumento"
                Dim tableAdapter As New dsProduccionTableAdapters.Vw_CXP_DocumentacionProvTableAdapter
                Dim rowDocu As dsProduccion.Vw_CXP_DocumentacionProvRow
                Dim tableDoc As New dsProduccion.Vw_CXP_DocumentacionProvDataTable
                Dim documento As String = ""
                tableAdapter.Fill(tableDoc, e.CommandArgument)

                If tableDoc.Rows.Count = 1 Then
                    rowDocu = tableDoc.Rows(0)
                    documento = rowDocu.descripcion
                End If

                enviaCorreoDocumentos(documento)

                tableAdapterDocumentacionProv.CambiaEstatus_UpdateQuery(19, Session.Item("usuario"), Date.Now, e.CommandArgument)
                If txtAutorizado.Text.Trim = "AUTORIZADO" Then
                    tableAdapterProveedores2.CambiaEstatus_UpdateQuery("PENDIENTE", txtNoProveedor.Text.Trim)
                    txtAutorizado.Text = "PENDIENTE"
                End If
                GridView2.DataBind()
        End Select
    End Sub


    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click


        Dim tableAdapterProveedor As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        Dim tableadapterProveedorArch As New dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter
        Dim tableadapterDocumentacionProv As New dsProduccionTableAdapters.CXP_DocumentacionProvTableAdapter
        Dim tableAdapterGenCorreos As New dsProduccionTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim tableAdapterCuentasBancarias As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim mensaje As String = ""
        Dim formaPago As String = "TRANSFERENCIA"

        If GridView1.Rows.Count = 0 And chkCheque.Checked = False Then
            lblErrorGeneral.Text = "Si no agrega una cuenta bancaria es necesario que indique si el pago será con cheque"
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If GridView3.Rows.Count = 0 Then
            lblErrorGeneral.Text = "La documentación del proveedor es incompleta, favor de revisar el listado de documentos obligatorios"
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If valida_Proveedor() = "ERR"
            Exit Sub
        End If

        If txtNoProveedor.Text <> String.Empty Then
            If tableadapterDocumentacionProv.NoDoctosOblig_ScalarQuery(Session.Item("tipoPersona")) <= tableadapterProveedorArch.ObtNoDoctsObligXProv_ScalarQuery(txtNoProveedor.Text.Trim) Then
                tableAdapterProveedor.AutorizaPro_UpdateQuery(Session.Item("mailUsuarioS"), chkCheque.Checked.ToString, CDec(txtNoProveedor.Text.Trim))
                tableAdapterCuentasBancarias.CambiaEstatusAll_UpdateQuery(22, txtNoProveedor.Text.Trim)
                tableadapterProveedorArch.CambiaEstatusAll_UpdateQuery(23, txtNoProveedor.Text.Trim)

                If chkCheque.Checked = True Then
                    formaPago = "CHEQUE"
                End If

                btnAutorizar.Enabled = False
                'GridView1.Enabled = False
                'GridView2.Enabled = False
                GridView1.DataBind()
                GridView2.DataBind()

                desactivarNuevosDatos()
                lblErrorGeneral.Text = "Se envió correctamente la solicitud de alta del proveedor: " & txtNoProveedor.Text.Trim
                ModalPopupExtender1.Show()
                'Envía mensaje a administrador
                mensaje = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de alta del proveedor con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>RFC</td>" &
                        "<td>Forma de Pago</td>" &
                    "</tr>" &
                    "<tr>" &
                            "<td>" & txtRazonSocial.Text.Trim & "</td>" &
                            "<td>" & txtRfc.Text.Trim & "</td>" &
                            "<td>" & formaPago & "</td>" &
                        "</tr></table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"

                'Notificación de alta por tesorería
                If Session.Item("Usuario") = "atorres" Or Session.Item("Usuario") = "gisvazquez" Then
                    mensaje = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de alta del proveedor con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>RFC</td>" &
                    "</tr>" &
                    "<tr>" &
                            "<td>" & txtRazonSocial.Text.Trim & "</td>" &
                            "<td>" & txtRfc.Text.Trim & "</td>" &
                        "</tr></table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"

                    tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "vcruz@lamoderna.com.mx", "Solicitud de alta de proveedor (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
                    tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "epineda@finagil.com.mx", "Solicitud de alta de proveedor (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
                    tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de alta de proveedor (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
                    tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de alta de proveedor (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
                End If

                tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", Session.Item("mailUsuarioS"), "Solicitud de alta de proveedor", mensaje, False, Date.Now.ToLongDateString, "")
                tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de alta de proveedor", mensaje, False, Date.Now.ToLongDateString, "")
                tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de alta de proveedor", mensaje, False, Date.Now.ToLongDateString, "")

                tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "atorres@lamoderna.com.mx", "Solicitud de alta de proveedor", mensaje, False, Date.Now.ToLongDateString, "")
                tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "gisvazquez@finagil.com.mx", "Solicitud de alta de proveedor", mensaje, False, Date.Now.ToLongDateString, "")
                txtAutorizado.Text = "EN PROCESO"
            Else
                lblErrorGeneral.Text = "La documentación del proveedor es incompleta, favor de revisar el listado de documentos obligatorios"
                ModalPopupExtender1.Show()
            End If
        End If
        Session.Item("tipoPersona") = ""
    End Sub



    Protected Sub btnActualizarArch_Click(sender As Object, e As EventArgs) Handles btnActualizarArch.Click
        Dim tableadapterProveedoresArc As New dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter
        Dim tableAdapterProveedores2 As New dsProduccionTableAdapters.CXP_Proveedores2TableAdapter
        Dim contador As Integer = 0
        For Each row As GridViewRow In GridView2.Rows
            If GridView2.Rows(contador).Cells(9).Text.Trim = ddlDocumentacionProv.SelectedValue And (GridView2.Rows(contador).Cells(11).Text.Trim = "16" Or GridView2.Rows(contador).Cells(11).Text.Trim = "17" Or GridView2.Rows(contador).Cells(11).Text.Trim = "19" Or GridView2.Rows(contador).Cells(11).Text.Trim = "23" Or GridView2.Rows(contador).Cells(11).Text.Trim = "24" Or GridView2.Rows(contador).Cells(11).Text.Trim = "26") Then
                lblErrorGeneral.Text = "El (La) " & ddlDocumentacionProv.SelectedItem.Text & " ya existe."
                ModalPopupExtender1.Show()
                Exit Sub
            End If
            contador += 1
        Next
        If afuDocumentacionProv.HasFile Then
            Dim guuidDcProv As String = Guid.NewGuid.ToString
            Dim filePath As String = "~/TmpFinagil/FilesProv/" & Convert.ToString(guuidDcProv) & ".pdf"
            afuDocumentacionProv.SaveAs(MapPath(filePath))

            If txtAutorizado.Text.Trim = "AUTORIZADO" Then
                tableAdapterProveedores2.CambiaEstatus_UpdateQuery("PENDIENTE", txtNoProveedor.Text.Trim)
                txtAutorizado.Text = "PENDIENTE"
            End If

            Select Case txtAutorizado.Text.Trim
                Case "AUTORIZADO"
                    tableadapterProveedoresArc.Insert(txtNoProveedor.Text.Trim, afuDocumentacionProv.FileName, guuidDcProv, ddlDocumentacionProv.SelectedValue, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 17)
                    enviaCorreoDocumentosActive(ddlDocumentacionProv.SelectedItem.Text)
                Case "PENDIENTE"
                    tableadapterProveedoresArc.Insert(txtNoProveedor.Text.Trim, afuDocumentacionProv.FileName, guuidDcProv, ddlDocumentacionProv.SelectedValue, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 17)
                    enviaCorreoDocumentosActive(ddlDocumentacionProv.SelectedItem.Text)
                Case "NO AUTORIZADO"
                    tableadapterProveedoresArc.Insert(txtNoProveedor.Text.Trim, afuDocumentacionProv.FileName, guuidDcProv, ddlDocumentacionProv.SelectedValue, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 23)
                Case "RECHAZADO"
                    tableadapterProveedoresArc.Insert(txtNoProveedor.Text.Trim, afuDocumentacionProv.FileName, guuidDcProv, ddlDocumentacionProv.SelectedValue, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 23)
                Case ""
                    txtAutorizado.Text = "NO AUTORIZADO"
                    tableadapterProveedoresArc.Insert(txtNoProveedor.Text.Trim, afuDocumentacionProv.FileName, guuidDcProv, ddlDocumentacionProv.SelectedValue, True, Session.Item("usuario"), Nothing, Nothing, Nothing, Date.Now, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, System.Data.SqlTypes.SqlDateTime.Null, 23)
            End Select

            GridView1.DataBind()
        Else
            lblErrorGeneral.Text = "Es necesario adjuntar el archivo correspondiente al estado de cuenta bancario en formato PDF."
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        GridView2.DataBind()
        'btnActualizarArch.Enabled = False
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Activa
            Select Case e.Row.Cells(13).Text
                Case "11" 'activa
                    e.Row.Cells(7).Enabled = True 'verPDF
                    e.Row.Cells(9).Enabled = False 'eliminar
                    e.Row.Cells(11).Enabled = True 'solicitar bloqueo
                Case "12" 'proceso de activación
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(11).Enabled = False
                Case "13" 'bloqueda
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(11).Enabled = False
                Case "14" 'en proceso de bloqueo
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(11).Enabled = False
                Case "15" 'rechazada
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = True
                    e.Row.Cells(11).Enabled = False
                Case "22" 'proceso de activación
                    If txtAutorizado.Text.Trim <> "EN PROCESO" Then
                        e.Row.Cells(7).Enabled = True
                        e.Row.Cells(9).Enabled = True
                        e.Row.Cells(11).Enabled = False
                    Else
                        e.Row.Cells(7).Enabled = True
                        e.Row.Cells(9).Enabled = False
                        e.Row.Cells(11).Enabled = False
                    End If
                Case "25" 'proceso de activación
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(11).Enabled = False
                Case "27" 'proceso de activación
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(11).Enabled = False
            End Select

        End If
    End Sub

    Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case e.Row.Cells(11).Text
                Case "16" 'activa
                    e.Row.Cells(4).Enabled = True 'verPDF
                    e.Row.Cells(7).Enabled = False 'eliminar
                    e.Row.Cells(8).Enabled = True 'solicitar bloqueo
                Case "17" 'proceso de activación
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = False
                    e.Row.Cells(8).Enabled = False
                Case "18" 'bloqueda
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = False
                    e.Row.Cells(8).Enabled = False
                Case "19" 'en proceso de bloqueo
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = False
                    e.Row.Cells(8).Enabled = False
                Case "20" 'rechazada
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = True
                    e.Row.Cells(8).Enabled = False
                Case "23" 'proceso de activación
                    If txtAutorizado.Text.Trim <> "EN PROCESO" Then
                        e.Row.Cells(4).Enabled = True
                        e.Row.Cells(7).Enabled = True
                        e.Row.Cells(8).Enabled = False
                    Else
                        e.Row.Cells(4).Enabled = True
                        e.Row.Cells(7).Enabled = False
                        e.Row.Cells(8).Enabled = False
                    End If
                Case "24" 'proceso de activación
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = False
                    e.Row.Cells(8).Enabled = False
                Case "26" 'proceso de activación
                    e.Row.Cells(4).Enabled = True
                    e.Row.Cells(7).Enabled = False
                    e.Row.Cells(8).Enabled = False
            End Select

        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/frmAltaProveedor.aspx")
    End Sub

    Protected Sub txtAutorizado_TextChanged(sender As Object, e As EventArgs) Handles txtAutorizado.TextChanged
        If txtAutorizado.Text = "PENDIENTE" Or txtAutorizado.Text.Trim = "RECHAZADO" Then
            btnAutorizar.Enabled = False
        End If
    End Sub

    Protected Sub enviaCorreoCuentasBancarias(banco As String, moneda As String, cuenta As String, clabe As String, convenio As String, referencia As String, attachCta As String)
        Dim tableAdapterGenCorreos As New dsProduccionTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim mensaje As String = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de bloqueo de cuenta con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>" & txtRazonSocial.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>RFC</td>" &
                        "<td>" & txtRfc.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Banco</td>" &
                        "<td>" & banco & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Moneda</td>" &
                        "<td>" & moneda & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Cuenta</td>" &
                        "<td>" & cuenta & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>CLABE</td>" &
                        "<td>" & clabe & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Convenio</td>" &
                        "<td>" & convenio & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Referencia</td>" &
                        "<td>" & referencia & "</td>" &
                    "</tr>" &
                   "</table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de bloqueo de cuentas", mensaje, False, Date.Now.ToLongDateString, "")

        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", Session.Item("mailUsuarioS"), "Solicitud de bloqueo de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de bloqueo de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "atorres@lamoderna.com.mx", "Solicitud de bloqueo de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "gisvazquez@finagil.com.mx", "Solicitud de bloqueo de cuentas", mensaje, False, Date.Now.ToLongDateString, "")

        'Notificación de alta por tesorería
        If Session.Item("Usuario") = "atorres" Or Session.Item("Usuario") = "gisvazquez" Then
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "vcruz@finagil.com.mx", "Solicitud de bloqueo de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "epineda@lamoderna.com.mx", "Solicitud de bloqueo de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de bloqueo de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de bloqueo de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
        End If

    End Sub

    Protected Sub enviaCorreoDocumentos(documento As String)
        Dim tableAdapterGenCorreos As New dsProduccionTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim mensaje As String = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de bloqueo del siguiente documento con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>" & txtRazonSocial.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>RFC</td>" &
                        "<td>" & txtRfc.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Documento</td>" &
                        "<td>" & documento & "</td>" &
                    "</tr>" &
                   "</table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de bloqueo de documentos", mensaje, False, Date.Now.ToLongDateString, "")

        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", Session.Item("mailUsuarioS"), "Solicitud de bloqueo de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de bloqueo de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "atorres@lamoderna.com.mx", "Solicitud de bloqueo de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "gisvazquez@finagil.com.mx", "Solicitud de bloqueo de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        'Notificación de alta por tesorería
        If Session.Item("Usuario") = "atorres" Or Session.Item("Usuario") = "gisvazquez" Then
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "vcruz@finagil.com.mx", "Solicitud de bloqueo de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "epineda@finagil.com.mx", "Solicitud de bloqueo de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de bloqueo de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de bloqueo de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
        End If
    End Sub

    Protected Sub enviaCorreoCuentasBancariasActiv(banco As String, moneda As String, cuenta As String, clabe As String, convenio As String, referencia As String, attachCta As String)
        Dim tableAdapterGenCorreos As New dsProduccionTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim mensaje As String = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de activación de cuenta con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>" & txtRazonSocial.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>RFC</td>" &
                        "<td>" & txtRfc.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Banco</td>" &
                        "<td>" & banco & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Moneda</td>" &
                        "<td>" & moneda & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Cuenta</td>" &
                        "<td>" & cuenta & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>CLABE</td>" &
                        "<td>" & clabe & "</td>" &
                    "</tr>" &
                     "<tr>" &
                        "<td>Convenio:</td>" &
                        "<td>" & convenio & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Referencia:</td>" &
                        "<td>" & referencia & "</td>" &
                    "</tr>" &
                   "</table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de activación de cuenta", mensaje, False, Date.Now.ToLongDateString, "")

        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", Session.Item("mailUsuarioS"), "Solicitud de activación de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de activación de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "atorres@lamoderna.com.mx", "Solicitud de activación de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "gisvazquez@finagil.com.mx", "Solicitud de activación de cuentas", mensaje, False, Date.Now.ToLongDateString, "")
        If Session.Item("Usuario") = "atorres" Or Session.Item("Usuario") = "gisvazquez" Then
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "vcruz@finagil.com.mx", "Solicitud de activación de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "epineda@finagil.com.mx", "Solicitud de activación de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de activación de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de activación de cuentas (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "CXP\FilesProv\" & attachCta & ".pdf")
        End If
    End Sub

    Protected Sub enviaCorreoDocumentosActive(documento As String)
        Dim tableAdapterGenCorreos As New dsProduccionTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim mensaje As String = "<html><body><font size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" &
                    "<h1><font size=3 align" & Chr(34) & "center" & Chr(34) & ">" & "Estimado (a), le notificamos que se ha generado la solicitud de activación del siguiente documento con los siguientes datos: </font></h1>" &
                     "<table  align=" & Chr(34) & "center" & Chr(34) & " border=1 cellspacing=0 cellpadding=2>" &
                    "<tr>" &
                        "<td>Razón Social</td>" &
                        "<td>" & txtRazonSocial.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>RFC</td>" &
                        "<td>" & txtRfc.Text & "</td>" &
                    "</tr>" &
                    "<tr>" &
                        "<td>Documento</td>" &
                        "<td>" & documento & "</td>" &
                    "</tr>" &
                   "</table><HR width=20%>" &
                   "<tfoot><tr><font align=" & Chr(34) & "center" & Chr(34) & "size=3 face=" & Chr(34) & "Arial" & Chr(34) & ">" & "Solicitante: " & Session.Item("Nombre") & vbNewLine & "</font></tr></tfoot>" &
                     "</body></html>"
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de activación de documentos", mensaje, False, Date.Now.ToLongDateString, "")

        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", Session.Item("mailUsuarioS"), "Solicitud de activación de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de activación de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "atorres@lamoderna.com.mx", "Solicitud de activación de documentos", mensaje, False, Date.Now.ToLongDateString, "")
        tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "gisvazquez@finagil.com.mx", "Solicitud de activación de documentos", mensaje, False, Date.Now.ToLongDateString, "")

        If Session.Item("Usuario") = "atorres" Or Session.Item("Usuario") = "gisvazquez" Then
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "vcruz@finagil.com.mx", "Solicitud de activación de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "epineda@finagil.com.mx", "Solicitud de activación de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "lgarcia@finagil.com.mx", "Solicitud de activación de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
            tableAdapterGenCorreos.Insert("AltaProveedores@finagil.com.mx", "viapolo@finagil.com.mx", "Solicitud de activación de documentos (Tesorería)", mensaje, False, Date.Now.ToLongDateString, "")
        End If
    End Sub

    Protected Sub validaYCambiaEstatus(idProveedor As String, rfcP As String)
        Dim taCtas As New dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter
        Dim taDctos As New dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter
        Dim taProv As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Dim taDocn As New dsProduccionTableAdapters.CXP_DocumentacionProvTableAdapter
        Dim tipoPersona As String = ""

        If Session.Item("rfcEmpleado") = "NE" Or IsNothing(Session.Item("rfcEmpleado")) Then
            If taProv.ObtClientProv_ScalarQuery(txtRfc.Text.Trim) = False Then
                If rfcP.Length = 12 Then
                    tipoPersona = "M"
                ElseIf rfcP.Length = 13 Then
                    tipoPersona = "F"
                End If
            Else
                tipoPersona = "C"
            End If
        Else
            tipoPersona = "E"
        End If

        If taDctos.ObtNoDoctsObligXProv_ScalarQuery(idProveedor) = taDocn.NoDoctosOblig_ScalarQuery(tipoPersona) Then
            If IsNothing(taCtas.Contador_ScalarQuery(idProveedor)) And IsNothing(taDctos.Contador_ScalarQuery(idProveedor)) Then
                taProv.CambiaEstatus_UpdateQuery("AUTORIZADO", idProveedor)
                txtAutorizado.Text = "AUTORIZADO"
            End If
        End If

    End Sub

    Protected Sub chkClientProv_CheckedChanged(sender As Object, e As EventArgs) Handles chkClientProv.CheckedChanged
        Dim tableAdapterProveedores As New dsProduccionTableAdapters.CXP_ProveedoresTableAdapter
        Dim taClientes As New dsProduccionTableAdapters.ClientesTableAdapter
        Dim dtClientes As New dsProduccion.ClientesDataTable
        Dim rwClientes As dsProduccion.ClientesRow

        If chkClientProv.Checked = True Then
            'valida RFC
            If txtRfc.Text <> String.Empty Then
                If tableAdapterProveedores.ExisteRFC_ScalarQuery(txtRfc.Text.Trim) = "NE" Then
                    If Regex.IsMatch(txtRfc.Text.Trim, "^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$") = False Then
                        lblErrorGeneral.Text = "Estructura del RFC incorrecta."
                        ModalPopupExtender1.Show()
                        Exit Sub
                    Else
                        taClientes.ClientesAntiguos_FillBy(dtClientes, txtRfc.Text.Trim)
                        If dtClientes.Rows.Count > 0 Then
                            rwClientes = dtClientes.Rows(0)

                            txtRfc.Text = rwClientes.RFC
                            txtRazonSocial.Text = rwClientes.Descr
                            txtColonia.Text = rwClientes.Colonia
                            txtCalle.Text = rwClientes.Calle
                            txtLocalidad.Text = rwClientes.Colonia
                            txtDelegacion.Text = rwClientes.Delegacion
                            txtEstado.Text = rwClientes.Estado

                            ddlPais.SelectedValue = "MEX"

                            txtCp.Text = rwClientes.Copos
                            txtActivo.Text = "NO ACTIVO"
                            txtAutorizado.Text = "NO AUTORIZADO"

                            txtCurp.Text = rwClientes.CURP
                            txtMail.Text = rwClientes.EMail1

                            chkClientProv.Checked = True
                        Else
                            lblErrorGeneral.Text = "El RFC no existe como cliente en el sistema de Finagil"
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    End If
                Else
                    lblErrorGeneral.Text = "El RFC ya existe en el catálogo de proveedores..."
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Function valida_Proveedor()
        Dim estatus As String = "OK"
        Try
            lbl69.Text = "PROVEEDOR VÁLIDO EN LISTA EFOS"
            lbl69B.Text = "PROVEEDOR VÁLIDO EN LISTA EFOS"
            lbl69.ForeColor = Color.Green
            lbl69B.ForeColor = Color.Green

            Dim ta69 As New dsProduccionTableAdapters.CRED_Lista_Art69TableAdapter
            Dim ta69B As New dsProduccionTableAdapters.CRED_Lista_Art69BTableAdapter
            Dim dt69 As New dsProduccion.CRED_Lista_Art69DataTable
            Dim dt69B As New dsProduccion.CRED_Lista_Art69BDataTable

            ta69.ObtEst_FillBy(dt69, txtRfc.Text.Trim)

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

            ta69B.ObtEst_FillBy(dt69B, txtRfc.Text.Trim)

            For Each rows69B As dsProduccion.CRED_Lista_Art69BRow In dt69B
                lbl69B.ForeColor = Color.Red
                If rows69B.status_cont <> "Desvirtuado" Or rows69B.status_cont <> "" Then
                    lbl69B.Text = "NO PROCEDE EL PAGO A PROVEEDOR, SOLICITAR ACLARACION"
                    estatus = "ERR"
                End If
            Next
        Catch ex As Exception
            lblErrorGeneral.Text = ex.ToString
            ModalPopupExtender1.Show()

        End Try
        Return estatus
    End Function
End Class


