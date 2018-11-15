<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="login_vip.aspx.cs" Inherits="DTcms.Web.login_vip" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>VIP登入</div>
    <div id="login_1">
        <span class="login_1_1">同行用戶</span></div>
    <div id="login">
        <div id="login_left">
            <ul class="login_left1">
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtusername" runat="server" class="inputtext" />
                </span></li>
                <li><span class="login_left_1_1">密碼:</span><span class="login_left_1_2"><input name="logins1"
                    type="password" id="txtpassword" runat="server" class="inputtext" />
                </span></li>
                <li><a href="#">
                    <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/sub.jpg' OnClientClick="return valdata()"
                        OnClick="btnlogin_Click" /></a></li>
            </ul>
        </div>
        <div id="login_right">
            <img src="../images/login_ys.jpg" /></div>
    </div>
</asp:Content>
