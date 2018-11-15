<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userSJ.aspx.cs" Inherits="DTcms.Web.userSJ" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Usercontrol/HelperPage.ascx" TagName="HelperPage" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商家中心</title>
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
            background-image: url(../images/title-bg2.png);
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
            background-image: url(../images/mark-ico.png);
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
    </style>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
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
        //打開DIV層
        function disp_cc() {
            if (gets_id('hh').style.display == 'none') {
                gets_id('hh').style.display = '';
            }
            else {
                gets_id('hh').style.display = 'none';
            }
        }
        //賦值
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
        //打開DIV層
        function disp_cc1() {
            if (gets_id('hh1').style.display == 'none') {
                gets_id('hh1').style.display = '';
            }
            else {
                gets_id('hh1').style.display = 'none';
            }
        }
        //賦值
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
        //打開DIV層
        function disp_cc2() {
            if (gets_id('hh2').style.display == 'none') {
                gets_id('hh2').style.display = '';
            }
            else {
                gets_id('hh2').style.display = 'none';
            }
        }
        //賦值
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
        //打開DIV層
        function disp_cc3() {
            if (gets_id('hh3').style.display == 'none') {
                gets_id('hh3').style.display = '';
            }
            else {
                gets_id('hh3').style.display = 'none';
            }
        }
        //賦值
        function gets_value(str) {
            gets_id('class3').value = str;
            gets_id('hh3').style.display = 'none';
        }
    </script>
    <link rel="stylesheet" type="text/css" href="mouseovertabs.css" />
    <script src="mouseovertabs.js" type="text/javascript"></script>
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
            /*調用方法如下：*/
            $.jqtab("#tabs", "#tab_conbox", "click");

            $.jqtab("#tabs2", "#tab_conbox2", "mouseenter");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="top">
        <div id="top_1">
            <ul>
                <a href="user.aspx">
                    <li>會員中心</li></a>
                <img src="images/top_ico3.jpg" name="top_ico1" id="top_ico1" />
                <%if (!Common.WEBUserCurrent.IsLogin)
                  {%>
                <a href="register.aspx">
                    <li>註冊</li></a>
                <img src="images/top_ico2.jpg" name="top_ico1" id="top_ico1" />
                <a href="login.aspx">
                    <li>登入</li></a>
                <img src="images/top_ico1.jpg" name="top_ico1" id="top_ico1" />
                <%}
                  else
                  { %>
                <asp:LinkButton ID="lbtnout" runat="server" OnClick="lbtnout_Click"> <li>&nbsp;退出&nbsp;</li></asp:LinkButton>
                <%} %>
            </ul>
        </div>
    </div>
    <div id="top_2">
        <a href="index.aspx">
            <img src="images/logo.jpg" name="logo" id="logo" /></a>
        <div id="daohang">
            <ul>
                <li class="daohang1"><a href="index.aspx">首頁</a></li>
                <li class="daohang2"><a href="productlist.aspx?mid=2">出售</a></li>
                <li class="daohang2"><a href="productlist.aspx?mid=3">出租</a></li>
                <li class="daohang2"><a href="kjgh.aspx?mid=4">空間規劃</a></li>
                <li class="daohang2"><a href="viplist.aspx">VIP</a></li>
                <li class="daohang2"><a href="producJP.aspx?mid=5">帝光精品</a></li>
                <li class="daohang2"><a href="productdsgx.aspx?mid=6">便利生活</a></li>
                <li class="daohang3"><a href="news.aspx?mid=7">知識分享</a></li>
                <li class="daohang2"><a href="UserNeed.aspx">市場需求</a></li>
                <li class="daohang2"><a href="Notibook.aspx">留言諮詢</a></li>
            </ul>
        </div>
    </div>
    <%-- <div id="kong" style="height: 10px">
    </div>--%>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">首頁</a> 帝光地產聯盟>><asp:Label ID="username" runat="server" />的店舖</div>
    <div class="dj-topbu">
        <h1>
            <asp:Label ID="dianming" runat="server" /></h1>
        <h3>
            <asp:Label ID="dianmiaoshu" runat="server" /></h3>
    </div>
    <div class="dj-nav">
        <a href="#"></a><a href="#"></a><a href="#sjjsxx">個人介紹</a>
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
                                    <span class="floatright"><a href="useredit.aspx">【修改資料】</a></span> <span class="floatright">
                                        <a href="#sjjsxx">【詳細瞭解我】</a></span><asp:Label ID="username2" runat="server" />-營業員</h3>
                            </blockquote>
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
                        <%-- <td>
                            服務區域：<asp:Label ID="fuwuquyu" runat="server" />
                        </td>--%>
                        <td colspan="2">
                            聯絡電話：<asp:Label ID="mobile2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; E-mail：<asp:Label
                                ID="email2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            官網： <a href="<%=Url %>" target="_blank">
                                <asp:Label ID="lblUrl" runat="server" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
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
        <br />
        <br />
    </div>
    <div style="clear: both; height: 29px;">
    </div>
    <div id="xiaogongju">
        <div id="xiaogongju_1">
            <!--幫助-->
            <uc1:HelperPage ID="HelperPage1" runat="server" />
        </div>
    </div>
    <div id="dibu">
        <span class="dibu_1">Copyright@2007-2013 by Addcn Technology Co;Ltd ALL Rights Reserved</span>
    </div>
    </form>
</body>
</html>
