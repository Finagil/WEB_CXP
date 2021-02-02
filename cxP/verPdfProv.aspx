<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="verPdfProv.aspx.vb" Inherits="cxP.verPdfProv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="nombre_hoja" rel="stylesheet" type="text/css" media="print"/>
     <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="<%= ResolveUrl("~/js/jquery-1.4.1.min.js") %>"></script>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
        .nover{
            visibility:hidden;
        }
    </style>

    <script type='text/javascript'>
    $(function(){
    $(document).bind("contextmenu",function(e){
        return false;
    });
});
</script>
</head>
<body>
   
    <form id="form1" runat="server">
              
    </form>
    <script type='text/javascript'>
        document.oncontextmenu = function ()
        { return false }
    </script>
</body>
</html>
