
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmConComprobante.aspx.vb" Inherits="cxP.frmConComprobante" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<% @ Register assembly = "AjaxControlToolkit" namespace = "AjaxControlToolkit"  tagprefix = "asp"  %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://static.codepen.io/assets/common/stopExecutionOnTimeout-157cd5b220a5c80d4ff8e0e70ac069bffd87a61252088146915e8726e5d9f147.js"></script>
<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.0/jquery.min.js'></script>
<script id="rendered-js">
// Select all links with hashes
    $('a[href*="#"]')

// Remove links that don't actually link to anything
.not('[href="#"]').
not('[href="#0"]').
click(function (event) {
  // On-page links
    if (<a href="frmConComprobante.aspx">frmConComprobante.aspx</a>
  location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') &&

  location.hostname == this.hostname)
  {
    // Figure out element to scroll to
    var target = $(this.hash);
    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
    // Does a scroll target exist?
    if (target.length) {
      // Only prevent default if animation is actually gonna happen
      event.preventDefault();
      $('html, body').animate({
        scrollTop: target.offset().top },
      1000, function () {
        // Callback after animation
        // Must change focus!
        var $target = $(target);
        $target.focus();
        if ($target.is(":focus")) {// Checking if the target was focused
          return false;
        } else {
          $target.attr('tabindex', '-1'); // Adding tabindex for elements not focusable
          $target.focus(); // Set focus again
        };
      });
    }
  }
});
//# sourceURL=pen.js
</script>
<script src="https://static.codepen.io/assets/editor/iframe/iframeRefreshCSS-e03f509ba0a671350b4b363ff105b2eb009850f34a2b4deaadaa63ed5d970b37.js"></script>

   
    <style type="text/css">
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
        .auto-style11 {
            height: 219px;
            text-align: center;
            font-family: Arial;
            font-size:small;
            
        }
        .auto-style13 {
            margin-left: 2px;
            margin-top: 14px;
        }
        .auto-style45 {
            width: 1268px;
        }
        .auto-style50 {
            width: 315px;
        }
        .auto-style51 {
            width: 316px;
        }
        .auto-style52 {
            text-align: center;
        }
        .auto-style54 {
            width: 1187px;
        }
        
        .auto-style63 {
            width: 394px;
        }
        .auto-style73 {
            height: 30px;
            font-family: Arial;
            font-size: small;
            width: 297px;
            text-align: center;
        }
        .auto-style75 {
            height: 30px;
            text-align: center;
            font-family: Arial;
            font-size: small;
            width: 761px;
        }
        .auto-style76 {
            height: 30px;
            font-family: Arial;
            font-size: small;
            text-align: center;
        }
        .auto-style77 {
            width: 315px;
            height: 28px;
        }
        .auto-style78 {
            width: 316px;
            height: 28px;
        }
        .auto-style82 {
            height: 30px;
            font-family: Arial;
            font-size: small;
            width: 298px;
            text-align: center;
        }
        .auto-style91 {
            color: #333333;
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
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="Botones"/>
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnAceptar2" PopupControlID="pnlMensajeError2" TargetControlID="lblErrorGeneral2" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
    <link href="styFW.css" rel="stylesheet" type="text/css" />
  
    <div style="overflow-y:auto; height:90%; margin-bottom:5px;">
        <table runat="server" id="tablaBuscar" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width: 95%; margin: 0 auto; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0;">
            <tr>
                <td>
                    Proveedor:
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                    <asp:Button ID="Button2" runat="server" Text="PruebURL" PostBackUrl="~/aut2TwoPrvCtas.aspx?idCuentas=66&archivo1=a01a11a3-5fef-4f69-981b-ee5535198723" Visible="False" />
                </td>
                <td  >

                    <span style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #FFFFFF; background-color: #F58220;color: rgb(0, 0, 0); font-family: Arial; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                    <asp:ObjectDataSource ID="proveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idProveedor" Type="Decimal" />
                            <asp:Parameter Name="Original_rfc" Type="String" />
                            <asp:Parameter Name="Original_nit" Type="String" />
                            <asp:Parameter Name="Original_curp" Type="String" />
                            <asp:Parameter Name="Original_razonSocial" Type="String" />
                            <asp:Parameter Name="Original_idSucursal" Type="String" />
                            <asp:Parameter Name="Original_relacionado" Type="Boolean" />
                            <asp:Parameter Name="Original_cuentaContablePagar" Type="String" />
                            <asp:Parameter Name="Original_montoMaxTransaccion" Type="Decimal" />
                            <asp:Parameter Name="Original_extranjero" Type="Boolean" />
                            <asp:Parameter Name="Original_nacionalidad" Type="String" />
                            <asp:Parameter Name="Original_fechaRegistro" Type="DateTime" />
                            <asp:Parameter Name="Original_mail" Type="String" />
                            <asp:Parameter Name="Original_empresa" Type="String" />
                            <asp:Parameter Name="Original_banco" Type="String" />
                            <asp:Parameter Name="Original_cuentaBancaria" Type="String" />
                            <asp:Parameter Name="Original_Clabe" Type="String" />
                            <asp:Parameter Name="Original_activo" Type="Boolean" />
                            <asp:Parameter Name="Original_autorizado" Type="Boolean" />
                            <asp:Parameter Name="Original_calle" Type="String" />
                            <asp:Parameter Name="Original_colonia" Type="String" />
                            <asp:Parameter Name="Original_localidad" Type="String" />
                            <asp:Parameter Name="Original_delegacion" Type="String" />
                            <asp:Parameter Name="Original_estado" Type="String" />
                            <asp:Parameter Name="Original_pais" Type="String" />
                            <asp:Parameter Name="Original_cp" Type="String" />
                            <asp:Parameter Name="Original_activoS" Type="String" />
                            <asp:Parameter Name="Original_autorizadoS" Type="String" />
                            <asp:Parameter Name="Original_usuarioSolicita" Type="String" />
                            <asp:Parameter Name="Original_motivoRechazo" Type="String" />
                            <asp:Parameter Name="Original_autorizaP1" Type="String" />
                            <asp:Parameter Name="Original_autorizaP2" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="rfc" Type="String" />
                            <asp:Parameter Name="nit" Type="String" />
                            <asp:Parameter Name="curp" Type="String" />
                            <asp:Parameter Name="razonSocial" Type="String" />
                            <asp:Parameter Name="idSucursal" Type="String" />
                            <asp:Parameter Name="relacionado" Type="Boolean" />
                            <asp:Parameter Name="cuentaContablePagar" Type="String" />
                            <asp:Parameter Name="montoMaxTransaccion" Type="Decimal" />
                            <asp:Parameter Name="extranjero" Type="Boolean" />
                            <asp:Parameter Name="nacionalidad" Type="String" />
                            <asp:Parameter Name="fechaRegistro" Type="DateTime" />
                            <asp:Parameter Name="mail" Type="String" />
                            <asp:Parameter Name="empresa" Type="String" />
                            <asp:Parameter Name="banco" Type="String" />
                            <asp:Parameter Name="cuentaBancaria" Type="String" />
                            <asp:Parameter Name="Clabe" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                            <asp:Parameter Name="autorizado" Type="Boolean" />
                            <asp:Parameter Name="calle" Type="String" />
                            <asp:Parameter Name="colonia" Type="String" />
                            <asp:Parameter Name="localidad" Type="String" />
                            <asp:Parameter Name="delegacion" Type="String" />
                            <asp:Parameter Name="estado" Type="String" />
                            <asp:Parameter Name="pais" Type="String" />
                            <asp:Parameter Name="cp" Type="String" />
                            <asp:Parameter Name="activoS" Type="String" />
                            <asp:Parameter Name="autorizadoS" Type="String" />
                            <asp:Parameter Name="usuarioSolicita" Type="String" />
                            <asp:Parameter Name="motivoRechazo" Type="String" />
                            <asp:Parameter Name="autorizaP1" Type="String" />
                            <asp:Parameter Name="autorizaP2" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="" Name="idSucursal" SessionField="idSucrusal" Type="String" />
                            <asp:SessionParameter DefaultValue="" Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="rfc" Type="String" />
                            <asp:Parameter Name="nit" Type="String" />
                            <asp:Parameter Name="curp" Type="String" />
                            <asp:Parameter Name="razonSocial" Type="String" />
                            <asp:Parameter Name="idSucursal" Type="String" />
                            <asp:Parameter Name="relacionado" Type="Boolean" />
                            <asp:Parameter Name="cuentaContablePagar" Type="String" />
                            <asp:Parameter Name="montoMaxTransaccion" Type="Decimal" />
                            <asp:Parameter Name="extranjero" Type="Boolean" />
                            <asp:Parameter Name="nacionalidad" Type="String" />
                            <asp:Parameter Name="fechaRegistro" Type="DateTime" />
                            <asp:Parameter Name="mail" Type="String" />
                            <asp:Parameter Name="empresa" Type="String" />
                            <asp:Parameter Name="banco" Type="String" />
                            <asp:Parameter Name="cuentaBancaria" Type="String" />
                            <asp:Parameter Name="Clabe" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                            <asp:Parameter Name="autorizado" Type="Boolean" />
                            <asp:Parameter Name="calle" Type="String" />
                            <asp:Parameter Name="colonia" Type="String" />
                            <asp:Parameter Name="localidad" Type="String" />
                            <asp:Parameter Name="delegacion" Type="String" />
                            <asp:Parameter Name="estado" Type="String" />
                            <asp:Parameter Name="pais" Type="String" />
                            <asp:Parameter Name="cp" Type="String" />
                            <asp:Parameter Name="activoS" Type="String" />
                            <asp:Parameter Name="autorizadoS" Type="String" />
                            <asp:Parameter Name="usuarioSolicita" Type="String" />
                            <asp:Parameter Name="motivoRechazo" Type="String" />
                            <asp:Parameter Name="autorizaP1" Type="String" />
                            <asp:Parameter Name="autorizaP2" Type="String" />
                            <asp:Parameter Name="Original_idProveedor" Type="Decimal" />
                            <asp:Parameter Name="Original_rfc" Type="String" />
                            <asp:Parameter Name="Original_nit" Type="String" />
                            <asp:Parameter Name="Original_curp" Type="String" />
                            <asp:Parameter Name="Original_razonSocial" Type="String" />
                            <asp:Parameter Name="Original_idSucursal" Type="String" />
                            <asp:Parameter Name="Original_relacionado" Type="Boolean" />
                            <asp:Parameter Name="Original_cuentaContablePagar" Type="String" />
                            <asp:Parameter Name="Original_montoMaxTransaccion" Type="Decimal" />
                            <asp:Parameter Name="Original_extranjero" Type="Boolean" />
                            <asp:Parameter Name="Original_nacionalidad" Type="String" />
                            <asp:Parameter Name="Original_fechaRegistro" Type="DateTime" />
                            <asp:Parameter Name="Original_mail" Type="String" />
                            <asp:Parameter Name="Original_empresa" Type="String" />
                            <asp:Parameter Name="Original_banco" Type="String" />
                            <asp:Parameter Name="Original_cuentaBancaria" Type="String" />
                            <asp:Parameter Name="Original_Clabe" Type="String" />
                            <asp:Parameter Name="Original_activo" Type="Boolean" />
                            <asp:Parameter Name="Original_autorizado" Type="Boolean" />
                            <asp:Parameter Name="Original_calle" Type="String" />
                            <asp:Parameter Name="Original_colonia" Type="String" />
                            <asp:Parameter Name="Original_localidad" Type="String" />
                            <asp:Parameter Name="Original_delegacion" Type="String" />
                            <asp:Parameter Name="Original_estado" Type="String" />
                            <asp:Parameter Name="Original_pais" Type="String" />
                            <asp:Parameter Name="Original_cp" Type="String" />
                            <asp:Parameter Name="Original_activoS" Type="String" />
                            <asp:Parameter Name="Original_autorizadoS" Type="String" />
                            <asp:Parameter Name="Original_usuarioSolicita" Type="String" />
                            <asp:Parameter Name="Original_motivoRechazo" Type="String" />
                            <asp:Parameter Name="Original_autorizaP1" Type="String" />
                            <asp:Parameter Name="Original_autorizaP2" Type="String" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    </span>
                </td>
            </tr>
            <tr>
                <td >
                        <asp:TextBox ID="txtBuscar" runat="server" Width="300px"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Botones" />
                    </td>
                <td  >

                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal;  font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                    <asp:DropDownList ID="ddlProveedores" runat="server" DataSourceID="proveedores" DataTextField="razonSocial" DataValueField="idProveedor" Width="300px">
                    </asp:DropDownList>
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                        <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="Botones" />
                    </span>
                    </span>
                    </td>
            </tr>
        </table>
        <div visible="false" id="divEfos" runat="server">
        <table runat="server" id="estatusEfos" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width:95%; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style45">
            <tr>
                <td colspan="2">

                    Estatus EFOS:<asp:ObjectDataSource ID="comprobantesFiscales" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" DefaultValue="" />
                            <asp:SessionParameter Name="meses" SessionField="mesesFacturas" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                        </td>
                <td>

                    Contratos:<asp:ObjectDataSource ID="odsClientesCtos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>

                </td>
                <td>

                    <asp:CheckBox ID="chkContrato" runat="server" Text="¿Si?" AutoPostBack="True" TextAlign="Left" Visible="False" />
                    <asp:ObjectDataSource ID="odsAnexosActivos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.AnexosTableAdapter"></asp:ObjectDataSource>

                </td>
            </tr>
            <tr>
                <td class="auto-style50">

                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal;  font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 (Créditos fiscales)</span></td>
                <td class="auto-style50">

                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 B (Operaciones inexistentes)</span></td>
                <td class="auto-style51" style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: x-small; color: #000000; font-weight: normal;">

                    Cliente:</td>
                <td class="auto-style51" style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: x-small; color: #000000; font-weight: normal;">

                    Número de contrato:</td>
            </tr>
            <tr>
                <td class="auto-style50">

                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal;  font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                    <asp:Label ID="lbl69" runat="server" ForeColor="Green" Font-Bold="True" Visible="False"></asp:Label>
                    </span>

                </td>
                <td class="auto-style50">

                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">
                    <asp:Label ID="lbl69B" runat="server" ForeColor="Green" Font-Bold="True" Visible="False"></asp:Label>
                    </span>

                </td>
                <td class="auto-style51">

                    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" DataSourceID="odsClientesCtos" DataTextField="Descr" DataValueField="Cliente" Enabled="False" Width="300px" Visible="False">
                    </asp:DropDownList>

                </td>
                <td class="auto-style51">

                    <asp:DropDownList ID="ddlContratos" runat="server" Enabled="False" DataSourceID="odsAnexosActivos" DataTextField="Anexo" DataValueField="Anexo" Width="300px" Visible="False">
                    </asp:DropDownList>

                </td>
            </tr>
        </table>
        </div>
        <section id="facturas">
            <div id="gridFacturas" style="overflow-x:auto; overflow-y:auto; height:200px; width:95%; margin-left:30px;" visible="false" >

                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataKeyNames="idXmlCfdi" DataSourceID="comprobantesFiscales" Height="14px" Width="100%" ForeColor="#333333" HorizontalAlign="Center">
                            <SelectedRowStyle BackColor="#E0E0E0" />
                            <Columns>
                                <asp:TemplateField HeaderText="-" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                         <asp:CheckBox ID="chk" runat="server" 
                                             CommandName="Select1"
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="fechaEmision" HeaderText="Fecha de Emisión" SortExpression="fechaEmision" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Small"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="fPago" HeaderText="Forma de Pago" SortExpression="fPago" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Small"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="mPago" HeaderText="Metodo de Pago" SortExpression="mPago" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Small"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="moneda" HeaderText="Moneda" SortExpression="moneda" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Small"></ItemStyle>
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="folio" HeaderText="Folio" SortExpression="folio" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="serie" HeaderText="Serie" SortExpression="serie" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Wrap="False" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="uuid" HeaderText="UUID" SortExpression="uuid" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Wrap="False" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="subTotal" HeaderText="SubTotal" SortExpression="subTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="montoImpuesto" HeaderText="Impuestos Trasladados" SortExpression="montoImpuesto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
                                <HeaderStyle HorizontalAlign="Center" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Right" Font-Size="Smaller" />
                                </asp:BoundField>
                                <asp:BoundField DataField="montoImpuestoR" HeaderText="Impuestos Retenidos" SortExpression="montoImpuestoR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                               
                                <HeaderStyle HorizontalAlign="Center" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Right" Font-Size="Smaller" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Parcialidad" DataField="Parcialidad" SortExpression="parcialidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                
                                
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="totalA" HeaderText="Importe de Parcialidades" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
<HeaderStyle HorizontalAlign="Center" Font-Size="X-Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="Smaller"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Sugerencia de Pago $" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                          <asp:TextBox ID="txtMontoAPagar" runat="server" DataField="total" Width="80" Align="Right"  Text-Align="Right" Text='<%#  DataBinder.Eval(Container, "DataItem.totalB") %>'></asp:TextBox>
                                    </ItemTemplate>

                                    

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField headerText="Sugerencia de Pago %" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                              <asp:TextBox ID="txtPorcentaje" Width="80" Align="Right" runat="server"></asp:TextBox>
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Font-Size="Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="10px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción del Pago" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtObservaciones" runat="server"  Width="160"></asp:TextBox>
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>    
                               
                                 <asp:TemplateField HeaderText="Concepto" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlConceptos" runat="server" AutoPostBack="True" DataSourceID="odsConceptos" DataTextField="nombre" DataValueField="idConcepto" Height="20px" Width="280px">
                        </asp:DropDownList>
                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate> 
                                        <asp:HyperLink ID="lnkView" Text="pdf" NavigateUrl='<%# Eval("uuid", "~/Procesados/" & Session.Item("rutaCFDI") & "/{0}.pdf") %>' runat="server" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F58220" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#F58220" />
                        </asp:GridView>
            </div>
        </section>
        <div visible="false" id="divOtros" runat="server">
           <table runat="server" id="otrosDatos" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width:95%; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style45">
            <tr>
                <td class="auto-style50">Autorizante:<asp:ObjectDataSource ID="odsAutorizantes" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter" UpdateMethod="Update">
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
                        
                        </td>
                <td class="auto-style50">Sucursal:<asp:ObjectDataSource ID="odsCentroCostos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCC_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_SucursalesTableAdapter" UpdateMethod="Update">
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
                <td class="auto-style51">Forma de Pago:<asp:ObjectDataSource ID="odsFormaPago" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="DocumentosEgreso_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter" UpdateMethod="Update">
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
                    </td>
                <td class="auto-style51">Fecha de pago:<asp:ObjectDataSource ID="odsConceptos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_ConceptosTableAdapter" UpdateMethod="Update">
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
            </tr>
            <tr>
                <td class="auto-style77">
                    <asp:DropDownList ID="ddlAutorizo" runat="server" Width="250px" DataSourceID="odsAutorizantes" DataTextField="Nombre" DataValueField="id_correo" Visible="False">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem>Valentin Cruz Barrios</asp:ListItem>
                            <asp:ListItem>Elisander Pineda Rojas</asp:ListItem>
                            <asp:ListItem>Gabriel Bello Hernández</asp:ListItem>
                        </asp:DropDownList>
                        
                        </td>
                <td class="auto-style77">
                    <asp:DropDownList ID="cmbCentroDeCostos" runat="server"  DataSourceID="odsCentroCostos" DataTextField="nombreSucursal" DataValueField="idSucursal" Font-Size="Small" MaxLength="0" style="display: inline;" Width="250px" RenderMode="Block" Visible="False" DropDownStyle="DropDownList">
                    </asp:DropDownList>
                </td>
                <td class="auto-style78">
                    <asp:DropDownList ID="cmbFormaPago" runat="server" DataSourceID="odsFormaPago" DataTextField="descripcion" DataValueField="idTipoDocumento" Font-Size="Small" MaxLength="0" style="display: inline;" Width="250px" RenderMode="Block" Visible="False" AutoCompleteMode="Suggest" DropDownStyle="DropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="auto-style78">
                            <asp:TextBox ID="txtFechaPago" runat="server" Visible="False" Width="250px" ></asp:TextBox> <ajaxToolkit:CalendarExtender ID="cexFechaPago" runat="server" TargetControlID="txtFechaPago" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                        </td>
            </tr>
        </table>
            </div>
        <div visible="false" id="divCtaBancaria" runat="server">
            <table visible="false" runat="server" id="tablaReferenciaBancaria" style="border-color:lightgray;width:95%; padding:5px; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style75">
        <tr>
            <td style="width:12.5%" >Banco:
                <asp:FormView ID="FormView5" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Banco" runat="server" Text='<%# Bind("Banco") %>' />
                    </ItemTemplate>
                </asp:FormView>
            </td>
            <td style="width:10%">Moneda:</td>
            <td style="width:12.5%">Cuenta:

             </td>
            <td style="width:12.5%">CLABE:
                <asp:FormView ID="FormView4" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="CuentaCLABELabel" runat="server" Text='<%# Bind("CuentaCLABE") %>' />
                    </ItemTemplate>
                </asp:FormView>
             </td>
            <td style="width:12.5%">Concepto:</td>
            <td style="width:12.5%">Convenio:</td>
            <td style="width:12.5%">Referencia:

             </td>
            <td style="width:15%">Adjunto:<asp:ObjectDataSource ID="odsBancos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_BancosTableAdapter" UpdateMethod="Update">
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
            <td style="width:12.5%">
                <asp:DropDownList ID="ddlBancos" runat="server" DataSourceID="odsBancos" DataTextField="nombreCorto" DataValueField="idBancos" Width="90%">
                </asp:DropDownList>

             </td>
            <td style="width:10%">
                <asp:DropDownList ID="ddlMonedas" runat="server" DataSourceID="odsMonedas" DataTextField="c_Moneda" DataValueField="c_Moneda">
                </asp:DropDownList>
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
            </td>
            <td style="width:12.5%">
                <asp:TextBox ID="txtCuenta" runat="server" Width="90%" Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="width:12.5%">
                <asp:TextBox ID="txtClabe" runat="server" Width="90%" Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="width:12.5%">
                <asp:TextBox ID="txtConcepto" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:12.5%">
                <asp:TextBox ID="txtConvenio" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:12.5%">
                <asp:TextBox ID="txtReferencia" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td style="width:15%">
              
                <ajaxToolkit:AsyncFileUpload ID="afuAdjuntoCta" runat="server" Width="100%" />
              
            </td>
        </tr>
      </table>

          <table runat="server" id="ctasBancarias" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width:95%; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style54">
            <tr>
                <td style="width:40%">

                    Cuenta bancaria:<asp:ObjectDataSource ID="odsCuentasBancarias" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="CuentasBancarias_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter" InsertMethod="Insert" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="idProveedor" Type="Decimal" />
                            <asp:Parameter Name="idBanco" Type="Decimal" />
                            <asp:Parameter Name="cuenta" Type="String" />
                            <asp:Parameter Name="clabe" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="moneda" Type="String" />
                            <asp:Parameter Name="archivo1" Type="String" />
                            <asp:Parameter Name="vigente" Type="Boolean" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="idProveedor" Type="Decimal" />
                            <asp:Parameter Name="idBanco" Type="Decimal" />
                            <asp:Parameter Name="cuenta" Type="String" />
                            <asp:Parameter Name="clabe" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="moneda" Type="String" />
                            <asp:Parameter Name="archivo1" Type="String" />
                            <asp:Parameter Name="vigente" Type="Boolean" />
                            <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>

                </td>
                <td style="width:25%">

                    &nbsp;</td>
                <td style="width:25%">

                    <asp:Label ID="lblImporte" runat="server" Text="Importe carta neteo:" Visible="False"></asp:Label>

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
                
                </td>
            </tr>
            <tr>
                <td class="auto-style62">
                    <asp:DropDownList ID="cmbCuentasBancarias" runat="server" DataSourceID="odsCuentasBancarias" DataTextField="descrip" DataValueField="idCuentas" MaxLength="0" style="display: block;" Width="95%" Font-Size="Small" DropDownStyle="DropDownList" Height="20px" RenderMode="Block" Visible="False" AutoCompleteMode="Suggest">
                    </asp:DropDownList>
                </td>
                <td class="auto-style62">

                    &nbsp;</td>
                <td class="auto-style63">

                    <asp:TextBox ID="txtImporteCartaNeteto" runat="server" Visible="False">0</asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style62">
                     
                    &nbsp;</td>
                <td class="auto-style62">

                    &nbsp;</td>
                <td class="auto-style63">

                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style52" colspan="3">

                    <asp:Button ID="btnVistaPrevia" runat="server" Text="Revisar" CssClass="Botones" Visible="False" Width="100px"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancelarBusqueda" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" Width="100px" />

                </td>
            </tr>
            <tr>
                <td class="auto-style52" colspan="3">

                    <asp:Label ID="lblError" runat="server" Font-Names="Arial Black" Font-Size="X-Large" ForeColor="#FF6401" Text="Error" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblError2" runat="server" Font-Names="Arial Black" Font-Size="Small" ForeColor="#006600" Text="Error" Visible="False" Width="900px"></asp:Label>

                </td>
            </tr>
        </table>
            </div>
        <div visible="false" id="divRevision" runat="server">
        <table runat="server" id="revision" style="position:fixed;top:30%;left:15%; border-radius:5px; border-style: groove; border-width: 5px; border-color: navy; width:70%; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background: linear-gradient(to bottom, rgba(245,130,32,1) 0%, rgba(245,130,32,1) 50%, rgba(241,188,142,1) 71%, rgba(237,237,237,1) 89%, rgba(246,246,246,1) 100%);">
            <tr>
                <td colspan="3">
                    Revisión previa y documentos adjuntos:
                    </td>
            </tr>            
            <tr>
                <td class="auto-style75" colspan="3">
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="auto-style13" Height="16px" Width="100%" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True" >
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
                                <asp:BoundField DataField="uuid" ItemStyle-Wrap="true" HeaderText="UUID">
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Monto a Pagar Sugerido">
                                <HeaderStyle Wrap="True" />
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="concepto" HeaderText="Concepto">
                                <ItemStyle BackColor="Yellow" Font-Bold="True" ForeColor="Red" Wrap="True" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
            <EditRowStyle BackColor="#F58220" />
                        </asp:GridView>
                        <a href="frmConComprobante.aspx"><span class="auto-style91">frmConComprobante.aspx</span></a></td>
            </tr>            
            <tr>
                <td class="auto-style76" colspan="3">
                    &nbsp;</td>
            </tr>            
            <tr>
                <td style="width:33%">

                    Tipo de cambio:</td>
                <td style="width:33%">

                    &nbsp;</td>
                <td style="width:36%">
                    &nbsp;</td>
            </tr>            
            <tr>
                <td style="width:33%">

                    <asp:TextBox ID="txtTipoDeCambio" Width="80%" runat="server"></asp:TextBox>

                </td>
                <td style="width:33%">

                    &nbsp;</td>
                <td style="width:36%">
                    &nbsp;</td>
            </tr>            
            <tr>
                <td class="auto-style73">
                    <asp:Label ID="lblAdjuntos" runat="server" Text="Archivos adjuntos (solo PDF):" Visible="False"></asp:Label>
                </td>
                <td class="auto-style73">
                    <asp:Label ID="lblCarteNeteo" runat="server" Text="Adjuntar Carta Neteo:" Visible="False"></asp:Label>
                </td>
                <td class="auto-style82">

                    <asp:Label ID="lblDescrCartaNeteo" runat="server" Text="Descripción Carta Neteo:" Visible="False"></asp:Label>

                </td>
            </tr>            
            <tr>
                <td style="width:33%">
                                <asp:FileUpload ID="fup1" runat="server" Visible="False" accept=".pdf" multiple="multiple" AllowMultiple="true" Width="95%"/>
                </td>
                <td style="width:33%">
                    <asp:FileUpload ID="fupCartaNeteo" runat="server"  Width="95%" Visible="False" />
                </td>
                <td style="width:36%">

                    <asp:TextBox ID="txtDescCartaNeteo" runat="server" Visible="False" Width="95%"></asp:TextBox>

                </td>
            </tr>            
            <tr>
                <td class="auto-style76" colspan="3">
                   <cc1:BotonEnviar CssClass="Botones" ID="btnProcesar" runat="server" Visible="false"
                        Text="Procesar" TextoEnviando="Procesando..." />
                    &nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
                </td>
            </tr>            
        </table>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
