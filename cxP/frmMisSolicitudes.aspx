﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisSolicitudes.aspx.vb" Inherits="cxP.frmMisSolicitudes" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<% @ Register assembly = "AjaxControlToolkit" namespace = "AjaxControlToolkit"  tagprefix = "asp"  %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <link href="estilos_gridview.css" rel="stylesheet"/>
    <link href="estilos.css" rel="stylesheet"/>
    <style type="text/css">
        .auto-style12 {
            width: 1268px;
            height: 90%;
        }
        .auto-style13 {
            width: 630px;
        }
        .auto-style14 {
            width: 630px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                   <div style="overflow-x:auto;overflow-y:auto;">
                            <asp:HiddenField ID="HiddenID" runat="server" />
                            <asp:HiddenField ID="HiddenEstatus" runat="server" />
                            <asp:Label ID="LabelError" runat="server" Text="Error" Font-Bold="True" ForeColor="#FF3300" Visible="False" Font-Size="X-Large"></asp:Label>
                            <table runat="server" id="tablaMisSolicitudes" style="margin: 3% auto; border-radius:5px; border-style: groove; border-width: 3px; " class="auto-style12">
                                <tr>
                                    <td style="text-align:center" colspan="3">

                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style13">

                                        Fecha inicial:
                                        <asp:TextBox ID="txtFechaInicial" runat="server" Visible="true" Width="250px" ></asp:TextBox> <ajaxToolkit:CalendarExtender ID="cexFechaInicial" runat="server" TargetControlID="txtFechaInicial" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                                    </td>
                                    <td class="auto-style13">
                                        Fecha inicial:
                                        <asp:TextBox ID="txtFechaFinal" runat="server" Visible="true" Width="250px" ></asp:TextBox> <ajaxToolkit:CalendarExtender ID="cexFechaFinal" runat="server" TargetControlID="txtFechaFinal" TodaysDateFormat="MMMM, yyyy" Format="dd/MM/yyyy" />
                                        </td>
                                    <td class="auto-style14">
                                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" />
                                        </td>
                                </tr>
                                <tr>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Font-Names="Verdana" BorderColor="Gray" BorderWidth="2px" Font-Size="Smaller" CellSpacing="2" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False" GridLines="None" DataSourceID="odsMisSolicitudes" Height="16px" Width="1230px" ForeColor="#333333" HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="folioSolicitud" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                                                
                                <asp:BoundField DataField="fechaSolicitud" HeaderText="Fecha Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Proveedor" DataField="razonSocial" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="totalPagado" HeaderText="Importe de Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ReadOnly="True" DataFormatString="{0:c}" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="estatus" HeaderText="Estatus Autorización" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Left" Font-Size="X-Small" />
                                </asp:BoundField>

                                 <asp:TemplateField headertext="Estatus Pago" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" Text='<%#Eval("st")%>' runat="server" NavigateUrl='<%# Eval("uuidPago", "~/TmpFinagil/ComPago/" & "{0}.pdf") %>' Target="_blank" enabled='<%#Eval("visible")%>' >HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Font-Size="X-Small" BorderStyle="None" BorderWidth="2px"></ItemStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="PDF" HeaderStyle-Font-Names="Arial" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioSolicitud", "~/TmpFinagil/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
                                    </ItemTemplate>

<HeaderStyle Font-Names="Arial"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <HeaderTemplate>
                                        <cc1:BotonEnviar ID="BotonEnviar1" runat="server" Text="Cancelar Solicitud" TextoEnviando="Cancelando..." CommandName="Cancelar" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("st") %>' CommandName="Select" Text='<%# Eval("folioSolicitud", "{0}") %>' Enabled='<%#Eval("habilitado")%>'></asp:LinkButton>
                                    </ItemTemplate>                                     
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                                               
                            </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFE0C0"  Font-Names="Verdana" Font-Size="Small"/>
                                <HeaderStyle BackColor="#F58220" ForeColor="Navy" Font-Size="Small"/>
                                <SelectedRowStyle BackColor="Gray" ForeColor="Navy" Font-Size="Small" />
                        </asp:GridView>
                                        </td>
                                </tr>
                            </table>
                    </div>
                    <br />
        <asp:ObjectDataSource ID="odsMisSolicitudes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisSolicitudes_GetData" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesTableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="fechaInicial" SessionField="fechaInicial" Type="DateTime" />
                <asp:SessionParameter DefaultValue="" Name="fechaFinal" SessionField="fechaFinal" Type="DateTime" />
                <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" />
                <asp:SessionParameter Name="empresa" SessionField="Empresa" Type="Decimal" />
            </SelectParameters>
        </asp:ObjectDataSource>
 
</asp:Content>
