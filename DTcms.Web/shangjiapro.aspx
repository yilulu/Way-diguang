<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="shangjiapro.aspx.cs" Inherits="DTcms.Web.WebForm9" %>
    <%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.dj-topbu {
	background-image: url(../images/title-bg2.png);
	height: 120px;
	width: 1100px;
	margin-right: auto;
	margin-left: auto;
}
.dj-topbu h1 {
	padding: 10px;
	margin-left: 30px;
}
.dj-topbu h3 {
	padding: 10px;
	margin-left: 35px;
}
.dj-nav {
	background-color: #87CF16;
	line-height: 30px;
	width: 1000px;
	margin-right: auto;
	margin-left: auto;
	padding-right: 50px;
	padding-left: 50px;
	height:30px;
}
.dj-nav a {
	padding-right: 20px;
	padding-left: 20px;
	display: block;
	color: #FFF;
	float: left;
	text-align: center;
}
.dj-nav a:hover {
	background-color: #FFF;
	color: green;
}
.sjjs {
	border-bottom-width: 1px;
	border-right-width: 1px;
	border-left-width: 1px;
	border-bottom-style: solid;
	border-right-style: solid;
	border-left-style: solid;
	border-bottom-color: #CCC;
	border-right-color: #CCC;
	border-left-color: #CCC;
	margin-top: 10px;
	margin-bottom: 10px;
	clear: both;
}
.jb {
	margin: 0px;
	background-image: url(../images/mark-ico.png);
	background-repeat: no-repeat;
	padding-top: 0px;
	padding-right: 0px;
	padding-bottom: 0px;
	padding-left: 25px;
}
.sjjs img {
	padding: 1px;
	border: 1px solid #CCC;
}
.sjjs td {
	line-height: 25px;
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #CCC;
	padding: 5px;
}
.floatright {
	float: right;
}
.clear {
	clear: both;
}

-->
</style>
<link href="style/cs.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs1 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_1 */
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
        當前位置：<a href="index.aspx">首頁</a> 商家的商品</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div id="main">
                <div id="menubox">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" onmousemove="setTab('zzjs',1,6)" class="hover">租 房</li>
                        <li id="zzjs2" onmousemove="setTab('zzjs',2,6)">出售</li>
                    </ul>
                    <!--樣式2 點擊選項卡 -->
                </div>
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_1">
                        <asp:Repeater ID="repdate1" runat="server">
                            <ItemTemplate>
                                <ul class="cs_tu">
                                    <a  href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>" target="_blank">
                                        <li class="cs_tu1">
                                            <%#ToSubstring(Eval("title").ToString(),50)%>
                                        </li>
                                    </a><a  href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>"
                                        target="_blank">
                                        <img src="<%#Eval("img_url")%>" name="cs_tu1" width="280" height="146" border="0"
                                            id="cs_tu1" /></a>
                                    <ul class="cs_tu2">
                                        <span class="cs_tu2_1">
                                            <%#Eval("gongsi")%></span> <span class="cs_tu2_2" style="width: 235px;">
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("quyu")), null)%>&nbsp;
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("xianlu")), null)%>
                                            </span><span class="cs_tu2_3">
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")), null)%>
                                            </span><span class="cs_tu2_4">
                                                <%#Eval("louceng")%></span> <span class="cs_tu2_5">
                                                    <%#Eval("xingneng")%></span> <span class="cs_tu2_6">
                                                        <%#Eval("zuoxiang")%></span> <span class="cs_tu2_7">
                                                            <%#Eval("add_time","{0:yyyy}")%></span> <span class="cs_tu2_8">
                                                                <%#Eval("shangpinType")%></span> <span class="cs_tu2_9" style="">狀態：
                                                                    <%#Eval("Status").ToString().Equals("0") ? "未通過" : Eval("Status").ToString().Equals("1") ? "通過" : Eval("Status").ToString().Equals("3")?"已成交":"刪除" %>
                                                                </span>
                                    </ul>
                                    <ul class="cs_tu3">
                                        <li class="cs_tu3_1">$<span class="cs_tu2_1_1"><%#Eval("sell_price")%></span>元/月</li>
                                        <li class="cs_tu3_1"><span class="cs_tu2_1_2">
                                            <%#GetTypleWhereTilte(Convert.ToInt32(Eval("mianji")), null)%></span></li>
                                        <li class="cs_tu3_1">
                                            <%#Eval("yongtu")%></li>
                                        <li class="cs_tu3_1"><a href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                            查看詳情>></a></li>
                                       
                                    </ul>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="www_zzjs_net_show" id="con_zzjs_2" style="display: none">
                        <asp:Repeater ID="repdate2" runat="server">
                            <ItemTemplate>
                                <ul class="cs_tu">
                                    <a  href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>" target="_blank">
                                        <li class="cs_tu1">
                                            <%#Eval("title")%></li></a> <a  href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>"
                                                target="_blank">
                                                <img src="<%#Eval("img_url")%>" name="cs_tu1" border="0" width="280" height="146"
                                                    id="cs_tu1" /></a>
                                    <ul class="cs_tu2">
                                        <span class="cs_tu2_1">
                                            <%#Eval("gongsi")%></span> <span class="cs_tu2_2" style="width: 235px;">
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("quyu")), null)%>&nbsp;
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("xianlu")), null)%>
                                            </span><span class="cs_tu2_3">
                                                <%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")), null)%>
                                            </span><span class="cs_tu2_4">
                                                <%#Eval("louceng")%></span> <span class="cs_tu2_5">
                                                    <%#Eval("xingneng")%></span> <span class="cs_tu2_6">
                                                        <%#Eval("zuoxiang")%></span> <span class="cs_tu2_7">
                                                            <%#Eval("add_time","{0:yyyy}")%></span> <span class="cs_tu2_8">
                                                                <%#Eval("shangpinType")%></span> <span class="cs_tu2_9" style="">狀態：
                                                                    <%#Eval("Status").ToString().Equals("0") ? "未通過" : Eval("Status").ToString().Equals("1") ? "通過" : Eval("Status").ToString().Equals("3")?"已成交":"刪除" %>
                                                                </span>
                                    </ul>
                                    <ul class="cs_tu3">
                                        <li class="cs_tu3_1">$<span class="cs_tu2_1_1"><%#Eval("sell_price")%></span>元/月</li>
                                        <li class="cs_tu3_1"><span class="cs_tu2_1_2">
                                            <%#GetTypleWhereTilte(Convert.ToInt32(Eval("mianji")), null)%></span></li>
                                        <li class="cs_tu3_1">
                                            <%#Eval("yongtu")%></li>
                                        <li class="cs_tu3_1"><a href="producJPview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                            查看詳情>></a></li>
                                       
                                    </ul>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div id="fenye_con_zzjs_1" class="fenye">
                        <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                            <tr>
                                <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" showpageindexbox="Never"
                                    runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                                    NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
                                    SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                                </webdiyer:AspNetPager>
                            </tr>
                        </table>
                    </div>
                    <div id="fenye_con_zzjs_2" style="display: none" class="fenye">
                        <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                            <tr>
                                <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage2" showpageindexbox="Never"
                                    runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                                    NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage2_PageChanged"
                                    SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                                </webdiyer:AspNetPager>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div id="cs_right">
            <span class="tjfy">推薦房源</span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
        </div>
    </div>
</asp:Content>
