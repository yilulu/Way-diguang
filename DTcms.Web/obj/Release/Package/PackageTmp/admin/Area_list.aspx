<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Area_list.aspx.cs" Inherits="DTcms.Web.admin.Area_list" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>系統模型清單</title>
    <link type="text/css" rel="stylesheet" href="../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 控制桌面 &gt; 縣市鄉鎮</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <%-- 類型：
             <asp:DropDownList id="ddlTypleID" CssClass="select2 required" runat="server" AutoPostBack="false">
                <asp:ListItem Value="1">區域</asp:ListItem>
                <asp:ListItem Value="2">總價</asp:ListItem>
                <asp:ListItem Value="3">面積</asp:ListItem>
                <asp:ListItem Value="4">戶型</asp:ListItem>
                <asp:ListItem Value="5">方式</asp:ListItem>
                <asp:ListItem Value="6">捷運沿線</asp:ListItem>
                </asp:DropDownList>&nbsp;--%>
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="Area_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&pid=<%=pid %>" class="tools_btn">
                <span><b class="add">添加</b></span></a> <a href="javascript:void(0);" onclick="checkAll(this);"
                    class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClick="btnDelete_Click"
                OnClientClick="return ExePostBack('btnDelete','確定要刪除嗎？');"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        選擇
                    </th>
                    <th align="left">
                        上一級
                    </th>
                    <th align="12%">
                        標題
                    </th>
                    <th align="12%">
                        代碼
                    </th>
                    <th width="12%">
                        排序
                    </th>
                    <th width="20%">
                        操作
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId"
                        Value='<%#Eval("id")%>' runat="server" />
                </td>
                <td align="center">
                    <%#Eval("parent")%>
                </td>
                <td>
                    <a href="Area_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        <%#Eval("title")%></a>
                </td>
                <td align="center">
                    <%#Eval("code")%>
                </td>
                <td align="center">
                    <%#Eval("sort")%>
                </td>
                <td align="center">
                    <a href="Area_edit.aspx?action=<%#DTEnums.ActionEnum.Add %>&pid=<%#Eval("parent")%>">
                        增加子級</a> <a href="Area_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                            修改</a> <a href="Area_list.aspx?pid=<%#Eval("id")%>">
                                <%#GetPath(Eval("id").ToString())%></a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="line15">
    </div>
    <div class="page_box">
        <div id="PageContent" runat="server" class="flickr right">
        </div>
        <div class="left">
            顯示<asp:TextBox ID="txtPageNum" runat="server" CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));"
                OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox>條/頁
        </div>
    </div>
    <div class="line10">
    </div>
    </form>
</body>
</html>
