<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="url_rewrite_list.aspx.cs" Inherits="DTcms.Web.admin.settings.url_rewrite_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>偽靜態URL替換規則</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 控制桌面 &gt; 偽靜態URL替換規則</div>
    <div id="navtips" class="navtips">管理此頁面需要具備正則表達式知識，否則請不要隨意更改！<a href="javascript:CloseTip('navtips');" class="close">關閉</a></div>
    <div class="line10"></div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <a href="url_rewrite_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加規則</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete');"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="30">選擇</th>
        <th align="left">名稱</th>
        <th width="15%" align="left">URL重寫表達式</th>
        <th width="18%" align="left">正則表達式</th>
        <th width="12%" align="left">源頁面地址</th>
        <th width="18%" align="left">傳輸參數</th>
        <th width="12%" align="left">範本檔</th>
        <th width="6%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hideName" Value='<%#Eval("name")%>' runat="server" /></td>
        <td><%#Eval("name")%></td>
        <td><%#Eval("path")%></td>
        <td><%#Eval("pattern")%></td>
        <td><%#Eval("page")%></td>
        <td><%#Eval("querystring")%></td>
        <td><%#Eval("templet")%></td>
        <td align="center"><a href="url_rewrite_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&name=<%#Eval("name")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
