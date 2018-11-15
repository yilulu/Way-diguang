<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bottom.ascx.cs" Inherits="DTcms.Web.style.bottom" %>
<a name="dibum2" id="dibum2"></a>
<div class="bottombg" onmouseover="javascript:location.hash='#dibum'">
    <div class="bottom">
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">皇品購物</a></li>
                <li class="xbt"><a href="/news/?tid=10">&#8226;最新公告</a></li>
                <li class="xbt"><a href="/news/?tid=12">&#8226;活動公告</a></li>
                <li class="xbt"><a href="/about.aspx?id=10">&#8226;品牌介紹</a></li>
                <!-- <li class="xbt"><a href="/user/">&#8226;會員中心</a></li>
          <li class="xbt"><a href="/user/order.aspx">&#8226;訂單查詢</a></li>-->
            </ul>
        </div>
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">購物須知</a></li>
                <asp:Repeater ID="data_11" runat="Server">
                    <ItemTemplate>
                        <li class="xbt"><a href="/about.aspx?id=<%#Eval("id")%>">&#8226;<%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">關於我們</a></li>
                <asp:Repeater ID="data_16" runat="Server">
                    <ItemTemplate>
                        <li class="xbt"><a href="/about.aspx?id=<%#Eval("id")%>">&#8226;<%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">網站地圖</a></li>
                <asp:Repeater ID="data_17" runat="Server">
                    <ItemTemplate>
                        <li class="xbt"><a href="/about.aspx?id=<%#Eval("id")%>">&#8226;<%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">聯絡我們</a></li>
                <asp:Repeater ID="data_18" runat="Server">
                    <ItemTemplate>
                        <li class="xbt"><a href="/about.aspx?id=<%#Eval("id")%>">&#8226;<%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <li class="xbt"><a href="/feedback.aspx">&#8226;合作專區</a></li>
            </ul>
        </div>
        <div class="bottomnav">
            <ul>
                <li class="dabt"><a href="javascript:;">服務中心</a></li>
                <asp:Repeater ID="data_19" runat="Server">
                    <ItemTemplate>
                        <li class="xbt"><a href="/about.aspx?id=<%#Eval("id")%>">&#8226;<%#Eval("title")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <li class="xbt"><a href="/shop/list.aspx?tid=14">&#8226;批發團購</a></li>
                <li class="xbt"><a href="/news/?tid=13">&#8226;寢具常識</a></li>
            </ul>
        </div>
    </div>
</div>
<a name="dibum" id="dibum"></a>