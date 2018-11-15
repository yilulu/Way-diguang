<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="register.aspx.cs" Inherits="DTcms.Web.WebForm7" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/lang/zh_CN.js"></script>
    <script src="../scripts/city.js" type="text/javascript"></script>
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
            var phone = $("#<%=txtphone.ClientID %>").val();
            var emall = $("#<%=txtemall.ClientID %>").val();
            var city = $("#<%=txtcity.ClientID %>").val();
            var quyu = $("#<%=txtcity1.ClientID %>").val();
            var Address = $("#<%=txtAddress.ClientID %>").val();
            var jiadian = $("#<%=dianming.ClientID %>").val();
            var regStr = /^[_a-zA-Z0-9\-]+(\.[_a-zA-Z0-9\-]*)*@[a-zA-Z0-9\-]+([\.][a-zA-Z0-9\-]+)+$/;
            var regStrPhone = /^[1][3-8]\d{9}$|^([6|9])\d{7}$|^[0][9]\d{8}$|^[6]([8|6])\d{5}$/;

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
                alert("兩次輸入密碼不一致!");
                return false;
            }
            var chkPhone = regStrPhone.test(phone);
            if (!chkPhone) {
                alert("聯繫手機格式錯誤!");
                return false;
            }
            if (city == '') {
                alert("請選擇城市!");
                return false;
            }
            if (quyu == '') {
                alert("請選擇縣市!");
                return false;
            }
            if (Address == '') {
                alert("請輸入地址!");
                return false;
            }
            //if (jiadian == "")
            //{
            //    alert("請輸入家電!");
            //    return false;
            //}
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
        當前位置：帝光地產聯盟>>註冊
    </div>
    <div id="login_1">
        <span class="login_1_1">個人用戶</span><span class="login_1_2">我已經註冊，現在就<span class="login_1_3"><a
            href="login.aspx">登入</a></span></span>
    </div>
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
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="txtusername"
                    type="text" id="txtusername" onblur="chkRegUserName(this.value)" class="inputtext"
                    runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請設定密碼:</span><span class="login_left_1_2"><input
                    name="txtpassword" type="password" class="inputtext" id="txtpassword" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請確認密碼:</span><span class="login_left_1_2"><input
                    name="txtpassword2" type="password" class="inputtext" id="txtpassword2" />
                </span></li>
                <li><span class="login_left_1_1">姓名:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtName" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">公司名稱:</span><span class="login_left_1_2"><input
                    name="CompanyName" type="text" id="CompanyName" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">介紹人帳號:</span><span class="login_left_1_2"><input
                    name="txtIntroduce" type="text" id="txtIntroduce" class="inputtext" runat="server"
                    onblur="chkUserName(this.value)" />
                </span></li>
                <li><span class="login_left_1_1">照片:</span><span class="login_left_1_2"><asp:FileUpload
                    ID="fileUpImage" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="txtemall"
                    type="text" class="inputtext email" onblur="chkUserEmail(this.value)" id="txtemall"
                    runat="server" />
                </span></li>
                 <li><span class="login_left_1_1">聯繫電話:</span><span class="login_left_1_2"><input
                    name="txtTel" type="text" class="inputtext" id="txtTel" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">聯繫手機:</span><span class="login_left_1_2"><input
                    name="txtphone" type="text" class="inputtext" id="txtphone" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">地址:</span>
                    <select name="txtcity" id="txtcity" class="order-sl" runat="Server" onchange="changeArea(this.value,document.all.ctl00$ContentPlaceHolder1$txtcity1)">
                        <option value="">選擇城市</option>
                        <option value='基隆市'>基隆市</option>
                        <option value='台北市'>台北市</option>
                        <option value='新北市'>新北市</option>
                        <option value='宜蘭縣'>宜蘭縣</option>
                        <option value='新竹市'>新竹市</option>
                        <option value='新竹縣'>新竹縣</option>
                        <option value='桃園縣'>桃園縣</option>
                        <option value='苗栗縣'>苗栗縣</option>
                        <option value='台中市'>台中市</option>
                        <option value='彰化縣'>彰化縣</option>
                        <option value='南投縣'>南投縣</option>
                        <option value='嘉義市'>嘉義市</option>
                        <option value='嘉義縣'>嘉義縣</option>
                        <option value='雲林縣'>雲林縣</option>
                        <option value='台南市'>台南市</option>
                        <option value='高雄市'>高雄市</option>
                        <option value='屏東縣'>屏東縣</option>
                        <option value='台東縣'>台東縣</option>
                        <option value='花蓮縣'>花蓮縣</option>
                    </select>
                    <select name="txtcity1" id="txtcity1" runat="server" class="order-sl" onchange="changeArea1(this.value,document.all.ctl00$ContentPlaceHolder1$txtZip)">
                        <option value="選擇縣市">選擇縣市</option>
                    </select><asp:TextBox ID="txtAdd" runat="server" class=" login-ipt"></asp:TextBox>
                    <%if (!string.IsNullOrEmpty(getcity(0, address)))
                      {%><script>                                 changeArea('<%=getcity(0, address)%>', document.all.ctl00$ContentPlaceHolder1$txtcity1)</script><%}%>
                    <%if (!string.IsNullOrEmpty(getcity(1, address)))
                      {%><script>                                 changeAreaSel(document.all.ctl00$ContentPlaceHolder1$txtcity1, '<%=getcity(1, address)%>')</script><%}%>
                    <%if (!string.IsNullOrEmpty(getcity(0, address)))
                      {%><script>                                 changeAreaSel(document.all.ctl00$ContentPlaceHolder1$txtcity, '<%=getcity(0, address)%>')</script><%}%></li>
                <li><span class="login_left_1_1">地址:</span><span class="login_left_1_2"><input name="txtAddress"
                    type="text" id="txtAddress" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">郵遞區號:</span><span class="login_left_1_2"><asp:TextBox ID="txtZip" CssClass="inputtext" runat="server" class=" login-ipt"></asp:TextBox>
                </span></li>
                <li id="zhifuModel"><span class="login_left_1_1">付款方式:</span><span class="login_left_1_2"><cc1:MyDropDownList
                    ID="ddlzhifu" name="ddlzhifu" runat="server" Where=" is_lock=0" Table_ID_Name="dt_payment*id*title">
                </cc1:MyDropDownList>
                </span></li>
                <li style="display:none;"><span class="login_left_1_1">家電:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="dianming" class="inputtext" runat="server" />
                </span></li>
                <div id="divsj" style="display: none">
                    <%--  
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
                    <li><span class="login_left_1_1">家電:</span><span class="login_left_1_2">
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
                    <%--<input id="Submit1" type="submit" value="submit" onclick="return valdata()" />--%>
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
