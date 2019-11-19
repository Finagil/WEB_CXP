<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisSolicitudesSCR.aspx.vb" Inherits="cxP.frmMisSolicitudesSCR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="height: 450px" >
                        <div class="auto-style14">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataSourceID="odsMisSolicitudesSC" Height="16px" Width="1226px" ForeColor="#333333" HorizontalAlign="Center" PageSize="20" Font-Names="Arial" AllowCustomPaging="True">
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
                                
                                
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField HeaderText="Importe de Solicitud" DataField="totalPagado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:c}" >
                                
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estatus" HeaderText="Estatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Font-Size="X-Small"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PDF Solicitud" HeaderStyle-Font-Names="Arial" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioSolicitud", "~/TmpFinagil/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
                                    </ItemTemplate>

<HeaderStyle Font-Names="Arial"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                       <asp:Button ID="btnOpciones" runat="server" Text="Cancelar Solicitud"
                                             CommandName="Cancelar" 
                                             CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
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
                    <asp:ObjectDataSource ID="odsMisSolicitudesSC" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MisSolicitudesSCFiltroB_GetDataBy1" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="" Name="usuario" SessionField="Usuario" Type="String" />
                            <asp:SessionParameter DefaultValue="" Name="empresa" SessionField="Empresa" Type="Decimal" />
                            <asp:SessionParameter Name="idConcepto" SessionField="idConcepto" Type="Decimal" />
                        </SelectParameters>
         </asp:ObjectDataSource>
         </div>
</asp:Content>
