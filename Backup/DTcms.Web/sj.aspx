<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="sj.aspx.cs" Inherits="DTcms.Web.sj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">首頁</a> 帝光地產聯盟>><asp:Label ID="username" runat="server" />的店舖</div>
    <div class="dj-topbu">
        <h1>
            <asp:Label ID="dianming" runat="server" /></h1>
        <h3>
            <asp:Label ID="dianmiaoshu" runat="server" /></h3>
    </div>
    <div class="dj-nav">
        <a href="#"></a><a href="#"></a><a href="#sjjsxx">個人介紹</a>
    </div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div class="sjjs">
                <a name="sjjsxx" id="sjjsxx"></a>
                <table width="100%" border="0" cellspacing="0" cellpadding="00">
                    <tr>
                        <td width="100" rowspan="5">
                            <img src="<%=Images %>" width="150" height="180" />
                        </td>
                        <td colspan="2">
                            <blockquote>
                                <h3 class="jb">
                                    <span class="floatright"><a href="useredit.aspx">【修改資料】</a></span> <span class="floatright">
                                        <a href="#sjjsxx">【詳細瞭解我】</a></span><asp:Label ID="username2" runat="server" />-營業員</h3>
                            </blockquote>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p>
                                公司地址：<asp:Label ID="lblAdd" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>
                            服務區域：<asp:Label ID="fuwuquyu" runat="server" />
                        </td>--%>
                        <td colspan="2">
                            聯絡電話：<asp:Label ID="mobile2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; E-mail：<asp:Label
                                ID="email2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            官網： <a href="<%=Url %>" target="_blank">
                                <asp:Label ID="lblUrl" runat="server" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="note2" runat="server" />
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
