<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_edit.aspx.cs" Inherits="DTcms.Web.admin.users.user_edit"
    ValidateRequest="false" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯會員資料</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script language="javascript" type="text/javascript" src="../js/getdate/WdatePicker.js"></script>
    <script type="text/javascript">
        //表單驗證
        $(function () {
            $("#form1").validate({
                invalidHandler: function (e, validator) {
                    parent.jsprint("有 " + validator.numberOfInvalids() + "項填寫有誤，請檢查！", "", "Warning");
                },
                errorPlacement: function (lable, element) {
                    //可見元素顯示錯誤提示
                    if (element.parents(".tab_con").css('display') != 'none') {
                        element.ligerTip({ content: lable.html(), appendIdTo: lable });
                    }
                },
                rules: {
                    txtUserName: {
                        required: true,
                        minlength: 2,
                        maxlength: 100,
                        remote: {
                            type: "post",
                            url: "../../tools/admin_ajax.ashx?action=validate_username",
                            data: {
                                username: function () {
                                    return $("#txtUserName").val();
                                },
                                oldusername: function () {
                                    return $("#hidUserName").val();
                                }
                            },
                            dataType: "html",
                            dataFilter: function (data, type) {
                                if (data == "true")
                                    return true;
                                else
                                    return false;
                            }
                        }
                    }
                },
                success: function (lable) {
                    lable.ligerHideTip();
                },
                messages: {
                    txtUserName: {
                        required: "輸入(2-100)字元",
                        minlength: "必須大於2字元",
                        maxlength: "必須大於100字元",
                        remote: "抱歉，該用戶名不可用"
                    }
                }
            });
        });
        function check(v1) {
            if (isNaN(v1) || v1 == 0) {
                alert("請輸入1-99的純數字");
            }
        };
        function chenkManagePwd(v1, v2) {
            $.ajax({
                type: "get",
                url: "chkManagePwd.ashx",
                data: "name=" + v1 + "",
                cache: false,
                timeout: 15000,
                dataType: 'html',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                },
                success: function (result) {
                    if (result == 0) {
                        alert("" + v2 + "主管碼輸入錯誤，點數不會被修改，請檢查");
                    } else {
                        alert("主管碼輸入正確");
                    }
                }
            });
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 會員管理 &gt; 編輯會員資料</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本資料</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:;">帳戶資料</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="150px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            所屬組別：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlGroupId" CssClass="select2" runat="server">
                                <%-- <asp:ListItem Value="0">請選擇類別...</asp:ListItem>
                                <asp:ListItem Value="1">普通卡</asp:ListItem>
                                <asp:ListItem Value="2">尊榮卡</asp:ListItem>
                                <asp:ListItem Value="3">柏金卡</asp:ListItem>
                                <asp:ListItem Value="4">御皇卡</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            用戶狀態：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">正常</asp:ListItem>
                                <asp:ListItem Value="1">待驗證</asp:ListItem>
                                <asp:ListItem Value="2">禁用會員</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                                *禁用的會員將無法登入。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            開通VIP：
                        </th>
                        <td>
                            <asp:CheckBox ID="chkVip" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            用戶名：
                        </th>
                        <td>
                            <asp:HiddenField ID="hidUserName" runat="server" Value="" />
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal" minlength="2"
                                MaxLength="100" /><label>*登入的用戶名，支援中文。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密 碼：
                        </th>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtInput normal" minlength="6"
                                MaxLength="100" TextMode="Password"></asp:TextBox><input type="hidden" id="txtPwd"
                                    runat="server" /><label id="lblPwd" runat="server">*登入的密碼，至少6位。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            郵 箱：
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtInput normal required email"
                                MaxLength="100"></asp:TextBox><label>*取回密碼時用到。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            介紹人帳號
                        </th>
                        <td>
                            <asp:Label ID="llbInTroduce" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            介紹售屋物件：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSellHoursePoint" onblur="check(this.value)" runat="server" CssClass="txtInput"
                                Width="50px" MaxLength="100"></asp:TextBox>
                            主管碼：<asp:TextBox ID="txtManagePwd" onblur="chenkManagePwd(this.value,'介紹售屋物件')" TextMode="Password"
                                runat="server" CssClass="txtInput" MaxLength="100"></asp:TextBox>
                            <input id="txtSell" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            介紹租屋物件：
                        </th>
                        <td>
                            <asp:TextBox ID="txtHireHoursePoint" onblur="check(this.value)" runat="server" CssClass="txtInput"
                                Width="50px" MaxLength="100"></asp:TextBox>
                            主管碼：<asp:TextBox ID="TextBox2" runat="server" onblur="chenkManagePwd(this.value,'介紹租屋物件')"
                                TextMode="Password" CssClass="txtInput" MaxLength="100"></asp:TextBox><input id="txtHire"
                                    type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            介紹租賃廣告：
                        </th>
                        <td>
                            <asp:TextBox ID="txtHireAdPoint" onblur="check(this.value)" runat="server" CssClass="txtInput"
                                Width="50px" MaxLength="100"></asp:TextBox>
                            主管碼：<asp:TextBox ID="TextBox4" runat="server" onblur="chenkManagePwd(this.value,'介紹租賃廣告')"
                                TextMode="Password" CssClass="txtInput" MaxLength="100"></asp:TextBox><input id="txtAd"
                                    type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            介紹精品廠商：
                        </th>
                        <td>
                            <asp:TextBox ID="txtHireMacPoint" onblur="check(this.value)" runat="server" CssClass="txtInput"
                                Width="50px" MaxLength="100"></asp:TextBox>
                            主管碼：<asp:TextBox ID="TextBox6" runat="server" onblur="chenkManagePwd(this.value,'介紹精品廠商')"
                                TextMode="Password" CssClass="txtInput" MaxLength="100"></asp:TextBox><input id="txtMac"
                                    type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            暱 稱：
                        </th>
                        <td>
                            <asp:TextBox ID="txtNickName" runat="server" CssClass="txtInput normal required"
                                MaxLength="100"></asp:TextBox><label>*會員的真實姓名或暱稱</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            照 片：
                        </th>
                        <td>
                            <asp:TextBox ID="txtAvatar" runat="server" CssClass="txtInput normal left"></asp:TextBox>
                            <a href="javascript:;" class="files">
                                <input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtAvatar', 'FileUpload');" /></a>
                            <span class="uploading">正在上傳，請稍候...</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性 別：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="保密">保密 </asp:ListItem>
                                <asp:ListItem Value="男">男 </asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            生 日：
                        </th>
                        <td>
                            <asp:TextBox ID="txtBirthday" runat="server" CssClass="txtInput normal" MaxLength="20"
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox><label></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            電 話：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTelphone" runat="server" CssClass="txtInput normal " MaxLength="50"></asp:TextBox><label></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            手 機：
                        </th>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="txtInput normal " MaxLength="20"></asp:TextBox><label></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Skype帳號：
                        </th>
                        <td>
                            <asp:TextBox ID="txtQQ" runat="server" CssClass="txtInput normal " MaxLength="30"></asp:TextBox><label></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            聯繫地址：
                        </th>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txtInput normal " MaxLength="255"></asp:TextBox><label></label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab_con">
            <table class="form_table">
                <col width="150px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            帳戶金額：
                        </th>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtInput small required number"
                                MaxLength="10">0</asp:TextBox>元<label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            帳戶點數：
                        </th>
                        <td>
                            <asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <th>
                            升級經驗值：
                        </th>
                        <td>
                            <asp:TextBox ID="txtExp" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox><label>*根據積分計算得來，與積分不用的是只增不減。</label>
                        </td>
                    </tr>--%>
                    <tr>
                        <th>
                            註冊時間：
                        </th>
                        <td>
                            <asp:Label ID="lblRegTime" Text="-" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            註冊IP：
                        </th>
                        <td>
                            <asp:Label ID="lblRegIP" Text="-" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            最近登入時間：
                        </th>
                        <td>
                            <asp:Label ID="lblLastTime" Text="-" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            最近登入IP：
                        </th>
                        <td>
                            <asp:Label ID="lblLastIP" Text="-" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="foot_btn_box">
            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
            &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
        </div>
    </div>
    </form>
</body>
</html>
