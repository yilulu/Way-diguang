<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="link_edit.aspx.cs" Inherits="DTcms.Web.Plugin.Link.admin.link_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯友情連結</title>
<link href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../../../<%=siteConfig.webmanagepath %>/images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../../scripts/jquery/jquery.form.js"></script>
<script type="text/javascript" src="../../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../../../<%=siteConfig.webmanagepath %>/js/function.js"></script>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 外掛程式管理 &gt; 友情連結</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯友情連結</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>標 題：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="255"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>審核狀態：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">已審核 </asp:ListItem>
                        <asp:ListItem Value="1">待審核</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>推薦到首頁：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsRed" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">不推薦 </asp:ListItem>
                        <asp:ListItem Value="1">推薦</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits">99</asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>連絡人：</th>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal" maxlength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <th>聯繫電話：</th>
                <td><asp:TextBox ID="txtUserTel" runat="server" CssClass="txtInput normal" maxlength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <th>電子郵箱：</th>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtInput normal email" maxlength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <th>連結網址：</th>
                <td><asp:TextBox ID="txtSiteUrl" runat="server" CssClass="txtInput normal required" maxlength="255"></asp:TextBox><label>URL地址</label></td>
            </tr>
            <tr>
                <th>Logo圖片：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="255"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload', 0, 0, '../../../');" /></a>
                    <span class="uploading">正在上傳，請稍候...</span>
                </td>
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
