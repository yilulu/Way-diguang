<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="newsview.aspx.cs" Inherits="DTcms.Web.newsview" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab1(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs11 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_11 */
                menu.className = i == cursel ? "hover" : ""; /*三目運算 等號優先*/
                con.style.display = i == cursel ? "block" : "none";
            }
        }
//-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>詳細訊息</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj" style="width: 100%">
            <div id="cs_sj_xia1" style="width: 100%">
                <span class="cs_sj_xia1_wz" style="text-align: center">
                    <asp:Label ID="lblTitle" runat="server" /></span>
                <div style="text-align: right">
                   <asp:Label ID="lblFrom" runat="server" /> <asp:Label ID="lblDatetime" runat="server" />&nbsp;&nbsp;</div>
                <div style="line-height: 24px;">
                    <asp:Label ID="lblContent" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
