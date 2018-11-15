<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="user.aspx.cs" Inherits="DTcms.Web.WebForm6" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>VIP</div>
    <span class="vip_1">歡迎您,<span class="vip_2"><asp:Label ID="lblUsername" runat="server" /></span><asp:Label
        ID="lblbUsertype" runat="server" />
        <a href="useredit.aspx">【修改資料】</a></span>
    <div id="vip_2">
        <div id="vip_2_1">
            <span class="vip_2_1_1">
                <img src="<%=Images %>" name="vip_touxiang" id="vip_touxiang" /></span>
            <div class="vip_2_1_2">
                <span class="vip_yxrz">
                    <img src="../images/vip_tu1.jpg" name="vip_tu1" id="Img1" />系統認證:未認證 去認證>></span>
                <span class="vip_yxrz">
                    <img src="../images/vip_tu1.jpg" name="vip_tu1" id="vip_tu1" />郵箱:認證</span>
                <span class="vip_yxrz">
                    <img src="../images/vip_tu2.jpg" name="vip_tu1" id="vip_tu1" />手機:未認證 | 未限制 去認證>></span>
                <span class="vip_yxrz">
                    <img src="../images/vip_tu3.jpg" name="vip_tu1" id="vip_tu1" />實名制:未認證</span>
                <span class="vip_yxrz">
                    <img src="../images/vip_tu4.jpg" name="vip_tu1" id="vip_tu1" />營業執照:未認證</span>
            </div>
        </div>
        <%--<div id="tabbox">
            <ul class="tabs" id="tabs2">
                <li><a href="#">結餘</a></li>
                <li><a href="#">我的帝光</a></li>
            </ul>
            <ul class="tab_conbox" id="tab_conbox2">
                <li class="tab_con">
                    <p>
                        <span><strong class="jieyu">$0.00</strong><em class="vip_jieyu2"><a href="#"><span
                            class="vip_chongzhi">充值</span></a><a href="#"><span class="vip_chongzhi1">餘額詳情</span></a><a
                                href="#"><span class="vip_chongzhi2">消費明細</span></a></em></span></p>
                </li>
                <li class="tab_con">
                    <p>
                        2<span><a href="http://www.51xuediannao.com/">懶人建站</a>2只收錄實用和能提高用戶體驗的代碼</span><br />
                        <span>我們只想解放出你的部分寫代碼時間來思考更高層次的設計，而不是要你懶惰、拼湊。我們只想解放出你的部分寫代碼時間來思考更高層次的設計，而不是要你懶惰、拼湊我們只想解放出你的部分寫代碼時間來思考更高層次的設計，而不是要你懶惰、拼湊</span></p>
                </li>
            </ul>
        </div>--%>
    </div>
    <div style="clear: both; height: 10px">
    </div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">我發佈的訊息</span> <span class="vip_3_2_2">
                <%if (Common.WEBUserCurrent.UserType == "5")
                  {%>
                <a href="userfabuedit.aspx"><span class="vip_ico1_1" style="font-weight: bold">點擊發佈</span></a>
                <%} %>
        </div>
        <div id="vip_3_3">
            <ul>
                <asp:Repeater ID="repddata" runat="server">
                    <ItemTemplate>
                        <li class="vip_3_3_1"><a href="<%#Eval("img_url")%>" target="_blank">
                            <img src="<%#Eval("img_url")%>" name="vip_ico3_3" id="vip_ico3_3" /></a> <a href="userfabuedit.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                                <span class="vip_3_3_1_1">
                                    <%#ToSubstring(Eval("title").ToString(),28)%>
                                </span></a><span class="vip_3_3_1_2" style="color: Red">&nbsp;&nbsp;<%#Eval("Status").ToString().Equals("0") ? "未通過" : Eval("Status").ToString().Equals("1") ? "通過" : Eval("Status").ToString().Equals("3")?"已成交":"刪除" %>&nbsp;&nbsp;</span>
                            <span class="vip_3_3_1_2">
                                <%#Eval("add_time","{0:yyyy-MM-dd}")%>&nbsp;&nbsp;</span>&nbsp; <span class="vip_3_3_1_2">
                                    <a href="orderadd.aspx?cjid=<%#Eval("id")%>">
                                        <%#Eval("Status").ToString().Equals("1") ? "[已成交]" : ""%></a></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <div id="fenye">
                    <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                        <tr>
                            <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                                runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                                NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
                                SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                            </webdiyer:AspNetPager>
                        </tr>
                    </table>
                </div>
            </ul>
        </div>
    </div>
</asp:Content>
