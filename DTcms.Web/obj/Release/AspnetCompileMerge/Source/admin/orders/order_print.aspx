<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_print.aspx.cs" Inherits="DTcms.Web.admin.orders.order_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>列印商品訂單</title>
    <script type="text/javascript">
        function printWin() {
            var oWin = window.open("", "_blank");
            oWin.document.write(document.getElementById("content").innerHTML);
            oWin.focus();
            oWin.document.close();
            oWin.print()
            oWin.close()
        }
    </script>
</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
    <div id="content">
        <table width="800" border="0" align="center" cellpadding="3" cellspacing="0" style="font-size: 12px;
            font-family: '微軟雅黑'; background: #fff;">
            <tr>
                <td width="346" height="50" style="font-size: 20px;">
                    <%=siteConfig.webname%>商品訂單
                </td>
                <td width="216">
                    訂單號：<%=model.order_no%><br />
                    日&nbsp;&nbsp; 期：<%=model.add_time.ToString("yyyy-MM-dd")%>
                </td>
                <td width="220">
                    操&nbsp; 作 人：<%=managerModel.user_name%><br />
                    列印時間：<%=DateTime.Now%>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 10px 0; border-top: 1px solid #000;">
                    <asp:Repeater ID="rptList" runat="server">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" style="font-size: 12px;
                                font-family: '微軟雅黑'; background: #fff;">
                                <tr>
                                    <td align="left" style="background: #ccc;">
                                        商品名稱</th>
                                        <td width="12%" align="left" style="background: #ccc;">
                                            銷售價
                                        </td>
                                        <td width="12%" align="left" style="background: #ccc;">
                                            市場價
                                        </td>
                                        <%--<td width="10%" align="left" style="background: #ccc;">
                                            折抵點數
                                        </td>--%>
                                        <td width="10%" align="left" style="background: #ccc;">
                                            數量
                                        </td>
                                        <td width="12%" align="left" style="background: #ccc;">
                                            金額合計
                                        </td>
                                        <%-- <td width="12%" align="left" style="background: #ccc;">
                                            積分合計
                                        </td>--%>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("goods_name")%>
                                </td>
                                <td>
                                    <%#Eval("goods_price")%>
                                </td>
                                <td>
                                    <%#Eval("real_price")%>
                                </td>
                                <%-- <td>
                                    <%#Convert.ToInt32(Eval("point")) > 0 ? "+" + Eval("point").ToString() : Eval("point").ToString()%>
                                </td>--%>
                                <td>
                                    <%#Eval("quantity")%>
                                </td>
                                <td>
                                    <%#Convert.ToDecimal(Eval("goods_price")) * Convert.ToInt32(Eval("quantity"))%>
                                </td>
                                <%-- <td>
                                    <%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border-top: 1px solid #000;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="5" style="margin: 5px auto;
                        font-size: 12px; font-family: '微軟雅黑'; background: #fff;">
                        <tr>
                            <td width="44%">
                                會員帳號：<%=model.user_name%>
                            </td>
                            <td width="56%">
                                客戶名稱：<%=model.accept_name%><br />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <!--索取發票：-->
                                付款方式：
                                <%=new DTcms.BLL.payment().GetTitle(model.payment_id) %>
                                <%if (model.payment_status == 2)
                                  { %>
                                (已付款)
                                <%}
                                  else
                                  { %>
                                (待付款)
                                <%} %>
                            </td>
                            <td>
                                送貨地址：<%=model.address%><br />
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3" valign="top">
                                用戶留言：<%=model.message%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 機：<%=model.mobile%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                固定電話：<%=model.telphone%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" style="border-top: 1px solid #000;
                        font-size: 12px; font-family: '微軟雅黑'; background: #fff;">
                        <tr>
                            <td width="44%">
                                配送方式：<%=new DTcms.BLL.distribution().GetTitle(model.distribution_id)%>
                            </td>
                            <td width="56%" colspan="2">
                                商品金額：＄<%=model.real_amount-model.real_freight+model.point%>
                                + 運費：＄<%=model.real_freight%>
                                + 付款手續費：＄<%=model.payment_fee%>
                                - 折抵金額：＄<%=model.point%>
                                = 訂單總額：<%=model.order_amount%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="margin-top: 30px;">
        <button onclick="printWin();return false;">
            確認無誤馬上列印</button>
    </div>
    </form>
</body>
</html>
