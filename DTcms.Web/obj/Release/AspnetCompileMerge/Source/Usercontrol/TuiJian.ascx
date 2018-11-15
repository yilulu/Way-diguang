<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuiJian.ascx.cs" Inherits="DTcms.Web.TuiJian" %>
<asp:Repeater ID="repdate2" runat="server">
    <ItemTemplate>
        <span class="right_tu"><a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
            <img src="<%#Eval("img_url")%>" name="ziye_right_tu" width="152" height="117" border="0"
                id="ziye_right_tu" /></a></span> <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                    <span class="tbsq_biaoti">
                        <%--<%#Eval("title")%>--%></span></a> <a href="productview.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>">
                            <span class="tbsq_neirong">
                                <%#ToSubstring(Eval("title").ToString(),20)%>
                            </span></a><span class="tbsq_nritong2">權狀:<%#Eval("mianji")%>坪</span><span class="tbsq_nritong2">
                                <%#getSellOrHire(Eval("sell_price").ToString(), Eval("channel_id").ToString())%></span>
        <span class="tbsq_nritong2">社區:<%#Eval("shequ")%></span> <span class="tbsq_nritong2">
            格局：<%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")), null)%></span> <span class="tbsq_nritong2">
                樓層：<%#Eval("louceng")%>樓</span> <span class="ziye_right_xian"></span>
    </ItemTemplate>
</asp:Repeater>
