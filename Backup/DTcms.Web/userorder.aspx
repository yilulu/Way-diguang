<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="userorder.aspx.cs" Inherits="DTcms.Web.userorder" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../../../admin/images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>交易的資料</div>
    <div style="clear: both; height: 10px">
    </div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">我的交易資料</span>
        </div>
        <div id="vip_3_3">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                 <tr style="text-align: center;">
                    <td>
                        訂單號
                    </td>
                    <td>
                        圖片
                    </td>
                    <td>
                        價格
                    </td>
                    <td>
                        下單時間
                    </td>
                    <td>
                        訂單狀態
                    </td>
                   <%-- <td>
                    </td>--%>
                </tr>
                <asp:Repeater ID="repddata" runat="server">
                    <ItemTemplate>
                        <%--<a href="<%#Eval("img_url")%>" target="_blank"><img src="<%#Eval("img_url")%>"  name="vip_ico3_3" id="vip_ico3_3" /></a>--%>
                        <tr style="text-align: center;">
                            <td>
                                <%#Eval("order_no")%>
                            </td>
                            <td>
                                <%#Getdateil(Convert.ToInt32(Eval("id")))%>
                            </td>
                            <td>
                                $<%#Eval("order_amount")%>
                            </td>
                            <td>
                                <%#Eval("add_time", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td>
                                <%#GetOrderStatus(Eval("id").ToString())%>
                            </td>
                          <%--  <td>
                                <%#CreateUrl(Eval("payment_id").ToString(), Eval("id").ToString())%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="fenye">
                <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                    <tr>
                        <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                            runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                            NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
                            SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                        </webdiyer:AspNetPager>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
