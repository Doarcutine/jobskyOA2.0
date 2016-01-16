<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyGoods.aspx.cs" Inherits="BuyGoods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="classcss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="head">
        <asp:Label ID="Label7" runat="server" Text="欢迎您，"></asp:Label>
        <asp:Label ID="lblname" runat="server" Text="<%#myname %>"></asp:Label>
        <asp:Button ID="btncancel" runat="server" Text="注销" onclick="btncancel_Click" />      
        <div class="head2">
            <asp:Button ID="btnmemory" runat="server" Text="购买记录" 
                onclick="btnmemory_Click" />
            <asp:Button ID="btnsolu" runat="server" Text="去结算" onclick="btnsolu_Click" />
            <asp:Button ID="btnupgoods" runat="server" Text="上架商品" 
                onclick="btnupgoods_Click" />
        </div>
    </div>
    <div class="BuyGoodsRp">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"Goods_Image") %>' Height="200px" Width="200px" />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="商品编号："></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Goods_ID") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="商品名称："></asp:Label>
                                <asp:Label ID="lblgoodsname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Goods_Name") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="商品单价："></asp:Label>
                                <asp:Label ID="lblgoodsprice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Goods_Price") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="商品数量："></asp:Label>
                                <asp:Label ID="lblgoddscount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Goods_Count") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="上架日期："></asp:Label>
                                <asp:Label ID="lblgoodsuptime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Goods_UpTime") %>'></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="请输入购买数量："></asp:Label>
        <asp:TextBox ID="txtBuyCount" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnAddCar" runat="server" Text="放入购物车" 
            onclick="btnAddCar_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
