<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="useredit.aspx.cs" Inherits="DTcms.Web.useredit" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            var pass2 = $("#<%=txtpassword2.ClientID %>").val();

            if (pass1 == "") {
                alert("請輸入密碼!");
                return false;
            }
            if (pass1 != pass2) {
                alert("重複密碼不一致!");
                return false;
            }
        }

        function select(obj) {
            var i = $(obj).val();
            if (i != 5) {
                $("#divsj").css("display", "none");
                $("#zhifuModel").css("display", "");
            }
            else {
                $("#divsj").css("display", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>修改資料</div>
    <div id="login_1">
        <span class="login_1_1">個人用戶</span><span class="login_1_2"></span></div>
    <div id="login" style="height: 780px; overflow: auto">
        <div id="login_left" style="height: auto;">
            <ul class="login_left1">
                <li><span class="login_left_1_1">組別:</span><span class="login_left_1_2">
                    <asp:DropDownList ID="ddlGroup" runat="server" onchange="select(this)">
                        <%--<asp:ListItem Text="普通會員" Value="0" Selected="True" />
                        <asp:ListItem Text="商家會員" Value="1" />--%>
                    </asp:DropDownList>
                </span></li>
                <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><asp:Label
                    ID="txtusername" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請設定密碼:</span><span class="login_left_1_2"><input
                    name="logins1" type="password" class="inputtext" id="txtpassword" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">請輸入密碼:</span><span class="login_left_1_2"><input
                    name="logins1" type="password" class="inputtext" runat="server" id="txtpassword2" />
                </span></li>
                <li><span class="login_left_1_1">暱稱:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtName" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">照片:</span><span class="login_left_1_2"><asp:FileUpload
                    ID="fileUpImage" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" class="inputtext" id="txtemall" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">聯繫手機:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtphone" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">地址:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtAddress" class="inputtext" runat="server" />
                </span></li>
                <div id="divsj" runat="server" style="display: none">
                    <li><span class="login_left_1_1">公司官網:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="gongsi" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">帝光會員優惠:</span><span class="login_left_1_2">
                        <textarea id="note" runat="server" class="inputtext" style="width: 800px; height: 140px;"></textarea>
                    </span></li>
                    <li id="zhifuModel" style="display: none;"><span class="login_left_1_1">付款方式:</span><span
                        class="login_left_1_2"><cc1:MyDropDownList ID="ddlzhifu" runat="server" Where=" is_lock=0"
                            Table_ID_Name="dt_payment*id*title">
                        </cc1:MyDropDownList>
                    </span></li>
                </div>
            </ul>
        </div>
        <div id="login_right">
            <img src="../images/login_ys.jpg" /></div>
    </div>
    <ul style="text-align: center">
        <li>
            <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/sub.jpg' OnClientClick="return valdata()"
                OnClick="btnlogin_Click" />
        </li>
    </ul>
</asp:Content>
