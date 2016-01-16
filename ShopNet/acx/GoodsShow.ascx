<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoodsShow.ascx.cs" Inherits="GoodsShow" %>
<link rel="Stylesheet" href="classcss.css" />
<div class="goodsrepeater">
    <ul class="rpul">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <li>
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
                    <a href="BuyGoods.aspx?Goods_ID=<%#Eval("Goods_ID") %>" target="_blank">放入购物车</a>
                    <a href="Judge.aspx?Goods_ID=<%#Eval("Goods_ID") %>" target="_blank">查看评论</a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
<div id="Guide" runat="server" style="height:30px;width:100%;">
    
    当前第<asp:Label ID="lblNowPage" runat="server" Text=""></asp:Label>页/共<asp:Label ID="lblTotalPage"
        runat="server" Text=""></asp:Label>页
    <asp:HyperLink ID="hlFirst" runat="server">首页</asp:HyperLink>
    <asp:HyperLink ID="hlPrevious" runat="server">上一页</asp:HyperLink>
    <asp:HyperLink ID="hlNext" runat="server">下一页</asp:HyperLink>
    <asp:HyperLink ID="hlLast" runat="server">尾页</asp:HyperLink>
    <asp:DropDownList ID="dpPageJump" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btGo" runat="server" Text="跳转" onclick="btGo_Click" />
</div>