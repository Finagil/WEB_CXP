<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmSinReembolso.aspx.vb" Inherits="cxP.frmSinReembolso" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<% @ Register assembly = "AjaxControlToolkit" namespace = "AjaxControlToolkit"  tagprefix = "asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="js/calendar-en.min.js" type="text/javascript"></script>
    <link href="css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script Language="JavaScript">if(history.forward(1)){history.replace(history.forward(1));}</script>
     <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFechaPago.ClientID %>").dynDateTime({
                showsTime: false,.
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            }); <a href="frmSinComprobante.aspx">frmSinComprobante.aspx</a>
         });
     </script>
      <style type="text/css">
          .scrollcss{
              overflow-x:auto;
              overflow-y:auto;
              height:150px;
            width:1350px;
          }
          .CajaDialogo
    {
        background-color: orangered;
        border-width: 4px;
        border-style: outset;
        border-color: darkblue;
        padding: 0px;
        width: 275px;
        font-family: Arial;
        font-weight:bold;
        border-radius:8px;
    }
    .CajaDialogo div
    {
        margin: 5px;
        text-align: center;
    }
          .auto-style30 {
              text-align: center;
          }
           .alinContainer{
              align-content:center;
              text-align:center;
              margin-left:20px;
          }
          
          .auto-style54 {
              width: 315px;
          }
          .auto-style55 {
              width: 315px;
              text-align: center;
          }
          .auto-style59 {
              width: 420px;
          }
          .auto-style60 {
              width: 420px;
              height: 43px;
          }
          .auto-style61 {
              height: 43px;
          }
          .auto-style62 {
              width: 452px;
          }
          .auto-style63 {
              width: 694px;
          }
          .auto-style66 {
              width: 346px;
          }
          .auto-style67 {
              width: 346px;
              text-align: center;
          }
          .auto-style68 {
              width: 70%;
              height: 28px;
          }
          .auto-style69 {
              width: 30%;
              height: 28px;
          }
          </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlMensajeError" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
        <tr>
            <td align="lefth">
                <asp:Label ID="lblEncabezadoMensaje" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblErrorGeneral" runat="server" Text="El total de las facturas debe ser igual o menor al saldo de la solicitud." />
    </div>
    <div>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
    </div>
</asp:Panel>

    <asp:Panel ID="pnlEFOS" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
        <tr>
            <td align="lefth">
                <asp:Label ID="lblEFOSEnc" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblEFOSDesc" runat="server" Text="El total de las facturas debe ser igual o menor al saldo de la solicitud." />
    </div>
    <div>
        <asp:Button ID="Button3" runat="server" Text="Aceptar" />
    </div>
</asp:Panel>

    <asp:Panel ID="pnlTotales" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" height="100px" width="250px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
        <tr>
            <td align="lefth">
                <asp:Label ID="lblEncabezado" runat="server" Text=" Datos previos: " BackColor="#FFF700" Font-Bold="true"/>
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblTotales" runat="server" Text="" />
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="Aceptar" />
    </div>
</asp:Panel>

    <asp:Panel ID="pnlMensajeError2" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
        <tr>
            <td align="center" style="border-radius:3px,">
                <asp:Label ID="Label1" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true" />
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblErrorGeneral2" runat="server" Text="El total de las facturas debe ser igual o menor al saldo de la solicitud." />
    </div>
    <div>
            <asp:LinkButton runat="server" PostBackUrl="~/frmAltaProveedor.aspx">Solicitar alta de proveedor</asp:LinkButton>
    </div>
    <div>
         <asp:Button ID="btnAceptar2" runat="server" Text="Aceptar" CssClass="Botones"/>
    </div>
</asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensajeError" TargetControlID="lblErrorGeneral" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="Button1" PopupControlID="pnlTotales" TargetControlID="lblTotales" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" OkControlID="Button1" PopupControlID="pnlEFOS" TargetControlID="lblEFOSDesc" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" OkControlID="btnAceptar2" PopupControlID="pnlMensajeError2" TargetControlID="lblErrorGeneral2" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
     
     <link href="styFW.css" rel="stylesheet" type="text/css" />
        <div style="overflow-y:auto; overflow-x:auto; height:80%; margin-bottom:5px;">
        <table runat="server" id="tablaSelecciona" style="width:95%; border-color:lightgray; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style59">
        <tr>
            <td>
                Pagar a:<asp:Button ID="Button4" runat="server" Text="Button" Visible="False" />
            </td>
            <td>
                Concepto:</td>
            <td>
                Deudor:</td>
        </tr>
        <tr>
            <td>
                    <asp:DropDownList ID="ddlMismoDeudor" runat="server" Height="20px" Width="400px" AutoPostBack="True">
                        <asp:ListItem>Elegir proveedor</asp:ListItem>
                        <asp:ListItem Selected="True">Mismo Deudor</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                    <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="odsConceptos" DataTextField="nombre" DataValueField="idConcepto" Height="20px" Width="400px" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td>
                    <asp:TextBox ID="txtDeudor" runat="server" Height="20px" ReadOnly="True" Width="402px"></asp:TextBox>
            </td>
        </tr>
        </table>

        <table runat="server"  id="tablaBuscar" style="border-color:lightgray;width:95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style53">
            <tr><td colspan="2">Proveedor: 
                    <asp:ObjectDataSource ID="odsDatosCuenta" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CuentasDomiTableAdapter">
                        <InsertParameters>
                            <asp:Parameter Name="Anexo" Type="String" />
                            <asp:Parameter Name="CuentaCLABE" Type="String" />
                            <asp:Parameter Name="Banco" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlContratos" DefaultValue="0" Name="Anexo" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                </asp:ObjectDataSource>
                
                        </td><td class="auto-style66" colspan="2">Estatus EFOS:<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
                </td></tr>
            <tr><td class="auto-style54">
                    &nbsp;<asp:TextBox ID="txtBuscarProveedor" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="    Buscar    " CssClass="Botones" Width="90px" Enabled="False" />
                </td><td class="auto-style62">
                    <asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="odsProveedores" DataTextField="razonSocial" DataValueField="idProveedor" Width="250px" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Button ID="btnSeleccionarProv" runat="server" Text="Seleccionar" CssClass="Botones" Width="90px" Enabled="False" />
                </td><td class="auto-style55">
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 (Créditos fiscales)<br />
                    <asp:Label ID="lbl69" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </span>
                    </td><td class="auto-style55">
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 B (Operaciones inexistentes)<br />
                    <asp:Label ID="lbl69B" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </span>
                    </td></tr>
        </table>

        <table visible="false" id="tablaAutorizante" runat="server" style="border-color:lightgray;width:95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style53">
            <tr><td>Autorizante:<asp:ObjectDataSource ID="odsAutorizantes" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter" UpdateMethod="Update">
                            <DeleteParameters>
                                <asp:Parameter Name="Original_id_correo" Type="Decimal" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Correo" Type="String" />
                                <asp:Parameter Name="Fase" Type="String" />
                                <asp:Parameter Name="Descripcion" Type="String" />
                                <asp:Parameter Name="Nombre" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Correo" Type="String" />
                                <asp:Parameter Name="Fase" Type="String" />
                                <asp:Parameter Name="Descripcion" Type="String" />
                                <asp:Parameter Name="Nombre" Type="String" />
                                <asp:Parameter Name="Original_id_correo" Type="Decimal" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                        </td><td>Sucursal (CC):<asp:ObjectDataSource ID="odsCentroDeCostos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCC_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_SucursalesTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idSucursal" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="nombreSucursal" Type="String" />
                            <asp:Parameter Name="id_Sucursal" Type="String" />
                            <asp:Parameter Name="idSuc" Type="String" />
                            <asp:Parameter Name="idEmpresa" Type="Decimal" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="nombreSucursal" Type="String" />
                            <asp:Parameter Name="id_Sucursal" Type="String" />
                            <asp:Parameter Name="idSuc" Type="String" />
                            <asp:Parameter Name="idEmpresa" Type="Decimal" />
                            <asp:Parameter Name="Original_idSucursal" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </td><td>Forma de pago:<asp:ObjectDataSource ID="odsFormaPago" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="DocumentosEgreso_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idTipoDocumento" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="claveSat" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="tipo" Type="String" />
                            <asp:Parameter Name="idEmpres" Type="Decimal" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="idEmpresa" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="claveSat" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="tipo" Type="String" />
                            <asp:Parameter Name="idEmpres" Type="Decimal" />
                            <asp:Parameter Name="Original_idTipoDocumento" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    </td><td>Fecha de pago:</td></tr>
            <tr><td>
                        <asp:DropDownList ID="ddlAutorizo" runat="server" Width="300px" DataSourceID="odsAutorizantes" DataTextField="Nombre" DataValueField="id_correo">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Valentin Cruz Barrios</asp:ListItem>
                            <asp:ListItem>Elisander Pineda Rojas</asp:ListItem>
                            <asp:ListItem>Gabriel Bello Hernández</asp:ListItem>
                        </asp:DropDownList>
                        </td><td>
                    <asp:DropDownList ID="cmbCentroDeCostos" runat="server" DataSourceID="odsCentroDeCostos" DataTextField="nombreSucursal" DataValueField="idSucursal" MaxLength="0" style="display: inline;" Width="300px">
                    </asp:DropDownList>
                    </td><td>
                    <asp:DropDownList ID="cmbFormaPago" runat="server" DataSourceID="odsFormaPago" DataTextField="descripcion" DataValueField="idTipoDocumento" MaxLength="0" style="display: inline;" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                    </td><td>
                    <asp:TextBox ID="txtFechaPago" runat="server" Width="100px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexFechaPago" runat="server" TargetControlID="txtFechaPago" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                </td></tr>
            </table>

            <table visible="false" runat="server" id="tablaReferenciaBancaria" style="border-color:lightgray;width:95%; padding:5px; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style75">
        <tr>
            <td style="width:14%">Banco:
                <asp:FormView ID="FormView5" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Banco" runat="server" Text='<%# Bind("Banco") %>' />
                    </ItemTemplate>
                </asp:FormView>
            </td>
            <td style="width:14%">Cuenta:

             </td>
            <td style="width:14%">CLABE:
                <asp:FormView ID="FormView4" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="CuentaCLABELabel" runat="server" Text='<%# Bind("CuentaCLABE") %>' />
                    </ItemTemplate>
                </asp:FormView>
             </td>
            <td style="width:14%">Concepto:</td>
            <td style="width:14%">Convenio:</td>
            <td style="width:14%">Referencia:

             </td>
            <td style="width:21%">Adjunto:<asp:ObjectDataSource ID="odsBancos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_BancosTableAdapter" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_idBancos" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="claveBancos" Type="String" />
                        <asp:Parameter Name="nombreCorto" Type="String" />
                        <asp:Parameter Name="razonSocial" Type="String" />
                        <asp:Parameter Name="rfc" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="claveBancos" Type="String" />
                        <asp:Parameter Name="nombreCorto" Type="String" />
                        <asp:Parameter Name="razonSocial" Type="String" />
                        <asp:Parameter Name="rfc" Type="String" />
                        <asp:Parameter Name="Original_idBancos" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>

             </td>
        </tr>
        <tr>
            <td style="width:14%">
                <asp:DropDownList ID="ddlBancos" runat="server" DataSourceID="odsBancos" DataTextField="nombreCorto" DataValueField="idBancos" Width="90%">
                </asp:DropDownList>

             </td>
            <td style="width:14%">
                <asp:TextBox ID="txtCuenta" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:14%">
                <asp:TextBox ID="txtClabe" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:14%">
                <asp:TextBox ID="txtConcepto" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:14%">
                <asp:TextBox ID="txtConvenio" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:14%">
                <asp:TextBox ID="txtReferencia" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:35%">
              
                <ajaxToolkit:AsyncFileUpload ID="afuAdjuntoCta" runat="server" Width="100%" />
              
            </td>
        </tr>
      </table>

        <table visible="false" id="tablaContratos" runat="server" style="border-color:lightgray;width:95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style53">
            <tr><td class="auto-style60">Cuenta bancaria:<asp:ObjectDataSource ID="odsCuentasBancarias" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CuentasBancarias_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter">
                    </asp:ObjectDataSource>
                    </td><td colspan="2" class="auto-style61">
                    <asp:CheckBox ID="chkContrato" runat="server" Font-Names="Arial" Font-Size="Small" Text="¿Si?" AutoPostBack="True" Visible="False" />
                </td></tr>
            <tr><td class="auto-style59">
                    <asp:DropDownList ID="cmbCuentasBancarias" runat="server" DataSourceID="odsCuentasBancarias" DataTextField="descrip" DataValueField="idCuentas" MaxLength="0" style="display: inline;" Width="600px" Font-Size="Small" DropDownStyle="DropDownList" Height="20px" RenderMode="Block">
                    </asp:DropDownList>
                    </td><td class="auto-style59">
                    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" DataSourceID="odsClientes" DataTextField="Descr" DataValueField="Cliente" Enabled="False" Visible="False">
                    </asp:DropDownList>
                </td><td class="auto-style59">
                    <asp:DropDownList ID="ddlContratos" runat="server" DataSourceID="odsContratos" DataTextField="Anexo" DataValueField="Anexo" Enabled="False" Visible="False">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtImporteCartaNeteto" runat="server" Width="93px" Enabled="False" Visible="False">0</asp:TextBox>
                    </td></tr>
        </table>

        <table visible="false" id="tablaDatos" runat="server" style="border-color:lightgray;width:95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style53">
        <tr><td class="auto-style54">Fecha de solicitud:<asp:ObjectDataSource ID="odsClientes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
            </td><td class="auto-style54">Moneda:<asp:ObjectDataSource ID="odsMonedas" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_c_MonedaTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_c_Moneda" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="c_Moneda" Type="String" />
                            <asp:Parameter Name="c_NombreMoneda" Type="String" />
                            <asp:Parameter Name="c_Decimales" Type="String" />
                            <asp:Parameter Name="c_Simbolo" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="c_NombreMoneda" Type="String" />
                            <asp:Parameter Name="c_Decimales" Type="String" />
                            <asp:Parameter Name="c_Simbolo" Type="String" />
                            <asp:Parameter Name="Original_c_Moneda" Type="String" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    </td><td class="auto-style54">Importe solicitado:<asp:ObjectDataSource ID="odsContratos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.AnexosTableAdapter"></asp:ObjectDataSource>
                    </td><td class="auto-style54">Descripción del pago:<asp:ObjectDataSource ID="odsProveedores0" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsConceptos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_ConceptosTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idConcepto" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="nombre" Type="String" />
                            <asp:Parameter Name="cuentaEgreso" Type="String" />
                            <asp:Parameter Name="impuesto" Type="String" />
                            <asp:Parameter Name="tipoProducto" Type="String" />
                            <asp:Parameter Name="cuentaProv" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="idEmpresa" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="nombre" Type="String" />
                            <asp:Parameter Name="cuentaEgreso" Type="String" />
                            <asp:Parameter Name="impuesto" Type="String" />
                            <asp:Parameter Name="tipoProducto" Type="String" />
                            <asp:Parameter Name="cuentaProv" Type="String" />
                            <asp:Parameter Name="Original_idConcepto" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsProveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td></tr>
        <tr><td class="auto-style54">
                    <asp:Label ID="lblFechaSolicitud" runat="server" ForeColor="Navy" Text="Label"></asp:Label>
                </td><td class="auto-style54">
                    <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="odsMonedas" DataTextField="c_NombreMoneda" DataValueField="c_Moneda">
                    </asp:DropDownList>
                </td><td class="auto-style54">
                    <asp:TextBox ID="txtMontoSolicitado" runat="server" Width="152px"></asp:TextBox>
                </td><td class="auto-style54">
                    <asp:TextBox ID="txtDescripcionPago" runat="server" Width="535px"></asp:TextBox>
                </td></tr>
        </table>

        <table runat="server" visible="false" id="tablaCFDI" style="border-color:lightgray;width:95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style53">
            <tr>
                <td>

                    <asp:TextBox ID="txtBuscar" runat="server" Width="174px" Visible="False"></asp:TextBox>
                    <asp:Button ID="btnBuscar0" runat="server" CssClass="Botones" Text="Buscar" Visible="False" />
                    
                    <asp:ObjectDataSource ID="odsCFDI" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" />
                            <asp:SessionParameter Name="meses" SessionField="mesesFacturas" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                </td>
                <td>

                    <asp:DropDownList ID="ddlProveedor0" runat="server" DataSourceID="odsProveedores0" DataTextField="razonSocial" DataValueField="idProveedor" Height="20px" Width="400px" Visible="False">
                    </asp:DropDownList>
