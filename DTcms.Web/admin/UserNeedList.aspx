<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserNeedList.aspx.cs" Inherits="DTcms.Web.admin.UserNeedList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        首頁 &gt; 市場需求管理-市場需求列表</div>
    <div class="tools_box">
        <div class="tools_bar">
            <a href="needlist.aspx?typ=add" class="tools_btn"><span><b class="add">添加市場需求</b></span></a>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClick="btnDelete_Click"
                OnClientClick="return ExePostBack('btnDelete','您確定要刪除嗎？');"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        選擇
                    </th>
                    <th>
                        買/租
                    </th>
                    <th>
                        地區
                    </th>
                    <th>
                        類型
                    </th>
                    <th>
                        坪數
                    </th>
                    <th>
                        備註
                    </th>
                    <th>
                        添加時間
                    </th>
                    <th>
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
                    <%#Eval("typeName")%>
                </td>
                <td align="center">
                    <%#Eval("AreaName")%>
                </td>
                <td align="center">
                    <%#Eval("CataName")%>
                </td>
                <td align="center">
                    <%#Eval("MianJi")%>
                </td>
                <td align="center">
                    <%#Eval("spec")%>
                </td>
                <td align="center">
                    <%#Eval("AddTime") %>
                </td>
                <td align="center">
                    <a href="needlist.aspx?typ=mod&ID=<%#Eval("ID") %>">編輯</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暫無記錄</td></tr>" : ""%>
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
