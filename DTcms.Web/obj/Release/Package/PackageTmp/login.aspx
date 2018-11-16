<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="DTcms.Web.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function valdata() {
            var username = $("#<%=txtusername.ClientID %>").val();
            var pass1 = $("#<%=txtpassword.ClientID %>").val();
            if (username == "") {
                alert("請輸入用戶名!");
                return false;
            }
            if (pass1 == "") {
                alert("請輸入密碼!");
                return false;
            }

        }
    </script>
    <script type="text/javascript">
        function btnLogin() {
            $("#btnlogin").click();
        }
        function getPass() {
            $("#ImageButton1").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>><a href="login.aspx">登入</a>
    </div>
    <div id="login_1">
        <span class="login_1_1">個人用戶</span><span class="login_1_2">我沒有帳號，現在就<span class="login_1_3">
            <a href="mem.aspx" class="login_1_3">註冊</a></span> <a href="javascript:void(0)" class="login_1_3"
                onclick="showul()">忘記密碼</a> </span>
    </div>
    <div id="login">
        <div id="login_left">
            <ul class="login_left1" id="ul1" style='<%=Request.QueryString["type"] == null ? "display:": "display:none"%>'>
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtusername" runat="server" class="inputtext" />
                </span></li>
                <li><span class="login_left_1_1">密碼:</span><span class="login_left_1_2"><input name="logins1"
                    onkeypress="btnLogin()" type="password" id="txtpassword" runat="server" class="inputtext" />
                </span></li>
                <li><a href="#">
                    <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/sub.jpg' OnClientClick="return valdata()"
                        OnClick="btnlogin_Click" /></a></li>
                <li>
                    <input name="input" type="button" value="google帳號快速登入" class="fblogin-btn btn-red" onclick="javascript: location.href = '/google.aspx'" />
                </li>
            </ul>
            <ul class="login_left1" id="ul2" style='<%=Request.QueryString["type"] == null ? "display:none": "display:"%>'>
                <li>通過帳號，郵箱找回密碼,請查收郵箱資料</li>
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="txtusername2"
                    type="text" id="txtusername2" runat="server" class="inputtext" />
                </span></li>
                <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="txtemail"
                    type="text" id="txtemail" runat="server" onkeypress="getPass()" class="inputtext" />
                </span></li>
                <li><a href="#">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='../images/sub.jpg' OnClick="ImageButton1_Click" /></a></li>

            </ul>
        </div>
        <div id="login_right">
            <img src="../images/login_ys.jpg" />
        </div>
    </div>
    <script type="text/javascript">
        function showul() {
            $("#ul1").hide();
            $("#ul2").show();
        }
    </script>
</asp:Content>
