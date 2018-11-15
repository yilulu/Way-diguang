<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oauth_edit.aspx.cs" Inherits="DTcms.Web.admin.users.oauth_edit" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯OAuth資料</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; OAuth管理 &gt; 編輯OAuth資料</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯OAuth資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>標題：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>啟用狀態：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">啟用</asp:ListItem>
                        <asp:ListItem Value="1">禁用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>排序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits" maxlength="10">99</asp:TextBox><label>*整數數字，越小越靠前。</label></td>
            </tr>
            <tr>
                <th>API目錄：</th>
                <td><asp:TextBox ID="txtApiPath" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*API目錄必須存在，存放站點api/oaut目錄下</label></td>
            </tr>
            <tr>
                <th>API Key：</th>
                <td><asp:TextBox ID="txtAppId" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*QQ互聯請填寫APP ID</label></td>
            </tr>
            <tr>
                <th>Secret Key：</th>
                <td><asp:TextBox ID="txtAppKey" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*QQ互聯請填寫KEY</label></td>
            </tr>
            <tr>
                <th>顯示圖標：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="500"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                    <span class="uploading">正在上傳，請稍候...</span>
                </td>
            </tr>
            <tr>
                <th>描述：</th>
                <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                    <label>支援HTML格式</label></td>
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
