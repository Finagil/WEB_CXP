<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmSinComprobante.aspx.vb" Inherits="cxP.frmSinComprobante" %>
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
                showsTime: false,
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
          .auto-style42 {
             height: 18px;
            font-family: Arial;
            font-size: medium;
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
          .auto-style55 {
              width: 268435552px;
          }
          .auto-style59 {
              width: 1268px;
          }
          .auto-style62 {
              width: 630px;
          }
          .auto-style68 {
              width: 461px;
          }
          .auto-style69 {
              width: 315px;
          }
          .auto-style70 {
              width: 315px;
              text-align: center;
          }
          .auto-style71 {
              width: 386px;
          }
          .auto-style72 {
              width: 210px;
          }
          .auto-style74 {
              width: 209px;
          }
          .auto-style75 {
              width: 1187px;
              position: relative;
          }
          .auto-style80 {
              width: 294px;
          }
          .auto-style81 {
              width: 295px;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        
    <div style="overflow-y:auto; height:90%; margin-bottom:5px;" id="contenedorID" runat="server">
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
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="Botones" />
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
        <asp:Button ID="Button3" runat="server" Text="Aceptar" CssClass="Botones" />
    </div>
    </asp:Panel>

    <asp:Panel ID="pnlTotales" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" height="100" width="250px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
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
        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Botones" />
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

       
        <table runat="server" id="tablaSelecciona" style="border-color:lightgray;width: 95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style59">
            <tr><td class="auto-style62">Pagar a:
     
                    <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />

                </td><td class="auto-style62">Concepto:<asp:ObjectDataSource ID="odsConceptos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_ConceptosTableAdapter" UpdateMethod="Update">
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
                </td><td class="auto-style62">Deudor:</td></tr>
            <tr><td class="auto-style62">
                    <asp:DropDownList ID="ddlMismoDeudor" runat="server" Width="300px" AutoPostBack="True">
                        <asp:ListItem>Elegir proveedor</asp:ListItem>
                        <asp:ListItem Selected="True">Mismo Deudor</asp:ListItem>
                        <asp:ListItem>Mismo Cliente</asp:ListItem>
                    </asp:DropDownList>
                </td><td class="auto-style62">
                    <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="odsConceptos" DataTextField="nombre" DataValueField="idConcepto" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td><td class="auto-style62">
                    <asp:TextBox ID="txtDeudor" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td></tr>
        </table>

         <table runat="server" visible="false" id="tablaContratos" style="width: 95%;border-color:lightgray;; padding:5px; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style59">
            <tr><td colspan="2" style="width: 25%">Contratos:&nbsp;
                    <asp:CheckBox ID="chkContrato" runat="server" Font-Names="Arial" Font-Size="Small" Text="¿Si?" AutoPostBack="True" />
                &nbsp;
                
                </td><td class="auto-style72" style="width: 15%; font-size: small;">&nbsp;</td><td class="auto-style62" rowspan="2" style="width:50%;">Cuenta bancaria:<asp:ObjectDataSource ID="odsCuentasBancarias" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="CuentasBancarias_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
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
                    <asp:ObjectDataSource ID="odsSaldosCts" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SaldosAnexos_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.AnexosTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlContratos" Name="Anexo" PropertyName="SelectedValue" Type="String" DefaultValue="0" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <ajaxToolkit:ComboBox ID="cmbCuentasBancarias" runat="server" DataSourceID="odsCuentasBancarias" DataTextField="descrip" DataValueField="idCuentas" MaxLength="0" style="display: inline;" Width="500px" Font-Size="Small" DropDownStyle="DropDownList">
                    </ajaxToolkit:ComboBox>
                    </td></tr>
            <tr><td class="auto-style74" style="width: 25%">
                    Cliente: <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" DataSourceID="odsClientes" DataTextField="Descr" DataValueField="Cliente" Enabled="False" Width="250px">
                    </asp:DropDownList>
                
                        </td><td colspan="2" style="width: 15%">
                    Contrato:&nbsp;
                    <asp:DropDownList ID="ddlContratos" runat="server" DataSourceID="odsContratos" DataTextField="Anexo" DataValueField="Anexo" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                            </td></tr>
            <tr><td style="font-size: small;">
                    Monto financiado:  <asp:FormView ID="FormView2" runat="server" DataSourceID="odsSaldosCts" EnableViewState="False" Font-Size="Small">
                                <ItemTemplate>
                                    <asp:Label ID="saldoContrato" runat="server" Text='<%# Eval("MontoFinanciado", "{0:C}") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                <td colspan="2" style="font-size: small;">
                    Saldo del contrato:&nbsp; <asp:FormView ID="FormView1" runat="server" DataSourceID="odsSaldosCts" EnableViewState="False" Font-Size="Small">
                                <ItemTemplate>
                                    <asp:Label ID="saldoContrato" runat="server" Text='<%# Eval("SaldoContrato", "{0:C}") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:FormView>
                            </td><td class="auto-style62" style="width:50%; font-size: small;">Tipo:<asp:FormView ID="FormView3" runat="server" DataSourceID="odsSaldosCts" EnableViewState="False" Font-Size="Small">
                                <ItemTemplate>
                                    <asp:Label ID="tipoContrato" runat="server" Text='<%# Eval("Tipar") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:FormView>
                            </td></tr>
        </table>
       
        <table runat="server" id="tablaBuscar" style="border-color:lightgray;width: 95%; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style59">
            <tr><td colspan="2">Proveedor:<asp:ObjectDataSource ID="odsProveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td><td class="auto-style68" colspan="2">Estatus EFOS:<asp:ObjectDataSource ID="odsClientes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
                </td></tr>
            <tr><td class="auto-style69">
                    <asp:TextBox ID="txtBuscarProveedor" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnBuscar" runat="server" Text="    Buscar    " CssClass="Botones" Width="100px" Enabled="False" />
                </td><td class="auto-style71">
                    <asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="odsProveedores" DataTextField="razonSocial" DataValueField="idProveedor" Width="250px" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSeleccionarProv" runat="server" Text="Seleccionar" CssClass="Botones" Width="90px" Enabled="False" />
                </td><td class="auto-style70">
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 (Créditos fiscales)<br />
                    <asp:Label ID="lbl69" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </span>
                    </td><td class="auto-style70">
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 B (Operaciones inexistentes)<br />
                    <asp:Label ID="lbl69B" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </span>
                    </td></tr>
        </table>

          <table visible="false" runat="server" id="tablaAutorizante" style="width: 95%;padding:5px; border-radius:3px; border-style: groove; border-width: 3px; border-color:lightgray; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style59">
        <tr><td>
            Autorizante:<asp:ObjectDataSource ID="odsAutorizantes" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter" UpdateMethod="Update">
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
            </td><td>
                Sucursal:<asp:ObjectDataSource ID="odsCentroDeCostos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCC_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_SucursalesTableAdapter" UpdateMethod="Update">
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
            </td><td>
                Forma de pago:<asp:ObjectDataSource ID="odsFormaPago" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="DocumentosEgreso_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_tipoDocumentoSatTableAdapter" UpdateMethod="Update">
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
            </td><td>
                Fecha de pago:</td></tr>
        <tr><td>
                    <div class="auto-style42">
                        <asp:DropDownList ID="ddlAutorizo" runat="server" Width="300px" DataSourceID="odsAutorizantes" DataTextField="Nombre" DataValueField="id_correo">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Valentin Cruz Barrios</asp:ListItem>
                            <asp:ListItem>Elisander Pineda Rojas</asp:ListItem>
                            <asp:ListItem>Gabriel Bello Hernández</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </div>
            </td><td>
                    <ajaxToolkit:ComboBox ID="cmbCentroDeCostos" runat="server" DataSourceID="odsCentroDeCostos" DataTextField="nombreSucursal" DataValueField="idSucursal" Font-Size="Small" MaxLength="0" style="display: inline;" Width="300px">
                    </ajaxToolkit:ComboBox>
            </td><td>
                    <ajaxToolkit:ComboBox ID="cmbFormaPago" runat="server" DataSourceID="odsFormaPago" DataTextField="descripcion" DataValueField="idTipoDocumento" Font-Size="Small" MaxLength="0" style="display: inline;" Width="300px" AutoPostBack="True">
                    </ajaxToolkit:ComboBox>
            </td><td>
                    <asp:TextBox ID="txtFechaPago" runat="server" Width="200px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexFechaPago" runat="server" TargetControlID="txtFechaPago" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
            </td></tr>
        </table>

       

      <table visible="false" runat="server" id="tablaReferenciaBancaria" style="width:95%; padding:5px; border-radius:5px; border-style: groove; border-width: 3px; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style75">
        <tr>
            <td class="auto-style80">Banco:
                <asp:FormView ID="FormView5" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Banco" runat="server" Text='<%# Bind("Banco") %>' />
                    </ItemTemplate>
                </asp:FormView>
            </td>
            <td class="auto-style81">Cuenta:

             </td>
            <td class="auto-style81">CLABE:
                <asp:FormView ID="FormView4" runat="server" DataSourceID="odsDatosCuenta" EnableViewState="False" Font-Size="Small" DataKeyNames="Anexo" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="CuentaCLABELabel" runat="server" Text='<%# Bind("CuentaCLABE") %>' />
                    </ItemTemplate>
                </asp:FormView>
             </td>
            <td class="auto-style81">Referencia:

             </td>
            <td class="auto-style81">Adjunto:<asp:ObjectDataSource ID="odsBancos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_BancosTableAdapter" UpdateMethod="Update">
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
            <td class="auto-style80">
                <asp:DropDownList ID="ddlBancos" runat="server" DataSourceID="odsBancos" DataTextField="nombreCorto" DataValueField="idBancos" Width="200px">
                </asp:DropDownList>

             </td>
            <td class="auto-style81">
                <asp:TextBox ID="txtCuenta" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style81">
                <asp:TextBox ID="txtClabe" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style81">
                <asp:TextBox ID="txtReferencia" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style81">
              
                <ajaxToolkit:AsyncFileUpload ID="afuAdjuntoCta" runat="server" />
              
            </td>
        </tr>
      </table>
    
        <table visible="false" runat="server" id="tablaDatosSol" style="width: 95%;padding:5px; border-radius:5px; border-style: groove; border-width: 3px; border-color:lightgray; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: auto; margin-right: auto; margin-bottom: 0;">
            <tr>
                <td>
                    Fecha de solicitud:</td>
                <td>
                    Moneda:</td>
                <td>
                    Importe solicitado:</td>
                <td colspan="2">
                    Descripción del pago:<asp:ObjectDataSource ID="odsMonedas" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_c_MonedaTableAdapter" UpdateMethod="Update">
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
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaSolicitud" runat="server" ForeColor="Navy" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="odsMonedas" DataTextField="c_NombreMoneda" DataValueField="c_Moneda" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtMontoSolicitado" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtDescripcionPago" runat="server" Width="700px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Importe carta neteo:</td>
                <td>
                    <asp:TextBox ID="txtImporteCartaNeteto" runat="server" Width="100px" Enabled="False">0</asp:TextBox>
                </td>
                <td>
                    Descripción carta neteo:</td>
                <td>
                    <asp:TextBox ID="txtDesccartaNeteo" runat="server" Width="350px" Enabled="false"></asp:TextBox>
                &nbsp;&nbsp;
                </td>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style30" colspan="5">
                    <asp:Button ID="btnRevisar" runat="server" CssClass="Botones" Text="Revisar" />
                &nbsp;
                    <asp:Button ID="btnCancelar1" runat="server" CssClass="Botones" Text="Cancelar" />
                </td>
            </tr>
        </table>
        <div id="divRevisar" runat="server" visible="false">
        <table runat="server" id="tablaRevisar" style="position:fixed;top:30%;left:20%; width:60%; padding:5px; border-radius:5px; border-style: groove; border-width: 5px; border-color:navy; margin-top: 20px; font-weight:600; font-family: Verdana; font-size: 15px; color: darkblue; background-color: #FFE0C0; margin-left: 32px; margin-right: auto; margin-bottom: initial;background: linear-gradient(to bottom, rgba(245,130,32,1) 0%, rgba(245,130,32,1) 50%, rgba(241,188,142,1) 71%, rgba(237,237,237,1) 89%, rgba(246,246,246,1) 100%);" class="auto-style59">
            <tr><td class="auto-style30" colspan="2">
                    <asp:TextBox ID="txtRevision" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="Arial" ForeColor="Red" Height="150px" ReadOnly="True" TextMode="MultiLine" Visible="False" Width="721px"></asp:TextBox>
                            </td></tr>
            <tr><td>
                                <asp:Label ID="lblAdjuntos" runat="server" Text="Archivos adjuntos (solo PDF):" Visible="False"></asp:Label>
                                </td><td>
                                <asp:FileUpload ID="fup1" runat="server" Visible="False" accept=".pdf" multiple="multiple" AllowMultiple="true" />
                            </td></tr>
            <tr><td>
                                <asp:Label ID="lblcartaNeteoAdjuntos" runat="server" Text="Carta neteo (solo PDF):"></asp:Label>
                                </td><td>
                                <asp:FileUpload ID="fupCarteNeteo" runat="server" Enabled="false"/>
                            </td></tr>
            <tr><td class="auto-style30" colspan="2">
            <cc1:BotonEnviar ID="btnSolicitar" runat="server" CssClass="Botones" Visible="false"
                       Text="Solicitar" TextoEnviando="Procesando..." />
    &nbsp;
     <asp:Button ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
    &nbsp;</td></tr>
            <tr><td class="auto-style30" colspan="2">
    <asp:Label ID="lblError" runat="server" Font-Names="Arial Black" Font-Size="X-Large" ForeColor="#FF6401" Text="Error" Visible="False"></asp:Label></td></tr>
        </table>
        </div>

        <div id="divOtros" runat="server" visible="false">
        <table style="width: 90%; margin-left:20px; margin-top:10px;" >
                        
            <tr>
                <td class="auto-style30" id="contenedor2ID" runat="server">
                    
                    <asp:ObjectDataSource ID="odsProveedores0" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsCFDI" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" />
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
                                                        <ItemStyle HorizontalAlign="Center" Width="300px" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="True" />
                                                        </asp:BoundField>
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
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td class="auto-style55">&nbsp;</td>
            </tr>
        </table>
        <div class="auto-style30">
     
        </div>
        </div>
        </div>
</asp:Content>
