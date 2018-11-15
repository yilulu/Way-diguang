<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="shengji.aspx.cs" Inherits="DTcms.Web.shengji" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>會員資料</div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc2:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">我的資料</span><asp:Literal ID="UpUserGroup" runat="server"></asp:Literal></div>
        <div id="vip_3_3">
            <div id="login" style="width: 800px; height: 920px; border: 0px none">
                <div id="login_left" style="border: 0px none">
                    <ul class="login_left1" style="margin-top: 0px">
                        <li><span class="login_left_1_1">組別:</span><span class="login_left_1_2">
                            <asp:DropDownList ID="ddlGroup" runat="server" onchange="select(this)">
                            </asp:DropDownList>
                        </span></li>
                        <li id="zhifuModel"><span class="login_left_1_1">付款方式:</span><span class="login_left_1_2"><cc1:MyDropDownList
                            ID="ddlzhifu" runat="server" Where=" is_lock=0" Table_ID_Name="dt_payment*id*title">
                        </cc1:MyDropDownList>
                        </span></li>
                        <li style="clear: both; margin: 10px auto; padding: 10px 0 10px 120px;">
                            <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/ljzc.jpg' OnClientClick="return valdata()"
                                OnClick="btnlogin_Click" />
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
