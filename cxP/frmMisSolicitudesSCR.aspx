﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisSolicitudesSCR.aspx.vb" Inherits="cxP.frmMisSolicitudesSCR" %>
<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style15 {
            text-align: center;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div style="overflow-x:auto;overflow-y:auto;" class="auto-style15">
                            <asp:HiddenField ID="HiddenID" runat="server" />
                            <asp:HiddenField ID="HiddenEstatus" runat="server" />
                            <asp:Label ID="LabelError" runat="server" Text="Error" Font-Bold="True" ForeColor="#FF3300" Visible="False" Font-Size="X-Large"></asp:Label>
                                         <table runat="server" id="tablaMisSolicitudes" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width:95%; height:90%; margin-left:auto; margin-right:auto; margin-bottom:3%; margin-top:3%;">
                                <tr>
                                    <td>
                                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Font-Names="Verdana" BorderColor="Gray" BorderWidth="2px" Font-Size="Smaller" CellSpacing="2" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False" GridLines="None" DataSourceID="odsMisSolicitudesSC" Height="16px" Width="1230px" ForeColor="#333333" HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="folioSolicitud" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="fechaSolicitud" HeaderText="Fecha Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="razonSocial" HeaderText="Proveedor" >
                                
                                
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Importe de Solicitud" DataField="totalPagado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:c}" >
                                
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estatus" HeaderText="Estatus Autorización" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Font-Size="X-Small"></ItemStyle>
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
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("st") %>' CommandName="Select" Text='<%# Eval("folioSolicitud", "{0}") %>' Enabled='<%#Eval("habilitado")%>'></asp:LinkButton>
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
                    <asp:ObjectDataSource ID="odsMisSolicitudesSC" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisSolicitudesSCFiltroB_GetDataBy1" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="" Name="usuario" SessionField="Usuario" Type="String" />
                            <asp:SessionParameter DefaultValue="" Name="empresa" SessionField="Empresa" Type="Decimal" />
                            <asp:SessionParameter Name="idConcepto" SessionField="idConcepto" Type="Decimal" />
                        </SelectParameters>
         </asp:ObjectDataSource>
</asp:Content>
