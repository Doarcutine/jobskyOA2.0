<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BuyGoodsShow.ascx.cs" Inherits="BuyGoodsShow" %>
<link rel="Stylesheet" href="classcss.css" />
<div>
    <asp:Repeater ID="Repeater1" runat="server" >
            <HeaderTemplate>
                <table class="buygoodsshowtb">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="商品编号"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="商品名称"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="购买人"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="商品单价"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="购买数量"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="共计"></asp:Label>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_Name") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_AcName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_Price") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_Count") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyGoods_EachTotal") %>'></asp:Label>
                    </td>                 
                </tr>
            </ItemTemplate>
        </asp:Repeater>
</div>
<div id="JudgeGuide" runat="server" style="margin-left:500px">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        当前第<asp:Label ID="lblCurrentPage" runat="server" Text=""></asp:Label>页/共
        <asp:Label ID="lblTotalPage" runat="server" Text=""></asp:Label>页
        <asp:HyperLink ID="hlFirst" runat="server">首页</asp:HyperLink>
        <asp:HyperLink ID="hlPrevious" runat="server">上一页</asp:HyperLink>
        <asp:HyperLink ID="hlNext" runat="server">下一页</asp:HyperLink>
        <asp:HyperLink ID="hlLast" runat="server">尾页</asp:HyperLink>
        <asp:DropDownList ID="dpPageJump" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btGo" runat="server" Text="跳转" onclick="btGo_Click" />
    </div>