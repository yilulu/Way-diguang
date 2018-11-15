<%@ Page Title="" Language="C#" MasterPageFile="~/Site4.Master" AutoEventWireup="true"
    CodeBehind="Notibook.aspx.cs" Inherits="DTcms.Web.Notibook" EnableEventValidation="false" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/Like.ascx" TagName="Like" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.ad-gallery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var dp1 = $("#dpProvince");
            var dp2 = $("#dpCity");
            //            var dp3 = $("#dpArea");
            //            var dp4 = $("#dpRows");
            //填充省的数据      
            loadAreas("0", "dpProvince");
            //给省绑定事件，触发事件后填充市的数据
            $(dp1).bind("change keyup", function () {
                var provinceID = dp1.attr("value");
                loadAreas(provinceID, "dpCity");
                //$("#" + dpArea).html("");
                //                $("#" + dpRows).html("");
                dp2.fadeIn("slow");
            });
            //给市绑定事件，触发事件后填充区的数据
            $(dp2).bind("change keyup", function () {
                var cityID = dp2.attr("value");
                loadAreas(cityID, "dpArea");
                //                $("#" + dp4).html("<option value='' selected='selected'>請選擇</option>");
                //                dp3.fadeIn("slow");
            });
            //给縣绑定事件，触发事件后填充区的数据
            //            $(dp3).bind("change keyup", function () {
            //                var cityID = dp3.attr("value");
            //                loadAreas(cityID, "dpRows");
            //                dp4.fadeIn("slow");
            //            });
        });
        function loadAreas(val, item) {
            $.ajax({
                type: "post",
                url: "tools/HtmlAjax.ashx",
                data: {
                    id: val,
                    a: Math.random()
                },
                error: function () {
                    return false;
                },
                success: function (data) {
                    var i;
                    var json = eval(data);
                    $("#" + item).html("");
                    $("#" + item).append("<option value='' selected='selected'>請選擇</option>");
                    for (i = 0; i < json.length; i++) {
                        $("#" + item).append($("<option></option>").val(json[i].c_code).html(json[i].c_name));
                    };
                }
            });
        }
    </script>
    <script type="text/javascript">

        function check(obj,value) {
            if(<%=HtmlisLogin %>==1)
            {
                if (obj.checked&&value==29) { $(".xqtj").show() } else {
                    $(".xqtj").hide()
                }
            }
            
        }
        $(function () {
            var galleries = $('.ad-gallery').adGallery();
            $('#switch-effect').change(
      function () {
          galleries[0].settings.effect = $(this).val();
          return false;
      }
    );
            $('#toggle-slideshow').click(
      function () {
          galleries[0].slideshow.toggle();
          return false;
      }
    );
            $('#toggle-description').click(
      function () {
          if (!galleries[0].settings.description_wrapper) {
              galleries[0].settings.description_wrapper = $('#descriptions');
          } else {
              galleries[0].settings.description_wrapper = false;
          }
          return false;
      }
    );
        });

        function valdata() {
            var UserName = $("#<%=txtUserName.ClientID %>").val();
            var sex=$('input[name="ctl00$ContentPlaceHolder1$radioSex"]:checked').val(); 
            var Phone = $("#<%=txtPhone.ClientID %>").val();
            var Tel = $("#<%=txtTel.ClientID %>").val();
            var Content = $("#<%=txtContent.ClientID %>").val();
            var zhuzhi = $('input[name="chkZhuZhi"]:checked').val();
            var leibie = $('input[name="chkCatagory"]:checked').val();
            var Email = $("#<%=txtEmail.ClientID %>").val();
            // var sheng=$("#dpProvince").val();
            // var shi=$("#dpCity").val();
            //var address=$("#<%=txtWrite.ClientID %>").val();
            var Ower = $("#<%=txtOwer.ClientID %>").val();
            var OwerTel = $("#<%=txtOwerTel.ClientID %>").val();
            var regStr = /^[_a-zA-Z0-9\-]+(\.[_a-zA-Z0-9\-]*)*@[a-zA-Z0-9\-]+([\.][a-zA-Z0-9\-]+)+$/;
            var regStrPhone = /^[1][3-8]\d{9}$|^([6|9])\d{7}$|^[0][9]\d{8}$|^[6]([8|6])\d{5}$/;
            if (UserName == "") {
                alert("請輸入姓名");
                $("#<%=txtUserName.ClientID %>").focus();
                return false;
            }
            if (sex == ""||sex==undefined) {
                alert("請選擇性別");
                return false;
            }
            if (Phone == "") {
                alert("請輸入手機");
                $("#<%=txtPhone.ClientID %>").focus();
                return false;
            }
            var chkPhone = regStrPhone.test(Phone);
            if (!chkPhone) {
                alert("聯繫手機格式錯誤!");
                $("#<%=txtPhone.ClientID %>").focus();
                return false;
            }
            if (Tel == "") {
                alert("請輸入住家電話");
                $("#<%=txtTel.ClientID %>").focus();
                return false;
            }
            
            var chk = regStr.test(Email);
            if (!chk) {
                alert("郵箱格式輸入不正確！");
                $("#<%=txtEmail.ClientID %>").focus();
                return false;
            }
            if (zhuzhi == undefined) {
                alert("請選擇主旨內容");
                $('input[name="chkZhuZhi"]:checked').focus();
                return false;
            }
            if(<%=HtmlisLogin %>==1&&zhuzhi=="介紹房屋出售")
            {
                
                if (Ower == "") {
                    alert("請輸入屋主姓名");
                    $("#<%=txtOwer.ClientID %>").focus();
                    return false;
                }  
                if (OwerTel == "") {
                    alert("請輸入屋主聯絡電話");
                    $("#<%=txtOwerTel.ClientID %>").focus();
                    return false;
                }
            
            }
            if (Content == "") {
                alert("請輸入內容");
                $("#<%=txtContent.ClientID %>").focus();
                return false;
            }

        }
    </script>
    <style type="text/css">
        .liuyanb {
            font-size: 14px;
            line-height: 30px;
            border-top-width: 1px;
            border-right-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-top-color: #CCC;
            border-right-color: #CCC;
        }

            .liuyanb td {
                padding: 5px;
                border-bottom-width: 1px;
                border-left-width: 1px;
                border-bottom-style: solid;
                border-left-style: solid;
                border-bottom-color: #CCC;
                border-left-color: #CCC;
            }

        .xqtj {
            display: none;
        }

        .liuyanblr {
            height: 120px;
            width: 450px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>留言板
    </div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj">
            <div id="main1">
                <table border="0" class="liuyanb" cellspacing="0" cellpadding="0" align="left" width="800">
                    <tr>
                        <td width="250" align="right">姓名 (必填)
                        </td>
                        <td width="492" align="left">
                            <input type="text" name="textfield8" id="txtUserName" runat="server" />
                        </td>
                        <td width="200" align="right">性別 (必填)
                        </td>
                        <td width="283">
                            <input type="radio" name="radioSex" id="radio" value="男" runat="server" />
                            男
                            <input type="radio" name="radioSex" id="radio2" value="女" runat="server" />
                            女 (勾選)
                        </td>
                    </tr>
                    <tr>
                        <td width="250" align="right">手機 (必填)
                        </td>
                        <td width="492">
                            <label for="textfield">
                            </label>
                            <input type="text" name="textfield" id="txtPhone" runat="server" />
                        </td>
                        <td width="200" align="right">住家電話 (必填)
                        </td>
                        <td>
                            <input type="text" name="textfield2" id="txtTel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="250" align="right">電子郵件
                        </td>
                        <td width="492" colspan="3">
                            <input type="text" name="textfield3" id="txtEmail" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="250" align="right">主旨 (必填)
                        </td>
                        <td width="492" colspan="3">
                            <label for="checkbox1">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" onclick="check(this,1)" id="checkbox1"
                                    value="房屋買賣" />
                                房屋買賣</label>
                            <label for="checkbox2">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" onclick="check(this,2)" id="checkbox2"
                                    value="房屋租賃" />
                                房屋租賃</label>
                            <label for="checkbox3">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" onclick="check(this,3)" id="checkbox3"
                                    value="土地買賣" />
                                土地買賣</label>
                            <label for="checkbox4">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" onclick="check(this,4)" id="checkbox4"
                                    value="土地租賃" />
                                土地租賃</label>
                            <label for="checkbox5">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" id="checkbox5" onclick="check(this,5)"
                                    value="裝潢設計" />
                                裝潢設計</label>
                            <label for="checkbox6">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" id="checkbox6" onclick="check(this,6)"
                                    value="帝光精品" />
                                帝光精品</label>
                            <label for="checkbox7">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" id="checkbox7" onclick="check(this,7)"
                                    value="便利生活" />
                                便利生活</label>
                            <label for="checkbox8">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" id="checkbox8" onclick="check(this,8)"
                                    value="商業聯盟" />
                                商業聯盟</label>
                            <label for="checkbox9">
                                <input name="chkZhuZhi" type="checkbox" id="checkbox9" onclick="check(this,9)" value="其它問題" />
                                其它問題</label>
                            <label for="checkbox10">
                                <input name="chkZhuZhi" type="checkbox" id="checkbox10" onclick="check(this,10)"
                                    value="意見分享" />
                                意見分享</label>
                            <label for="checkbox29">
                                <input name="chkZhuZhi" type="checkbox" class="fztc" id="checkbox29" onclick="check(this,29)"
                                    value="介紹房屋出售" />
                                介紹房屋出售<span style="color: Red;">(可享回饋點數5000點)</span></label>
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <td width="250" align="right">地點
                        </td>
                        <td width="492" colspan="3">
                            <select id="dpProvince" name="dpProvince">
                            </select>
                            -
                            <select id="dpCity" name="dpCity">
                                <option value="0">請選擇</option>
                            </select>
                            -
                            <input type="text" id="txtWrite" name="txtWrite" runat="server" />
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <td width="250" align="right">類別
                        </td>
                        <td width="492" colspan="3">
                            <input type="checkbox" name="chkCatagory" id="checkbox22" value="透天厝" />
                            透天厝
                            <input type="checkbox" name="chkCatagory" id="checkbox11" value="住宅大樓" />
                            住宅大樓
                            <input type="checkbox" name="chkCatagory" id="checkbox14" value="華廈 " />
                            華廈
                            <input type="checkbox" name="chkCatagory" id="checkbox15" value="公寓" />
                            公寓
                            <input type="checkbox" name="chkCatagory" id="checkbox17" value="雅房" />
                            雅房
                            <input type="checkbox" name="chkCatagory" id="checkbox21" value="店面" />
                            店面
                            <input type="checkbox" name="chkCatagory" id="checkbox20" value="廠辦" />
                            廠辦
                            <input type="checkbox" name="chkCatagory" id="checkbox12" value="工廠" />
                            工廠
                            <input type="checkbox" name="chkCatagory" id="checkbox13" value="倉庫" />
                            倉庫
                            <input type="checkbox" name="chkCatagory" id="checkbox19" value="農地" />
                            農地
                            <input type="checkbox" name="chkCatagory" id="checkbox18" value="重劃區" />
                            重劃區
                            <input type="checkbox" name="chkCatagory" id="checkbox23" value="其它" />
                            其它
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <td width="250" align="right">屋主姓名 (必填)
                        </td>
                        <td width="492">
                            <label for="textfield">
                            </label>
                            <input type="text" name="textfield" id="txtOwer" runat="server" />
                        </td>
                        <td width="200" align="right">屋主聯絡電話 (必填)
                        </td>
                        <td>
                            <input type="text" name="textfield2" id="txtOwerTel" runat="server" />
                        </td>
                    </tr>
                    <%-- <tr class="xqtj">
                        <td width="250" align="right">
                            功能
                        </td>
                        <td width="492" colspan="3">
                            <input type="checkbox" name="chkFunction" id="checkbox24" value="自用" />
                            自用
                            <input type="checkbox" name="chkFunction" id="checkbox25" value="投資" />
                            投資
                            <input type="checkbox" name="chkFunction" id="checkbox26" value="傳承" />
                            傳承
                            <input type="checkbox" name="chkFunction" id="checkbox27" value="節稅" />
                            節稅
                            <input type="checkbox" name="chkFunction" id="checkbox28" value="其它" />
                            其它
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <td width="250" align="right">
                            預算
                        </td>
                        <td width="492">
                            <input name="textfield4" type="text" id="txtMoneyMin" size="6" runat="server" />
                            萬 至
                            <input name="textfield5" type="text" id="txtMoneyMax" size="6" runat="server" />
                            萬
                        </td>
                        <td width="200" align="right">
                            坪數
                        </td>
                        <td width="283">
                            <input name="textfield6" type="text" id="txtMianjiMin" size="5" runat="server" />
                            坪 至
                            <input name="textfield7" type="text" id="txtMianjiMax" size="5" runat="server" />
                            坪
                        </td>
                    </tr>--%>
                    <tr>
                        <td width="250" align="right">內容 (必填)
                        </td>
                        <td width="492" colspan="3">
                            <label for="textarea">
                            </label>
                            <textarea name="txtContent" class="liuyanblr" id="txtContent" runat="server" cols="45"
                                rows="5"></textarea>
                        </td>
                    </tr>
                    <%--<tr>
                <td width="250" align="right">
                    上傳檔案
                </td>
                <td width="492" colspan="3">
                    <p>
                        <label for="fileField">
                        </label>
                        <input type="file" name="fileField" id="fileField" />
                </td>
            </tr>--%>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="送出留言" OnClientClick="return valdata()"
                                OnClick="btnSave_Click" />
                            <input type="submit" name="button" id="button" value="" />
                            <input type="reset" name="button2" id="button2" value="重填內容" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
