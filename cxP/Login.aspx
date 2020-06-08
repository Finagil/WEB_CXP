<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="cxP.WebForm1" %>

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
            font-family: Arial, Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: large;
            color: #F58220;
        }
        .auto-style3 {
            font-family: Arial;
            font-weight: bold;
            color: #F58220;
        }
        .auto-style4 {
            margin-left: 0px;
        }
    </style>
</head>

<body background="imagenes/Final_1.gif" onload="window.history.fordward();" style="background-size: 100% auto; ">
        <link href="styFW.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
        <div class="auto-style1">
            <br />
            <asp:Image ID="Image1" runat="server" Height="187px" ImageUrl="~/imagenes/logo-small.png" Width="397px" />
            <br />
            <br />
            <span class="auto-style2">SOLICITUD DE PAGOS<br />
            <br />
            Empresa</span><br />
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="auto-style4" DataSourceID="ObjectDataSource1" DataTextField="razonSocial" DataValueField="idEmpresas" Height="23px" Width="153px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <asp:Label ID="LabelUser" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FF6401" Text="Usuario"></asp:Label>
            <br />
            <asp:TextBox ID="txtUsuario" runat="server" Width="150px"></asp:TextBox>
            <br />
            <span class="auto-style3">Contraseña</span><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
            <span class="auto-style3"><br />
            </span>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="cxP.dsProduccionTableAdapters.CXP_EmpresasTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_idEmpresas" Type="Decimal" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="razonSocial" Type="String" />
                    <asp:Parameter Name="rfc" Type="String" />
                    <asp:Parameter Name="calle" Type="String" />
                    <asp:Parameter Name="numeroInterior" Type="String" />
                    <asp:Parameter Name="numeroExterior" Type="String" />
                    <asp:Parameter Name="colonia" Type="String" />
                    <asp:Parameter Name="delegacion" Type="String" />
                    <asp:Parameter Name="estado" Type="String" />
                    <asp:Parameter Name="pais" Type="String" />
                    <asp:Parameter Name="codigoPostal" Type="String" />
                    <asp:Parameter Name="condicionesDePago" Type="String" />
                    <asp:Parameter Name="idMoneda" Type="String" />
                    <asp:Parameter Name="idRegimenFiscal" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="razonSocial" Type="String" />
                    <asp:Parameter Name="rfc" Type="String" />
                    <asp:Parameter Name="calle" Type="String" />
                    <asp:Parameter Name="numeroInterior" Type="String" />
                    <asp:Parameter Name="numeroExterior" Type="String" />
                    <asp:Parameter Name="colonia" Type="String" />
                    <asp:Parameter Name="delegacion" Type="String" />
                    <asp:Parameter Name="estado" Type="String" />
                    <asp:Parameter Name="pais" Type="String" />
                    <asp:Parameter Name="codigoPostal" Type="String" />
                    <asp:Parameter Name="condicionesDePago" Type="String" />
                    <asp:Parameter Name="idMoneda" Type="String" />
                    <asp:Parameter Name="idRegimenFiscal" Type="String" />
                    <asp:Parameter Name="Original_idEmpresas" Type="Decimal" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <br />
            <br />
            <asp:Button ID="btnEntrar" runat="server" CssClass="Botones" Text="Entrar" />
            <br />
            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="XX-Large" ForeColor="#FF6401" Width="640px"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
