<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_config.aspx.cs" Inherits="DTcms.Web.admin.users.user_config" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>用戶參數配置</title>
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
            invalidHandler: function (e, validator) {
                parent.jsprint("有 " + validator.numberOfInvalids() + " 項填寫錯誤，請檢查！", "", "Warning");
            },
            errorPlacement: function (lable, element) {
                //可見元素顯示錯誤提示
                if (element.parents(".tab_con").css('display') != 'none') {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                }
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
<div class="navigation">首頁 &gt; 用戶管理 &gt; 用戶參數配置</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本參數設置</a></li>
        <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">用戶積分策略</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>新用戶註冊設置：</th>
                <td>
                    <asp:RadioButtonList ID="regstatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">開放註冊 </asp:ListItem>
                        <asp:ListItem Value="0">不允許註冊 </asp:ListItem>
                        <asp:ListItem Value="2">僅通過邀請註冊</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>用戶名保留關鍵字：</th>
                <td><asp:TextBox ID="regkeywords" runat="server" CssClass="txtInput normal" maxlength="500"></asp:TextBox>
                    <label>以英文逗號“,”分隔開</label></td>
            </tr>
            <tr>
                <th>新用戶註冊驗證：</th>
                <td>
                    <asp:RadioButtonList ID="regverify" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">無驗證 </asp:ListItem>
                        <asp:ListItem Value="1">Email驗證 </asp:ListItem>
                        <asp:ListItem Value="2">人工審核</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>IP註冊時間限制：</th>
                <td><asp:TextBox ID="regctrl" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>小時<label>*同一IP的註冊間隔，0為不限制。</label></td>
            </tr>
            <tr>
                <th>Email驗證請求有效期：</th>
                <td><asp:TextBox ID="regemailexpired" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>天<label>*新用戶註冊Email驗證有效期，0為不限制。</label></td>
            </tr>
            <tr>
                <th>同一Email註冊不同用戶：</th>
                <td>
                    <asp:RadioButtonList ID="regemailditto" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">允許</asp:ListItem>
                        <asp:ListItem Value="0">不允許 </asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>允許Email登入：</th>
                <td>
                    <asp:RadioButtonList ID="emaillogin" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">允許</asp:ListItem>
                        <asp:ListItem Value="0">不允許 </asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>註冊許可協議：</th>
                <td>
                    <asp:RadioButtonList ID="regrules" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">關閉 </asp:ListItem>
                        <asp:ListItem Value="1">開啟</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>許可協議內容：</th>
                <td><asp:TextBox ID="regrulestxt" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                    <label>支援HTML格式</label></td>
            </tr>
            <tr>
                <th>註冊歡迎訊息：</th>
                <td>
                    <asp:RadioButtonList ID="regmsgstatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">關閉 </asp:ListItem>
                        <asp:ListItem Value="1">開啟</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>歡迎訊息內容：</th>
                <td><asp:TextBox ID="regmsgtxt" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox><label>支援HTML格式</label></td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="tab_con">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>邀請碼使用期限：</th>
                <td><asp:TextBox ID="invitecodeexpired" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>天<label>*邀請碼有效天數，0為不限制。</label></td>
            </tr>
            <tr>
                <th>邀請碼可使用次數：</th>
                <td><asp:TextBox ID="invitecodecount" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>次<label>*邀請碼使用次數，0為不限制。</label></td>
            </tr>
            <tr>
                <th>每天可申請邀請碼數量：</th>
                <td><asp:TextBox ID="invitecodenum" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>個<label>*每天可申請邀請碼數量，0為不限制。</label></td>
            </tr>
            <tr>
                <th>現金/積分兌換比例：</th>
                <td><asp:TextBox ID="pointcashrate" runat="server" CssClass="txtInput small required number" maxlength="10">0</asp:TextBox>個<label>*1元等於多少個積分，0為禁用兌換功能。</label></td>
            </tr>
            <tr>
                <th>邀請註冊獲得積分：</th>
                <td><asp:TextBox ID="pointinvitenum" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>分<label>*邀請一個註冊成功用戶所獲得的積分。</label></td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="foot_btn_box">
    <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" onclick="btnSubmit_Click" />
    &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
    </div>
</div>
</form>
</body>
</html>
