<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="ValidaCfdi.aspx.vb" Inherits="cxP.ValidaCfdi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Validación de CFDI</title>
    <style type="text/css">
        .auto-style12 {
            text-align: center;
        }
        </style>
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="styFW.css" rel="stylesheet" type="text/css" />
    <div style="overflow-x:auto;">
        <table id="tablaValidaCFDI" runat="server" style="border-radius:5px;width:60%;height:60%;margin:5% auto auto auto;font-family:Verdana;font-size:small;font-weight:600;color:navy;">
            <tr>
                <td class="auto-style12">
                    Archivo XML:</td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <asp:FileUpload ID="FileUpload1" runat="server" accept=".xml" multiple="multiple" AllowMultiple="true" CssClass="Botones"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">
                    Archivo PDF:</td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <asp:FileUpload ID="FileUpload2" runat="server" accept=".pdf" multiple="multiple" AllowMultiple="true" CssClass="Botones"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <asp:Button ID="btnValidar" runat="server" CssClass="Botones" Text="Validar" />
                </td>
            </tr>
            <tr>
                <td class="auto-style20">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="uuid" HeaderText="UUID">
                            <HeaderStyle Font-Size="Larger" />
                            <ItemStyle Wrap="False" Font-Size="Larger" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sat" HeaderText="Estatus SAT" >
                            <HeaderStyle Font-Size="Larger" />
                            <ItemStyle Font-Size="Larger" />
                            </asp:BoundField>
                            <asp:BoundField DataField="xsd" HeaderText="Análisis XSD" >
                            <HeaderStyle Font-Size="Larger" />
                            <ItemStyle Font-Size="Larger" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F58220" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FFCA33" Font-Bold="True" ForeColor="#3336FF" />
            <HeaderStyle BackColor="#F58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#F58220" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style20">
                    <asp:Label ID="lblError" runat="server" Text="Error" Font-Size="Smaller" ForeColor="#F58220"  Visible="False" Width="1000px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
