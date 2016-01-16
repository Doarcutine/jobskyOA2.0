<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="账号："></asp:Label><asp:TextBox ID="txtAccount"
            runat="server" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode>=65&&event.keyCode<=90||event.keyCode>=97&&event.keyCode<=122" MaxLength="10"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="昵称："></asp:Label><asp:TextBox ID="txtName"
            runat="server" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode>=65&&event.keyCode<=90||event.keyCode>=97&&event.keyCode<=122" MaxLength="10"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="密码："></asp:Label><asp:TextBox ID="txtPassword"
            runat="server" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode>=65&&event.keyCode<=90||event.keyCode>=97&&event.keyCode<=122" MaxLength="20"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="确认密码："></asp:Label><asp:TextBox ID="txtConfirmps"
            runat="server" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode>=65&&event.keyCode<=90||event.keyCode>=97&&event.keyCode<=122" MaxLength="20"></asp:TextBox><br />
        <asp:Button ID="btnRegister" runat="server" Text="注册" 
            onclick="btnRegister_Click" />
        <asp:Button ID="btnReturn"
            runat="server" Text="返回" onclick="btnReturn_Click" /><asp:Label ID="lblMessage" runat="server"
                Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
