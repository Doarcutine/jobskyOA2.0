<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddGoods.aspx.cs" Inherits="AddGoods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblname" runat="server" Text="商品名称："></asp:Label>
        <asp:TextBox ID="txtname" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblprice" runat="server" Text="商品价格："></asp:Label>
        <asp:TextBox ID="txtprice" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblcount" runat="server" Text="商品数量："></asp:Label>
        <asp:TextBox ID="txtcount" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblpic" runat="server" Text="商品图片："></asp:Label>
        <asp:FileUpload ID="GoodsPicUpLoad" runat="server" />
        <asp:Button ID="btbup" runat="server"
            Text="上传" onclick="btbup_Click" /><asp:Label ID="lblPicPath" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="btnadd" runat="server" Text="添加" onclick="btnadd_Click" /><asp:Label
            ID="lblmessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
