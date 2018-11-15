<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="productTdList.aspx.cs" Inherits="DTcms.Web.productTdList" %>

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
//-->     </script>
    <script type="text/javascript">
 $(function () {
            $("#zzjs" + <%=Cid %>).addClass("hover");
            $("#zzjs1").removeClass("hover");
        });
    </script>
    <style type="text/css">
        #menubox
        {
            width: 850px;
            height: 24px;
            margin-bottom: 7px;
            clear: both;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><%=GetTitle() %></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <!--搜尋關鍵字選擇-->
            <div id="main">
                <div id="menubox">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" onmousemove="setTab('zzjs',1,6)" class="hover">
                            <%=title %></li>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <li id="zzjs<%#Eval("id") %>"><a href="productlist.aspx?mid=<%#Eval("channel_id") %>&cid=<%#Eval("id") %>">
                                    <%#Eval("title")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_1">
                        <asp:Repeater ID="repdate1" runat="server">
                            <ItemTemplate>
                                <ul class="cs_tu">
                                    <a href="TdView.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                        <li class="cs_tu1">
                                            <%#ToSubstring(Eval("title").ToString(),50)%>
                                        </li>
                                    </a><a href="TdView.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                        <img src="<%#Eval("img_url")%>" name="cs_tu1" width="280" height="146" border="0"
                                            id="cs_tu1" /></a>
                                    <ul class="cs_tu2">
                                        <span class="cs_tu2_1">區域;
                                            <%#GetA(Eval("Areaid").ToString())%></span><span class="cs_tu2_3" style="width: 160px;">
                                                案件編號:<%#Eval("shequ")%>&nbsp; </span><span class="cs_tu2_3" style="width: 160px;">分區:<%#GetCategory(Eval("point").ToString())%>&nbsp;
                                                </span><span class="cs_tu2_3">持分:<%#Eval("fuwuxiangju")%>&nbsp; </span><span class="cs_tu2_4"
                                                    style="width: 180px;">公告現值:
                                                    <%#Eval("shangpinType")%>元/㎡&nbsp;</span> <span class="cs_tu2_5" style="width: 140px;">
                                                        地上物:
                                                        <%#Eval("stock_quantity").ToString() == "1" ? "有" : "無"%></span>
                                        <%--<span class="cs_tu2_8">
                                                <%#Eval("shangpinType")%></span>--%>
                                    </ul>
                                    <ul class="cs_tu3">
                                        <li class="cs_tu3_1"><span class="cs_tu2_1_1">
                                            <%#getSellOrHire(Eval("ID").ToString())%></span></li>
                                        <li class="cs_tu3_1">總價:<span class="cs_tu2_1_2"> <%#Eval("sell_price").ToString()%></span>萬</li>
                                        <li class="cs_tu3_1">權狀:<span class="cs_tu2_1_2"> 約<%#Eval("mianji").ToString()%></span>坪</li>
                                        <li class="cs_tu3_1">
                                            <%#Eval("yongtu")%></li>
                                        <li class="cs_tu3_1"><a href="TdView.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
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
                                    NextPageText="下一頁" ShowInputBox="Always" OnPageChanging="aspPage_PageChanging"
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
            <span class="tjfy">
                <%=Request.QueryString["mid"].ToString() == "4" ? "經典規劃案例" : "推薦房源"%></span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
