﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisPagoContratos.aspx.vb" Inherits="cxP.frmMisPagoContratos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">


        .auto-style13 {
            margin-left: 10px;
            margin-top: 20px;
        }
         .auto-style14 {
            text-align: center;
            overflow-y:auto;
            height:400px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
              <div style="overflow-x:auto;overflow-y:auto;">
                  <table runat="server" id="tablaMisSolicitudes" style="border-radius:5px; border-style: groove; border-width: 3px; border-color: lightgray; width:95%; height:90%; margin-left:auto; margin-right:auto; margin-bottom:3%; margin-top:3%;">
                                <tr>
                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Font-Names="Verdana" BorderColor="Gray" BorderWidth="2px" Font-Size="Smaller" CellSpacing="2" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False" GridLines="None" DataSourceID="odsMisSolicitudesSC" Height="16px" Width="1230px" ForeColor="#333333" HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="folioSolicitud" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" SortExpression="folioSolicitud">
                                <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
                                <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="fechaSolicitud" HeaderText="Fecha Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}" SortExpression="fechaSolicitud" >
                                <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="razonSocial" HeaderText="Beneficiario" SortExpression="razonSocial" >
                                
                                
                                <ItemStyle HorizontalAlign="Left" Font-Size="X-Small" Width="250px" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="Descr" HeaderText="Cliente" SortExpression="Descr">
                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Importe de Solicitud" DataField="totalPagado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:c}" SortExpression="totalPagado" >
                                
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Font-Size="X-Small" Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="noContrato" HeaderText="Contrato" SortExpression="noContrato">
                                <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Promotor" SortExpression="nombre">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Left" Width="180px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PDF Solicitud" HeaderStyle-Font-Names="Arial" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioSolicitud", "~/TmpFinagil/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
                                    </ItemTemplate>

<HeaderStyle Font-Names="Arial" Font-Size="Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                             <RowStyle BackColor="#FFE0C0" />
                             <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                                                                                </td>
                                </tr>
                            </table>
                   </div>
              <asp:ObjectDataSource ID="odsMisSolicitudesSC" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisPagosContratosTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idEmpresas" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
              </asp:ObjectDataSource>
</asp:Content>
