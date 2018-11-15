<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.nav_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯導航</title>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後台</a>首頁 &gt; 控制桌面 &gt; 導航管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯導航資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
             <tr>
                <th>導航名稱：</th>
                <td><asp:TextBox ID="txtNavTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr> 
            <tr>
                <th>連結：</th>
                <td><asp:TextBox ID="txtNavUrl" runat="server" CssClass="txtInput normal required" 
                        minlength="2" maxlength="30" Width="356px"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>狀態：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsState" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">啟用 </asp:ListItem>
                        <asp:ListItem Value="1">禁用 </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
           
          
            <tr>
                <th>排序：</th>
                <td><asp:TextBox ID="txtNavSequence" runat="server" CssClass="txtInput normal small required digits" 
                      Width="51px"></asp:TextBox></td>
            </tr>
            <tr>
                <th>描述：</th>
                <td><asp:TextBox ID="txtNavDesc" runat="server" CssClass="txtInput normal " 
                         Height="62px" 
                        TextMode="MultiLine" Width="508px"></asp:TextBox></td>
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
