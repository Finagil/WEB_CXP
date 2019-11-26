
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmConComprobante.aspx.vb" Inherits="cxP.frmConComprobante" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<% @ Register assembly = "AjaxControlToolkit" namespace = "AjaxControlToolkit"  tagprefix = "asp"  %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
     <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFechaPago.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
    <style type="text/css">
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

    .auto-style7 {
        width: 50%;
        height: 915px;
        margin-left: 20px;
        margin-top: 20px;
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
        .auto-style16 {
            width: 45px;
        }
        .auto-style18 {
            width: 558px;
            text-align: center;
        }
        .auto-style19 {
            text-align: center;
            width: 205px;
        }
        .auto-style20a {
            overflow-y: auto;
            overflow-x: auto;
            height:250px;
            width:1350px;
            text-align: left;          
        }
        .auto-style21 {
           margin-left: 10px;
            margin-top: 10px;
            overflow-y: auto;
            height: 650px;
        }
        .auto-style22 {
            text-align: left;
        }
        .auto-style23 {
            height: 114px;
        }
        .auto-style25 {
            height: 114px;
        width: 319px;
            text-align: center;
            top: auto;
        }
    .auto-style27 {
        width: 426px;
    }
        .auto-style28 {
            text-align: left;
            height: 18px;
            width: 205px;
        }
        .auto-style29 {
            height: 18px;
        }
        .auto-style31 {
            height: 18px;
            width: 319px;
            text-align: center;
        }
        .auto-style32 {
             height: 18px;
            text-align: center;
            font-family: Arial;
            font-size: medium;
            color: #FFFFFF;
        }
        .auto-style33 {
            height: 18px;
            width: 250px;
        }
        .auto-style34 {
            height: 114px;
            width: 250px;
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensajeError" TargetControlID="lblErrorGeneral" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
     <link href="styFW.css" rel="stylesheet" type="text/css" />
    <div class="auto-style21">
     
        <table class="auto-style7">
            <tr>
                <td id="id0" runat="server" class="auto-style29" style="font-family: Arial; font-size: medium; font-weight: bold; color: #FFFFFF; background-color: #FF6600;" colspan="2">
                    Proveedor:</td>
                <td id="id2" runat="server" class="auto-style28" style="font-family: Arial; font-size: medium; font-weight: bold; color: #FFFFFF; background-color: #FF6600;" colspan="2">
                    Estatus EFOS:</td>
                <td id="id3" runat="server" class="auto-style33" style="font-family: Arial; font-size: medium; font-weight: bold; color: #FFFFFF; background-color: #FF6600">
                    Autoriza:</td>
                <td id="id4" runat="server" class="auto-style31" style="font-family: Arial; font-size: medium; font-weight: bold; color: #FFFFFF; background-color: #FF6600">
                    Contrato</td>
            </tr>
            <tr>
                <td class="auto-style23" style="font-family: Arial, Helvetica, sans-serif; font-size: medium" colspan="2">
                    <div class="auto-style22">
                        <br />
                        <asp:TextBox ID="txtBuscar" runat="server" Width="392px" Height="20px"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" Text="    Buscar    " CssClass="Botones" />
                        <br />
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
                    <asp:DropDownList ID="ddlProveedores" runat="server" DataSourceID="proveedores" DataTextField="razonSocial" DataValueField="idProveedor" Height="20px" Width="396px">
                    </asp:DropDownList>
                        <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="Botones" />
                        <br />
                    </div>
                </td>
                <td class="auto-style19" style="font-family: Arial" colspan="2">
                    <br />
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 (Créditos fiscales)<br />
                    <asp:Label ID="lbl69" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    <br />
                    <span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: x-small; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Artículo 69 B (Operaciones inexistentes)<br />
                    <asp:Label ID="lbl69B" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label>
                    <br />
                    </span></span>
                </td>
                <td class="auto-style34" style="font-family: Arial; font-size: small;">
                    <div class="auto-style22" style="font-size: medium; font-family: Arial, Helvetica, sans-serif">
                        
                        &nbsp;<asp:DropDownList ID="ddlAutorizo" runat="server" Height="20px" Width="256px" DataSourceID="odsAutorizantes" DataTextField="Nombre" DataValueField="id_correo">
                            <asp:ListItem Selected="True"></asp:ListItem>
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
                        
                    </div>
                    <asp:ObjectDataSource ID="proveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                   
                    
                   
                    <br />
                    Fecha de pago:<br />
                            <asp:TextBox ID="txtFechaPago" runat="server" ></asp:TextBox> <ajaxToolkit:CalendarExtender ID="cexFechaPago" runat="server" TargetControlID="txtFechaPago" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                            </td>
                <td class="auto-style25" style="font-family: Arial; font-size: small; ">
                    <asp:CheckBox ID="chkContrato" runat="server" Text="¿Si?" AutoPostBack="True" TextAlign="Left" />
                    <br />
                    <br />
                    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" DataSourceID="odsClientesCtos" DataTextField="Descr" DataValueField="Cliente" Enabled="False" Width="300px">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsClientesCtos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ClientesAnexosActNoPagados_GetData" TypeName="cxP.dsProduccionTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
                    <br />
                    <br />
                    <asp:DropDownList ID="ddlContratos" runat="server" Enabled="False" DataSourceID="odsAnexosActivos" DataTextField="Anexo" DataValueField="Anexo">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsAnexosActivos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.AnexosTableAdapter"></asp:ObjectDataSource>
                    <br />
                </td>
            </tr>
            <tr>
                <td id="id5a" runat="server" class="auto-style32" colspan="3" style="background-color: #FF6600">
                    Sucursal (CC):</td>
                <td id="id5b" runat="server" class="auto-style32" colspan="3" style="background-color: #FF6600">
                    Forma de Pago:</td>
            </tr>
            <tr>
                <td id="id5c" runat="server" class="auto-style32" colspan="3" style="vertical-align: middle;">
                    <ajaxToolkit:ComboBox ID="cmbCentroDeCostos" runat="server"  DataSourceID="odsCentroCostos" DataTextField="nombreSucursal" DataValueField="idSucursal" Font-Size="Small" MaxLength="0" style="display: inline;" Width="200px">
                    </ajaxToolkit:ComboBox>
                    <asp:ObjectDataSource ID="odsCentroCostos" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCC_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_SucursalesTableAdapter" UpdateMethod="Update">
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
                <td id="id5d" runat="server" class="auto-style32" colspan="3" style="vertical-align: middle;">
                    <ajaxToolkit:ComboBox ID="cmbFormaPago" runat="server" DataSourceID="odsFormaPago" DataTextField="descripcion" DataValueField="idTipoDocumento" Font-Size="Small" MaxLength="0" style="display: inline;" Width="200px">
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
                </td>
            </tr>
            <tr>
                <td id="id5" runat="server" class="auto-style32" colspan="6" style="background-color: #FF6600">
                    Selección de facturas para pago:</td>
            </tr>
            <tr>
                <td class="auto-style11" colspan="6">
                    <div class="auto-style20a">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataKeyNames="idXmlCfdi" DataSourceID="comprobantesFiscales" Height="16px" Width="1226px" ForeColor="#333333" HorizontalAlign="Center">
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
            <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                    </div>
                    <asp:ObjectDataSource ID="comprobantesFiscales" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" DefaultValue="GAU030322BV4" />
                            <asp:SessionParameter Name="meses" SessionField="mesesFacturas" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblImporte" runat="server" Text="Importe carta neteo:" Visible="False"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtImporteCartaNeteto" runat="server" Visible="False">0</asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblImporte0" runat="server" Text="Descripción Carta Neteo:" Visible="False"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtDescCartaNeteo" runat="server" Visible="False" Width="312px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnVistaPrevia" runat="server" Text="Revisar" CssClass="Botones" Visible="False" Width="100px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelarBusqueda" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" Width="100px" />
                    <br />
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="auto-style13" Height="16px" Width="794px" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True">
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
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                    <asp:Label ID="lblError2" runat="server" Font-Names="Arial Black" Font-Size="Small" ForeColor="#006600" Text="Error" Visible="False" Width="900px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblCarteNeteo" runat="server" Text="Adjuntar Carta Neteo:" Visible="False"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:FileUpload ID="fupCartaNeteo" runat="server"  Width="216px" Visible="False" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblAdjuntos" runat="server" Text="Archivos adjuntos (solo PDF):" Visible="False"></asp:Label>
&nbsp;&nbsp;
                                <asp:FileUpload ID="fup1" runat="server" Visible="False" accept=".pdf" multiple="multiple" AllowMultiple="true"/>
                    <br />
                    <br />
                    <asp:Button ID="btnProcesar" runat="server" CssClass="Botones" Text="Procesar" Visible="False" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                    <br />
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial Black" Font-Size="X-Large" ForeColor="#FF6401" Text="Error" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style18" colspan="3">
                    &nbsp;</td>
                <td class="auto-style27" colspan="2">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
