<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="shjdown.ascx.cs" Inherits="DTcms.Web.shjdown" %>
<asp:Repeater ID="repdata" runat="server">
    <ItemTemplate>
        <li class="cllm3"><a href="userlook.aspx?id=<%#Eval("id") %>">
            <img src="<%#Eval("avatar") %>" border="0" width="155" height="165" /></a>
            <div class="djbt">
                <%#Eval("user_name") %></div>
        </li>
    </ItemTemplate>
</asp:Repeater>
