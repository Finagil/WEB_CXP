<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisPagoContratos.aspx.vb" Inherits="cxP.frmMisPagoContratos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">


        .auto-style13 {
            margin-left: 20px;
            margin-top: 20px;
        }
         .auto-style14 {
            text-align: center;
            overflow-y:auto;
            height:420px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 450px" >
              <div class="auto-style14">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="auto-style13" DataSourceID="odsMisSolicitudesSC" Height="16px" Width="1349px" ForeColor="#333333" HorizontalAlign="Center" Font-Names="Arial">
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
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FF6600" ForeColor="Black" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#FF6600" />
                        </asp:GridView>
                      <br />
                    <asp:ObjectDataSource ID="odsMisSolicitudesSC" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisPagosContratosTableAdapter">
                        <SelectParameters>
                            <asp:SessionParameter Name="idEmpresas" SessionField="Empresa" Type="Decimal" />
                        </SelectParameters>
         </asp:ObjectDataSource>
              </div>
    </div>
</asp:Content>
