<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ShopList.aspx.cs" Inherits="DTcms.Web.ShopList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="style/zzsc.css" />
    <link rel="stylesheet" type="text/css" href="style/style.css" />
    <link href="style/Search.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><a href="ShopList.aspx">商家列表</a></div>
    <div class="jplbx">
        <div id="cllm" style="height: auto;">
            <ul>
                <asp:Repeater ID="repdata" runat="server">
                    <ItemTemplate>
                        <li class="cllm3"><a href="userlook.aspx?id=<%#Eval("id") %>">
                            <img src="<%#Eval("avatar") %>" border="0" width="155" height="165" /></a>
                            <div class="djbt">
                                <%#Eval("user_name") %></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
