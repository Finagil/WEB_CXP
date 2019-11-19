<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisSolicitudes.aspx.vb" Inherits="cxP.frmMisSolicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <style type="text/css">

        .auto-style13 {
            margin-left: 20px;
            margin-top: 20px;
        }
        .auto-style14 {
            text-align: center;
            overflow-y:auto;
            height:300px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div style="height: 450px">
                        <div class="auto-style14">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataSourceID="odsMisSolicitudes" Height="16px" Width="1226px" ForeColor="#333333" HorizontalAlign="Center" PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="folioSolicitud" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="fechaSolicitud" HeaderText="Fecha Solicitud" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Proveedor" DataField="razonSocial" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                
                                
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totalPagado" HeaderText="Importe de Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ReadOnly="True" DataFormatString="{0:c}" >
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" HorizontalAlign="Left" Font-Size="X-Small" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="PDF Solicitud" HeaderStyle-Font-Names="Arial" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioSolicitud", "~/TmpFinagil/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
                                    </ItemTemplate>

<HeaderStyle Font-Names="Arial"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnOpciones" runat="server" Text="Cancelar Solicitud"
                                             CommandName="Cancelar" 
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                                               
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FF6600" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                    </div>
                    <br />
        <asp:ObjectDataSource ID="odsMisSolicitudes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisSolicitudes_GetData" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesTableAdapter">
            <SelectParameters>
                <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" />
                <asp:SessionParameter Name="empresa" SessionField="Empresa" Type="Decimal" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
