<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JudgeShow.ascx.cs" Inherits="JudgeShow" %>
<link rel="Stylesheet" href="classcss.css" />
    <div class="judgerepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="rpall">
                    <div class="rphead">
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Person_Name") %>'></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Judge_Time") %>'></asp:Label>
                    </div>
                    <div class="rpcontent">
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Judge_Content") %>'></asp:Label>
                    </div>
                </div>
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