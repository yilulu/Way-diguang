<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="productview1.aspx.cs" Inherits="DTcms.Web.productview1" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/Like.ascx" TagName="Like" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab1(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs11 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_11 */
                menu.className = i == cursel ? "hover" : ""; /*三目運算 等號優先*/
                con.style.display = i == cursel ? "block" : "none";
            }
        }
//-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>詳細資料</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj">
            <div class="cs_sj1">
                <span class="cs_sj1_bt">
                    <asp:Label ID="lblTitle" runat="server" /></span>
                <div id="tbody">
                    <div id="mainbody">
                        <img src="<%=Image1Url %>" width="345" height="262" id="mainphoto" rel="<%=Image1Url %>" />
                    </div>
                    <img src="../images/cs_zuo.jpg" width="18" height="38" id="goleft" />
                    <img src="../images/cs_you.jpg" width="18" height="38" id="goright" />
                    <div id="photos">
                        <div id="showArea">
                            <!--
          SRC: 縮圖地址
          REL: 大圖地址
          NAME: 網址
        -->
                            <%=Images %>
                            <%-- <img src="../images/01.jpg" alt="懶人圖庫" width="68" height="50" rel="../images/01.jpg"
                                name="http://www.lanrentuku.com" />
                            <img src="../images/02.jpg" alt="懶人圖庫" width="68" height="50" rel="../images/02.jpg"
                                name="http://www.lanrentuku.com" />
                            <img src="../images/01.jpg" alt="懶人圖庫" width="68" height="50" rel="../images/01.jpg"
                                name="http://www.lanrentuku.com" />
                            <img src="../images/01.jpg" alt="懶人圖庫" width="68" height="50" rel="../images/01.jpg"
                                name="http://www.lanrentuku.com" />
                            <img src="../images/01.jpg" alt="懶人圖庫" width="68" height="50" rel="../images/01.jpg"
                                name="http://www.lanrentuku.com" />--%>
                        </div>
                    </div>
                </div>
                <div id="cs_sj_right2">
                    <span class="cs_sj_right2_1">租金：<span class="cs_sj_right_zi"><asp:Label ID="lblprice"
                        runat="server" /></span>元/月</span> <span class="cs_sj_right2_1">押金：<asp:Label ID="lblyajin"
                            runat="server" />個月</span> <span class="cs_sj_right2_1">格局：<asp:Label ID="lblhuxing"
                                runat="server" /></span> <span class="cs_sj_right2_1">座向：<asp:Label ID="lblzuoxiang"
                                    runat="server" /></span> <span class="cs_sj_right2_1">坪數：<asp:Label ID="lblpingshu"
                                        runat="server" /></span> <span class="cs_sj_right2_1">樓層：<asp:Label ID="lbllouceng"
                                            runat="server" /></span> <span class="cs_sj_right2_1">型態：<asp:Label ID="lblxingneng"
                                                runat="server" /></span> <span class="cs_sj_right2_1">用途：<asp:Label ID="lblyongtu"
                                                    runat="server" /></span> <span class="cs_sj_right2_2">車位：<asp:Label ID="lblchwei"
                                                        runat="server" /></span> <span class="cs_sj_right2_2">社區：<asp:Label ID="lblshequ"
                                                            runat="server" /></span> <span class="cs_sj_right2_2">地址：<asp:Label ID="lbldizhi"
                                                                runat="server" /></span>
                    <div id="cs_dianhua">
                        <span class="cs_dianhua1">0975-020-994</span> <span class="cs_dianhua2">打電話時請說在帝光上面看到的</span>
                    </div>
                </div>
                <!-- 輪播 JS-->
                <script src="js/lunbo.js" type="text/javascript"></script>
            </div>
            <div id="main1">
                <div id="menubox1">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <li id="zzjs1" onmousemove="setTab1('zzjs',1,6)" class="hover">詳細資料</li>
                        <li id="zzjs2" onmousemove="setTab1('zzjs',2,6)">地區和周邊</li>
                    </ul>
                </div>
                <div id="conten1">
                    <div class="www_zzjs_net_show1" id="con_zzjs_1">
                        <asp:Label ID="lblContent" runat="server" Text="" />
                    </div>
                    <div class="www_zzjs_net_show1" id="con_zzjs_2">
                        <ul class="tab_conbox" id="tab_conbox2">
                            <li class="tab_con"><span class="cs_huan1"><span class="cs_huan2">區域:</span><span
                                class="cs_huan4"><asp:Label ID="lblquyu" runat="server" /></span></span> <span class="cs_huan1">
                                    <span class="cs_huan2">總價:</span><span class="cs_huan4"><asp:Label ID="lbljiaqianqj"
                                        runat="server" /></span></span> <span class="cs_huan1"><span class="cs_huan2">面積:</span><span
                                            class="cs_huan3"><asp:Label ID="lblmianji" runat="server" /></span></span>
                                <span class="cs_huan1"><span class="cs_huan2">戶型:</span><span class="cs_huan4"><asp:Label
                                    ID="lblhuxing2" runat="server" /></span></span> <span class="cs_huan1"><span class="cs_huan2">
                                        方式:</span><span class="cs_huan4"><asp:Label ID="lblfangshi" runat="server" /></span></span>
                            </li>
                        </ul>
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
                    <iframe width="800" height="400" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" src="http://ditu.google.cn/?ie=UTF8&amp;ll=35.86166,104.195397&amp;spn=34.956781,86.572266&amp;t=m&amp;z=4&amp;brcurrent=3,0x31508e64e5c642c1:0x951daa7c349f366f,1%3B5,0,1&amp;output=embed">
                    </iframe>
                </div>
            </div>
            <div id="cs_sj_xia1">
                <span class="cs_sj_xia1_wz">留言討論</span>
                <div class="goomap">
                </div>
                <div class="liuyan1">
                    <h1>
                        問：這裏是問題， 這裏是問題， 這裏是問題，這裏是問題，。<span class="fbsj">發表於：2014/02/25 09:30</span></h1>
                    <h2>
                        答：群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區群峰搬家-萬華區</h2>
                </div>
                <div class="liuyan">
                    <div class="liuyan2">
                        <h1>
                            我要提問</h1>
                        <div class="pllr">
                            <textarea name="" cols="" rows=""></textarea>
                        </div>
                        <div class="plan">
                            <input type="submit" name="button" id="button" value="提交問題" />
                            <input type="reset" name="button2" id="button2" value="重填內容" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ziye_middle_right">
            <span class="tjfy">推薦房源</span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
            &nbsp;</div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
    <div id="cs_xia3">
        <span class="cs_xia4">您可能感興趣的房子 </span>
        <uc2:Like ID="Like1" runat="server" />
        &nbsp;</div>
</asp:Content>
