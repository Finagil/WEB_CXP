<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="aut2TwoPrvCtas.aspx.vb" Inherits="cxP.aut2TwoPrv" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          <link href="styFW.css" rel="stylesheet" type="text/css" />
        <div>
            <table style="margin-left:20%; margin-top:5%; height:60%; border: 1px solid #000000; width:60%; font-family:Verdana;border-radius:5px;">
                <tr>
                    <td style="background-color: #FFE0C0; font-weight:600;" class="auto-style1" colspan="2">
                        Solicitud de alta de cuenta bancaria para el sigueinte proveedor:<asp:ObjectDataSource ID="odsCuentasBancarias" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtieneDatos_GetDataBy" TypeName="cxP.dsProduccionTableAdapters.Vw_CXP_CuentasBancariasProvTableAdapter">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="idCuentas" QueryStringField="idCuentas" Type="Decimal" />
                                <asp:QueryStringParameter DefaultValue="0" Name="archivo1" QueryStringField="archivo1" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFE0C0">
                        Razón Social:
                    </td>
                     <td style="background-color: #FFE0C0">
                         <asp:FormView ID="FormView1" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("razonSocialP") %>
                                </ItemTemplate>
                            </asp:FormView>

                     </td>
                </tr>
                <tr>
                    <td>
                        RFC:
                    </td>
                    <td>
                       <asp:FormView ID="FormView2" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("rfcP") %>
                                </ItemTemplate>
                            </asp:FormView>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #FFE0C0">
                        Banco:
                    </td>
                     <td style="background-color: #FFE0C0">
                        <asp:FormView ID="FormView3" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("nombreCorto") %>
                                </ItemTemplate>
                            </asp:FormView>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style2">
                        Descripción:</td>
                    <td class="auto-style2">
                         <asp:FormView ID="FormView4" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("descripcion") %>
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                </tr>
                 <tr>
                   <td style="background-color: #FFE0C0">
                        Moneda:</td>
                    <td style="background-color: #FFE0C0">
                     <asp:FormView ID="FormView5" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("c_NombreMoneda") %>
                                </ItemTemplate>
                            </asp:FormView>    
                    </td>
                </tr>
                 <tr>
                    <td>
                        Cuenta:</td>
                    <td>
                         <asp:FormView ID="FormView6" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("cuenta") %>
                                </ItemTemplate>
                            </asp:FormView>

                    </td>
                </tr>
                 <tr>
                     <td style="background-color: #FFE0C0">
                        CLABE:</td>
                    <td style="background-color: #FFE0C0">
                     <asp:FormView ID="FormView7" runat="server" DataSourceID="odsCuentasBancarias" EnableViewState="False">
                                <ItemTemplate>
                                    <%# Eval("clabe") %>
                                </ItemTemplate>
                            </asp:FormView>    
                    </td>
                </tr>
                 <tr>
                    <td>
                        Comentarios:</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="auto-style1" colspan="2" style="background-color: #FFE0C0">
                        <cc1:BotonEnviar ID="btnAutorizar" runat="server" Text="Autorizar" CssClass="Botones" />
&nbsp;&nbsp;&nbsp;
                        <cc1:BotonEnviar ID="btnRechazar" runat="server" Text="Rechazar"  CssClass="Botones"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
