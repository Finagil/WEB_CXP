Public Class Finagil
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblUsuario.Text = Session.Item("Nombre") & " (" & Session.Item("Usuario") & ")"
            lblSucursal.Text = Session.Item("Sucursal")
            lblDepartamento.Text = Session.Item("Departamento")
            lblLeyenda.Text = Session.Item("Leyenda")



            If Session.Item("Empresa") = "24" Then
                identificadorUnico1.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico1.Attributes.Add("class", "labelsA")
                identificadorUnico2.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico2.Attributes.Add("class", "labelsA")
                identificadorUnico3.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico3.Attributes.Add("class", "labelsA")
                identificadorUnico4.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico4.Attributes.Add("class", "headerA")
                identificadorUnico6.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico6.Attributes.Add("class", "headerA")
                identificadorUnico8.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico8.Attributes.Add("class", "headerA")
                identificadorUnico9.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico9.Attributes.Add("class", "headerA")
                identificadorUnico10.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico10.Attributes.Add("class", "headerA")
                identificadorUnico11.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico11.Attributes.Add("class", "headerA")
                If Session.Item("Usuario") <> "viapolo" And Session.Item("Usuario") <> "lgarciac" And Session.Item("Usuario") <> "vcruz" Then
                    identificadorUnico12.Attributes.Add("style", "pointer-events:none;opacity:0.6;background-color: #4BA5FF;")
                    identificadorUnico12.Attributes.Add("class", "disabledA")
                Else
                    identificadorUnico12.Attributes.Add("style", "background-color: #4BA5FF;")
                    identificadorUnico12.Attributes.Add("class", "headerA")
                End If

                If Session.Item("Usuario") <> "lmercado" And Session.Item("Usuario") <> "maria.montes" Then
                    identificadorUnico19.Attributes.Add("style", "pointer-events:none;opacity:0.6;background-color: #4BA5FF;")
                    identificadorUnico19.Attributes.Add("class", "disabledA")
                Else
                    identificadorUnico19.Attributes.Add("style", "background-color: #4BA5FF;")
                    identificadorUnico19.Attributes.Add("class", "headerA")
                End If

                identificadorUnico13.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico13.Attributes.Add("class", "headerA")
                identificadorUnico14.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico14.Attributes.Add("class", "headerA")
                identificadorUnico15.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico15.Attributes.Add("class", "headerA")
                identificadorUnico16.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico16.Attributes.Add("class", "labelsA")
                identificadorUnico17.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico17.Attributes.Add("class", "labelsA")
                identificadorUnico18.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico18.Attributes.Add("class", "labelsA")

                identificadorUnico20.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico20.Attributes.Add("class", "labelsA")
                identificadorUnico21.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico21.Attributes.Add("class", "labelsA")
                identificadorUnico22.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico22.Attributes.Add("class", "labelsA")
                identificadorUnico23.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico23.Attributes.Add("class", "labelsA")
                identificadorUnico24.Attributes.Add("style", "background-color: #4BA5FF;")
                identificadorUnico24.Attributes.Add("class", "labelsA")
                imgURL.Src = "imagenes/logoArfin.png"
            Else
                identificadorUnico1.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico1.Attributes.Add("class", "labelsA")
                identificadorUnico2.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico2.Attributes.Add("class", "labelsF")
                identificadorUnico3.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico3.Attributes.Add("class", "labelsF")
                identificadorUnico4.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico4.Attributes.Add("class", "headerA")
                identificadorUnico6.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico6.Attributes.Add("class", "labelsF")
                identificadorUnico8.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico8.Attributes.Add("class", "headerA")
                identificadorUnico9.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico9.Attributes.Add("class", "headerA")
                identificadorUnico10.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico10.Attributes.Add("class", "headerA")
                identificadorUnico11.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico11.Attributes.Add("class", "headerA")

                If Session.Item("Usuario") <> "viapolo" And Session.Item("Usuario") <> "lgarciac" And Session.Item("Usuario") <> "vcruz" Then
                    identificadorUnico12.Attributes.Add("style", "pointer-events:none;opacity:0.6;background-color: #ff6600;")
                    identificadorUnico12.Attributes.Add("class", "disabledF")
                Else
                    identificadorUnico12.Attributes.Add("style", "background-color: #ff6600;")
                    identificadorUnico12.Attributes.Add("class", "headerF")
                End If

                If Session.Item("Usuario") <> "lmercado" And Session.Item("Usuario") <> "maria.montes" Then
                    identificadorUnico19.Attributes.Add("style", "pointer-events:none;opacity:0.6;background-color: #ff6600;")
                    identificadorUnico19.Attributes.Add("class", "disabledF")
                Else
                    identificadorUnico19.Attributes.Add("style", "background-color: #ff6600;")
                    identificadorUnico19.Attributes.Add("class", "headerF")
                End If

                identificadorUnico13.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico13.Attributes.Add("class", "headerA")
                identificadorUnico14.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico14.Attributes.Add("class", "headerA")
                identificadorUnico15.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico15.Attributes.Add("class", "headerA")
                identificadorUnico16.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico16.Attributes.Add("class", "labelsA")
                identificadorUnico17.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico17.Attributes.Add("class", "labelsA")
                identificadorUnico18.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico18.Attributes.Add("class", "labelsA")

                identificadorUnico20.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico20.Attributes.Add("class", "labelsA")
                identificadorUnico21.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico21.Attributes.Add("class", "labelsA")
                identificadorUnico22.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico22.Attributes.Add("class", "labelsA")
                identificadorUnico23.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico23.Attributes.Add("class", "labelsA")
                identificadorUnico24.Attributes.Add("style", "background-color: #ff6600;")
                identificadorUnico24.Attributes.Add("class", "labelsA")
                imgURL.Src = "imagenes/logo-shop-primary.png"
            End If
        End If
    End Sub



End Class