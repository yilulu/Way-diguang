<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usermenu.ascx.cs" Inherits="DTcms.Web.usermenu" %>
<div id="vip_3_1">
    <a href="user.aspx"><span class="vip_3_1_1">會員中心</span></a> <a href="userinfo.aspx">
        <span class="vip_3_1_2">個人資料</span></a> <a href="OrderList.aspx"><span class="vip_3_1_2">
            消費記錄</span></a> <a href="pointList.aspx"><span class="vip_3_1_2">積點記錄</span></a>
    <%if (Common.WEBUserCurrent.UserType == "5")
      {%>
    <a href="user.aspx"><span class="vip_3_1_2">我發佈的消息</span></a>
    <%} %>
    <a href="userorder.aspx"><span class="vip_3_1_2">我的訂單</span></a> <a href="Notebook.aspx">
        <span class="vip_3_1_2">留言諮詢</span></a>
</div>
