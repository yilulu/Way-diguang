<%@ Page Title="" Language="C#" MasterPageFile="~/Site3.Master" AutoEventWireup="true"
    CodeBehind="kjghsearch.aspx.cs" Inherits="DTcms.Web.kjghsearch" %>

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
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><%=GetTitle() %></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div id="main">
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_1">
                        <asp:Repeater ID="repdate1" runat="server">
                            <ItemTemplate>
                                <div class="sjjs">
                                    <a name="sjjsxx" id="sjjsxx"></a>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="00">
                                        <tr>
                                            <td width="100" rowspan="5">
                                                <a href="User_zh.aspx?id=<%#Eval("ID") %>&mid=4">
                                                    <img src="<%#Eval("img_url") %>" width="150" height="180" /></a>
                                            </td>
                                            <td colspan="2">
                                                <blockquote>
                                                    <h3 class="jb">
                                                        <%#Eval("title")%>
                                                    </h3>
                                                </blockquote>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <p>
                                                    公司地址：<%#Eval("dizhi")%>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                聯絡人：<%#Eval("lianxiren")%>
                                            </td>
                                            <td>
                                                聯絡電話：<%#Eval("dianhua")%>&nbsp;&nbsp;&nbsp;&nbsp; E-mail：<%#Eval("gongsi")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                服務項目：<%#Eval("fuwuxiangju")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                業務特長：<%#Eval("shequ")%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
