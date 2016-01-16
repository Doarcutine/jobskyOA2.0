<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/acx/GoodsShow.ascx" TagName="GoodsShow" TagPrefix="gs1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="classcss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="head">
        <asp:Label ID="Label1" runat="server" Text="欢迎您，"></asp:Label>
        <asp:Label ID="lblname" runat="server" Text='<%#myname %>'></asp:Label><%=myname %>
        <asp:Button ID="btncancel" runat="server" Text="注销" onclick="btncancel_Click" />      
        <div class="head2">
            <asp:Button ID="btnmemory" runat="server" Text="购买记录" 
                onclick="btnmemory_Click" />
            <asp:Button ID="btnsolu" runat="server" Text="去结算" onclick="btnsolu_Click" />
            <asp:Button ID="btnupgoods" runat="server" Text="上架商品" 
                onclick="btnupgoods_Click" />
        </div>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="请输入搜索内容："></asp:Label>
        <asp:TextBox ID="txtSearchInfo" runat="server"></asp:TextBox>
        <asp:DropDownList ID="dpSearchGoods" runat="server">
            <asp:ListItem Value="0">按编号搜索</asp:ListItem>
            <asp:ListItem Value="1">按商品名搜索</asp:ListItem>
            <asp:ListItem Value="2">按上架日期搜索</asp:ListItem>
        </asp:DropDownList>      
        <asp:Button ID="btSearch" runat="server" Text="搜索" onclick="btSearch_Click" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
        <div id="goodsshow" runat="server">
            <gs1:GoodsShow ID="GoodsShow1" runat="server" />
        </div>     
    </form>
</body>
</html>
