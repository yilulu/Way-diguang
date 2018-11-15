<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="cjhq.aspx.cs" Inherits="DTcms.Web.WebForm10" %>

<%@ Register Src="Usercontrol/ditie.ascx" TagName="ditie" TagPrefix="uc2" %>
<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>成交行情</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <!--搜尋關鍵字選擇-->
            <uc2:ditie ID="ditie1" runat="server" />
            <div id="main">
                <div id="menubox">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" onmousemove="setTab('zzjs',1,6)" class="hover">成交行情</li>
                    </ul>
                </div>
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_1">
                        <span class="cjhq_biaoti"><span class="cjhq_1">市/區</span><span class="cjhq_2">路段</span><span
                            class="cjhq_3">坪數</span><span class="cjhq_4">金額</span><span class="cjhq_5">瀏覽</span><span
                                class="cjhq_6">成交時間</span></span>
                        <ul class="cjhq_ul">
                       
                            <asp:Repeater ID="repdate" runat="server">
                                <ItemTemplate>
                                    <li><span class="cjhq_1_1">&nbsp;<%#GetAreaName(Eval("Areaid"))%></span><span class="cjhq_2_1">&nbsp;<%#GetTypleWhereTilte(Convert.ToInt32(Eval("xianlu")), null)%></span><span class="cjhq_3_1"><%#GetTypleWhereTilte(Convert.ToInt32(Eval("mianji")),null)%></span><span
                                        class="cjhq_4_1">&nbsp;<%#Eval("sell_price")%></span><span class="cjhq_5_1">&nbsp;<%#Eval("click")%></span><span class="cjhq_6_1">&nbsp;<%#GetChengjiaodate(Eval("id"))%></span><span
                                            class="cjhq_7_1"><%--<a href="cjhq_sj.html"><img src="../images/cjhq_xiangqing.jpg" border="0" /></a>--%></span></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div id="fenye">
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
        <div id="cs_right">
            <span class="tjfy">推薦房源</span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
