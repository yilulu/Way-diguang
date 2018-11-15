<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true"
    CodeBehind="producJPview.aspx.cs" Inherits="DTcms.Web.WebForm4" %>

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
    </script>
    <script type="text/javascript">
        function jia() {
            var num = $("#text_box").val();
            var nums = parseFloat(num) + 1;
            $("#text_box").val(nums);
        }
        function jian() {
            var num = $("#text_box").val();
            if (num > 1) {
                $("#text_box").val(num - 1);
            }

        }
        function js_Update(id, v) {
            $("#txt_num").val(v);
            $("#txtnum").html(v);
        }
        function ToCart() {
            var num = $("#text_box").val();
            window.open("usercart.aspx?id=<%=id %>&Num=" + num + "");
        }
        function ToOrder() {
            var num = $("#text_box").val();
            window.open("orderadd.aspx?id=<%=id %>&Num=" + num + "");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><a href="productdsgx.aspx?mid=5">帝光精品</a></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj">
            <div class="cs_sj1">
                <span class="cs_sj1_bt">
                    <asp:Label ID="lbltitle2" runat="server" /></span>
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
                    <h1>
                        商品名稱：<asp:Label ID="lbltitle" runat="server" /></h1>
                    <h2>
                        市場價：$<asp:Label ID="lblMarkePrice" runat="server"></asp:Label>元</h2>
                    <h2>
                        網路價：$<asp:Label ID="lblprice" runat="server" />元</h2>
                    <span class="dsgx_sj21">
                        <asp:Label ID="lblIntroduce" runat="server"></asp:Label></span>
                    <input type="hidden" name="txt_num" id="Hidden1" value="1" />
                </div>
                <div class="aniu">
                    <span class="dsgx_sj21">數量：
                        <input id="min" class="gmshulinagjj" name="" onclick="jian()" type="button" value="-" />
                        <input id="text_box" class="gmshulianginput" name="text_box" type="text" value="1"
                            style="text-align: center; width: 20px; border: 1px solid #ccc;" />
                        <input id="add" class="gmshulinagjj" onclick="jia()" name="" type="button" value="+" />
                    </span>
                </div>
                <div class="aniu">
                    <%=HtmlFee %>
                </div>
                <div class="aniu">
                    <span class="dsgx_sj21" style="text-align: center">
                        <%if (Status == 3)
                          {%>
                        <span style="color: Red; font-weight: bold;">已出售完</span>
                        <%}
                          else
                          {%>
                        <asp:ImageButton ID="btnAddcart" runat="server" ImageUrl='images/gouwuche.jpg' Width="118px"
                            Height="34px" OnClick="btnAddcart_Click" />
                        <asp:ImageButton ID="btnAddOrder" runat="server" ImageUrl='images/ljjz.png' Width="118px"
                            Height="34px" OnClick="btnAddOrder_Click" />
                        <%} %>
                        <%--</a>--%>
                    </span>
                </div>
            </div>
            <div id="main1">
                <div id="menubox1">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" class="hover">商品資料</li>
                    </ul>
                    <!--樣式2 點擊選項卡 -->
                </div>
                <div id="conten1">
                    <div class="www_zzjs_net_show1" id="con_zzjs_1">
                        <asp:Label ID="lblContent" runat="server" Text="" />
                    </div>
                </div>
            </div>
        </div>
        <div id="ziye_middle_right">
            <span class="tjfy_jp">最新商品</span>
            <asp:Repeater ID="repdatezuixin" runat="server">
                <ItemTemplate>
                    <ul class="dg_sjj">
                        <li class="4l_xiaotu"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                            <img src="<%#Eval("img_url")%>" name="dgjp_tu1" border="0" width='170' height='150'
                                id="dgjp_tu1" /></a></li>
                        <a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>"><span
                            class="l_jp_wenzi_tu_sanji">
                            <%# ToSubstring(Eval("title"),9)%></span></a>
                        <li class="l_jp_wenzi_tu2"><span class="miaoshu1_sanji">
                            <%#Eval("sell_price")%>元</span><span class="miaoshu2_sanji">銷量：<%#Eval("click")%>件</span></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
    <div id="dgjp_chugui">
        <span class="dgjp_tjsp">特價商品</span>
        <asp:Repeater ID="repdatetemai" runat="server">
            <ItemTemplate>
                <ul class="dg_tj">
                    <li class="tjsp_1"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                        <img src="<%#Eval("img_url")%>" name="dgjp_tu1" border="0" width='185' height='139'
                            id="dgjp_tu1" /></a></li>
                    <li class="tjsp_2"><a href="producJPview?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                        <%#Eval("title")%></a></li>
                    <li class="tjsp_3">
                        <%#Eval("sell_price")%>元</li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
