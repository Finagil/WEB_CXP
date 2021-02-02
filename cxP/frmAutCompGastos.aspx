<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmAutCompGastos.aspx.vb" Inherits="cxP.frmAutCompGastos" %>
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
    <div style="height: 450px">
                        <div class="auto-style14">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataSourceID="odsAutConComprobante" Height="16px" Width="1361px" ForeColor="#333333" HorizontalAlign="Center" PageSize="20" CellPadding="4" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="folioSolicitud" HeaderText="Folio De Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" SortExpression="folioSolicitud">

<HeaderStyle HorizontalAlign="Center" Font-Size="Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="fechaSolicitud" HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}" SortExpression="fechaSolicitud" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Importe Solicitado" DataField="totalPagado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="totalPagado" DataFormatString="{0:c}" >
                                
                                
<HeaderStyle HorizontalAlign="Center" Font-Size="Small"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Solicitante" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" SortExpression="usuario" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="X-Small"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="razonSocial" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" SortExpression="razonSocial">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Font-Size="X-Small"></ItemStyle>
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" ReadOnly="True" SortExpression="estatus" >
                                <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Autoriza1" HeaderText="Autoriza 1">
                                <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Autoriza2" HeaderText="Autoriza 2">
                                <ItemStyle Font-Size="X-Small" />
                                </asp:BoundField>
                                                               
                                <asp:TemplateField HeaderText="PDF Solicitud" HeaderStyle-Font-Names="Arial" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioSolicitud", "~/GTS/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
                                    </ItemTemplate>

<HeaderStyle Font-Names="Arial"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                            <asp:ObjectDataSource ID="odsAutConComprobante" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisAutorizaciones_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_AutCompGastosTableAdapter">
                                <SelectParameters>
                                    <asp:SessionParameter Name="idEmpresa" SessionField="Empresa" Type="Decimal" DefaultValue="23" />
                                    <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" DefaultValue="lgarciac" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                    </div>
                    <br />
    </div>
</asp:Content>
