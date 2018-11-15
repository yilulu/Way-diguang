<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="mem.aspx.cs" Inherits="DTcms.Web.mem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .hyflx td
        {
            border-top-width: 1px;
            border-right-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-top-color: #CCC;
            border-right-color: #CCC;
        }
        #aspnetForm #ziye_middle #ziye_middle_left_sj #cs_sj_xia1 div .hyflx .zcan
        {
            background-image: url(images/20080405154401570.png);
            height: 35px;
            width: 154px;
            color: #FFF;
            font-size: 14px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" src="js/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="js/top.js"></script>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/zzsc.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".sousuo_22").each(function () {
                var chushi = $(this).val();
                $(this).focus(function () {
                    if ($(this).value == "") {
                        $(this).val(chushi);
                    } else {
                        $(this).val("");
                    }
                })
                $(this).blur(function () {
                    if ($(this).val() == "") {
                        $(this).val(chushi);
                    }
                })
            })
        })
    </script>
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

        });
        function ShowValue(i) {
            $("#hh" + i).show();
        }
        function HideValue(i) {
            $("#hh" + i).hide();
        }
        //賦值
        function GetModel(obj) {
            var txt1 = $(obj).html();
            $("#title0").val(txt1);
            if (txt1 == "出售") {
                $("#ctl00_hdf0").val(2);
            }
            else {
                $("#ctl00_hdf0").val(3);
            }
            HideValue(0);
        }
        function gets_value(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#ctl00_hdf1").val(QID);
            if (!isNaN(QID)) {
                GetArea(QID);
            }
            HideValue(i);
        }
        //賦值
        function getsCounry(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#ctl00_hdf2").val(QID);
            HideValue(i);
        }
        function GetArea(id) {
            $.ajax({
                type: "get",
                url: "tools/pagAjax.ashx",
                data: "id=" + id,
                success: function (data) {
                    $("#hh1").hide();
                    $("#hh2").html(data);
                    $("#hh2").show();
                }
            })
        }
        function SetValue(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#ctl00_hdf4").val(QID);
            HideValue(i);
        }
        function Sethuxing(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#ctl00_hdf5").val(QID);
            HideValue(i);
        }
    </script>
    <script type="text/javascript">
        function $$$$$(_sId) {
            return document.getElementById(_sId);
        }
        function hide(_sId)
        { $$$$$(_sId).style.display = $$$$$(_sId).style.display == "none" ? "" : "none"; }
        function pick(v) {
            document.getElementById('am').value = v;
            hide('HMF-1')
        }
        function pick2(v) {
            document.getElementById('bm2').value = v;
            hide('HMF-2')
        }
        function pick3(v) {
            document.getElementById('bm3').value = v;
            hide('HMF-3')
        }
        function bgcolor(id) {
            document.getElementById(id).style.background = "#F7FFFA";
            document.getElementById(id).style.color = "#000";
        }
        function nocolor(id) {
            document.getElementById(id).style.background = "";
            document.getElementById(id).style.color = "#788F72";
        }
    </script>
    <style type="text/css">
        #Layer1
        {
            position: absolute;
            left: 6px;
            top: 3109px;
            width: 354px;
            height: 65px;
            z-index: 1;
            background-color: #C3C3C3;
        }
    </style>
    <style type="text/css">
        .pager
        {
            margin: 10px auto 0px auto;
        }
        .pager td
        {
            font-size: 12px;
            padding: 2px;
        }
        .pager td a
        {
            border: 1px solid #CECECE;
            float: left;
            font-size: 12px;
            font-weight: normal;
            height: 23px;
            line-height: 25px;
            margin: 20px 10px 0 0;
            padding: 0;
            text-align: center;
            width: 40px;
        }
        #aspPage_input
        {
            border: 1px;
            margin-bottom: 2px;
        }
        .curpage
        {
            border: 0px solid #CECECE;
            float: left;
            font-size: 12px;
            font-weight: normal;
            height: 23px;
            line-height: 25px;
            margin: 24px 10px 0 0;
            padding: 0;
            text-align: center;
            width: 40px;
        }
        .trunspage
        {
            border: 0px solid #CECECE;
            float: left;
            font-size: 12px;
            font-weight: normal;
            height: 23px;
            line-height: 25px;
            margin: 25px 10px 0 0;
            padding: 0;
            text-align: center;
            width: 60px;
        }
        .trunspage input
        {
            border: 1px solid #CECECE;
        }
    </style>
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body, td, th
        {
            font-family: "normal 宋体" , Arial, Helvetica, sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>註冊</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj" style="width: 100%">
            <div id="cs_sj_xia1" style="width: 100%">
                <span class="cs_sj_xia1_wz" style="text-align: center"><span id="ctl00_ContentPlaceHolder1_lblTitle">
                    會員制度及好康</span></span>
                <div style="text-align: right">
                    &nbsp;&nbsp;</div>
                <div style="line-height: 24px;">
                    <span id="ctl00_ContentPlaceHolder1_lblContent"></span>
                    <table width="100%" border="1" cellpadding="5" class="hyflx" cellspacing="0" style="border-bottom: 1px solid #CCC;
                        border-left: 1px solid #CCC;">
                        <tr>
                            <td align="center">
                                <strong>會員</strong>
                            </td>
                            <td width="400" align="center">
                                <strong>說 明</strong>
                            </td>
                            <td align="center">
                                <p align="center">
                                    <strong>朋友成為以下會員</strong><strong> </strong>
                                    <br />
                                    <strong>可回饋點數</strong><strong> </strong>
                                </p>
                                <strong>(以同等級回饋)</strong>
                            </td>
                            <td align="center">
                                註冊
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>普通卡</strong>
                            </td>
                            <td width="400">
                                <p>
                                    條件：填寫個人基本資料
                                </p>
                                權益：購物可回饋
                                <%=bllUser.GetZheKou(1)%>% 績點
                            </td>
                            <td align="center">
                                <strong>無</strong>
                            </td>
                            <td align="center">
                                <input type="button" name="button" id="button" onclick="javascript:location.href='http://<%=HostUrl %>/register.aspx?type=1'"
                                    class="zcan" value="註冊普通卡" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>尊榮卡</strong>
                            </td>
                            <td width="400">
                                <p>
                                    條件：入會費100元
                                </p>
                                權益：購物可回饋
                                <%=bllUser.GetZheKou(2)%>% 績點
                            </td>
                            <td align="center">
                                <strong>50點</strong>
                            </td>
                            <td align="center">
                                <input type="button" name="button2" id="button2" onclick="javascript:location.href='http://<%=HostUrl %>/register.aspx?type=2'"
                                    class="zcan" value="註冊尊榮卡" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>柏金卡</strong>
                            </td>
                            <td width="400">
                                <p>
                                    條件：入會費200元
                                </p>
                                權益：購物可回饋
                                <%=bllUser.GetZheKou(3)%>% 績點
                            </td>
                            <td align="center">
                                <strong>100點</strong>
                            </td>
                            <td align="center">
                                <input type="button" name="button3" id="button3" onclick="javascript:location.href='http://<%=HostUrl %>/register.aspx?type=3'"
                                    class="zcan" value="註冊柏金卡" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>御皇卡</strong>
                            </td>
                            <td width="400">
                                <p>
                                    條件：入會費300元
                                </p>
                                權益：購物可回饋
                                <%=bllUser.GetZheKou(4)%>% 績點
                            </td>
                            <td align="center">
                                150點
                            </td>
                            <td align="center">
                                <input type="button" name="button4" id="button4" onclick="javascript:location.href='http://<%=HostUrl %>/register.aspx?type=4'"
                                    class="zcan" value="註冊御皇卡" />
                            </td>
                        </tr>
                    </table>
                    <span>
                        <p style="text-align: center;">
                            &nbsp;</p>
                        <p style="text-align: center;">
                            <br />
                        </p>
                        <p style="text-align: center;">
                            &nbsp;</p>
                        <p>
                            <br />
                        </p>
                    </span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
