<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg.aspx.cs" Inherits="DTcms.Web.reg" %>

<%@ Register Src="Usercontrol/top.ascx" TagName="top" TagPrefix="uc3" %>
<%@ Register Src="Usercontrol/foot.ascx" TagName="foot" TagPrefix="uc4" %>
<%@ Register Src="Usercontrol/HelperPage.ascx" TagName="HelperPage" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/lunbo.ascx" TagName="lunbo" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%=title%></title>
    <meta content="<%=keywrod%>" name="keywords" />
    <meta content="<%=describe%>" name="description" />
    <script type="text/javascript" src="js/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="js/top.js"></script>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/zzsc.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/lang/zh_CN.js"></script>
    <script src="js/reg.js"></script>
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
                $("#<%=hdf0.ClientID %>").val(2);
            }
            else {
                $("#<%=hdf0.ClientID %>").val(3);
            }
            HideValue(0);
        }
        function gets_value(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#<%=hdf1.ClientID %>").val(QID);
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
            $("#<%=hdf2.ClientID %>").val(QID);
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
            $("#<%=hdf4.ClientID %>").val(QID);
            HideValue(i);
        }
        function Sethuxing(obj, i, QID) {
            var txt1 = $(obj).html();
            var id1 = $(obj).attr("data");
            $("#title" + i).val(txt1);
            $("#<%=hdf5.ClientID %>").val(QID);
            HideValue(i);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--頭部-->
        <uc3:top id="top" runat="server">
            <!--頭部廣告圖片-->
            <div id="lunhuan">
                <uc2:lunbo ID="lunbo1" runat="server" />
            </div>
            <!--搜索-->
            <div id="souxun">
                <div class="xmkc1">
                    <div>
                        <input id="title0" style="cursor: pointer;" class="wena1" onclick="ShowValue(0);"
                            value="項目選擇" />
                        <div id="hh0" style="display: none" class="bm">
                            <span data="2" onclick="GetModel(this)" class="cur">出售</span> <span data="3" onclick="GetModel(this)"
                                class="cur">出租</span> <span onclick="HideValue(0)" class="close">關閉</span>
                        </div>
                    </div>
                </div>
                <div class="xmkc">
                    <div>
                        <input id="title1" name="class" type="text" style="cursor: pointer;" class="wena"
                            onclick="ShowValue(1);" value="行政區選擇" />
                    </div>
                    <div id="hh1" style="display: none" class="classlist">
                        <div class="qingxuanze">
                            <span onclick="HideValue(1)" class="close">關閉</span>行政區選擇
                        </div>
                        <ul>
                            <!--可迴圈  -->
                            <%=TitleHtml1 %>
                        </ul>
                    </div>
                </div>
                <div class="xmkc">
                    <div>
                        <input id="title2" name="class" type="text" class="wena" onclick="ShowValue(2);"
                            value="區域選擇" />
                    </div>
                    <div id="hh2" style="display: none" class="classlist">
                    </div>
                </div>
                <div class="xmkc">
                    <div>
                        <input id="title4" style="cursor: pointer;" class="wena" onclick="ShowValue(4);"
                            value="類別選擇" />
                        <div id="hh4" style="display: none" class="classlist">
                            <div class="qingxuanze">
                                <span onclick="HideValue(4)" class="close">關閉</span>類別選擇
                            </div>
                            <ul>
                                <%=TitleHtml2 %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="xmkc">
                    <div>
                        <input id="title5" style="cursor: pointer;" class="wena" onclick="ShowValue(5);"
                            value="戶型選擇" />
                        <div id="hh5" style="display: none" class="classlist">
                            <div class="qingxuanze">
                                <span onclick="HideValue(5)" class="close">關閉</span>戶型選擇
                            </div>
                            <ul>
                                <%=TitleHtml3 %>
                            </ul>
                        </div>
                    </div>
                </div>
                <input type="text" value="請輸入社區、街道、學校、商圈" class="sousuo_22" id="sousuo_22" runat="server"
                    name="sousuo_22">
                <input id="hdf0" runat="server" type="hidden" value="2" />
                <input id="hdf1" runat="server" type="hidden" />
                <input id="hdf2" runat="server" type="hidden" />
                <input id="hdf3" runat="server" type="hidden" />
                <input id="hdf4" runat="server" type="hidden" />
                <input id="hdf5" runat="server" type="hidden" />
                <a>
                    <asp:Button ID="btnSearch" runat="server" CssClass="loginbutton" Text="" OnClick="btnSearch_Click" />
                    <%--<asp:ImageButton ID="btnSearch" runat="server" ImageUrl='images/souxun.jpg' OnClick="btnSearch_Click" />--%>
                </a>
            </div>
            <!--中間-->
            <div id="kong" style="height: 10px">
            </div>
            <div id="ziye_ymx">
                當前位置：帝光地產聯盟>>註冊
            </div>
            <div id="login_1">
                <span class="login_1_1">個人用戶</span><span class="login_1_2">我已經註冊，現在就<span class="login_1_3"><a
                    href="login.aspx">登入</a></span></span>
            </div>
            <div id="login">
                <div id="login_left" style="width: 1000px; margin: o auto; border: 0; background: url(images/login_ys.jpg) no-repeat right 100px;">
                    <ul class="login_left1" style="width: 1000px; margin: o auto;">
                        <li><span class="login_left_1_1">組別:</span><span class="login_left_1_2">
                            <asp:DropDownList ID="ddlGroup" runat="server" onchange="select(this)">
                                <%-- <asp:ListItem Text="請選擇...." Value="0" />
                        <asp:ListItem Text="普通卡會員" Value="1" />
                        <asp:ListItem Text="尊榮卡會員" Value="2" />
                        <asp:ListItem Text="柏金卡會員" Value="3" />
                        <asp:ListItem Text="御皇卡會員" Value="4" />
                        <asp:ListItem Text="商家會員" Value="5" />--%>
                            </asp:DropDownList>
                        </span></li>
                        <li><span class="login_left_1_1">帳戶名:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" id="txtusername" onblur="chkRegUserName(this.value)" class="inputtext"
                            runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">請設定密碼:</span><span class="login_left_1_2"><input
                            name="logins1" type="password" class="inputtext" id="txtpassword" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">請確認密碼:</span><span class="login_left_1_2"><input
                            name="logins1" type="password" class="inputtext" id="txtpassword2" />
                        </span></li>
                        <li><span class="login_left_1_1">姓名:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" id="txtName" class="inputtext" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">公司名稱:</span><span class="login_left_1_2"><input
                            name="CompanyName" type="text" id="CompanyName" class="inputtext" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">介紹人帳號:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" id="txtIntroduce" class="inputtext" runat="server"
                            onblur="chkUserName(this.value)" />
                        </span></li>
                        <li><span class="login_left_1_1">照片:</span><span class="login_left_1_2"><asp:FileUpload
                            ID="fileUpImage" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">郵箱:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" class="inputtext email" onblur="chkUserEmail(this.value)" id="txtemall"
                            runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">聯繫手機:</span><span class="login_left_1_2"><input
                            name="logins1" type="text" class="inputtext" id="txtphone" runat="server" />
                        </span></li>
                        <li><span class="login_left_1_1">地址:</span><span class="login_left_1_2"><input name="logins1"
                            type="text" id="txtAddress" class="inputtext" runat="server" />
                        </span></li>
                        <li id="zhifuModel"><span class="login_left_1_1">付款方式:</span><span class="login_left_1_2"><cc1:mydropdownlist
                            id="ddlzhifu" runat="server" where=" is_lock=0" table_id_name="dt_payment*id*title">
                </cc1:mydropdownlist>
                        </span></li>
                        <div id="divsj" style="display: none">
                            <%--  <li><span class="login_left_1_1">店名:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="dianming" class="inputtext" runat="server" />
                    </span></li>
                  <li><span class="login_left_1_1">店描述:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="dianmiaoshu" class="inputtext" runat="server" />
                    </span></li>--%>
                            <%-- <li><span class="login_left_1_1">從業:</span><span class="login_left_1_2"><input name="logins1"
                        type="text" id="congye" class="inputtext" runat="server" />
                    </span></li>--%>
                            <li><span class="login_left_1_1">公司官網:</span><span class="login_left_1_2"><input
                                name="logins1" type="text" id="gongsi" class="inputtext" runat="server" />
                            </span></li>
                            <%-- <li><span class="login_left_1_1">服務區域:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="fuwuquyu" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">熟悉社區:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="shuxishequ" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">業務特長:</span><span class="login_left_1_2"><input
                        name="logins1" type="text" id="fuwutechang" class="inputtext" runat="server" />
                    </span></li>
                    <li><span class="login_left_1_1">從業經歷:</span><span class="login_left_1_2">
                        <textarea id="jingli" runat="server" class="inputtext" style="width: 800px; height: 100px;"></textarea>
                    </span></li>
                    <li><span class="login_left_1_1">證書:</span><span class="login_left_1_2">
                        <textarea id="zhengshu" runat="server" class="inputtext" style="width: 800px; height: 100px;"></textarea>
                    </span></li>--%>
                            <li><span class="login_left_1_1">帝光會員優惠:</span><span class="login_left_1_2">
                                <textarea id="note" runat="server" class="inputtext" style="width: 800px; height: 140px;"></textarea>
                            </span></li>
                        </div>
                        <%--   <li><span class="login_left_1_1">統一編號:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtsyscoe" runat="server" />
                </span></li>--%>
                        <li style="clear: both; margin: 10px auto; padding: 10px 0 10px 120px;">
                            <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/ljzc.jpg' OnClientClick="return valdata()"
                                OnClick="btnlogin_Click" />
                        </li>
                    </ul>
                </div>
                <!--<div id="login_right">
            <img src="../images/login_ys.jpg" /></div>-->
                <div class="clear">
                </div>
            </div>
            <!--幫助-->
            <div id="xiaogongju">
                <div id="xiaogongju_1">
                    <uc1:HelperPage ID="HelperPage1" runat="server" />
                </div>
            </div>
            <!--底部-->
            <div id="dibu">
                <uc4:foot ID="foot" runat="server" />
            </div>
            <!-- 代码开始 -->
            <div id="tbox">
                <a id="pinglun" href="<%=webUrl %>/Notibook.aspx"></a><a id="gotop"
                    href="javascript:void(0)"></a>
            </div>
    </form>
</body>
</html>