&nbsp;
                    <asp:Button ID="btnAsignar" runat="server" CssClass="Botones" Text="Seleccionar" Width="107px" Visible="False" PostBackUrl="#gridFacturas"/>

                </td>
            </tr>
            </table>
        <p>
        &nbsp;</p>
        <div id="gridFacturas" style="overflow-x:auto; overflow-y:auto; height:120px; width:95%; margin-left:auto;margin-right:auto;" visible="false" >

                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataKeyNames="idXmlCfdi" DataSourceID="odsCFDI" Height="16px" Width="100%" ForeColor="#333333" HorizontalAlign="Center" Visible="False" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderText="-" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                         <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="fechaEmision" HeaderText="Fecha de Emisión" SortExpression="fechaEmision" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="fPago" HeaderText="Forma de Pago" SortExpression="fPago" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="mPago" HeaderText="Metodo de Pago" SortExpression="mPago" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="moneda" HeaderText="Moneda" SortExpression="moneda" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="folio" HeaderText="Folio" SortExpression="folio" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="serie" HeaderText="Serie" SortExpression="serie" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="uuid" HeaderText="UUID" SortExpression="uuid" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="subTotal" HeaderText="SubTotal" SortExpression="subTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="montoImpuesto" HeaderText="Impuestos Trasladados" SortExpression="montoImpuesto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                                <HeaderStyle HorizontalAlign="Center" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="montoImpuestoR" HeaderText="Impuestos Retenidos" SortExpression="montoImpuestoR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                               
                                <HeaderStyle HorizontalAlign="Center" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Parcialidad" DataField="Parcialidad" SortExpression="parcialidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                
                                
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="totalA" HeaderText="Importe de Parcialidades" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Sugerencia de Pago" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                          <asp:TextBox ID="txtMontoAPagar" runat="server" DataField="total" Width="100" Align="Right"  Text-Align="Right" Text='<%#  DataBinder.Eval(Container, "DataItem.totalB") %>'></asp:TextBox>
                                    </ItemTemplate>

                                    

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción y/o Concepto del Gasto">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConceptoFactura" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="X-Small" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
 
                                        <asp:HyperLink ID="lnkView" Text="pdf" NavigateUrl='<%# Eval("uuid", "~/Procesados/" & Session.Item("rutaCFDI") & "/{0}.pdf") %>' runat="server" Target="_blank" />
                                    
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

                                </asp:TemplateField>    
                               
                               
                               
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F58220" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#F58220" />
                        </asp:GridView>
                    </div>
            <div style="align-items:center;" class="auto-style30">
                <table>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>

            </div>
            <div style="align-items:center;" class="auto-style30">
                  <asp:Button ID="btnAgregar" runat="server" CssClass="Botones" Text="Agregar" Visible="False" PostBackUrl="#gridFacturas"/>
            </div>
        <table id="tablaAgregarCFDI" runat="server" style="position:relative; margin-top:20px; width:60%; border-radius:5px; border-style: groove; border-width: 3px;font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; ">
            <tr><td class="auto-style30">

                   

                </td></tr>
            <tr><td>

                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" CssClass="auto-style48" Visible="False" >
                         <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Deducibles" BackColor="#FFFFFF" >
                            <ContentTemplate>
               
                                <asp:GridView ID="GridView2" runat="server" AllowSorting="True" Height="16px" Width="975px" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True" PageSize="2" Font-Size="Small">
                            <FooterStyle BackColor="#F58220" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#F58220" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="serie" HeaderText="Serie">
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="folio" HeaderText="Folio">
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="uuid" HeaderText="UUID">
                                <ItemStyle HorizontalAlign="Right" Wrap="True" Width="350px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="concepto" HeaderText="Descripción" />
                                <asp:BoundField DataField="total1" DataFormatString="{0:c}" HeaderText="Total" />
                                <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Monto a Pagar Sugerido">
                                <HeaderStyle Wrap="True" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                         <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="BotonesGrid"
                                             CommandName="Eliminar" PostBackUrl="#gridFacturas"
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            <EditRowStyle BackColor="#F58220" />
                        </asp:GridView>
                            </ContentTemplate>
                         </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="No Deducibles" BackColor="#FFFFFF" >
                            <ContentTemplate>
                                                                     
                                <table style="align-content: center; margin-left:auto;margin-right:auto;" class="auto-style63">
                                    <tr>
                                       <td class="auto-style66">
                                           Descripción y /o concepto del gasto:
                                       </td>
                                        <td class="auto-style66" colspan="2">Importe:</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style66">
                                            <asp:TextBox ID="txtConceptoND" runat="server" Width="240px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style66">
                                            <asp:TextBox ID="txtImporteND" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="auto-style67">
                                            <asp:Button ID="btnNoDeducible" runat="server" CssClass="Botones" PostBackUrl="#gridFacturas" Text="Agregar" />
                                        </td>
                                    </tr>
                                    <tr>
                                       <td class="auto-style30" colspan="3">
                                            &nbsp;</td>
                                    </tr>
                                    <caption>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" Font-Size="Small" ForeColor="#333333" Height="16px" HorizontalAlign="Center" ShowFooter="True" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción y/o Concepto del Gasto">
                                                        <ItemStyle HorizontalAlign="Left" Width="400px" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                        <ItemStyle HorizontalAlign="Right" Width="150px" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEliminar1" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Eliminar" Text="Eliminar" CssClass="BotonesGrid" PostBackUrl="#gridFacturas"/>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#F58220" />
                                                    <FooterStyle BackColor="#F58220" Font-Bold="True" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F58220" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </caption>
                                </table>     

                            </ContentTemplate>
                         </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>

                </td></tr>
            <tr><td class="auto-style30">

                    <cc1:BotonEnviar ID="btnRevisar" runat="server" CssClass="Botones" Text="Revisar" />

                &nbsp;

                    <cc1:BotonEnviar ID="btnCancelarRev" runat="server" CssClass="Botones" Text="Cancelar" />

                </td></tr>
            </table>

            <table id="tablaTotales" runat="server" visible="false" style="width:120px; position:fixed;left:85%;right:auto; top:70%;border-color:lightgray; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 10px; color: darkred; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;">
                <tr>
                    <td class="auto-style68" >
                        Total Deducibles:
                    </td>
                     <td style="text-align:right;" class="auto-style69" >
                        <asp:Label ID="lblDeducibles" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:70%;" >
                        Total no Deducibles:
                    </td>
                    <td style="width:30%; text-align:right;" >
                        <asp:Label ID="lblNDeducibles" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
            </table>

        <table id="divRevisar" runat="server" visible="false" style="width:60%; position:fixed;top:30%;left:20%; padding:5px; border-radius:5px; border-style: groove; border-width: 5px; border-color:navy; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: initial;background: linear-gradient(to bottom, rgba(245,130,32,1) 0%, rgba(245,130,32,1) 50%, rgba(241,188,142,1) 71%, rgba(237,237,237,1) 89%, rgba(246,246,246,1) 100%);" class="auto-style59">
            <tr><td colspan="2" class="auto-style30">
                    <asp:TextBox ID="txtRevision" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="Arial" ForeColor="Red" Height="102px" ReadOnly="True" TextMode="MultiLine" Visible="False" Width="650px"></asp:TextBox>
                </td></tr>
            <tr><td>
                                <asp:Label ID="lblAdjuntos" runat="server" Text="Archivos adjuntos (solo PDF):" Visible="False"></asp:Label>
                                </td><td>
            <asp:Label ID="lblError" runat="server" Font-Names="Arial Black" Font-Size="X-Large" ForeColor="#FF6401" Text="Error" Visible="False"></asp:Label>
                </td></tr>
            <tr><td>
                                <asp:FileUpload ID="fup1" runat="server" Visible="False" accept=".pdf" multiple="multiple" AllowMultiple="true" ValidateRequestMode="Enabled" ViewStateMode="Enabled" />
                </td><td>
            <asp:FileUpload ID="fupCarteNeteo" runat="server" Visible="False" /></td></tr>
            <tr><td colspan="2" class="auto-style30">
            <cc1:BotonEnviar ID="btnSolicitar" runat="server" CssClass="Botones" Text="Solicitar" Visible="False" TextoEnviando="Enviando..." />&nbsp;
            <cc1:BotonEnviar ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" TextoEnviando="Cancelando..." /></td></tr>
        </table>
         </div>
</asp:Content>

