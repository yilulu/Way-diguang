<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="sell.aspx.cs" Inherits="DTcms.Web.front.sell" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>編輯商品資料</title>
        <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
        <link href="../admin/images/style.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
        <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
        <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
        <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
        <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
        <script type='text/javascript' src="../../scripts/swfupload/swfupload.js"></script>
        <script type='text/javascript' src="../../scripts/swfupload/swfupload.queue.js"></script>
        <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
        <script type="text/javascript" src="../admin/js/function.js"></script>
        <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
        <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
        <script language="javascript" type="text/javascript" src="../admin/js/getdate/WdatePicker.js"></script>
        <script type="text/javascript">
            //載入編輯器
            $(function () {
                var editor = KindEditor.create('textarea[name="ctl00$ContentPlaceHolder1$txtContent"]', {
                    resizeType: 1,
                    uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                    fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                    allowFileManager: true
                });

            });
            //表單驗證
            $(function () {
                $("#aspnetForm").validate({
                    invalidHandler: function (e, validator) {
                        parent.jsprint("有 " + validator.numberOfInvalids() + " 項填寫有誤，請檢查！", "", "Warning");
                    },
                    errorPlacement: function (lable, element) {
                        //可見元素顯示錯誤提示
                        if (element.parents(".tab_con").css('display') != 'none') {
                            element.ligerTip({ content: lable.html(), appendIdTo: lable });
                        }
                    },
                    success: function (lable) {
                        lable.ligerHideTip();
                    }
                });
            });
            //初始化上傳控制項
            $(function () {
                InitSWFUpload("../../tools/upload_ajax.ashx", "Filedata", "1000KB", "../../scripts/swfupload/swfupload.swf", 1, 1);
            });
            //計算用戶組價格
            $(function () {
                $("#txtSellPrice").change(function () {
                    var sprice = $(this).val();
                    if (sprice > 0) {
                        $(".groupprice").each(function () {
                            //$(this).val($(this).attr("discount") * sprice / 100);
                            var num = $(this).attr("discount") * sprice / 100;
                            $(this).val(ForDight(num, 2));
                            //$(this).val(num);
                        });
                    }
                });
            });
            //四捨五入函數
            function ForDight(Dight, How) {
                Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
                return Dight;
            }
        </script>
        <script type="text/javascript">
            function showxiajia() {
                var ch = $("#xiajiacheck").attr("checked");
                if (ch == true) {
                    $("#xiajiatext").css("display", "");
                }
                else {
                    $("#xiajiatext").css("display", "none");
                    $("#xiajiatext").val("");
                }
            }
        </script>
    </head>
    <style>
        #contentTab select {
            border: 1px solid #ccc !important;
        }
    </style>
    <body class="mainbody">

        <%--<div class="navigation">
        首頁 &gt; 商品管理 &gt; 編輯資料</div>--%>
        <div id="contentTab" style="width: 1100px; margin: 5px auto;">
            <ul class="tab_nav">
                <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本資料</a></li>
                <li><a onclick="tabs('#contentTab',1);" href="javascript:;">詳細描述</a></li>
                <%-- <li><a onclick="tabs('#contentTab',2);" href="javascript:;">SEO選項</a></li>--%>
            </ul>
            <div class="tab_con" style="display: block;">
                <table class="form_table">
                    <col width="150px">
                    <col>
                    <tbody>
                        <tr>
                            <th>所屬類別：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlCategoryId" CssClass="select2" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>商品名稱：
                            </th>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" Width="350"
                                    MaxLength="100" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>桌數：
                            </th>
                            <td>
                                <asp:TextBox ID="txtZhuoShu" runat="server" CssClass="txtInput normal required" Width="350"
                                    Text="0" MaxLength="100" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>尺寸：
                            </th>
                            <td>
                                <asp:TextBox ID="txtSize" runat="server" CssClass="txtInput normal required" Width="350"
                                    Text="0" MaxLength="100" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>廚房：
                            </th>
                            <td>有
                            <asp:CheckBox ID="chkKitchen" runat="server" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>舞台：
                            </th>
                            <td>有
                            <asp:CheckBox ID="chkStage" runat="server" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>音響：
                            </th>
                            <td>有
                            <asp:CheckBox ID="chkSound" runat="server" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>投螢幕：
                            </th>
                            <td>有
                            <asp:CheckBox ID="chkScreen" runat="server" />
                            </td>
                        </tr>
                        <tr class="y1" style="display: none;">
                            <th>形式：
                            </th>
                            <td>
                                <asp:TextBox ID="ddlForm" runat="server" Text="0" CssClass="txtInput normal required"
                                    Width="350" MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <th>添加時間：
                            </th>
                            <td>
                                <asp:TextBox ID="txtTime" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                    runat="server" CssClass="txtInput " MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>是否首頁顯示：
                            </th>
                            <td>
                                <asp:DropDownList ID="cblItem" runat="server">
                                    <asp:ListItem Value="0" Selected="True">請選擇</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="2">否</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>首頁顯示位置：
                            </th>
                            <td>
                                <asp:DropDownList ID="CheckBoxList1" runat="server">
                                    <asp:ListItem Value="" Selected="True">請選擇</asp:ListItem>
                                    <asp:ListItem Value="1">新房屋上架</asp:ListItem>
                                    <asp:ListItem Value="2">不動產出租</asp:ListItem>
                                    <asp:ListItem Value="3">不動產出售</asp:ListItem>
                                    <asp:ListItem Value="4">裝潢設計</asp:ListItem>
                                    <asp:ListItem Value="5">帝光精品</asp:ListItem>
                                    <asp:ListItem Value="6">新房屋推薦</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>縣市鄉鎮：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlAreaid" runat="server">
                                    <asp:ListItem Value="0" Selected="True">請選擇</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>價格：
                            </th>
                            <td>
                                <asp:TextBox ID="txtMarketPrice" runat="server" CssClass="txtInput small required number"
                                    MaxLength="10">0</asp:TextBox><span id="TiShi1" runat="server"></span>
                                <label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>每坪單價：
                            </th>
                            <td>
                                <asp:TextBox ID="txtSinglePrice" runat="server" CssClass="txtInput small required number"
                                    MaxLength="10">0</asp:TextBox><span id="TiShi" runat="server"></span>
                                <label>
                                </label>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptPrice" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th valign="top" style="padding-top: 10px;">會員價格：
                                    </th>
                                    <td>
                                        <table border="0" cellspacing="0" cellpadding="0" class="border_table">
                                            <tbody>
                                                <col width="80px">
                                                <col>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <th>
                                        <%#Eval("title")%>
                                    </th>
                                    <td>
                                        <asp:HiddenField ID="hidePriceId" runat="server" />
                                        <asp:HiddenField ID="hideGroupId" Value='<%#Eval("id") %>' runat="server" />
                                        <asp:TextBox ID="txtGroupPrice" runat="server" size="10" discount='<%#Eval("discount") %>'
                                            CssClass="txtInput groupprice small required number" MaxLength="10">0</asp:TextBox>
                                        <label>
                                            享受<%#Eval("discount") %>折優惠</label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table> </td> </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                        <tr id="trjifen" runat="server">
                            <th>購物積分：
                            </th>
                            <td>
                                <asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput small required number"
                                    MaxLength="10">0</asp:TextBox>
                                <label>
                                    *如果整數則返還用戶積分，負數則扣取積分</label>
                            </td>
                        </tr>
                        <%--  <tr>
                        <th>
                            排序數字：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">99</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            瀏覽次數：
                        </th>
                        <td>
                            <asp:TextBox ID="txtClick" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox>
                        </td>
                    </tr>--%>
                        <!-- <tr id="trid" runat="server">
                            <th>下架：
                            </th>
                            <td>
                                <input id="xiajiacheck" runat="server" onclick='showxiajia()' type="checkbox" />
                                <input id="xiajiatext" type="text" runat="server" class="txtInput" style="width: 80%; display: none" />
                            </td>
                        </tr>-->
                        <tr>
                            <th>權狀：
                            </th>
                            <td>
                                <asp:TextBox ID="txtMianJi" runat="server" class="txtInput normal required" Text="0"></asp:TextBox>(坪)
                            </td>
                        </tr>
                        <tr>
                            <th>戶型：
                            </th>
                            <td>
                                <cc1:MyDropDownList ID="ddlhuxing" runat="server" Where=" inherit_index=4" Table_ID_Name="dt_sys_model*id*title">
                                </cc1:MyDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>捷運沿線：
                            </th>
                            <td>
                                <cc1:MyDropDownList ID="ddlditie" runat="server">
                                </cc1:MyDropDownList>
                            </td>
                        </tr>
                        <tr id="tryajin" runat="server">
                            <th>押金：
                            </th>
                            <td>
                                <asp:TextBox ID="txtyajin" runat="server" class="txtInput normal left" Text="0" /><label>
                                    數位,月單位</label>
                            </td>
                        </tr>
                        <tr>
                            <th>座向：
                            </th>
                            <td>
                                <asp:TextBox ID="txtzuoxiang" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>樓層：
                            </th>
                            <td>
                                <asp:TextBox ID="txtlouceng" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>屋齡 ：
                            </th>
                            <td>
                                <asp:TextBox ID="txtDiggGood" runat="server" class="txtInput normal left" />(年)
                            </td>
                        </tr>
                        <tr>
                            <th>公共設施：
                            </th>
                            <td>
                                <asp:TextBox ID="txtxingneng" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr id="tdyongtu" runat="server">
                            <th>用途：
                            </th>
                            <td>
                                <asp:TextBox ID="txtyongtu" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>車位：
                            </th>
                            <td>有
                            <asp:CheckBox ID="chkPort" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>社區名稱：
                            </th>
                            <td>
                                <asp:TextBox ID="txtshequ" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>聯絡人：
                            </th>
                            <td>
                                <asp:TextBox ID="txtuser" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>聯繫電話：
                            </th>
                            <td>
                                <asp:TextBox ID="txtdianhua" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <tr>
                            <th>公司：
                            </th>
                            <td>
                                <asp:TextBox ID="txtgongsi" runat="server" class="txtInput normal left" />
                            </td>
                        </tr>
                        <%-- <tr>
                        <th>
                            服務專案：
                        </th>
                        <td>
                            <asp:TextBox ID="txtfuwxiangmu" runat="server" class="txtInput normal left" Width="500px" />
                        </td>
                    </tr>--%>
                        <tr>
                            <th>物件地址：
                            </th>
                            <td>
                                <asp:TextBox ID="txtdizhi" runat="server" class="txtInput normal left" Width="500px"
                                    onblur="Get_info()" />
                            </td>
                        </tr>
                        <tr>
                            <th>物件經緯度：
                            </th>
                            <td>
                                <asp:TextBox ID="txtY" runat="server" class="txtInput normal left" Width="500px" />
                                <label>
                                    格式：20.3232|120.34242432</label>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top" style="padding-top: 10px;">上傳圖片：
                            </th>
                            <td>
                                <input type="text" class="txtInput normal left" />
                                <div class="upload_btn">
                                    <span id="upload"></span>
                                </div>
                                <label>
                                    <%--可以上傳多張圖片。--%></label>
                                <div class="clear">
                                </div>
                                <!--封面隱藏值.開始-->

                                <input type="hidden" name="focus_photo" id="focus_photo" value="" />

                                <%--<asp:HiddenField ID="focus_photo" runat="server" />--%>
                                <!--封面隱藏值.結束-->
                                <!--上傳提示.開始-->
                                <div id="show">
                                </div>
                                <!--上傳提示.結束-->
                                <!--圖片清單.開始-->
                                <div id="show_list">
                                    <ul>
                                        <asp:Literal ID="LitAlbumList" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                                <!--圖片清單.結束-->
                            </td>
                        </tr>
                        <!--
            <tr>
                <th>擴展屬性：</th>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" class="border_table">
                        <tbody>
                        <col width="80px"><col>
                        <tr>
                            <th>屬性一</th>
                            <td><input name="nav_url" type="text" value="" class="txtInput middle" /></td>
                        </tr>
                        <tr>
                            <th>屬性二</th>
                            <td><input name="nav_url" type="text" value="" class="txtInput middle" /></td>
                        </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            -->
                        <!--擴展屬性.開始-->
                        <asp:Literal ID="LitAttributeList" runat="server"></asp:Literal>
                        <!--擴展屬性.結束-->
                    </tbody>
                </table>
            </div>
            <div class="tab_con">
                <table class="form_table">
                    <col width="150px">
                    <col>
                    <tbody>
                        <tr>
                        </tr>
                        <tr>
                            <th valign="top">詳細描述：
                            </th>
                            <td>
                                <textarea id="txtContent" cols="100" rows="8" style="width: 99%; height: 350px; visibility: hidden; word-wrap: break-word;"
                                    runat="server"></textarea>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="tab_con">
                <table class="form_table">
                    <col width="150px">
                    <col>
                    <tbody>
                        <tr>
                            <th>SEO標題：
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="255" CssClass="txtInput normal" />
                            </td>
                        </tr>
                        <tr>
                            <th>SEO關鍵字：
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeoKeywords" runat="server" MaxLength="255" TextMode="MultiLine"
                                    CssClass="small" />
                            </td>
                        </tr>
                        <tr>
                            <th>SEO描述：
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeoDescription" runat="server" MaxLength="255" TextMode="MultiLine"
                                    CssClass="small" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="foot_btn_box">
                <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
                &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
            </div>
        </div>
        <script type="text/javascript">
            $(function(){
                var aa='<%=img_Url%>';
                $("#focus_photo").val(aa);
            });
            if(<%=CataID %>==298||<%=CataID %>==299)
            {
                $(".y1").show();
            }
            $("select[name='ddlCategoryId']").unbind("change").change(function () {
                if ($("#ddlCategoryId").val() == 298||$("#ddlCategoryId").val()==299) {
                    $(".y1").show();
                }else{
                    $(".y1").hide();
                }

            });
        </script>
        <script type="text/javascript">

            function Get_info() {
                getLat();
            }
            //ajax獲取類別目錄
            ///隨機函數

            var xmlhttp;
            var myMath = 1000;
            xmlhttp = null;
            if (window.XMLHttpRequest)
                xmlhttp = new XMLHttpRequest();
            else
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

            function getLat() {
                var add = $("#<%=txtdizhi.ClientID %>").val();
                xmlhttp.open("GET", "../../tools/getLat.ashx?add=" + escape(add) + "&math=" + myMath, true);
                xmlhttp.onreadystatechange = doadd;
                xmlhttp.send(null);
            }
            function doadd() {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        var result = xmlhttp.responseText;
                        $("#<%=txtY.ClientID %>").val(result);
                    }
                }
            }
        </script>
    </body>
    </html>

</asp:Content>
