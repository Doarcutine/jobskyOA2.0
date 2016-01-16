<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Judge.aspx.cs" Inherits="Judge" %>
<%@ Register Src="~/acx/JudgeShow.ascx" TagName="JudgeShow" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="classcss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:JudgeShow ID="JudgeShowRepeater" runat="server"/>
    </div>
    <div class="publishjudge">
        <asp:TextBox ID="txtjudge" runat="server" ReadOnly="False" Width="598px" TextMode="MultiLine" Height="100px"></asp:TextBox><br />
        <asp:Button ID="btnpublish" runat="server" Text="发表评论" 
            onclick="btnpublish_Click" />
        <asp:Button ID="btnreturn" runat="server" Text="返回" onclick="btnreturn_Click" />
    </div>
    </form>
</body>
</html>
