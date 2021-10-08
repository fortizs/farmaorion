<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Imprimir.aspx.cs" Inherits="Farmacia.Ventas.Imprimir" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>

        .ImpresionNoConfig
        {
             border-left-color: #F9C800;  
              border-left-style: solid;
              border-left-width: 10px;              
              background-color: #CCC;
              color:#7C5610;
              font-family: 'Open Sans',Helvetica Neue,Arial,Helvetica,sans-serif;
              font-size: 17px;
              padding: 15px 13px;
              padding-left:70px;
              margin: 25px 0;              
              
        }

        body {
            font-size:0px;
        }

    </style>
</head>
<body>
    <form id="aspnetForm" runat="server">
    <div class="ImpresionNoConfig">
    
        <asp:Label ID="lblMensaje" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    </div>
    </form>
</body>
</html>
