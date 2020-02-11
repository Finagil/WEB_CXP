<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmComprobarGastos.aspx.vb" Inherits="cxP.frmComprobarGastos" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<% @ Register assembly = "AjaxControlToolkit" namespace = "AjaxControlToolkit"  tagprefix = "asp"  %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="js/calendar-en.min.js" type="text/javascript"></script>
    <link href="css/calendar-blue.css" rel="stylesheet" type="text/css" />
    
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
        .auto-style8 {
            width: 488px;
        }
        .auto-style9 {
            width: 186px;
        }
        .auto-style11 {
            width: 488px;
            height: 79px;
        }
        .auto-style12 {
            width: 186px;
            height: 79px;
        }
        .auto-style13 {
            height: 79px;
        }
        .auto-style14 {
            margin-left: 20px;
            margin-top: 20px;
            width: 95%;
        }
        .auto-scroll {
            overflow-x:auto;
            
        }
        .auto-style17 {
            width: 232px;
        }
        .auto-style18 {
            width: 201px;
        }
        .auto-style20 {
            height: 22px;
        }
        .auto-style21 {
            text-align: center;
        }
        .auto-style25 {
            margin-left: 0px;
        }
        .auto-style26 {
            overflow-x: auto;
            height: 100%;
        }
        .auto-style27 {
            width: 148px;
        }
        .auto-style28 {
            height: 22px;
            text-align: center;
        }
        .auto-style29 {
            height: 22px;
            text-align: left;
        }
        .auto-style31 {
            width: 186px;
            height: 22px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMensaje1" runat="server" CssClass="CajaDialogo" style="display: none;">
    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">
        <tr>
            <td align="center">
                <asp:Label ID="Label5" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>
            </td>
            <td width="12%">
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="errLabel17" runat="server" Text="El total de las facturas debe ser igual o menor al saldo de la solicitud." />
    </div>
    <div>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="Botones" />
    </div>
</asp:Panel>
    <asp:Panel ID="pnlMensaje2" runat="server" CssClass="CajaDialogo" style="display: none;">

   <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">

        <tr>

            <td align="center">

                <asp:Label ID="Label1" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>

            </td>

            <td width="12%">


            </td>

        </tr>

    </table>

    <div>

             &nbsp;&nbsp;

       
        <asp:Label ID="errLabel18" runat="server" Text="El importe no es un valor numérico." />
       
    </div>

    <div>

        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Botones" />

    </div>

</asp:Panel>
    <asp:Panel ID="pnlMensaje3" runat="server" CssClass="CajaDialogo" style="display: none;">

    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">

        <tr>

            <td align="center">

                <asp:Label ID="Label6" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>

            </td>

            <td width="12%">


            </td>

        </tr>

    </table>

    <div>

             &nbsp;&nbsp;

       
        <asp:Label ID="errLabel19" runat="server" Text="No se ha seleccionado ningún autorizante." />
    </div>

    <div>

        <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Botones" />

    </div>

</asp:Panel>
    <asp:Panel ID="pnlMensaje4" runat="server" CssClass="CajaDialogo" style="display: none;">

   <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">

        <tr>

            <td align="center">

                <asp:Label ID="Label2" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>

            </td>

            <td width="12%">


            </td>

        </tr>

    </table>

    <div>

             &nbsp;&nbsp;

       
        <asp:Label ID="errLabel20" runat="server" Text="No está configurado el deudor" />
    </div>

    <div>

        <asp:Button ID="Button3" runat="server" Text="Aceptar" CssClass="Botones" />

    </div>

</asp:Panel>
     <asp:Panel ID="pnlMensaje5" runat="server" CssClass="CajaDialogo" style="display: none;">

    <table border="0" width="200px" style="margin: 2px; padding: 0px; background-color: #FFF700; color: #000000;">

        <tr>

            <td align="center">

                <asp:Label ID="Label3" runat="server" Text="¡ Atención !" BackColor="#FFF700" Font-Bold="true"/>

            </td>

            <td width="12%">


            </td>

        </tr>

    </table>

    <div>

             &nbsp;&nbsp;

       
        <asp:Label ID="errLabel21" runat="server" Text="No está configurado el deudor" />
    </div>

    <div>

        <asp:Button ID="Button4" runat="server" Text="Aceptar" CssClass="Botones" />

    </div>

</asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       
     <link href="styFW.css" rel="stylesheet" type="text/css" />
    <div class="auto-style26">

        <table class="auto-style14">
            <tr>
                <td id="id1" runat="server" class="auto-style8" style="font-family: Arial; font-size: medium; background-color: #FF6600; font-weight: bold; color: #FFFFFF;" colspan="5">Solicitud:</td>
                <td id="id2" runat="server" style="font-family: Arial; font-size: medium; background-color: #FF6600; font-weight: bold; color: #FFFFFF;" colspan="3">Datos de la solicitud:</td>
            </tr>
            <tr>
                <td class="auto-style11" style="font-family: Arial; font-size: medium" colspan="5">Folio de solicitud:<br />
                    <asp:DropDownList ID="ddlFolioSolicitud" runat="server" DataSourceID="odbSolicitudes2" DataTextField="folioSolicitud" DataValueField="uuid" Width="200px" Height="20px">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odbSolicitudes2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisSolicitudesSCXComp_GetDataBy1" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    &nbsp;<asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="Botones" Width="106px" />
                    <asp:HyperLink ID="hlkPdf" Target="_blank" runat="server">Ver PDF</asp:HyperLink>
                </td>
                <td class="auto-style12" style="font-family: Arial; font-size: medium" colspan="2">Fecha de solicitud:<br />
                    <asp:Label ID="lblFechaSolicitud" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    Descripción:</td>
                <td style="font-family: Arial; font-size: medium" class="auto-style13">Importe solicitado:<br />
                    <asp:Label ID="lblImporteSolicitado" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    Saldo:<br />
                    <asp:Label ID="lblSaldo" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td id="id3" runat="server" class="auto-style8" style="font-family: Arial; font-size: medium; border-bottom-style: solid; border-bottom-color: #FF6600; border-bottom-width: 3px;" colspan="5">
                    <asp:TextBox ID="txtDescripcion" runat="server" Font-Bold="True" Height="41px" ReadOnly="True" Rows="2" TextMode="MultiLine" Width="399px" CssClass="auto-style25"></asp:TextBox>
                    <asp:ObjectDataSource ID="odsProveedores" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtProv_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.CXP_ProveedoresTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idSucursal" SessionField="idSucursal" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="empresa" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    </td>
                <td id="id4" runat="server" class="auto-style9" style="font-family: Arial; font-size: small; border-bottom-style: solid; border-bottom-color: #FF6600; border-bottom-width: 3px;" colspan="2">
                    Autoriza:<asp:DropDownList ID="ddlAutorizo" runat="server" Height="20px" Width="221px" Enabled="False" DataSourceID="odsAutoriza" DataTextField="Nombre" DataValueField="id_correo">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Valentin Cruz Barrios</asp:ListItem>
                        <asp:ListItem>Elisander Pineda Rojas</asp:ListItem>
                        <asp:ListItem>Gabriel Bello Hernández</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsAutoriza" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="NoMc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.GEN_CorreosFasesTableAdapter" UpdateMethod="Update">
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
                <td id="id9" runat="server" style="font-family: Arial; font-size: medium; border-bottom-style: solid; border-bottom-color: #FF6600; border-bottom-width: 3px;">
                    <asp:ObjectDataSource ID="odsSolicitudes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ConSaldo_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" />
                            <asp:SessionParameter Name="empresa" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: small" colspan="3" rowspan="2">
                    Motivo de comprobación:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td style="font-family: Arial; font-size: small" class="auto-style27" colspan="2" rowspan="2">
                    <asp:TextBox ID="txtDestinoNacional" runat="server" Width="293px" Enabled="False"></asp:TextBox>
                    </td>
                <td style="font-family: Arial; font-size: small">
                 Fecha inicial:
                    &nbsp;</td>
                <td style="font-family: Arial; font-size: small" colspan="2">
               Fecha final:&nbsp; </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: small" colspan="2" class="auto-style31">
                    <asp:TextBox ID="txtFechaSalida" runat="server" Enabled="true" Width="100px" ForeColor="Black"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cexFechaSalida" runat="server" TargetControlID="txtFechaSalida" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                    </td>
                <td style="font-family: Arial; font-size: small" class="auto-style20">
                    <asp:TextBox ID="txtFechaLlegada" runat="server" Enabled="true" Width="100px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexFechaLlegada" runat="server" TargetControlID="txtFechaLlegada" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy"  />
                </td>
            </tr>
            <tr>
                <td id="id5" runat="server" class="auto-style17" style="font-family: Arial; font-size: medium; background-color: #FF6600; color: #FFFFFF;" colspan="2">Asignar facturas:</td>
                <td id="id6" runat="server" class="auto-style18" style="font-family: Arial; font-size: medium; background-color: #FF6600;" colspan="3">&nbsp;</td>
                <td id="id7" runat="server" class="auto-style9" style="font-family: Arial; font-size: medium; background-color: #FF6600;" colspan="2">&nbsp;</td>
                <td id="id8" runat="server" style="font-family: Arial; font-size: medium; background-color: #FF6600;">&nbsp;</td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="5">Buscar proveedor:&nbsp;
                    <asp:TextBox ID="txtBuscar" runat="server" Width="174px" Enabled="False"></asp:TextBox>
&nbsp;&nbsp;
                    <asp:Button ID="btnBuscar" runat="server" CssClass="Botones" Text="Buscar" Enabled="False" />
                </td>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="3">
                    <asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="odsProveedores" DataTextField="razonSocial" DataValueField="idProveedor" Height="20px" Width="400px" Enabled="False" Visible="False">
                    </asp:DropDownList>
&nbsp;&nbsp;
                    <asp:Button ID="btnAsignar" runat="server" CssClass="Botones" Text="Seleccionar" Width="107px" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="8">&nbsp;</td>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensaje1" TargetControlID="errLabel17" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensaje2" TargetControlID="errLabel18" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensaje3" TargetControlID="errLabel19" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensaje4" TargetControlID="errLabel20" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" OkControlID="btnAceptar" PopupControlID="pnlMensaje5" TargetControlID="errLabel21" BackgroundCssClass="FondoAplicacion" OnOkScript="mpeMensajeOnOk()"></ajaxToolkit:ModalPopupExtender>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="8">
                    <div style="height: 150px" class="auto-scroll">
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
                    </div>
                    <asp:ObjectDataSource ID="odsCFDI" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtCFDiXRfc_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.vw_CXP_XmlCfdi2_grpUuidTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="rfcEmisor" SessionField="rfcEmisor" Type="String" />
                            <asp:SessionParameter Name="meses" SessionField="mesesFacturas" Type="Decimal" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="8"></td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;">&nbsp;</td>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="5" class="auto-style21">
                        <asp:Button ID="btnAgregar" runat="server" CssClass="Botones" Text="Agregar" Visible="False" />
                &nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
                </td>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="2">
                        <asp:Label ID="lblError" runat="server" Font-Size="Small" ForeColor="#FF6600" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" class="auto-style20"></td>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="4" class="auto-style20">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="3" class="auto-style20">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="8" class="auto-style21">
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Visible="False">
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Deducibles" BackColor="#FFFFFF">
                            <ContentTemplate>
               
                                <asp:GridView ID="GridView2" runat="server" AllowSorting="True" Height="16px" Width="1111px" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True" PageSize="2" Font-Size="Small">
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
                            <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="Black" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
                        </asp:GridView>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="No Deducibles" BackColor="#FFFFFF" >
                            <ContentTemplate>
                                                                     
                                <table style="align-content: center ; width: 90%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <caption>
                                            Descripción y /o concepto del gasto:<asp:TextBox ID="txtConceptoND" runat="server" Width="240px"></asp:TextBox>
                                            Importe:
                                            <asp:TextBox ID="txtImporteND" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnNoDeducible" runat="server" CssClass="Botones" Text="Agregar" />
                                            <tr>
                                                <td>&nbsp;</td>
                                                <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" Font-Size="Small" ForeColor="#333333" Height="16px" HorizontalAlign="Center" ShowFooter="True" Width="419px">
                                                    <Columns>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción y/o Concepto del Gasto">
                                                        <ControlStyle Width="200px" />
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" Width="450px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Documentos adjuntos">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="fupNoDeducibles" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
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
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: bottom;" colspan="8">
                    
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="font-family: Arial; " colspan="8" class="auto-style21">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; " colspan="4" class="auto-style28">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalGastos" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td style="font-family: Arial; " colspan="4" class="auto-style29">
                    <asp:Button ID="btnComprobar" runat="server" CssClass="Botones" Text="Procesa Comprobación" Visible="False" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btncancelarComprobacion" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="8" class="auto-style21">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;

            </tr>
            <tr>
                <td style="font-family: Arial; vertical-align: super;" colspan="8">
                    &nbsp;</td>
            </tr>
        </table>


    </div>
</asp:Content>
