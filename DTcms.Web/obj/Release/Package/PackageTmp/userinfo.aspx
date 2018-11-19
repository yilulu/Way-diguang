<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="userinfo.aspx.cs" Inherits="DTcms.Web.userinfo" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function valdata() {
            var pass1 = $("#<%=txtpassword.ClientID %>").val();
            var pass2 = $("#<%=txtpassword2.ClientID %>").val();
            //        if (pass1 == "") {
            //            alert("請輸入密碼!");
            //            return false;
            //        }
            if (pass1 != pass2) {
                alert("重複密碼不一致!");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>會員資料</div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">我的資料</span><asp:Literal ID="UpUserGroup" Visible="false" runat="server"></asp:Literal></div>
        <div id="vip_3_3">
            <div id="login" style="width: 800px; height: 920px; border: 0px none">
                <div id="login_left" style="border: 0px none">
                    <ul class="login_left1" style="margin-top: 0px">
                        <li><span class="login_left_1_1">會員等級:</span><span class="login_left_1_2"><asp:Label
                            ID="lblGroupName" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><asp:Label
                            ID="lblUsername" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">請設定密碼:</span><span class="login_left_1_2">
                            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" class="inputtext" />
                        </span></li>
                        <li><span class="login_left_1_1">請輸入密碼:</span><span class="login_left_1_2">
                            <asp:TextBox ID="txtpassword2" runat="server" TextMode="Password" class="inputtext" />
                        </span></li>
                        <li><span class="login_left_1_1">暱稱:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" id="txtName" class="inputtext" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">我的點數:</span><span class="login_left_1_2"><asp:Label
                            ID="lblPoint" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">性別:</span><span class="login_left_1_2">
                            <asp:DropDownList ID="rblSex" runat="server">
                                <asp:ListItem Text="保密" Selected="True" />
                                <asp:ListItem Text="男" />
                                <asp:ListItem Text="女" />
                            </asp:DropDownList>
                        </span></li>
                        <li><span class="login_left_1_1">照片:</span><span class="login_left_1_2"><asp:FileUpload
                            ID="fileUpImage" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" class="inputtext" id="txtemall" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">聯絡電話:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" class="inputtext" id="txtphone" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">手機:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" class="inputtext" id="txtMobile" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">Skype:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" class="inputtext" id="txtQQ" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">地址:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" id="txtAddress" class="inputtext" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">安全問題:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" class="inputtext" id="txtsafe_question" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">問題答案:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" class="inputtext" id="txtsafe_answer" runat="server" />
                        </span></li>
                        <li>
                            <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/sub.jpg' OnClientClick="return valdata()"
                                OnClick="btnlogin_Click" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
