<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="news.aspx.cs" Inherits="DTcms.Web.news" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .lunboltu h1, h2, h3, h4, h5, h6, p, ul, ol, dl, em
        {
            color: #333;
            font-style: normal;
            font-family: "宋體" , Verdana, Arial, Helvetica, sans-serif;
        }
    </style>
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
        當前位置：<a href="index.aspx">帝光地產聯盟</a>><a href="news.aspx">知識分享</a></div>
    <div id="ziye_middle">
        <div id="menubox">
            <!--樣式1 滑動選項卡-->
            <ul>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li id="zzjs2" class="newsfenlei"><a href="news.aspx?mid=<%#Eval("channel_id") %>&category_id=<%#Eval("id") %>">
                            <%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="ziye_middle_left" style="text-align: left">
            <h2>
                <%=channel_id == 1 ? "謄本申請" : "知識分享"%>
            </h2>
            <ul>
                <asp:Repeater ID="repdata" runat="server">
                    <ItemTemplate>
                        <li class="ziye_tbsq_nr">
                            <img src="../images/ziye_ico.jpg" name="ziye_tbsq_ico1" id="ziye_tbsq_ico1" /><span
                                class="biaoti"> <a href="newsview.aspx?id=<%#Eval("id") %>" target="_blank" style="text-align: left">
                                    <%# ToSubstring(Eval("title").ToString(),40)%>
                                </a></span><span class="riqi">發佈時間：<%# string.Format("{0:g}",Eval("add_time"))%></span><span
                                    class="riqi">來源：<%#Eval("From")%></span></li>
                        <span class="ziye_tbsq_nr2" style="max-height: 135px;"><a href="newsview.aspx?id=<%#Eval("id") %>">
                            <%#HtmlSubstring(Eval("content"))%></a></span>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div id="fenye_con_zzjs_1" class="fenye" style="width: 700px;">
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
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
