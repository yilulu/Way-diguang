<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_list.aspx.cs" Inherits="DTcms.Web.admin.orders.payment_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>付款方式清單</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 銷售管理 &gt; 付款方式清單</div>
    <div id="navtips" class="navtips">請設置正確的線上付款API的帳戶資料，否則無法正常收款！<a href="javascript:CloseTip('navtips');" class="close">關閉</a></div>
    <div class="line10"></div>
    <!--列表展示.開始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">序號</th>
        <th width="15%" align="left">名稱</th>
        <th width="15%">圖標</th>
        <th align="left">付款描述</th>
        <th width="8%">排序</th>
        <th width="8%">狀態</th>
        <th width="6%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><%#Eval("id")%></td>
        <td><%#Eval("title")%></td>
        <td align="center"><img width="125" height="45" src="<%#Eval("img_url")%>" /></td>
        <td><%#Eval("remark")%></td>
        <td align="center"><%#Eval("sort_id")%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 1 ? "已停用" : "正常"%></td>
        <td align="center"><a href="payment_edit.aspx?id=<%#Eval("id")%>">配置</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暫無記錄</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.結束-->

    <div class="line10"></div>
</form>
</body>
</html>
