<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmAltaProveedor.aspx.vb" Inherits="cxP.frmAltaProveedor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Javascript -->
    <script> 
        function ventanaSecundaria (URL){ 
            window.open(URL,"verPDF.aspx","width=120,height=300,scrollbars=NO") 
        } 
</script>
	<script>
			// Instantiate Scrolls
			var scroll = new SmoothScroll('a[href*="#"]', {
				topOnEmptyHash: false
			});
		</script>	

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
            .flotante {
                position:fixed;
                margin-left:22px;
                font-size:small;
            }
            .style-scrollbar {
            overflow-y: auto;
            overflow-x: auto;
            height:100%;
            width:100%;
            text-align: left;            
        }
        .auto-style9 {
            text-align: center;
        }
        .auto-style10 {
            width: 112px;
            text-align: center;
        }
        .auto-style14 {
            width: 80%;
        }
        .auto-style16 {
            width: 141px;
        }
        .auto-style23 {
            height: 22px;
        }
        .auto-style24 {
            text-align: center;
            width: 355px;
        }
        .auto-style25 {
            width: 1068px;
        }
        .auto-style26 {
            width: 355px;
        }
        .auto-style27 {
            width: 356px;
        }
        .auto-style29 {
            text-align: center;
            width: 169px;
        }
        .auto-style30 {
            width: 212px;
        }
        .auto-style31 {
            width: 624px;
            text-align: left;
            height: 30px;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlDoctosOblig" runat="server" CssClass="CajaDialogo" style="display: none;">
        <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000; border-radius:5px; align-content:center;">
        <tr>
            <td align="lefth">
                <asp:Label ID="lblEncabezado" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true" />
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblError" runat="server" Text="El total de las facturas debe ser igual o menor al saldo de la solicitud." />
    </div>
    <div>
        <asp:Button ID="btnAceptar2" runat="server" Text="Aceptar" CssClass="Botones"/>
    </div>
    </asp:Panel>
    <asp:Panel ID="pnlMensajeError" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000; border-radius:5px; align-content:center;">
        <tr>
            <td align="lefth">
                <asp:Label ID="lblEncabezadoMensaje" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true" />
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensajeError" TargetControlID="lblErrorGeneral" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnAceptar2" PopupControlID="pnlDoctosOblig" TargetControlID="lblError" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
  
        <link href="styFW.css" rel="stylesheet" type="text/css" />
        <div style="overflow-y:auto;height:90%; margin-bottom:10px;">
            
            <table style="border-radius: 5px; border-style: solid; border-width: thin; width: 95%; margin: 0 auto; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; background-color: #FFE0C0;">
        <tr>
            <td class="auto-style16">
                <asp:TextBox ID="txtBuscar" runat="server" Width="420px"></asp:TextBox>
            </td>
            <td class="auto-style31">
                <cc1:BotonEnviar ID="btnBuscar" runat="server" CssClass="Botones" Text="Buscar" />
            </td>
            <td class="auto-style9">
                <asp:DropDownList ID="ddlBuscar" runat="server" Width="420px" DataSourceID="odsProveedores" DataTextField="razonSocial" DataValueField="idProveedor">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsProveedores" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter" UpdateMethod="Update">
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
                    </InsertParameters>
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="idSucursal" SessionField="idSucursal" Type="String" />
                        <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
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
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </td>
            <td class="auto-style9">
                <cc1:BotonEnviar ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="Botones" PostBackUrl="#tablaDatos"/>
            </td>
        </tr>
        </table>
           
            <table id="tablaDatos" runat="server" style="border-radius: 5px; width:95%; border-style: solid; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0; background-color: #FFE0C0;" class="auto-style14">
        <tr>
            <td class="auto-style23">RFC:</td>
            <td class="auto-style23">
                <asp:TextBox ID="txtRfc" runat="server"></asp:TextBox>
            &nbsp;
                <asp:CheckBox ID="chkClientProv" runat="server" AutoPostBack="True" Text="¿Cliente?" />
            </td>
            <td class="auto-style23">Calle:</td>
            <td class="auto-style23">
                <asp:TextBox ID="txtCalle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Razón social:</td>
            <td rowspan="2" style="vertical-align:top">
                <asp:TextBox ID="txtRazonSocial" runat="server" Width="350px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>Colonia:</td>
            <td>
                <asp:TextBox ID="txtColonia" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Localidad:</td>
            <td>
                <asp:TextBox ID="txtLocalidad" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>NIT:</td>
            <td style="vertical-align:top">
                <asp:TextBox ID="txtNit" runat="server" Width="227px"></asp:TextBox>
            </td>
            <td>Delegación/Municipio:</td>
            <td>
                <asp:TextBox ID="txtDelegacion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>CURP:</td>
            <td style="vertical-align:top">
                <asp:TextBox ID="txtCurp" runat="server" Width="284px"></asp:TextBox>
            </td>
            <td>Estado:</td>
            <td>
                <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td>Activo:</td>
            <td>
                <asp:TextBox ID="txtActivo" runat="server" Enabled="False"></asp:TextBox>
                    </td>
            <td>País:</td>
            <td>
                <asp:DropDownList ID="ddlPais" runat="server" DataSourceID="odsPaises" DataTextField="Descripcion" DataValueField="c_Pais" Width="250px">
                </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsPaises" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_c_PaisTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_c_Pais" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="c_Pais" Type="String" />
                            <asp:Parameter Name="Descripcion" Type="String" />
                            <asp:Parameter Name="nacionalidad" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Descripcion" Type="String" />
                            <asp:Parameter Name="nacionalidad" Type="String" />
                            <asp:Parameter Name="Original_c_Pais" Type="String" />
                        </UpdateParameters>
                </asp:ObjectDataSource>
                    </td>
        </tr>
                <tr>
            <td>Autorizado:</td>
            <td>
                <asp:TextBox ID="txtAutorizado" runat="server" Enabled="False"></asp:TextBox>
                    </td>
            <td>Código Postal:</td>
            <td>
                <asp:TextBox ID="txtCp" runat="server"></asp:TextBox>
                    </td>
        </tr>
                <tr>
            <td>Mail:</td>
            <td>
                <asp:TextBox ID="txtMail" runat="server" Width="350px"></asp:TextBox>
                    </td>
            <td>No de proveedor:</td>
            <td>
                <asp:TextBox ID="txtNoProveedor" runat="server" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
                <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td class="auto-style9" colspan="4">
                <cc1:BotonEnviar ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Botones"/>
                    &nbsp;&nbsp;
                <cc1:BotonEnviar ID="btnGuardar" runat="server" Text="Guardar" CssClass="Botones" Enabled="False" PostBackUrl="#tablaCtas"/>
                    &nbsp;&nbsp;
                <cc1:BotonEnviar ID="btnActualizar" runat="server" Text="Actualizar" CssClass="Botones" PostBackUrl="#tablaCtas"/>
                    &nbsp;&nbsp;
                <cc1:BotonEnviar ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Botones"/>
                    </td>
        </tr>
    </table>

            <div id="divDetalles" runat="server" visible="false">
            <table style="border-style: none; width:95%; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style25">
                <tr>
                    <td class="auto-style26">
                        &nbsp;</td>
                    <td class="auto-style24">
                    &nbsp;&nbsp;
                    &nbsp;&nbsp;
                    </td>
                    <td class="auto-style27">
                    </td>
                </tr>
            </table>
             
            <table id="tablaCuentas" runat="server" style="border-radius: 5px; width:95%; border-style: solid; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0; background-color: #FFE0C0;" class="auto-style14">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="idCuentas" DataSourceID="odsCuentas" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="idCuentas" HeaderText="idCuentas" InsertVisible="False" ReadOnly="True" SortExpression="idCuentas" Visible="False" />
                        <asp:BoundField DataField="idProveedor" HeaderText="idProveedor" SortExpression="idProveedor" Visible="False" />
                        <asp:BoundField DataField="idBanco" HeaderText="idBanco" SortExpression="idBanco" Visible="False" />
                        <asp:BoundField DataField="nombreCorto" HeaderText="Banco" SortExpression="banco" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" SortExpression="descripcion" />
                        <asp:BoundField DataField="c_NombreMoneda" HeaderText="Moneda" SortExpression="moneda" >
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuenta" HeaderText="Cuenta" SortExpression="cuenta" >
                        <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate> 
                                         <asp:HyperLink NavigateUrl='<%# Eval("archivo1", "~/TmpFinagil/FilesProv/" & "{0}.pdf") %>' ID="btnVerCta" runat="server" Text="VerPDF" CssClass="BotonesGrid" TextoEnviando="Env" Target="_blank"/>
                                   </ItemTemplate>
                        <HeaderStyle Width="35px"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="clabe" HeaderText="CLABE" SortExpression="clabe" >
                        <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <cc1:BotonEnviar ID="btnEliminaCta" runat="server" Text="Eliminar" CssClass="BotonesGrid" TextoEnviando="Eliminando"  CommandName="Eliminar" CommandArgument='<%# Eval("idCuentas") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="archivo1" HeaderText="archivo1" Visible="False"/>
                        <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <cc1:BotonEnviar ID="btnSolicitarDesactivacion" runat="server" Text="Sol Bloqueo" CssClass="BotonesGrid" TextoEnviando="Enviando"  CommandName="DesactivarCuenta" CommandArgument='<%# Eval("idCuentas") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="status" HeaderText="Estatus" SortExpression="status" >
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="idEstatus" SortExpression="isEstatus">
                            <HeaderStyle Width="5px" />
                        <ItemStyle ForeColor="#FFE0C0" Width="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="convenio" HeaderText="Convenio" >
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="referencia" HeaderText="Referencia" >
                        <ItemStyle Width="130px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                             <RowStyle BackColor="#FFE0C0" />
                             <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
                             <EditRowStyle BackColor="#7C6F57" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsCuentas" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCtasProveedor_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_CuentasBancariasProvTableAdapter" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        <asp:Parameter Name="Original_idProveedor" Type="Decimal" />
                        <asp:Parameter Name="Original_idBanco" Type="Decimal" />
                        <asp:Parameter Name="Original_cuenta" Type="String" />
                        <asp:Parameter Name="Original_clabe" Type="String" />
                        <asp:Parameter Name="Original_descripcion" Type="String" />
                        <asp:Parameter Name="Original_moneda" Type="String" />
                        <asp:Parameter Name="Original_archivo1" Type="String" />
                        <asp:Parameter Name="Original_vigente" Type="Boolean" />
                        <asp:Parameter Name="Original_usuarioSolicita1" Type="String" />
                        <asp:Parameter Name="Original_usuarioSolicita2" Type="String" />
                        <asp:Parameter Name="Original_usuarioAutoriza1" Type="String" />
                        <asp:Parameter Name="Original_usuarioAutoriza2" Type="String" />
                        <asp:Parameter Name="Original_fechaSolicita1" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaSolicita2" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaAutoriza1" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaAutoriza2" Type="DateTime" />
                        <asp:Parameter Name="Original_estatus" Type="Decimal" />
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
                        <asp:Parameter Name="usuarioSolicita1" Type="String" />
                        <asp:Parameter Name="usuarioSolicita2" Type="String" />
                        <asp:Parameter Name="usuarioAutoriza1" Type="String" />
                        <asp:Parameter Name="usuarioAutoriza2" Type="String" />
                        <asp:Parameter Name="fechaSolicita1" Type="DateTime" />
                        <asp:Parameter Name="fechaSolicita2" Type="DateTime" />
                        <asp:Parameter Name="fechaAutoriza1" Type="DateTime" />
                        <asp:Parameter Name="fechaAutoriza2" Type="DateTime" />
                        <asp:Parameter Name="estatus" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:SessionParameter Name="idProveedor" SessionField="noProveedor" Type="Decimal" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="idProveedor" Type="Decimal" />
                        <asp:Parameter Name="idBanco" Type="Decimal" />
                        <asp:Parameter Name="cuenta" Type="String" />
                        <asp:Parameter Name="clabe" Type="String" />
                        <asp:Parameter Name="descripcion" Type="String" />
                        <asp:Parameter Name="moneda" Type="String" />
                        <asp:Parameter Name="archivo1" Type="String" />
                        <asp:Parameter Name="vigente" Type="Boolean" />
                        <asp:Parameter Name="usuarioSolicita1" Type="String" />
                        <asp:Parameter Name="usuarioSolicita2" Type="String" />
                        <asp:Parameter Name="usuarioAutoriza1" Type="String" />
                        <asp:Parameter Name="usuarioAutoriza2" Type="String" />
                        <asp:Parameter Name="fechaSolicita1" Type="DateTime" />
                        <asp:Parameter Name="fechaSolicita2" Type="DateTime" />
                        <asp:Parameter Name="fechaAutoriza1" Type="DateTime" />
                        <asp:Parameter Name="fechaAutoriza2" Type="DateTime" />
                        <asp:Parameter Name="estatus" Type="Decimal" />
                        <asp:Parameter Name="Original_idCuentas" Type="Decimal" />
                        <asp:Parameter Name="Original_idProveedor" Type="Decimal" />
                        <asp:Parameter Name="Original_idBanco" Type="Decimal" />
                        <asp:Parameter Name="Original_cuenta" Type="String" />
                        <asp:Parameter Name="Original_clabe" Type="String" />
                        <asp:Parameter Name="Original_descripcion" Type="String" />
                        <asp:Parameter Name="Original_moneda" Type="String" />
                        <asp:Parameter Name="Original_archivo1" Type="String" />
                        <asp:Parameter Name="Original_vigente" Type="Boolean" />
                        <asp:Parameter Name="Original_usuarioSolicita1" Type="String" />
                        <asp:Parameter Name="Original_usuarioSolicita2" Type="String" />
                        <asp:Parameter Name="Original_usuarioAutoriza1" Type="String" />
                        <asp:Parameter Name="Original_usuarioAutoriza2" Type="String" />
                        <asp:Parameter Name="Original_fechaSolicita1" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaSolicita2" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaAutoriza1" Type="DateTime" />
                        <asp:Parameter Name="Original_fechaAutoriza2" Type="DateTime" />
                        <asp:Parameter Name="Original_estatus" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        </table>
            <table id="tablaCtas" style="border-radius: 5px; width:95%; border-style: solid; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0; background-color: #FFE0C0;" class="auto-style14">
                <tr>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Banco:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Descripción:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Moneda:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Cuenta:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        CLABE.</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Convenio:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Referencia:</td>
                    <td style="font-family: Arial, Helvetica, sans-serif; color: #000000; ">
                        Adjuntar EdoCta:</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlBanco" runat="server" DataSourceID="odsBancos" DataTextField="nombreCorto" DataValueField="idBancos" Width="150px">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsBancos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_BancosTableAdapter" UpdateMethod="Update">
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
                    <td>
                        <asp:TextBox ID="txtDescipcion" runat="server" Width="150px" Wrap="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="odsMonedas" DataTextField="c_Moneda" DataValueField="c_Moneda" Width="80px">
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
                    <td>
                        <asp:TextBox ID="txtCuentaBancaria" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClabe" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConvenio" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReferencia" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <ajaxToolkit:AsyncFileUpload ID="afuArcCta" runat="server" Width="180px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="align-content:center" class="auto-style9">
                <cc1:BotonEnviar ID="btnAgregarCta" runat="server" Text="Agregar" CssClass="Botones" PostBackUrl="#tablaCtas"/>
                    </td>
                </tr>
            </table>
           
            <div id="divDocumentacionProv" runat="server">
            <table id="tableDocumentacionPro" runat="server" style="border-radius: 5px; width:95%; border-style: solid; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0; background-color: #FFE0C0;" class="auto-style14">
            <tr>
                <td class="auto-style30" rowspan="3" style="vertical-align:top;">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="idDocAdjunto" DataSourceID="odsDocumentacion" GridLines="None" Width="100%" Height="100%" >
                        <Columns>
                            <asp:BoundField DataField="descripcion" HeaderText="Tipo Documento" SortExpression="descripcion">
                            <HeaderStyle Font-Size="XX-Small" />
                            <ItemStyle Font-Size="XX-Small" Width="500px" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="obligatorio" HeaderText="Obligatorio" SortExpression="obligatorio">
                            <HeaderStyle Font-Size="XX-Small" />
                            <ItemStyle Font-Size="XX-Small" />
                            </asp:CheckBoxField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsDocumentacion" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtDocumentacionProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_DocumentacionProvTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idDocAdjunto" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="tipoPersona" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="obligatorio" Type="Boolean" />
                            <asp:Parameter Name="fechaDeCarga" Type="DateTime" />
                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                            <asp:Parameter Name="vigenciaAnios" Type="String" />
                            <asp:Parameter Name="vigenciaMeses" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="tipoPersona" SessionField="tipoPersona" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="tipoPersona" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="obligatorio" Type="Boolean" />
                            <asp:Parameter Name="fechaDeCarga" Type="DateTime" />
                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                            <asp:Parameter Name="vigenciaAnios" Type="String" />
                            <asp:Parameter Name="vigenciaMeses" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                            <asp:Parameter Name="Original_idDocAdjunto" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </td>
                <td colspan="3">
                    <asp:ObjectDataSource ID="odsProveedoresArch" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBy11" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresArchTableAdapter">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idProveedoresArch" Type="Decimal" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="idProveedor" SessionField="noProveedor" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="idProveedoresArch" DataSourceID="odsProveedoresArch" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="idProveedoresArch" HeaderText="idProveedoresArch" InsertVisible="False" ReadOnly="True" SortExpression="idProveedoresArch" Visible="False" />
                            <asp:BoundField DataField="idProveedor" HeaderText="idProveedor" SortExpression="idProveedor" Visible="False" />
                            <asp:BoundField DataField="uuid" HeaderText="uuid" SortExpression="uuid" Visible="False" />
                            <asp:BoundField DataField="descripcion" HeaderText="Tipo Documento" SortExpression="uuid" />
                             <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate> 
                                         <asp:HyperLink NavigateUrl='<%# Eval("uuid", "~/TmpFinagil/FilesProv/" & "{0}.pdf") %>' ID="btnVerProvArc" runat="server" Text="VerPDF" CssClass="BotonesGrid" TextoEnviando="Env" Target="_blank"/>
                                   </ItemTemplate>
                        <HeaderStyle Width="35px"></HeaderStyle>
                        </asp:TemplateField>
                            <asp:BoundField DataField="nombreArchivo" HeaderText="Archivo" SortExpression="nombreArchivo" Visible="False" />
                            <asp:CheckBoxField DataField="activo" HeaderText="activo" SortExpression="activo" Visible="False" />
                            <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <cc1:BotonEnviar ID="btnEliminaProvArch" runat="server" Text="Eliminar" CssClass="BotonesGrid" TextoEnviando="Eli"  CommandName="Eliminar" CommandArgument='<%# Eval("idProveedoresArch") %>' />
                                    </ItemTemplate>
                                <HeaderStyle Width="35px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <cc1:BotonEnviar ID="btnSolicitarDesactivacionD" runat="server" Text="Sol Bloqueo" CssClass="BotonesGrid" TextoEnviando="Enviando"  CommandName="DesactivarDocumento" CommandArgument='<%# Eval("idProveedoresArch") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                        </asp:TemplateField>
                            <asp:BoundField DataField="idDocAdjunto" SortExpression="idDocAdjunto" >
                            <ItemStyle Font-Size="XX-Small" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Estatus" ItemStyle-Width="220px" >
<ItemStyle Width="220px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="idEstatus" ItemStyle-Width="5px" >
<ItemStyle Width="5px" ForeColor="#FFE0C0"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                             <RowStyle BackColor="#FFE0C0" />
                             <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
                             <EditRowStyle BackColor="#7C6F57" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style29">
                    Tipo de documento:</td>
                <td class="auto-style29">
                    <asp:DropDownList ID="ddlDocumentacionProv" runat="server" DataSourceID="odsDocumentacionProv" DataTextField="descripcion" DataValueField="idDocAdjunto" Width="139px">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsDocumentacionProv" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtDocumentacionProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_DocumentacionProvTableAdapter" UpdateMethod="Update">
                        <DeleteParameters>
                            <asp:Parameter Name="Original_idDocAdjunto" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="tipoPersona" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="obligatorio" Type="Boolean" />
                            <asp:Parameter Name="fechaDeCarga" Type="DateTime" />
                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                            <asp:Parameter Name="vigenciaAnios" Type="String" />
                            <asp:Parameter Name="vigenciaMeses" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="tipoPersona" SessionField="tipoPersona" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="tipoPersona" Type="String" />
                            <asp:Parameter Name="descripcion" Type="String" />
                            <asp:Parameter Name="obligatorio" Type="Boolean" />
                            <asp:Parameter Name="fechaDeCarga" Type="DateTime" />
                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                            <asp:Parameter Name="vigenciaAnios" Type="String" />
                            <asp:Parameter Name="vigenciaMeses" Type="String" />
                            <asp:Parameter Name="activo" Type="Boolean" />
                            <asp:Parameter Name="Original_idDocAdjunto" Type="Decimal" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="auto-style9">
                    <ajaxToolkit:AsyncFileUpload ID="afuDocumentacionProv" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style9" colspan="3">
                <cc1:BotonEnviar ID="btnActualizarArch" runat="server" Text="Agregar" CssClass="Botones" PostBackUrl="#divDocumentacionProv"/>
                </td>
            </tr>
                </table>
            </div>
            
            <table style="border-style: none; border-width: thin; margin-top: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #000000; margin-left: auto; margin-right: auto; margin-bottom: 0;" class="auto-style25">
                <tr>
                    <td class="auto-style26">

                    </td>
                    <td class="auto-style24">

                <cc1:BotonEnviar ID="btnAutorizar" runat="server" Text="Solicitar autorización" CssClass="Botones"/>

                    &nbsp;

                    </td>
                    <td class="auto-style27">

                        &nbsp;</td>
                </tr>
            </table>
            </div>
        </div>
    <div id="footProv" runat="server" >

    </div>
    <br />
    <br />
    <br />
</asp:Content>
