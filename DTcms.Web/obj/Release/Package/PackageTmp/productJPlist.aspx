<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true"
    CodeBehind="productJPlist.aspx.cs" Inherits="DTcms.Web.WebForm12" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>帝光精品</div>
    <div id="dgjp_chugui">
        <span class="dgjp_tjsp">
            <%=CataName%></span>
        <asp:Repeater ID="repdatetemai" runat="server">
            <ItemTemplate>
                <ul class="dg_tj">
                    <li class="tjsp_1"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                        <img src="<%#Eval("img_url")%>" border="0" width="185" height="139" /></a></li>
                    <li class="tjsp_2"><a href="producJPview.aspx?id=<%#Eval("id") %>&mid=<%#Eval("channel_id") %>">
                        <%#ToSubstring(Eval("title").ToString(),11)%>
                    </a></li>
                    <li class="tjsp_3">
                        <%#Eval("sell_price")%>元</li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
        <div id="fenye">
            <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                <tr>
                    <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                        runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                        NextPageText="下一頁" ShowInputBox="Always"
                        SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" 
                        ShowDisabledButtons="False" onpagechanging="aspPage_PageChanging">
                    </webdiyer:AspNetPager>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
