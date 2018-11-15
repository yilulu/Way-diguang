<%@ Page Title="" Language="C#" MasterPageFile="~/Site3.Master" AutoEventWireup="true"
    CodeBehind="User_zh.aspx.cs" Inherits="DTcms.Web.User_zh" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Usercontrol/HelperPage.ascx" TagName="HelperPage" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
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
        -- ></style>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <%--<div id="kong" style="height:10px";></div>--%>
    <div id="ziye_ymx">
        當前位置： <a href="index.aspx">帝光地產聯盟</a>>><a href="kjgh.aspx?mid=4">裝潢設計</a>>><asp:Label
            ID="username" runat="server"></asp:Label></div>
    <div class="dj-topbu">
        <h1>
            <asp:Label ID="username2" runat="server" />
        </h1>
    </div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div class="sjjs">
                <a name="sjjsxx" id="sjjsxx"></a>
                <table width="100%" border="0" cellspacing="0" cellpadding="00">
                    <tr>
                        <td width="100" rowspan="4">
                            <img src="<%=Images %>" width="150" height="180" />
                        </td>
                        <td colspan="2">
                            <blockquote>
                                <h3 class="jb">
                                    <asp:Label ID="username4" runat="server" />
                                </h3>
                            </blockquote>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <p>
                                公司地址：<asp:Label ID="lblAdd" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            聯絡人：<asp:Label ID="lblLianXiRen" runat="server" />
                        </td>
                        <td>
                            聯絡電話：<asp:Label ID="mobile2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; E-mail：<asp:Label
                                ID="email2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            服務項目：<asp:Label ID="shuxishequ2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            業務特長：<asp:Label ID="fuwutechang2" runat="server" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="3">
                            服务项目： 2011.2-2013.11 台慶不動產-美麗島捷運站 任 房屋好幫手
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="note2" runat="server" />
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
