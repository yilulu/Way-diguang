<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Like.ascx.cs" Inherits="DTcms.Web.Like" %>
<asp:Repeater ID="repdate1" runat="server">
    <ItemTemplate>
        <ul class="cs_xia3_1">
            <li class="cs_xia3_1_1"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                <img src="<%#Eval("img_url")%>" name="cs_xia41" width='224' height='149' id="cs_xia41" />
            </a></li>
            <li class="cs_xia3_1_2"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                <%#ToSubstring(Eval("title").ToString(),15)%>
            </a></li>
            <li class="cs_xia3_1_3">
                <%#getSellOrHire(Eval("sell_price").ToString())%></li>
        </ul>
    </ItemTemplate>
</asp:Repeater>
