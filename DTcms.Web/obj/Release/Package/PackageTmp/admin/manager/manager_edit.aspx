<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.manager_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯管理員</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    //表單驗證
    $(function () {
        $("#form1").validate({
            errorPlacement: function (lable, element) {
                element.ligerTip({ content: lable.html(), appendIdTo: lable });
            },
            success: function (lable) {
                lable.ligerHideTip();
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後台</a>首頁 &gt; 控制桌面 &gt; 管理員管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯管理員資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>管理角色：</th>
                <td>
                    <asp:DropDownList ID="ddlRoleId" runat="server" CssClass="select2 required"/>
                </td>
            </tr>
            <tr>
                <th>帳戶狀態：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">啟用 </asp:ListItem>
                        <asp:ListItem Value="1">禁用 </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>用戶名：</th>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>登入密碼：</th>
                <td><asp:TextBox ID="txtUserPwd" runat="server" CssClass="txtInput normal required" 
                        minlength="2" maxlength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>確認密碼：</th>
                <td><asp:TextBox ID="txtUserPwd1" runat="server" CssClass="txtInput normal required" 
                        minlength="6" maxlength="100" TextMode="Password" equalTo="#txtUserPwd"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>姓名：</th>
                <td><asp:TextBox ID="txtRealName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="30"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>電話：</th>
                <td><asp:TextBox ID="txtTelephone" runat="server" CssClass="txtInput normal" minlength="2" maxlength="30"></asp:TextBox></td>
            </tr>
            <tr>
                <th>郵箱：</th>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtInput normal email" minlength="2" maxlength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" onclick="btnSubmit_Click" /></td>
            </tr>
            </tbody>
        </table>
    </div>
    
</div>
</form>
</body>
</html>
