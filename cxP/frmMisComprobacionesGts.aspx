<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="frmMisComprobacionesGts.aspx.vb" Inherits="cxP.frmMisComprobacionesGts" %>
<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         .auto-style13 {
            margin-left: 10px;
            margin-top: 20px;
            width:98%;
        }
        .auto-style8 {
            text-align: center;
            overflow-y:auto;
            height:400px;
        }
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style8">        
        <asp:HiddenField ID="HiddenID" runat="server" />
        <asp:HiddenField ID="HiddenEstatus" runat="server" />
        <asp:Label ID="LabelError" runat="server" Text="Error" Font-Bold="True" ForeColor="#FF3300" Visible="False" Font-Size="X-Large"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsMisComprobacionesGts" HorizontalAlign="Center" CssClass="auto-style13" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="folioComprobacion" HeaderText="Folio de  Comprobación" SortExpression="folioComprobacion" >
        <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
        <ItemStyle Width="100px"  HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="idFolioSolicitud" HeaderText="Folio de Solicitud" SortExpression="idFolioSolicitud" >
        <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
        <ItemStyle HorizontalAlign="Right" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="razonSocial" HeaderText="Beneficiario" SortExpression="razonSocial" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Left" Width="300px" Font-Size="Small" />
        </asp:BoundField>
        <asp:BoundField DataField="decripcion" HeaderText="Descripción" SortExpression="decripcion" >
        <HeaderStyle HorizontalAlign="Center" Width="250px" />
        <ItemStyle HorizontalAlign="Left" Font-Size="Small" />
        </asp:BoundField>
        <asp:BoundField DataField="impDepositado" HeaderText="Importe depositado" SortExpression="impDepositado" DataFormatString="{0:c}">
        <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
        <ItemStyle Width="100px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="totalPagado" HeaderText="Importe comprobado" SortExpression="totalPagado" DataFormatString="{0:c}" >
        <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
        <ItemStyle HorizontalAlign="Right" Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="saldoSolicitud" HeaderText="Importe por comprobar" SortExpression="saldoSolicitud" DataFormatString="{0:c}" >
            <HeaderStyle Font-Names="Arial" HorizontalAlign="Center" Font-Size="Small" />
        <ItemStyle HorizontalAlign="Right" Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="estatus" HeaderText="Estatus" />
        <asp:TemplateField HeaderText="PDF Comprobación">
              <ItemTemplate>
                      <asp:HyperLink Font-Names="Arial" ID="lnkPdf" NavigateUrl='<%# Eval("folioComprobacion", "~/GTS/" & Session.Item("Empresa") & "-" & "{0}.pdf") %>' Target="_blank" runat="server">pdf</asp:HyperLink>
              </ItemTemplate>
              <HeaderStyle HorizontalAlign="Center" Font-Size="Small" />
              <ItemStyle Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="False">
            <HeaderTemplate>
                 <cc1:BotonEnviar ID="BotonEnviar1" runat="server" Text="Cancelar Solicitud" TextoEnviando="Cancelando..." CommandName="Cancelar" />
                 </HeaderTemplate>
             <ItemTemplate>
                 <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("estatus") %>' CommandName="Select" Text='<%# Eval("folioComprobacion", "{0}") & "|" & Eval("idFolioSolicitud", "{0}") %>' ></asp:LinkButton>
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
            
<asp:ObjectDataSource ID="odsMisComprobacionesGts" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MayorCero_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_MisComprobacionesTableAdapter">
    <SelectParameters>
        <asp:SessionParameter Name="idEmpresa" SessionField="Empresa" Type="Decimal" />
        <asp:SessionParameter Name="usuario" SessionField="Usuario" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<p>
</p>
           </div>
</asp:Content>
