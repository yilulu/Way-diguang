<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userfabuedit.aspx.cs" ValidateRequest="false"
    Inherits="DTcms.Web.userfabuedit" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>發佈資料</title>
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="admin/images/style.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/ui/js/ligerBuild.min.js" type="text/javascript"></script>
    <script src="admin/js/function.js" type="text/javascript"></script>
    <link href="scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="admin/images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="scripts/ui/js/ligerBuild.min.js"></script>
    <script type='text/javascript' src="scripts/swfupload/swfupload.js"></script>
    <script type='text/javascript' src="scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="admin/js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="editor/lang/zh_CN.js"></script>
    <script language="javascript" type="text/javascript" src="admin/js/getdate/WdatePicker.js"></script>
    <script type="text/javascript">
        //載入編輯器
        $(function () {
            var editor = KindEditor.create('textarea[name="txtContent"]', {
                resizeType: 1,
                uploadJson: 'tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: 'tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });

        });
        //表單驗證
        $(function () {
            $("#form1").validate({
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
            InitSWFUpload("tools/upload_ajax.ashx", "Filedata", "<%=siteConfig.attachimgsize%> KB", "scripts/swfupload/swfupload.swf", 1, 1);
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
                <%} %>
            </ul>
        </div>
    </div>
    <div id="top_2">
        <a href="index.aspx"><img src="images/logo.jpg" name="logo" id="logo" /></a>
        <div id="daohang">
            <ul>
                <li class="daohang1"><a href="index.aspx">首頁</a></li>
                <li class="daohang2"><a>謄本申請</a></li>
                <li class="daohang2"><a href="productlist.aspx?mid=2">出售</a></li>
                <li class="daohang2"><a href="productlist.aspx?mid=3">出租</a></li>
                <li class="daohang2"><a href="productlist.aspx?mid=4">空間規劃</a></li>
                <li class="daohang2"><a href="viplist.aspx">VIP</a></li>
                <li class="daohang2"><a href="cjhq.aspx">成交行情</a></li>
                <li class="daohang2"><a href="producJP.aspx?mid=5">帝光精品</a></li>
                <li class="daohang2"><a href="productdsgx.aspx?mid=6">搬家幫手</a></li>
                <li class="daohang3"><a href="news.aspx?mid=7">知識分享</a></li>
            </ul>
        </div>
    </div>
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>><a href="user.aspx">會員中心</a>>會員發佈</div>
    <div id="contentTab" style="width: 1100px; margin: auto">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本資料</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:;">詳細描述</a></li>
         <%--   <li><a onclick="tabs('#contentTab',2);" href="javascript:;">SEO選項</a></li>--%>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="150px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            所屬模組：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlmodel" CssClass="inputtext" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlmodel_SelectedIndexChanged">
                                <asp:ListItem Text="謄本申請" Value="1" Selected="True" />
                                <asp:ListItem Text="出售" Value="2" />
                                <asp:ListItem Text="出租" Value="3" />
                                <asp:ListItem Text="空間規劃" Value="4" />
                                <asp:ListItem Text="都市更新" Value="6" />
                                <asp:ListItem Text="知識分享" Value="4" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            所屬類別：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlCategoryId" CssClass="select2" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商品名稱：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" Width="350"
                                MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            添加時間：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTime" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                runat="server" CssClass="txtInput " MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            推薦類型：
                        </th>
                        <td>
                            <asp:DropDownList ID="cblItem" runat="server">
                                <asp:ListItem Value="0" Selected="True">請選擇</asp:ListItem>
                                <asp:ListItem Value="1">最新</asp:ListItem>
                                <asp:ListItem Value="2">推薦</asp:ListItem>
                                <asp:ListItem Value="3">特賣</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            首頁顯示位置：
                        </th>
                        <td>
                            <asp:DropDownList ID="CheckBoxList1" runat="server">
                                <asp:ListItem Value="" Selected="True">請選擇</asp:ListItem>
                                <asp:ListItem Value="1">新房屋上架</asp:ListItem>
                                <asp:ListItem Value="2">不動產出租</asp:ListItem>
                                <asp:ListItem Value="3">不動產出售</asp:ListItem>
                                <asp:ListItem Value="4">壯潢設計</asp:ListItem>
                                <asp:ListItem Value="5">帝光精品</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            縣市鄉鎮：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlAreaid" runat="server">
                                <asp:ListItem Value="0" Selected="True">請選擇</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商品貨號：
                        </th>
                        <td>
                            <asp:TextBox ID="txtGoodsNo" runat="server" CssClass="txtInput normal" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            庫存數量：
                        </th>
                        <td>
                            <asp:TextBox ID="txtStockQuantity" runat="server" CssClass="txtInput small required digits"
                                MaxLength="100">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            市場價格：
                        </th>
                        <td>
                            <asp:TextBox ID="txtMarketPrice" runat="server" CssClass="txtInput small required number"
                                MaxLength="10">0</asp:TextBox>
                            <label>
                                *只供參考的市場價格</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            銷售價格：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSellPrice" runat="server" CssClass="txtInput small required number"
                                MaxLength="10">0</asp:TextBox>
                            <label>
                                *用戶交易的實際價格</label>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptPrice" runat="server">
                        <HeaderTemplate>
                            <tr>
                                <th valign="top" style="padding-top: 10px;">
                                    會員價格：
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
                    <tr>
                        <th>
                            購物積分：
                        </th>
                        <td>
                            <asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput small required number"
                                MaxLength="10">0</asp:TextBox>
                            <label>
                                *如果整數則返還用戶積分，負數則扣取積分</label>
                        </td>
                    </tr>
                    <tr>
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
                    </tr>
                    <tr id="trid" runat="server">
                        <th>
                            下架：
                        </th>
                        <td>
                            <input id="xiajiacheck" runat="server" onclick='showxiajia()' type="checkbox" />
                            <input id="xiajiatext" type="text" runat="server" class="txtInput" style="width: 80%;
                                display: none" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商品新品：
                        </th>
                        <td>
                            <asp:TextBox ID="txtshangpintype" runat="server" CssClass="txtInput small required"
                                MaxLength="10" Text="新品" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            區域：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddlquyu" runat="server" Where=" inherit_index=1" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            總價：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddljiaqian" runat="server" Where=" inherit_index=2" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            面積：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddlmianji" runat="server" Where=" inherit_index=3" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            戶型：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddlhuxing" runat="server" Where=" inherit_index=4" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            方式：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddlfangshi" runat="server" Where=" inherit_index=5" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            捷運沿線：
                        </th>
                        <td>
                            <cc1:MyDropDownList ID="ddlditie" runat="server" Where=" inherit_index=6" Table_ID_Name="dt_sys_model*id*title">
                            </cc1:MyDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            押金：
                        </th>
                        <td>
                            <asp:TextBox ID="txtyajin" runat="server" class="txtInput normal left" /><label>
                                數位,月單位</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            座向：
                        </th>
                        <td>
                            <asp:TextBox ID="txtzuoxiang" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            樓層：
                        </th>
                        <td>
                            <asp:TextBox ID="txtlouceng" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性能：
                        </th>
                        <td>
                            <asp:TextBox ID="txtxingneng" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            用途：
                        </th>
                        <td>
                            <asp:TextBox ID="txtyongtu" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            車位：
                        </th>
                        <td>
                            <asp:TextBox ID="txtchewei" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            社區：
                        </th>
                        <td>
                            <asp:TextBox ID="txtshequ" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            聯絡人：
                        </th>
                        <td>
                            <%-- <asp:TextBox ID="" runat="server" class="txtInput normal left" />--%>
                            <asp:TextBox ID="txtuser" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            聯繫電話：
                        </th>
                        <td>
                            <asp:TextBox ID="txtdianhua" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司：
                        </th>
                        <td>
                            <asp:TextBox ID="txtgongsi" runat="server" class="txtInput normal left" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            服務項目：
                        </th>
                        <td>
                            <asp:TextBox ID="txtfuwxiangmu" runat="server" class="txtInput normal left" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            地址：
                        </th>
                        <td>
                            <asp:TextBox ID="txtdizhi" runat="server" class="txtInput normal left" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" style="padding-top: 10px;">
                            上傳圖片：
                        </th>
                        <td>
                            <input type="text" class="txtInput normal left" />
                            <div class="upload_btn">
                                <span id="upload"></span>
                            </div>
                            <label>
                                可以上傳多張圖片。</label>
                            <div class="clear">
                            </div>
                            <!--封面隱藏值.開始-->
                            <!--
                    <input type="hidden" name="focus_photo" id="focus_photo" value=""/>
                    -->
                            <asp:HiddenField ID="focus_photo" runat="server" />
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
                        <th>
                            贊成人數：
                        </th>
                        <td>
                            <asp:TextBox ID="txtDiggGood" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            反對人數：
                        </th>
                        <td>
                            <asp:TextBox ID="txtDiggBad" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            URL連結：
                        </th>
                        <td>
                            <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="txtInput normal" MaxLength="255"></asp:TextBox><label>URL跳轉地址</label>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top">
                            詳細描述：
                        </th>
                        <td>
                            <textarea id="txtContent" cols="100" rows="8" style="width: 99%; height: 350px; visibility: hidden;"
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
                        <th>
                            SEO標題：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="255" CssClass="txtInput normal" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            SEO關鍵字：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSeoKeywords" runat="server" MaxLength="255" TextMode="MultiLine"
                                CssClass="small" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            SEO描述：
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
            <asp:Button ID="btnSubmit" runat="server" Text="儲存" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
            &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
        </div>
    </div>
    </form>
</body>
</html>
