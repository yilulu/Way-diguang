<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="userlook.aspx.cs" Inherits="DTcms.Web.userlook1" %>

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
        .lbl
        {
            word-wrap: break-word;
        }
        -- ></style>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs1 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_1 */
                menu.className = i == cursel ? "hover" : ""; /*三目运算 等号优先*/
                con.style.display = i == cursel ? "block" : "none";
            }
        }
//-->
    </script>
    <script type="text/javascript">
        function gets_id(objName) {
            if (document.getElementById) {
                return eval('document.getElementById("' + objName + '")');
            } else if (document.layers) {
                return eval("document.layers['" + objName + "']");
            } else {
                return eval('document.all.' + objName);
            }
        }
        //打开DIV层
        function disp_cc() {
            if (gets_id('hh').style.display == 'none') {
                gets_id('hh').style.display = '';
            }
            else {
                gets_id('hh').style.display = 'none';
            }
        }
        //赋值
        function gets_value(str) {
            gets_id('class1').value = str;
            gets_id('hh1').style.display = 'none';
        }
        function gets_id(objName) {
            if (document.getElementById) {
                return eval('document.getElementById("' + objName + '")');
            } else if (document.layers) {
                return eval("document.layers['" + objName + "']");
            } else {
                return eval('document.all.' + objName);
            }
        }
        //打开DIV层
        function disp_cc1() {
            if (gets_id('hh1').style.display == 'none') {
                gets_id('hh1').style.display = '';
            }
            else {
                gets_id('hh1').style.display = 'none';
            }
        }
        //赋值
        function gets_value(str) {
            gets_id('class1').value = str;
            gets_id('hh1').style.display = 'none';
        }

        function gets_id(objName) {
            if (document.getElementById) {
                return eval('document.getElementById("' + objName + '")');
            } else if (document.layers) {
                return eval("document.layers['" + objName + "']");
            } else {
                return eval('document.all.' + objName);
            }
        }
        //打开DIV层
        function disp_cc2() {
            if (gets_id('hh2').style.display == 'none') {
                gets_id('hh2').style.display = '';
            }
            else {
                gets_id('hh2').style.display = 'none';
            }
        }
        //赋值
        function gets_value(str) {
            gets_id('class3').value = str;
            gets_id('hh3').style.display = 'none';
        }

        function gets_id(objName) {
            if (document.getElementById) {
                return eval('document.getElementById("' + objName + '")');
            } else if (document.layers) {
                return eval("document.layers['" + objName + "']");
            } else {
                return eval('document.all.' + objName);
            }
        }
        //打开DIV层
        function disp_cc3() {
            if (gets_id('hh3').style.display == 'none') {
                gets_id('hh3').style.display = '';
            }
            else {
                gets_id('hh3').style.display = 'none';
            }
        }
        //赋值
        function gets_value(str) {
            gets_id('class3').value = str;
            gets_id('hh3').style.display = 'none';
        }
    </script>
    <link rel="stylesheet" type="text/css" href="ziye/mouseovertabs.css" />
    <script src="ziye/mouseovertabs.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery.jqtab = function (tabtit, tab_conbox, shijian) {
                $(tab_conbox).find("li").hide();
                $(tabtit).find("li:first").addClass("thistab").show();
                $(tab_conbox).find("li:first").show();

                $(tabtit).find("li").bind(shijian, function () {
                    $(this).addClass("thistab").siblings("li").removeClass("thistab");
                    var activeindex = $(tabtit).find("li").index(this);
                    $(tab_conbox).children().eq(activeindex).show().siblings().hide();
                    return false;
                });

            };
            /*调用方法如下：*/
            $.jqtab("#tabs", "#tab_conbox", "click");

            $.jqtab("#tabs2", "#tab_conbox2", "mouseenter");

        });
    </script>
    <%--<div id="kong" style="height:10px";></div>--%>
    <div id="ziye_ymx">
        當前位置： 帝光地產聯盟>><asp:Label ID="username" runat="server" />的店舖</div>
    <div class="dj-topbu">
        <h3>
            <asp:Label ID="username3" runat="server" /></h3>
    </div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div class="sjjs">
                <a name="sjjsxx" id="sjjsxx"></a>
                <table width="100%" border="0" cellspacing="0" cellpadding="00">
                    <tr>
                        <td width="100" rowspan="5">
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
                    <tr>
                        <%-- <td>
                            服務區域：<asp:Label ID="fuwuquyu" runat="server" />
                        </td>--%>
                        <td colspan="2">
                            聯絡電話：<asp:Label ID="mobile2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; 官網： <a href="<%=Url %>"
                                target="_blank">
                                <asp:Label ID="lblUrl" runat="server" /></a><asp:Label Visible="false" ID="email2"
                                    runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p>
                                公司地址：<asp:Label ID="lblAdd" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            帝光會員優惠:
                            <asp:Label ID="note2" CssClass="lbl" runat="server" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="100">
                            商家簡介 ：
                        </td>
                        <td>
                            <asp:Label ID="lbl_info" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
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
