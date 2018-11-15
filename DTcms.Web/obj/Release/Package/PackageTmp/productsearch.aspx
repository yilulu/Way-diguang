<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="productsearch.aspx.cs" Inherits="DTcms.Web.productsearch" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/ditie.ascx" TagName="ditie" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs1 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_1 */
                menu.className = i == cursel ? "hover" : ""; /*三目運算 等號優先*/
                con.style.display = i == cursel ? "block" : "none";

                var pagecon = document.getElementById("fenye_con_" + name + "_" + i); /* con_zzjs_1 */
                pagecon.style.display = i == cursel ? "block" : "none";
            }
        }
//-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><%=GetTitle() %></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div id="main">
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_1">
                        <asp:Repeater ID="repdate1" runat="server">
                            <ItemTemplate>
                                <ul class="cs_tu">
                                    <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                        <li class="cs_tu1">
                                            <%#Eval("title").ToString()%>
                                        </li>
                                    </a><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                        <img src="<%#Eval("img_url")%>" name="cs_tu1" width="280" height="146" border="0"
                                            id="cs_tu1" /></a>
                                    <ul class="cs_tu2">
                                        <span class="cs_tu2_1">區域;
                                            <%#GetA(Eval("Areaid").ToString())%></span> <span class="cs_tu2_2" style="width: 300px;">
                                                地址:<%#Eval("dizhi")%>&nbsp; </span><span class="cs_tu2_3">戶型:
                                                    <%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")), null)%>
                                                </span><span class="cs_tu2_4">樓層:
                                                    <%#Eval("louceng")%>樓</span> <span class="cs_tu2_5">車位:
                                                        <%#Eval("chewei")%></span> <span class="cs_tu2_6">坐向:<%#Eval("zuoxiang")%></span>
                                        <span class="cs_tu2_7">屋齡:
                                            <%#Eval("digg_good")%>年</span>
                                        <%--<span class="cs_tu2_8">
                                                <%#Eval("shangpinType")%></span>--%>
                                        <span class="cs_tu2_9" style="width: 300px;">公共設施:<%#Eval("xingneng")%></span>
                                    </ul>
                                    <ul class="cs_tu3">
                                        <li class="cs_tu3_1"><span class="cs_tu2_1_1">
                                            <%#getSellOrHire(Eval("sell_price").ToString())%></span></li>
                                        <li class="cs_tu3_1">權狀:<span class="cs_tu2_1_2">
                                            <%#Eval("mianji").ToString()%></span>坪</li>
                                        <li class="cs_tu3_1">
                                            <%#Eval("yongtu")%></li>
                                        <li class="cs_tu3_1"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                            查看詳情>></a></li>
                                    </ul>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div id="fenye_con_zzjs_1" class="fenye">
                        <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                            <tr>
                                <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                                    runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                                    NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
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
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
