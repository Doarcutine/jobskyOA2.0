<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinishDeal.aspx.cs" Inherits="FinishDeal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="classcss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemcommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                <table class="finishdealtb">
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
                        <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TPGoods_ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TP_Name") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TP_AcName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TP_Price") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TP_Count") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TP_EachTotal") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"TP_ID")%>'>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Button ID="btnPay" runat="server" Text="购买" onclick="btnPay_Click" />
        <asp:Button ID="btnClear" runat="server" Text="清空购物车" 
            onclick="btnClear_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />
    </div>
    </form>
</body>
</html>
