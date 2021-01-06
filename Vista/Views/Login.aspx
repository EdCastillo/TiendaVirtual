<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vista.Views.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="usernameTB" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:TextBox ID="passwordTB" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="TryLogin" />
        </p>
    </form>
</body>
</html>
