<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_edit.aspx.cs" Inherits="DTcms.Web.admin.orders.payment_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯付款方式</title>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 銷售管理 &gt; 編輯付款方式</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯付款方式</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>付款名稱：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>付款類型：</th>
                <td>
                    <asp:RadioButtonList ID="rblType" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">線上 </asp:ListItem>
                        <asp:ListItem Value="2">線下</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
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
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits" maxlength="10">99</asp:TextBox><label>*整形數字，越小越向前。</label></td>
            </tr>
            <tr>
                <th>手續費類型：</th>
                <td>
                    <asp:RadioButtonList ID="rblPoundageType" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">百分比 </asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">固定金額</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*選擇百分比的計算公式：商品總金額+(商品總金額*百分比)+配送費用=訂單總金額</label>
                </td>
            </tr>
            <tr>
                <th>付款手續費：</th>
                <td><asp:TextBox ID="txtPoundageAmount" runat="server" CssClass="txtInput normal small required number">0</asp:TextBox><label>*注意：百分比取值範圍：0-100，固定金額單位為“元”</label></td>
            </tr>
            <%if (model.api_path.ToLower() == "alipay")
              { %>
            <tr>
                <th>付款寶帳號：</th>
                <td><asp:TextBox ID="txtAlipaySellerEmail" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*簽約付款寶帳號或賣家付款寶帳號</label></td>
            </tr>
            <tr>
                <th>合作者身份(partner ID)：</th>
                <td><asp:TextBox ID="txtAlipayPartner" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*合作身份者ID，以2088開頭由16位純數字組成的字串</label></td>
            </tr>
            <tr>
                <th>交易安全校驗碼(key)：</th>
                <td><asp:TextBox ID="txtAlipayKey" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*交易安全校驗碼，由數字和字母組成的32位元字串</label></td>
            </tr>
            <%}
              else if (model.api_path.ToLower() == "tenpay")
              {%>
            <tr>
                <th>財付通商戶號：</th>
                <td><asp:TextBox ID="txtTenpayBargainorId" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>財付通密鑰：</th>
                <td><asp:TextBox ID="txtTenpayKey" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*</label></td>
            </tr>
            <%} %>
            <tr>
                <th>顯示圖標：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="255"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                    <span class="uploading">正在上傳，請稍候...</span>
                </td>
            </tr>
            <tr>
                <th>描述：</th>
                <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                    <label>支援HTML格式，限500字元</label></td>
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
