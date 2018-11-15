<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="UserNeed.aspx.cs" Inherits="DTcms.Web.UserNeed" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<%@ Register Src="Usercontrol/Like.ascx" TagName="Like" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <link href="style/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
    <link type="text/css" rel="stylesheet" href="admin/images/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><a href="UserNeed">市場需求</a></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj">
            <div id="main1">
                <asp:Repeater ID="rptList" runat="server">
                    <HeaderTemplate>
                        <table width="100%" class="xqbd" border="0" cellspacing="0" cellpadding="0" class="msgtable"
                            style="font-size: 18px;">
                            <tr>
                                <td width="150" align="center">
                                    <strong>買/租</strong>
                                </td>
                                <td width="150" align="center">
                                    <strong>地區</strong>
                                </td>
                                <td width="150" align="center">
                                    <strong>類型 </strong>
                                </td>
                                <td width="150" align="center">
                                    <strong>坪數</strong>
                                </td>
                                <td width="150" align="center">
                                    <strong>備註</strong>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("typeName")%>
                            </td>
                            <td align="center">
                                <%#Eval("AreaName")%>
                            </td>
                            <td align="center">
                                <%#Eval("CataName")%>
                            </td>
                            <td align="center">
                                <%#Eval("MianJi")%>
                            </td>
                            <td align="center">
                                <%#Eval("spec")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div class="line15">
                </div>
                <div class="page_box">
                    <div id="PageContent" runat="server" class="flickr right">
                    </div>
                    <div class="left">
                        顯示<asp:TextBox ID="txtPageNum" runat="server" CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));"
                            OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox>條/頁
                    </div>
                </div>
                <div class="line10">
                </div>
            </div>
        </div>
        <div id="ziye_middle_right">
            <span class="tjfy">推薦房源</span>
            <uc1:TuiJian ID="TuiJian1" runat="server" />
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
    <%--<div id="cs_xia3">
        <span class="cs_xia4">您可能感興趣的房子</span>
        <uc2:Like ID="Like1" runat="server" />
    </div>--%>
    <%--<div id="dibu">
        <span class="dibu_1">Copyright@2007-2013 by Addcn Technology Co;Ltd ALL Rights Reserved</span>
    </div>--%>
    <%-- <!-- 代码开始 -->
    <div id="tbox">
        <a id="pinglun" href="http://diguang.myflysoft.com/Notibook.aspx"></a><a id="gotop"
            href="javascript:void(0)"></a>
    </div>--%>
    <!-- 代码结束 -->
</asp:Content>
