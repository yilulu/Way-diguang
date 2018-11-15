<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true"
    CodeBehind="Search.aspx.cs" Inherits="DTcms.Web.Search" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="style/zzsc.css">
    <link rel="stylesheet" type="text/css" href="style/style.css">
    <link href="style/Search.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jplbx">
        <div class="jplbxleft">
            <div id="HtmlList" runat="server">
            </div>
        </div>
        <div class="jplbxright">
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <ul style='display: '>
                        <li class="4l_xiaotu"><a href="producJPview.aspx?id=<%#Eval("ID") %>&mid=5">
                            <img src="<%#Eval("img_url") %>" width='170' height='150' border="0" /></a></li>
                        <a href="producJPview.aspx?id=<%#Eval("ID") %>&mid=5"><span class="l_jp_wenzi_tu">
                            <%#Eval("title")%>
                        </span></a>
                        <li class="l_jp_wenzi_tu2"><span class="miaoshu1">
                            <%#Eval("sell_price")%></span></li>
                    </ul>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<div align=\"center\">暫無記錄</div>" : ""%>
                    </ul> </div>
                </FooterTemplate>
            </asp:Repeater>
            <div id="fenye">
                <div class="page_box">
                    <div id="PageContent" runat="server" class="flickr right">
                    </div>
                    <div class="left">
                        顯示<asp:TextBox ID="txtPageNum" runat="server" CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));"
                            OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox>條/頁
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
