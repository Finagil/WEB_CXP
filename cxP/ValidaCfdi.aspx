<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Finagil.Master" CodeBehind="ValidaCfdi.aspx.vb" Inherits="cxP.ValidaCfdi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Validación de CFDI</title>
    <style type="text/css">
        .auto-style7 {
            height:100%;
        }
        .auto-style8 {
            width: 100%;
            height: 205px;
        }
        .auto-style14 {
            width: 130px;
            height: 198px;
        }
        .auto-style15 {
            width: 996px;
            height: 198px;
            text-align: center;
        }
        .auto-style16 {
            height: 198px;
        }
        .auto-style17 {
            width: 130px;
            height: 116px;
            text-align: center;
        }
        .auto-style18 {
            width: 996px;
            height: 116px;
            text-align: center;
        }
        </style>
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="styFW.css" rel="stylesheet" type="text/css" />
    <div class="auto-style7" >
        <table class="auto-style8" style="font-size: x-small; font-family: Arial">
            <tr>
                <td class="auto-style14"></td>
                <td class="auto-style15">Archivo XML:<br />
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" accept=".xml" multiple="multiple" AllowMultiple="true" CssClass="Botones"/>
                    <br />

                    <br />
                    Archivo PDF:<br />
                    <br />
                    <asp:FileUpload ID="FileUpload2" runat="server" accept=".pdf" multiple="multiple" AllowMultiple="true" CssClass="Botones"/>
                    <br />

                    <br />

                    <br />
                    <asp:Button ID="btnValidar" runat="server" CssClass="Botones" Text="Validar" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="505px">
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
                <td class="auto-style16"></td>
            </tr>
            <tr>
                <td class="auto-style17"></td>
                <td class="auto-style18" style="font-family: Arial; font-size: medium; font-weight: bold; color: #000000">
                    <div>
                        &nbsp;&nbsp;<asp:Label ID="lblError" runat="server" Text="Error" Font-Size="Smaller" ForeColor="#F58220" Height="59px" Visible="False" Width="1000px"></asp:Label>
                    </div>
            </tr>
        </table>
    </div>
</asp:Content>
