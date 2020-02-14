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
        background-color: #FFF700;
        border-width: 4px;
        border-style: outset;
        border-color: red;
        padding: 0px;
        width: 275px;
        font-family: Arial
    }
    .CajaDialogo div
    {
        margin: 7px;
        text-align: center;
    }
          .auto-style28 {
              width: 579px;
          }
          .auto-style29 {
              width: 551px;
          }
          .auto-style30 {
              text-align: center;
          }
    .auto-style33 {
        width: 70px;
    }
    .auto-style35 {
              width: 162px;
          }
    .auto-style36 {
        width: 123px;
    }
    .auto-style37 {
        width: 123px;
        text-align: center;
    }
    .auto-style40 {
        width: 90%;
        margin-left: 20px;
        margin-top: 20px;
    }
          .auto-style41 {
              width: 90px;
          }
          .auto-style42 {
              text-align: left;
          }
           .auto-style21a {
            overflow-y: auto;
            height:100%;
        }
          .auto-style43 {
              width: 184px;
          }
          .auto-style44 {
              width: 184px;
              text-align: center;
          }
          .auto-style45 {
              text-align: center;
          }
          .alinContainer{
              align-content:center;
              text-align:center;
              margin-left:20px;
          }
          .auto-style47 {
              width: 8px;
          }
          .auto-style48 {
              align-content: center;
              margin-left: 100px;
          }
          .auto-style49 {
              text-align: left;
              width: 517px;
          }
          .auto-style50 {
              text-align: center;
              width: 517px;
          }
          .auto-style51 {
              text-align: left;
              width: 579px;
          }
          .auto-style52 {
              width: 100%;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="auto-style21a" id="contenedorID" runat="server" >
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

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensajeError" TargetControlID="lblErrorGeneral" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="Button1" PopupControlID="pnlTotales" TargetControlID="lblTotales" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" OkControlID="Button1" PopupControlID="pnlEFOS" TargetControlID="lblEFOSDesc" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
     
     <link href="styFW.css" rel="stylesheet" type="text/css" />
    <p>
        <table class="auto-style40">
            <tr>
                <td id="id1" runat="server" class="auto-style42" style="font-family: Arial; font-size: medium; background-color: #FF6600; color: #FFFFFF; font-weight: bold;" colspan="2">
                    Proveedor:</td>
                <td id="id2" runat="server" class="auto-style42" style="font-family: Arial; font-size: medium; background-color: #FF6600; color: #FFFFFF; font-weight: bold;">
                        Concepto:</td>
            </tr>
            <tr>
                <td class="auto-style42" style="font-family: Arial; font-size: medium" colspan="2">
                    <asp:DropDownList ID="ddlMismoDeudor" runat="server" Height="20px" Width="400px" AutoPostBack="True">
                        <asp:ListItem>Elegir proveedor</asp:ListItem>
                        <asp:ListItem Selected="True">Mismo Deudor</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style42" style="font-family: Arial; font-size: medium">
                    <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="odsConceptos" DataTextField="nombre" DataValueField="idConcepto" Height="20px" Width="400px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style51" style="border-color: #FF6600; border-width: 2px; font-family: Arial; font-size: small; border-top-style: solid; border-bottom-style: solid;">
                    Buscar proveedor:<br />
                    <asp:TextBox ID="txtBuscarProveedor" runat="server" Width="182px" Enabled="False"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="    Buscar    " CssClass="Botones" Width="90px" Enabled="False" />
                </td>
                <td class="auto-style49" style="border-color: #FF6600; border-width: 2px; font-family: Arial; font-size: small; border-top-style: solid; border-bottom-style: solid;">
                    Seleccionar proveedor:<asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="odsProveedores" DataTextField="razonSocial" DataValueField="idProveedor" Height="20px" Width="330px" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="auto-style50" style="border-color: #FF6600; border-width: 2px; font-family: Arial; font-size: small; border-top-style: solid; border-bottom-style: solid;">
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 (Créditos fiscales)<br />
                    <asp:Label ID="lbl69" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    <br />
                    Artículo 69 B (Operaciones inexistentes)<br />
                    <asp:Label ID="lbl69B" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style42" style="font-family: Arial; font-size: small" colspan="2">
                    Cuenta Bancaria:<br />
                    <ajaxToolkit:ComboBox ID="cmbCuentasBancarias" runat="server" DataSourceID="odsCuentasBancarias" DataTextField="cuenta" DataValueField="idCuentas" MaxLength="0" style="display: inline;" Width="600px" Font-Size="Small" DropDownStyle="DropDownList" Height="20px" RenderMode="Block" Visible="False">
                    </ajaxToolkit:ComboBox>
                    <br />
                    <asp:ObjectDataSource ID="odsCuentasBancarias" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="CuentasBancarias_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="idProveedor" Type="Decimal" />
                            <asp:Parameter Name="idBanco" Type="Decimal" />
                            <asp:Parameter Name="cuenta" Type="String" />
                            <asp:Parameter Name="clabe" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="idProveedor" Type="Decimal" />
                            <asp:Parameter Name="idBanco" Type="Decimal" />
                            <asp:Parameter Name="cuenta" Type="String" />
                            <asp:Parameter Name="clabe" Type="String" />
                            <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    <br />
                </td>
                <td class="auto-style42" style="font-family: Arial; font-size: small">
                    Forma de Pago:<br />
                    <ajaxToolkit:ComboBox ID="cmbFormaPago" runat="server" DataSourceID="odsFormaPago" DataTextField="descripcion" DataValueField="idTipoDocumento" MaxLength="0" style="display: inline;" Width="200px">
                    </ajaxToolkit:ComboBox>
                    <asp:ObjectDataSource ID="odsFormaPago" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="DocumentosEgreso_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter" UpdateMethod="Update">
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
                    <br />
                    Sucursal (CC):<br />
                    <ajaxToolkit:ComboBox ID="cmbCentroDeCostos" runat="server" DataSourceID="odsCentroDeCostos" DataTextField="nombreSucursal" DataValueField="idSucursal" MaxLength="0" style="display: inline;" Width="200px">
                    </ajaxToolkit:ComboBox>
                    <br />
                    <asp:ObjectDataSource ID="odsCentroDeCostos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCC_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_SucursalesTableAdapter" UpdateMethod="Update">
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
                </td>
            </tr>
            <tr>
                <td id="id3" runat="server" class="auto-style42" style="font-family: Arial; font-size: medium; color: #FFFFFF; font-weight: bold; background-color: #FF6600;" colspan="2">
                    Deudor: </td>
                <td id="id4" runat="server" class="auto-style42" style="font-family: Arial; font-size: medium; color: #FFFFFF; font-weight: bold; background-color: #FF6600;">
                    Autoriza:</td>
            </tr>
            <tr>
                <td class="auto-style42" style="font-family: Arial; font-size: medium" colspan="2">
                    <div class="auto-style42">
                    <asp:TextBox ID="txtDeudor" runat="server" Height="20px" ReadOnly="True" Width="402px"></asp:TextBox>
                        <br />
                    </div>
                </td>
                <td class="auto-style42" style="font-family: Arial; font-size: medium">
                    <div class="auto-style42">
                        <asp:DropDownList ID="ddlAutorizo" runat="server" Height="20px" Width="400px" DataSourceID="odsAutorizantes" DataTextField="Nombre" DataValueField="id_correo">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Valentin Cruz Barrios</asp:ListItem>
                            <asp:ListItem>Elisander Pineda Rojas</asp:ListItem>
                            <asp:ListItem>Gabriel Bello Hernández</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsAutorizantes" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter" UpdateMethod="Update">
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
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29">
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
                </td>
                <td>
                    <asp:ObjectDataSource ID="odsMonedas" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_c_MonedaTableAdapter" UpdateMethod="Update">
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
                    <asp:ObjectDataSource ID="odsProveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td id="id0" runat="server" class="auto-style30" colspan="3" style="font-weight: bold; color: #FFFFFF; background-color: #FF6600; font-family: Arial;">Datos de la solicitud:</td>
            </tr>
            <asp:ObjectDataSource ID="odsClientes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
        </table>
    </p>
<p class="auto-style30">
        <table style="margin-left:20px; margin-top:10px;" class="auto-style52" >
            <tr >
                
                <td id="id6" runat="server" class="auto-style36" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold; color: #FFFFFF;">Fecha de Solicitud</td>
                <td id="id7" runat="server" class="auto-style33" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold; color: #FFFFFF;">Moneda</td>
                <td id="id8" runat="server" class="auto-style41" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold; color: #FFFFFF;">Importe Solicitado</td>
                <td id="id9" runat="server" class="auto-style35" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold; color: #FFFFFF;" colspan="2">Descripción del Pago</td>
                <td id="id10" runat="server" class="auto-style44" style="font-family: Arial; font-weight: bold; color: #FFFFFF; background-color: #FF6600; font-size: small;" colspan="2">Fecha de Pago</td>
                <td id="id11" runat="server" class="auto-style44" style="font-family: Arial; font-weight: bold; color: #FFFFFF; background-color: #FF6600; font-size: small;">Contrato</td>
            </tr>
            <tr>
                
                <td id="id12" runat="server" class="auto-style37" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold;">
                    <asp:Label ID="lblFechaSolicitud" runat="server" ForeColor="White" Text="Label"></asp:Label>
                </td>
                <td id="id13" runat="server" class="auto-style33" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold;">
                    <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="odsMonedas" DataTextField="c_NombreMoneda" DataValueField="c_Moneda">
                    </asp:DropDownList>
                </td>
                <td id="id14" runat="server" class="auto-style41" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold;">
                    <asp:TextBox ID="txtMontoSolicitado" runat="server" Width="152px"></asp:TextBox>
                </td>
                <td id="id15" runat="server" class="auto-style35" style="background-color: #FF6600; font-family: Arial; font-size: small; font-weight: bold;" colspan="2">
                    <asp:TextBox ID="txtDescripcionPago" runat="server" Width="535px"></asp:TextBox>
                </td>
                <td id="id16" runat="server" class="auto-style43" colspan="2">
                    <asp:TextBox ID="txtFechaPago" runat="server" Width="94px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexFechaPago" runat="server" TargetControlID="txtFechaPago" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
               </td>
                <td id="id17" runat="server" class="auto-style44">
                    <asp:CheckBox ID="chkContrato" runat="server" Font-Names="Arial" Font-Size="Small" Text="¿Si?" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                
                <td id="id18" runat="server" class="auto-style45" colspan="8" style="font-family: Arial; font-size: small; background-color: #FF6600;">
                    Cliente:
                    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" DataSourceID="odsClientes" DataTextField="Descr" DataValueField="Cliente" Enabled="False">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp; Contrato:
                    <asp:DropDownList ID="ddlContratos" runat="server" DataSourceID="odsContratos" DataTextField="Anexo" DataValueField="Anexo" Enabled="False">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp; Importe carta neteo:
                    <asp:TextBox ID="txtImporteCartaNeteto" runat="server" Width="93px" Enabled="False">0</asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                    </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style30" colspan="4">
                    &nbsp;</td>
                <td class="auto-style30">
                    &nbsp;</td>
                <td class="auto-style30">
                    &nbsp;</td>
                <td class="auto-style30" colspan="2">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style30" colspan="8" style="border-color: #FF6600; background-color: #FF6600" id="identificadroUnico0" runat="server">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style30" colspan="8" id="contenedor2ID1" runat="server">
                    
                    <asp:ObjectDataSource ID="odsProveedores0" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsCFDI" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" />
                            <asp:SessionParameter Name="meses" SessionField="mesesFacturas" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:TextBox ID="txtBuscar" runat="server" Width="174px" Visible="False"></asp:TextBox>
&nbsp;
                    <asp:Button ID="btnBuscar0" runat="server" CssClass="Botones" Text="Buscar" Visible="False" />
                &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlProveedor0" runat="server" DataSourceID="odsProveedores0" DataTextField="razonSocial" DataValueField="idProveedor" Height="20px" Width="400px" Visible="False">
                    </asp:DropDownList>
&nbsp;
                    <asp:Button ID="btnAsignar" runat="server" CssClass="Botones" Text="Seleccionar" Width="107px" Visible="False" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="scrollcss">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataKeyNames="idXmlCfdi" DataSourceID="odsCFDI" Height="16px" Width="1226px" ForeColor="#333333" HorizontalAlign="Center" Visible="False" PageSize="5">
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
            <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                        <br />
                    </div>
                     <asp:Button ID="btnAgregar" runat="server" CssClass="Botones" Text="Agregar" Visible="False" />
                    <br />
                    <asp:ObjectDataSource ID="odsContratos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.AnexosTableAdapter"></asp:ObjectDataSource>
                    <div class="auto-style48" >
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="1035px" ActiveTabIndex="0" CssClass="auto-style48" Visible="False" >
                         <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Deducibles" BackColor="#FFFFFF" >
                            <ContentTemplate>
               
                                <asp:GridView ID="GridView2" runat="server" AllowSorting="True" Height="16px" Width="975px" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True" PageSize="2" Font-Size="Small">
                            <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
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
                                         <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                             CommandName="Eliminar" 
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                            </ContentTemplate>
                         </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="No Deducibles" BackColor="#FFFFFF" >
                            <ContentTemplate>
                                                                     
                                <table style="align-content: center ; width: 90%;">
                                    <tr>
                                        <td class="auto-style47">&nbsp;</td>
                                        <caption>
                                            Descripción y /o concepto del gasto:<asp:TextBox ID="txtConceptoND" runat="server" Width="240px"></asp:TextBox>
                                            Importe:
                                            <asp:TextBox ID="txtImporteND" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnNoDeducible" runat="server" CssClass="Botones" Text="Agregar" />
                                            <tr>
                                                <td class="auto-style47">&nbsp;</td>
                                                <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" Font-Size="Small" ForeColor="#333333" Height="16px" HorizontalAlign="Center" ShowFooter="True" Width="419px">
                                                    <Columns>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción y/o Concepto del Gasto">
                                                        <ItemStyle HorizontalAlign="Left" Width="300px" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" Wrap="True" />
                                                        </asp:BoundField>
                                                         <asp:TemplateField>
                                    <ItemTemplate>
                                         <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                             CommandName="Eliminar" 
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#FF6600" />
                                                    <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
                                                </asp:GridView>
                                            </tr>
                                        </caption>
                                               </tr>
                                </table>     

                            </ContentTemplate>
                         </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    </div>
                    
                    <br />
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRevisar" runat="server" CssClass="Botones" Text="Revisar" /><br />
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style30" colspan="4" id="contenedor2IDa" runat="server">
                    <asp:TextBox ID="txtRevision" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="Arial" ForeColor="Red" Height="102px" ReadOnly="True" TextMode="MultiLine" Visible="False" Width="650px"></asp:TextBox>
                </td>
                <td class="auto-style42" colspan="4" id="contenedor2IDb" runat="server">
                                <asp:Label ID="lblAdjuntos" runat="server" Text="Archivos adjuntos (solo PDF):" Visible="False"></asp:Label>
                                <br />
                                <asp:FileUpload ID="fup1" runat="server" Visible="False" accept=".pdf" multiple="multiple" AllowMultiple="true" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div class="auto-style30"><br />
            <asp:FileUpload ID="fupCarteNeteo" runat="server" Visible="False" /><br /><br />
            <asp:Button ID="btnSolicitar" runat="server" CssClass="Botones" Text="Solicitar" Visible="False" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" /><br />
            <asp:Label ID="lblError" runat="server" Font-Names="Arial Black" Font-Size="X-Large" ForeColor="#FF6401" Text="Error" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
        </div>
    </p>
         </div>
</asp:Content>
