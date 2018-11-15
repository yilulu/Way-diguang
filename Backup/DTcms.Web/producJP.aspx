<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true"
    CodeBehind="producJP.aspx.cs" Inherits="DTcms.Web.WebForm3" %>

<%@ Register Src="Usercontrol/shjdown.ascx" TagName="shjdown" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="bdccz_1l">
        <a href="productJPlist.aspx?cid=1">
            <img src="../images/dgjp_1.jpg" name="dgjp_1" width="125" height="40" id="dgjp_1" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="dgjp_yilu1">
        <div id="dgjp_left" style="width: 100%">
            <asp:Repeater ID="repdatenew" runat="server">
                <ItemTemplate>
                    <ul class="dgjp_ul2">
                        <li><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                            <img src="<%#Eval("img_url")%>" name="dgjp_tu1" border="0" width='158' height='118'
                                id="dgjp_tu1" /></a></li>
                        <a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                            <li>
                                <%#Eval("title").ToString()%></li></a>
                        <li class="jiage">
                            <%#Eval("sell_price")%>元</li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="bdccz_1l">
        <a href="productJPlist.aspx?cid=2">
            <img src="../images/dgjp_2.jpg" name="dgjp_1" width="125" height="40" id="dgjp_1" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="index_4l">
        <div id="index_4l_left">
            <div class="huiyoubt2">
                <%=HtmlTuijian %>
            </div>
        </div>
        <div id="index_4l_right">
            <asp:Repeater ID="repdateTuijian" runat="server">
                <ItemTemplate>
                    <ul style='display: <%# Container.ItemIndex==0?"none;width:0px;heigth:0px":"" %>'>
                        <li class="4l_xiaotu"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                            <img src="<%#Eval("img_url")%>" width='170' height='150' border="0" /></a></li>
                        <a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>"><span
                            class="l_jp_wenzi_tu">
                            <%#Eval("title").ToString()%>
                        </span></a>
                        <li class="l_jp_wenzi_tu2"><span class="miaoshu1">
                            <%#Eval("sell_price")%>元</span></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="bdccz_1l">
        <a href="productJPlist.aspx?cid=3">
            <img src="../images/dgjp_3.jpg" name="dgjp_1" width="125" height="40" id="dgjp_1" /></a>
        <div id="zzcz_a">
        </div>
    </div>
    <div id="index_4l">
        <div id="index_4l_left">
            <div class="huiyoubt2">
                <%=HtmlTemai %>
            </div>
        </div>
        <div id="index_4l_right">
            <asp:Repeater ID="repdatetemai" runat="server">
                <ItemTemplate>
                    <ul style='display: <%# Container.ItemIndex==0?"none":"" %>'>
                        <li class="4l_xiaotu"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                            <img src="<%#Eval("img_url")%>" width='170' height='150' border="0" /></a></li>
                        <a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>"><span
                            class="l_jp_wenzi_tu">
                            <%#Eval("title")%></span></a>
                        <li class="l_jp_wenzi_tu2"><span class="miaoshu1">
                            <%#Eval("sell_price")%>元</span></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="kong" style="height: 10px">
    </div>
    <div id="cllm">
        <ul>
            <h2 class="cllm1">
                <span class="cllm2">商家店鋪</span></h2>
            <uc1:shjdown ID="shjdown1" runat="server" />
        </ul>
    </div>
</asp:Content>
