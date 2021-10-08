<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cargararchivo.aspx.cs" Inherits="Farmacia.cargararchivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:FileUpload ID="FileUpload1" runat="server" />

        <asp:Button ID="Button1" runat="server" Text="Upload"
            OnClick="Button1_Click" />
    </form>
</body>
</html>
