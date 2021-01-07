<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ViewsTiendaVirtual.Views.Login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="UsernameTB" runat="server"></asp:TextBox>
        </div>
        <asp:TextBox ID="PasswordTB" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="FailureText" runat="server" Text="Label"></asp:Label>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="LoginBtn" />
    </form>
</body>
</html>
