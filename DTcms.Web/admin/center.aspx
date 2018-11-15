<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="center.aspx.cs" Inherits="DTcms.Web.admin.center" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>帝光房屋後台管理首頁</title>
<link href="images/style.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation nav_icon">您好，<i><%=admin_info.user_name %>(<%=new DTcms.BLL.manager_role().GetTitle(admin_info.role_id) %>)</i>，歡迎進入後台管理中心</div>
<div class="line10"></div>
<div class="nlist1">
	<ul>
    	<li>本次登入IP：<asp:Literal ID="litIP" runat="server" Text="-" /></li>
        <li>上次登入IP：<asp:Literal ID="litBackIP" runat="server" Text="-" /></li>
        <li>上次登入時間：<asp:Literal ID="litBackTime" runat="server" Text="-" /></li>
    </ul>
</div>

<div class="line10"></div>
<div class="nlist2 clearfix">
    <h2>站點信息</h2>
    <ul>
    	<li>站點名稱：<%=siteConfig.webname %></li>
        <li>公司名稱：<%=siteConfig.webcompany %></li>
        <li>網站功能變數名稱：<%=siteConfig.weburl %></li>
        <li>安裝目錄：<%=siteConfig.webpath %></li>
        <li>網站管理目錄：<%=siteConfig.webmanagepath %></li>
        <li>附件上傳目錄：<%=siteConfig.attachpath %></li>
        <li>伺服器名稱：<%=Server.MachineName%> </li>
        <li>伺服器IP：<%=Request.ServerVariables["LOCAL_ADDR"] %></li>
        <li>NET框架版本：<%=Environment.Version.ToString()%></li>
        <li>操作系統：<%=Environment.OSVersion.ToString()%></li>
        <li>IIS環境：<%=Request.ServerVariables["SERVER_SOFTWARE"]%></li>
        <li>伺服器埠：<%=Request.ServerVariables["SERVER_PORT"]%></li>
        <li>目錄物理路徑：<%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%></li>
       
        </li>
    </ul>
    <div class="line10"></div>
</div>

<div class="clear" style="height:20px;"></div>
<div class="sub_nav_list">
    <h3>建站快捷導航</h3>
    <ul>
        <li><a href="javascript:parent.f_addTab('sys_config','系統參數設置','settings/sys_config.aspx')"><img src="images/icon_setting.png" /><br />參數設置</a></li>
        <li><a href="javascript:parent.f_addTab('sys_channel','系統頻道設置','settings/sys_channel_list.aspx')"><img src="images/icon_channel.png" /><br />頻道設置</a></li>
        <li><a href="javascript:parent.f_addTab('templet_list','系統範本管理','settings/templet_list.aspx')"><img src="images/icon_templet.png" /><br />生成範本</a></li>
      
        <li><a href="javascript:parent.f_addTab('plugin_list','系統外掛程式管理','settings/plugin_list.aspx')"><img src="images/icon_plugin.png" /><br />外掛程式管理</a></li>
        <li><a href="javascript:parent.f_addTab('user_list','會員管理','users/user_list.aspx')"><img src="images/icon_user.png" /><br />會員管理</a></li>
        <li><a href="javascript:parent.f_addTab('manager_list','管理員管理','manager/manager_list.aspx')"><img src="images/icon_manaer.png" /><br />管理員</a></li>
        <li><a href="javascript:parent.f_addTab('manager_log','系統日誌','manager/manager_log.aspx')"><img src="images/icon_log.png" /><br />系統日誌</a></li>
    </ul>
</div>
    


</form>
</body>
</html>
