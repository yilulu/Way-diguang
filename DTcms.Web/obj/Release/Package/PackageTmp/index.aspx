<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="DTcms.Web.index" %>


<%@ Register Src="Usercontrol/shjdown.ascx" TagName="shjdown" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#smallImg img').mouseover(function () {
                $('#bigImg img').attr('src', $(this).attr('rel'));
                $('#bigImg p > a').text($(this).attr('alt'));
                $('#bigImg a').attr('href', $(this).parent().attr('href'));
            })
            $('#smallImg .img').hover(function () {
                $('.mask').fadeIn();
                $(this).children('.mask').hide();
            },
function () {
    $('.mask').fadeIn();
})
        })
    </script>
    <script type="text/javascript" src="js/koala.min.1.5.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="lu1">
        <img src="images/xsjfw_ico.jpg" name="lu1_ico1" id="lu1_ico1" /><a href="#"><img
            src="images/xsjfw.jpg" name="xsjfw" border="0" id="xsjfw" /></a><a href="#"><%--<img
                src="images/more.jpg" name="more" border="0" id="more" />--%></a></div>
    <div id="lu1_1">
        <div class="lunboltu">
            <div class="image_show clearfix">
                <div class="user_tabs">
                    <div class="tabs_panes">
                        <div style="display: block" class="tabs_con">
                            <%=strHtmlPost1%>
                        </div>
                    </div>
                    <div id="smallImg">
                        <div class="col_left">
                            <%=strHtmlPost1_left%>
                        </div>
                        <div class="col_right">
                            <div class="r_top">
                                <%=strHtmlPost1_top%>
                            </div>
                            <div class="r_bottom">
                                <%=strHtmlPost1_bottom%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--<iframe src="index1.html" frameborder="0" width="860px;" height="444px" scrolling="no" class="jiugongge"></iframe>
-->
        <div id="lu1_right">
            <h2 class="lu1_biaoti">
                新房屋推薦</h2>
            <ul class="lu1_nr1">
                <asp:Repeater ID="repdateNew" runat="server">
                    <ItemTemplate>
                        <li class="tu1"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                            <img src="<%#Eval("img_url")%>" width="179" height="130" border="0" /></a></li>
                        <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                            <li class="tu2">
                                <%#ToSubstring(Eval("title").ToString(), 10)%>
                            </li>
                        </a>
                        <li class="tu3"><span class="tu3_sz_1">
                            <%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")),null)%></span><span class="tu3_sz"><%#Eval("mianji")%>坪</span></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    <div id="guanggao1">
        <%=strAdvImage1 %>
    </div>
    <div id="bdccz_1l">
        <a href="productlist.aspx?mid=2">
            <img src="images/bdccs_2l.jpg" name="bdccz_1ld" border="0" id="bdccz_1ld" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="bdccz_l1_nr">
        <!--不動產出售-->
        <asp:Repeater ID="repdatepost3" runat="server">
            <ItemTemplate>
                <ul class="ul_1">
                    <li class="ul_1_1"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                        <img src="<%#Eval("img_url")%>" border="0" width="179" height="130" /></a></li>
                    <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                        <li class="ul_1_2">
                            <%#ToSubstring(Eval("title").ToString(), 10)%></li></a>
                    <li class="ul_1_3"><span class="ul_1_3_1">
                        <%#GetNameByID(Eval("huxing").ToString())%></span><span class="ul_1_3_2"><%#Eval("mianji")%>坪</span></li>
                    <li class="ul_1_4">
                        <%#Eval("sell_price").ToString() %>萬</li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="bdccz_1l">
        <a href="productlist.aspx?mid=3">
            <img src="images/bdccz_1l.jpg" name="bdccz_1ld" width="154" height="32" border="0"
                id="bdccz_1ld" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="bdccz_l1_nr">
        <!--不動產出租-->
        <asp:Repeater ID="repdatepost2" runat="server">
            <ItemTemplate>
                <ul class="ul_1">
                    <li class="ul_1_1"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                        <img src="<%#Eval("img_url")%>" width='179' height='130' border="0" /></a></li>
                    <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                        <li class="ul_1_2">
                            <%#ToSubstring(Eval("title").ToString(), 10)%>
                        </li>
                    </a>
                    <li class="ul_1_3"><span class="ul_1_3_1">
                        <%#GetNameByID(Eval("huxing").ToString())%></span><span class="ul_1_3_2"><%#Eval("mianji")%>坪</span></li>
                    <li class="ul_1_4">
                        <%#Eval("sell_price")%>元/月</li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="guanggao2">
        <%= strAdvImage2 %>
        <div id="bdccz_1l">
            <a href="kjgh.aspx?mid=4">
                <img src="images/zhsj_3l.jpg" name="bdccz_1ld" width="154" height="32" id="bdccz_1ld" /></a>
            <div id="zzcz_a">
            </div>
        </div>
        <!--裝橫設計-->
        <div id="bdccz_4l">
            <div id="bdccz_left" class="floatleft">
                <%=strHtmlPost4_left %>
            </div>
            <div id="bdccz_zhongjian">
                <!-- 代码 开始 -->
                <div id="fsD1" class="focus">
                    <div id="D1pic1" class="fPic">
                        <asp:Label ID="lblList" runat="server"></asp:Label>
                    </div>
                    <div class="fbg">
                        <div class="D1fBt" id="D1fBt">
                            <asp:Label ID="lblNum" runat="server"></asp:Label>
                        </div>
                    </div>
                    <span class="prev"></span><span class="next"></span>
                </div>
                <script type="text/javascript">
                    Qfast.add('widgets', { path: "js/terminator2.2.min.js", type: "js", requires: ['fx'] });
                    Qfast(false, 'widgets', function () {
                        K.tabs({
                            id: 'fsD1',   //焦点图包裹id  
                            conId: "D1pic1",  //** 大图域包裹id  
                            tabId: "D1fBt",
                            tabTn: "a",
                            conCn: '.fcon', //** 大图域配置class       
                            auto: 1,   //自动播放 1或0
                            effect: 'fade',   //效果配置
                            eType: 'click', //** 鼠标事件
                            pageBt: true, //是否有按钮切换页码
                            bns: ['.prev', '.next'], //** 前后按钮配置class                          
                            interval: 3000  //** 停顿时间  
                        })
                    })  
                </script>
                <!-- 代码 结束
                <%=strHtmlPost4_zhongjian%> -->
            </div>
            <div id="bdccz_left" class="floatrt">
                <%=strHtmlPost4_right %>
            </div>
        </div>
        <div id="bdccz_1l">
            <a href="producJP.aspx?mid=5">
                <img src="images/dgjp_4l.jpg" name="bdccz_1ld" width="154" height="32" border="0"
                    id="bdccz_1ld" /></a>
            <div id="zzcz_a">
            </div>
        </div>
        <div id="index_4l">
            <div id="index_4l_left">
                <div class="huiyoubt2">
                    <%=strHtmlPost5_left%>
                </div>
            </div>
            <div id="index_4l_right">
                <%=strHtmlPost5_right%>
            </div>
        </div>
    </div>
    <div id="kong" style="height: 10px">
    </div>
    <div id="bdccz_1l">
        <a href="ShopList.aspx">
            <img src="images/bdccs_25.jpg" name="bdccz_1ld" width="154" height="32" id="bdccz_1ld" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="cllm" style="height: auto;">
        <ul>
            <uc1:shjdown ID="shjdown1" runat="server" />
        </ul>
    </div>
</asp:Content>
