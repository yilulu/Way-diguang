<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="usercart.aspx.cs" Inherits="DTcms.Web.usercart" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .gouwuche {
            margin-right: auto;
            margin-left: auto;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
            line-height: 30px;
            font-size: 12px;
        }

            .gouwuche td {
                border-bottom-width: 1px;
                border-bottom-style: dashed;
                border-bottom-color: #CCC;
                padding-top: 5px;
                padding-bottom: 5px;
            }

            .gouwuche p {
                margin: 0px;
                padding: 0px;
                height: 25px;
                line-height: 25px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>購物車
    </div>
    <div style="clear: both; height: 10px">
    </div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">我的購物車</span> <span class="vip_3_2_2">
                <input type="button" onclick="window.location.href = 'producJP.aspx?mid=5'" style="background-image: url('images/jxgw.png'); width: 100px; height: 30px;" />
                <%if (ISb)
                  { %><%--<a href="orderadd.aspx" target="_blank">--%>
                <input type="button" onclick="window.location.href = 'orderadd.aspx'" style="background-image: url('images/addorder.jpg'); width: 100px; height: 30px;" /><%--</a>--%>
                <%} %>
            </span>
        </div>
        <div id="vip_3_3">
            <asp:Repeater ID="repddata" runat="server" OnItemCommand="repddata_ItemCommand">
                <HeaderTemplate>
                    <table width="800" border="0" align="center" cellpadding="0" cellspacing="0" class="gouwuche">
                        <tr bgcolor="#EFEFEF">
                            <td colspan="2" align="center">商品詳情
                            </td>
                            <td align="center">數 量
                            </td>
                            <td align="center">小 計
                            </td>
                            <td align="center">總計
                            </td>
                            <td align="center">操 作
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td width="150">
                            <a href="<%#Eval("img_url")%>" target="_blank">
                                <img src="<%#Eval("img_url")%>" name="vip_ico3_3" width="127" height="96" /></a>
                        </td>
                        <td width="300">
                            <span class="xgj_ms"><a href="#">
                                <%#Eval("title")%></a></span>
                        </td>
                        <td width="100" align="center">
                            <%#Eval("quantity")%>
                        </td>
                        <td width="100" align="center">
                            <%#Eval("price")%>
                        </td>
                        <td width="100" align="center">
                            <%#double.Parse(Eval("price").ToString()) * double.Parse(Eval("quantity").ToString())%>
                        </td>
                        <td width="100" align="center">
                            <p>
                                <asp:LinkButton ID="lbtndelete" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Eval("id")%>' />
                            </p>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="6" style="text-align: right; margin-right: 30px;">金額小計： $<%=cartModel.payable_amount%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right; margin-right: 30px;">運費小計： $<%=Fee%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right; margin-right: 30px;">總計： $<%=cartModel.payable_amount%>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <input id="hideFee" type="hidden" runat="server" />
        </div>
    </div>
</asp:Content>
