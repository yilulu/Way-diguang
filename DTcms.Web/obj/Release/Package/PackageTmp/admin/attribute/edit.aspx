<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.admin.attribute.edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯屬性資料</title>
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
    <div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 擴展屬性 &gt; 編輯屬性</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">屬性資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>屬性名稱：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>備註資料：</th>
                <td><asp:TextBox ID="txtRemark" runat="server" maxlength="500" TextMode="MultiLine" CssClass="small required" /></td>
            </tr>
            <tr>
                <th>顯示類型：</th>
                <td>
                    <asp:DropDownList id="ddlType" CssClass="select2 required" runat="server">
                        <asp:ListItem Value="">請選擇類型...</asp:ListItem>
                        <asp:ListItem Value="0">輸入框</asp:ListItem>
                        <asp:ListItem Value="1">下拉清單</asp:ListItem>
                        <asp:ListItem Value="2">單選框</asp:ListItem>
                        <asp:ListItem Value="3">複選框</asp:ListItem>
                    </asp:DropDownList>
                    <label>*填寫屬性值時表示的方式</label>
                </td>
            </tr>
            <tr>
                <th>默認值：</th>
                <td><asp:TextBox ID="txtDefaultValue" runat="server" CssClass="txtInput normal" maxlength="500"></asp:TextBox><label>默認值可用“,”逗號分隔開</label></td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits">99</asp:TextBox><label>*</label></td>
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
