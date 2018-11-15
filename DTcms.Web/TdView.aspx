<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="TdView.aspx.cs" Inherits="DTcms.Web.TdView" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/Like.ascx" TagName="Like" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.ad-gallery.js" type="text/javascript"></script>
    <script type="text/javascript">
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
            var Content = $("#txtContent").val();
            if (Content == "") {
                alert("請輸入評論內容");
                $("#txtContent").focus();
                return false;
            }
        }
        function SetNull() {
            $("#txtContent").val() == "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>>詳細資料
    </div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj">
            <div class="cs_sj1">
                <span class="cs_sj1_bt">
                    <asp:Label ID="lblTitle" runat="server" /></span>
                <%--<div id="tbody">
                    <div id="mainbody">
                        <img src="<%=Image1Url %>" width="345" height="262" id="mainphoto" rel="<%=Image1Url %>" />
                    </div>
                    <img src="../images/cs_zuo.jpg" width="18" height="38" id="goleft" />
                    <img src="../images/cs_you.jpg" width="18" height="38" id="goright" />
                    <div id="photos">
                        <div id="showArea">
                            <%=Images %>
                        </div>
                    </div>
                </div>--%>
                <div id="gallery" class="ad-gallery" style="float: left">
                    <div class="ad-image-wrapper">
                    </div>
                    <div class="ad-controls" style="display: none">
                    </div>
                    <div class="ad-nav">
                        <div class="ad-thumbs">
                            <ul class="ad-thumb-list">
                                <%=Images %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="cs_sj_right2">
                    <span class="cs_sj_right2_1" id="sell" runat="server">單價：<span class="cs_sj_right_zi"><asp:Label ID="lblSellPrice" runat="server" /></span>元/坪</span>
                    <span class="cs_sj_right2_1" id="sellTd" runat="server">總價：<span class="cs_sj_right_zi"><asp:Label ID="lblTotalPrice" runat="server" /></span>萬</span>
                    <span class="cs_sj_right2_1" id="hire" runat="server">租金：<span class="cs_sj_right_zi"><asp:Label ID="lblZjprice" runat="server" /></span>元/月</span>
                    <span class="cs_sj_right2_1">案件編號：<asp:Label ID="lblNo" runat="server" /></span>
                    <span class="cs_sj_right2_1">公告現值：<asp:Label ID="lblyajin" runat="server" />元/㎡</span>
                    <span class="cs_sj_right2_1">坪數：約<asp:Label ID="lblpingshu" runat="server" />坪</span>
                    <span class="cs_sj_right2_1">地上物：<asp:Label ID="lblhuxing" runat="server" /></span>
                    <span class="cs_sj_right2_1">持分：<asp:Label ID="lblAge" runat="server" /></span>
                    <span class="cs_sj_right2_1">地目：<asp:Label ID="lbllouceng" runat="server" /></span>
                    <%--  <span class="cs_sj_right2_1">用途：<asp:Label ID="lblyongtu" runat="server" /></span>--%>
                    <span class="cs_sj_right2_1">使用分區：<asp:Label ID="lblFenQu" runat="server" /></span>
                    <span class="cs_sj_right2_1">容積率：<asp:Label ID="lblchwei" runat="server" /></span>
                    <span class="cs_sj_right2_2">建蔽率：<asp:Label ID="lblStation" runat="server" /></span>
                    <span class="cs_sj_right2_2">現況用途：<asp:Label ID="lblxingneng" runat="server" /></span>


                    <span class="cs_sj_right2_2">面前路寬：<asp:Label ID="lbldizhi" runat="server" /></span>
                    <div id="cs_dianhua">
                        <span class="cs_dianhua1"><span class="cs_sj_right2_2">電話：</span>
                            <asp:Label ID="LabTel" runat="server" />
                            <asp:Label ID="lblConnet" runat="server" /></span> <span class="cs_dianhua2">打電話時請說在帝光上面看到的</span>
                    </div>
                </div>
            </div>
            <div id="main1">
                <div id="menubox1">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" onmousemove="setTab1('zzjs',1,6)" class="hover">詳細資料</li>
                    </ul>
                </div>
                <div id="conten1">
                    <div class="www_zzjs_net_show1" id="con_zzjs_1">
                        <asp:Label ID="lblContent" runat="server" Text="" />
                    </div>
                </div>
            </div>
            <div id="cs_sj_xia1">
                <span class="cs_sj_xia1_wz">圖片展示</span>
                <asp:Repeater ID="repdateImgae" runat="server">
                    <ItemTemplate>
                        <span class="cs_sj_xia_tu">
                            <img src="<%#Eval("big_img") %>" name="cs_sj_tu" id="cs_sj_tu" /></span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="cs_sj_xia1">
                <span class="cs_sj_xia1_wz">Google 地圖</span>
                <div class="goomap">
                    <%--<iframe width="800" height="400" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" src="http://ditu.google.cn/maps?f=q&amp;source=s_q&amp;hl=zh-TW&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E5%A3%AB%E6%9E%97%E5%8D%80%E6%89%BF%E5%BE%B7%E8%B7%AF%E5%9B%9B%E6%AE%B5+210+%E8%99%9F+4F&amp;aq=&amp;sll=<%=X %>,<%=Y %>&amp;sspn=0.012301,0.021844&amp;brcurrent=3,0x31508e64e5c642c1:0x951daa7c349f366f,0%3B5,0,0&amp;ie=UTF8&amp;hq=&amp;hnear=<%=big5Address %>&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed">
                    </iframe>--%><iframe width="800" height="400" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" src="http://ditu.google.cn/maps?f=q&amp;source=s_q&amp;hl=zh-TW&amp;geocode=&amp;q=<%=big5Address %>&amp;aq=&amp;sll=<%=X %>,<%=Y %>&amp;sspn=0.012301,0.021844&amp;brcurrent=3,0x31508e64e5c642c1:0x951daa7c349f366f,0%3B5,0,0&amp;ie=UTF8&amp;hq=&amp;hnear=<%=big5Address %>&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed"></iframe>
                </div>
            </div>
        </div>
        <div id="ziye_middle_right">
            <span class="tjfy">推薦房源</span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
            &nbsp;
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
    <div id="cs_xia3">
        <span class="cs_xia4">您可能感興趣的房子 </span>
        <uc2:Like ID="Like1" runat="server" />
        &nbsp;
    </div>
</asp:Content>
