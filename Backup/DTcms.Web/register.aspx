<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="register.aspx.cs" Inherits="DTcms.Web.WebForm7" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/lang/zh_CN.js"></script>
    <script type="text/javascript">
        //載入編輯器
        $(function () {
            var editor = KindEditor.create('textarea[name="ctl00$ContentPlaceHolder1$note"]', {
                resizeType: 1,
                uploadJson: 'tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: 'tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });

        });
        function valdata() {
            var username = $("#<%=txtusername.ClientID %>").val();
            var pass1 = $("#<%=txtpassword.ClientID %>").val();
            var pass2 = $("#txtpassword2").val();
            var emall = $("#<%=txtemall.ClientID %>").val();
            var regStr = /^[_a-zA-Z0-9\-]+(\.[_a-zA-Z0-9\-]*)*@[a-zA-Z0-9\-]+([\.][a-zA-Z0-9\-]+)+$/;

            if (username == "") {
                alert("請輸入用戶名!");
                return false;
            }
            if (pass1 == "") {
                alert("請輸入密碼!");
                return false;
            }
            if (emall == "") {
                alert("請輸入郵箱!");
                return false;
            }
            else {
                var chk = regStr.test(emall);
                if (!chk) {
                    alert("郵箱格式錯誤!");
                    return false;
                }

            }
            if (pass1 != pass2) {
                alert("重複密碼不一致!");
                return false;
            }
        }

        function select(obj) {
            var i = $(obj).val();
            if (i == 1) {
                $("#zhifuModel").css("display", "none");
            }
            else {
                $("#zhifuModel").css("display", "block");
            }
        }
        function chkUserName(v1) {
            $.ajax({
                type: "get",
                url: "tools/regChk.ashx",
                data: "action=userName&name=" + v1 + "",
                cache: false,
                timeout: 15000,
                dataType: 'html',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                },
                success: function (result) {
                    if (result == 0) {
                        alert("你輸入的帳號" + v1 + "系統中不存在！");
                    }
                }
            });
        }
        function chkRegUserName(v1) {
            $.ajax({
                type: "get",
                url: "tools/regChk.ashx",
                data: "action=userName&name=" + v1 + "",
                cache: false,
                timeout: 15000,
                dataType: 'html',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                },
                success: function (result) {
                    if (result == 1) {
                        alert("你輸入的帳號" + v1 + "系統中已經存在！");
                    }
                }
            });
        }
        function chkUserEmail(v1) {
            $.ajax({
                type: "get",
                url: "tools/regChk.ashx",
                data: "action=Email&name=" + v1 + "",
                cache: false,
                timeout: 15000,
                dataType: 'html',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                },
                success: function (result) {
                    if (result == 1) {
                        alert("你輸入的Email" + v1 + "系統中已經存在！");
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>註冊</div>
    <div id="login_1">
        <span class="login_1_1">個人用戶</span><span class="login_1_2">我已經註冊，現在就<span class="login_1_3"><a
            href="login.aspx">登入</a></span></span></div>
    <div id="login">
        <div id="login_left" style="width: 1000px; margin: o auto; border: 0; background: url(images/login_ys.jpg) no-repeat right 100px;">
            <ul class="login_left1" style="width: 1000px; margin: o auto;">
                <li><span class="login_left_1_1">組別:</span><span class="login_left_1_2">
                    <asp:DropDownList ID="ddlGroup" runat="server" onchange="select(this)">
                        <%-- <asp:ListItem Text="請選擇...." Value="0" />
                        <asp:ListItem Text="普通卡會員" Value="1" />
                        <asp:ListItem Text="尊榮卡會員" Value="2" />
                        <asp:ListItem Text="柏金卡會員" Value="3" />
                        <asp:ListItem Text="御皇卡會員" Value="4" />
                        <asp:ListItem Text="商家會員" Value="5" />--%>
                    </asp:DropDownList>
                </span></li>
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtusername" onblur="chkRegUserName(this.value)" class="inputtext"
                    runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請設定密碼:</span><span class="login_left_1_2"><input
                    name="logins1" type="password" class="inputtext" id="txtpassword" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請確認密碼:</span><span class="login_left_1_2"><input
                    name="logins1" type="password" class="inputtext" id="txtpassword2" />
                </span></li>
                <li><span class="login_left_1_1">姓名:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtName" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">公司名稱:</span><span class="login_left_1_2"><input
                    name="CompanyName" type="text" id="CompanyName" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">介紹人帳號:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" id="txtIntroduce" class="inputtext" runat="server"
                    onblur="chkUserName(this.value)" />
                </span></li>
                <li><span class="login_left_1_1">照片:</span><span class="login_left_1_2"><asp:FileUpload
                    ID="fileUpImage" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" class="inputtext email" onblur="chkUserEmail(this.value)" id="txtemall"
                    runat="server" />
                </span></li>
                <li><span class="login_left_1_1">聯繫手機:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtphone" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">地址:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtAddress" class="inputtext" runat="server" />
                </span></li>
                <li id="zhifuModel"><span class="login_left_1_1">付款方式:</span><span class="login_left_1_2"><cc1:MyDropDownList
                    ID="ddlzhifu" runat="server" Where=" is_lock=0" Table_ID_Name="dt_payment*id*title">
                </cc1:MyDropDownList>
                </span></li>
                <div id="divsj" style="display: none">
                    <%--  <li><span class="login_left_1_1">店名:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="dianming" class="inputtext" runat="server" />
                    </span></li>
                  <li><span class="login_left_1_1">店描述:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="dianmiaoshu" class="inputtext" runat="server" />
                    </span></li>--%>
                    <%-- <li><span class="login_left_1_1">從業:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="congye" class="inputtext" runat="server" />
                    </span></li>--%>
                    <li><span class="login_left_1_1">公司官網:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="gongsi" class="inputtext" runat="server" />
                    </span></li>
                    <%-- <li><span class="login_left_1_1">服務區域:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="fuwuquyu" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">熟悉社區:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="shuxishequ" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">業務特長:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="fuwutechang" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">從業經歷:</span><span class="login_left_1_2">
                        <textarea id="jingli" runat="server" class="inputtext" style="width: 800px; height: 100px;"></textarea>
                    </span></li>
                    <li><span class="login_left_1_1">證書:</span><span class="login_left_1_2">
                        <textarea id="zhengshu" runat="server" class="inputtext" style="width: 800px; height: 100px;"></textarea>
                    </span></li>--%>
                    <li><span class="login_left_1_1">帝光會員優惠:</span><span class="login_left_1_2">
                        <textarea id="note" runat="server" class="inputtext" style="width: 800px; height: 140px;"></textarea>
                    </span></li>
                </div>
                <%--   <li><span class="login_left_1_1">統一編號:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtsyscoe" runat="server" />
                </span></li>--%>
                <li style="clear: both; margin: 10px auto; padding: 10px 0 10px 120px;">
                    <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/ljzc.jpg' OnClientClick="return valdata()"
                        OnClick="btnlogin_Click" />
                </li>
            </ul>
        </div>
        <!--<div id="login_right">
            <img src="../images/login_ys.jpg" /></div>-->
        <div class="clear">
        </div>
    </div>
</asp:Content>
