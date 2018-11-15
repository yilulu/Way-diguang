<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="url_rewrite_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.url_rewrite_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯偽靜態URL替換規則</title>
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
            success: function(lable){
                lable.ligerHideTip();
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後臺</a>首頁 &gt; 控制桌面 &gt; 編輯偽靜態URL替換規則</div>
<div id="navtips" class="navtips">管理此頁面需要具備正則表達式知識，否則請不要隨意更改！<a href="javascript:CloseTip('navtips');" class="close">關閉</a></div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯URL重寫規則</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>名稱：</th>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*唯一標識名稱，不可更改</label></td>
            </tr>
            <tr>
                <th>URL重寫：</th>
                <td><asp:TextBox ID="txtPath" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*URL重寫表達式，如：test/{0}.html</label></td>
            </tr>
            <tr>
                <th>正則表達式：</th>
                <td><asp:TextBox ID="txtPattern" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*匹配重寫的正則表達式，如：test/(\d+).html$</label></td>
            </tr>
            <tr>
                <th>源頁面：</th>
                <td><asp:TextBox ID="txtPage" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>源aspx頁面地址</label></td>
            </tr>
            <tr>
                <th>傳輸參數：</th>
                <td><asp:TextBox ID="txtQueryString" runat="server" CssClass="txtInput normal"></asp:TextBox><label>需要通過URL傳輸參數，如：page=$1</label></td>
            </tr>
            <tr>
                <th>範本檔：</th>
                <td><asp:TextBox ID="txtTemplet" runat="server" CssClass="txtInput normal"></asp:TextBox><label>所對應的範本檔案名稱</label></td>
            </tr>
            <tr>
                <th>所屬頻道：</th>
                <td><asp:TextBox ID="txtChannel" runat="server" CssClass="txtInput normal digits"></asp:TextBox><label>頻道的ID；如果不是頻道，默認0。</label></td>
            </tr>
            <tr>
                <th>頻道類型：</th>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="select2">
                        <asp:ListItem Value="">不屬於任何類型</asp:ListItem>
                        <asp:ListItem Value="index">首頁</asp:ListItem>
                        <asp:ListItem Value="list">列表頁</asp:ListItem>
                        <asp:ListItem Value="detail">詳細頁面</asp:ListItem>
                        <asp:ListItem Value="no">禁用重寫</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>繼承類名：</th>
                <td><asp:TextBox ID="txtInherit" runat="server" CssClass="txtInput normal"></asp:TextBox><label>該頁面所對應的類名</label></td>
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
