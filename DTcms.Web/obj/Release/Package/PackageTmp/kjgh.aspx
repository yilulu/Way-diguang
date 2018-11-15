<%@ Page Title="" Language="C#" MasterPageFile="~/Site3.Master" AutoEventWireup="true"
    CodeBehind="kjgh.aspx.cs" Inherits="DTcms.Web.kjgh" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/ditie.ascx" TagName="ditie" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .dj-topbu
        {
            background-image: url(images/title-bg2.png);
            height: 120px;
            width: 1100px;
            margin-right: auto;
            margin-left: auto;
        }
        .dj-topbu h1
        {
            padding: 10px;
            margin-left: 30px;
        }
        .dj-topbu h3
        {
            padding: 10px;
            margin-left: 35px;
        }
        .dj-nav
        {
            background-color: #87CF16;
            line-height: 30px;
            width: 1000px;
            margin-right: auto;
            margin-left: auto;
            padding-right: 50px;
            padding-left: 50px;
            height: 30px;
        }
        .dj-nav a
        {
            padding-right: 20px;
            padding-left: 20px;
            display: block;
            color: #FFF;
            float: left;
            text-align: center;
        }
        .dj-nav a:hover
        {
            background-color: #FFF;
            color: green;
        }
        .sjjs
        {
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
        .jb
        {
            margin: 0px;
            background-image: url(images/mark-ico.png);
            background-repeat: no-repeat;
            padding-top: 0px;
            padding-right: 0px;
            padding-bottom: 0px;
            padding-left: 25px;
        }
        .sjjs img
        {
            padding: 1px;
            border: 1px solid #CCC;
        }
        .sjjs td
        {
            line-height: 25px;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CCC;
            padding: 5px;
        }
        .floatright
        {
            float: right;
        }
        .clear
        {
            clear: both;
        }
        .sjs-left
        {
            float: left;
            width: 275px;
        }
        .sjs-lf-tu
        {
            float: left;
            position: relative;
            text-align: center;
            width: 125px;
            margin: 5px;
            height: 150px;
        }
        .sjs-bt
        {
            line-height: 25px;
            background-color: #000;
            color: #FFF;
            position: absolute;
            bottom: 0px;
            z-index: 9;
            width: 123px;
            margin-left: 2px;
        }
        .sjs-ct
        {
            float: left;
            width: 285px;
            margin: 5px;
            position: relative;
        }
        .sjs-ct-bt
        {
            line-height: 25px;
            color: #FFF;
            background-color: #000;
            text-align: center;
            position: absolute;
            z-index: 9;
            bottom: 0px;
            width: 283px;
            margin-left: 2px;
        }
        .sjs-lf-tu img
        {
            width: 125px;
            height: 150px;
        }
        .sjs
        {
            width: 850px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>><a href="kjgh.aspx?mid=4">空間規劃</a></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <%--<div class=" sjs">
                <div class="sjs-left">
                    <asp:Repeater ID="repdate1" runat="server">
                        <ItemTemplate>
                            <div class="sjs-lf-tu">
                                <a href="User_zh.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                                    <img src="<%#Eval("img_url") %>" name="<%#Eval("title") %>" height="150" width="125"
                                        border="0" /></a>
                                <div class="sjs-bt">
                                    <%#Eval("title") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>--%>
            <div id="bdccz_4l">
                <div id="bdccz_left" class="floatleft">
                    <%=strHtmlPost4_left %>
                </div>
                <div id="bdccz_zhongjian">
                    <%=strHtmlPost4_zhongjian%>
                </div>
                <div id="bdccz_left" class="floatrt">
                    <%=strHtmlPost4_right %>
                </div>
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
</asp:Content>
