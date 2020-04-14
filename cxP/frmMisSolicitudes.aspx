<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisSolicitudes.aspx.vb" Inherits="cxP.frmMisSolicitudes" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div style="height: 100%">
                        <div class="auto-style14">
                            <asp:HiddenField ID="HiddenID" runat="server" />
                            <asp:HiddenField ID="HiddenEstatus" runat="server" />
                            <asp:Label ID="LabelError" runat="server" Text="Error" Font-Bold="True" ForeColor="#FF3300" Visible="False" Font-Size="X-Large"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataSourceID="odsMisSolicitudes" Height="16px" Width="1226px" ForeColor="#333333" HorizontalAlign="Center" PageSize="20" CellPadding="4" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
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

                                <asp:TemplateField ShowHeader="False">
                                    <HeaderTemplate>
                                        <cc1:BotonEnviar ID="BotonEnviar1" runat="server" Text="Cancelar Solicitud" TextoEnviando="Cancelando..." CommandName="Cancelar" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("st") %>' CommandName="Select" Text='<%# Eval("folioSolicitud", "{0}") %>'></asp:LinkButton>
                                    </ItemTemplate>                                     
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
