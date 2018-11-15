<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_edit.aspx.cs" Inherits="DTcms.Web.admin.orders.order_edit"
    ValidateRequest="false" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯訂單資料</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 銷售管理 &gt; 編輯訂單資料</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯訂單資料</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <!--訂單狀態操作.開始-->
            <div class="order_box">
                <h3>
                    &gt;&gt;更改狀態（訂單號：<%=model.order_no %>）</h3>
                <%if (model.status < 4)
                  { %>
                <div class="order_flow" style="width: 460px;">
                    <div class="order_flow_left">
                        <a title="訂單已生成" class="order_flow_input">生成</a> <span>
                            <p class="name">
                                生成訂單</p>
                            <p>
                                <%=model.add_time%></p>
                        </span>
                    </div>
                    <%if (payModel.type == 2)
                      { %>
                    <%if (model.payment_status > 1)
                      { %>
                    <div class="order_flow_arrive">
                        <a class="order_flow_input">付款</a> <span>
                            <p class="name">
                                已付款</p>
                            <p>
                                <%=model.payment_time%></p>
                        </span>
                    </div>
                    <%}
                      else
                      { %>
                    <div class="order_flow_wait">
                        <a class="order_flow_input">付款</a> <span>
                            <p class="name">
                                等待付款</p>
                        </span>
                    </div>
                    <%} %>
                    <%} %>
                    <%if (model.status < 2 || model.confirm_time == null)
                      { %>
                    <div class="order_flow_wait">
                        <asp:LinkButton ID="lbtnConfirm" runat="server" Text="確認" CssClass="order_flow_input"
                            ToolTip="點擊確認訂單" Enabled="False" OnClick="lbtnConfirm_Click" />
                        <span>
                            <p class="name">
                                確認訂單</p>
                        </span>
                    </div>
                    <%}
                      else
                      { %>
                    <div class="order_flow_arrive">
                        <a title="訂單已確認" class="order_flow_input">確認</a> <span>
                            <p class="name">
                                確認訂單</p>
                            <p>
                                <%=model.confirm_time%></p>
                        </span>
                    </div>
                    <%} %>
                    <%if (model.distribution_status < 2)
                      { %>
                    <div class="order_flow_wait">
                        <asp:LinkButton ID="lbtnSend" runat="server" Text="發貨" CssClass="order_flow_input"
                            ToolTip="點擊發貨" Enabled="False" OnClick="lbtnSend_Click" />
                        <span>
                            <p class="name">
                                商家發貨</p>
                        </span>
                    </div>
                    <%}
                      else
                      { %>
                    <div class="order_flow_arrive">
                        <a title="訂單已發貨" class="order_flow_input">發貨</a> <span>
                            <p class="name">
                                商家已發貨</p>
                            <p>
                                <%=model.distribution_time%></p>
                        </span>
                    </div>
                    <%} %>
                    <%if (model.status > 2)
                      { %>
                    <div class="order_flow_right_arrive">
                        <a title="訂單已完成" class="order_flow_input">完成</a> <span>
                            <p class="name">
                                訂單已完成</p>
                            <p>
                                <%=model.complete_time%></p>
                        </span>
                    </div>
                    <%}
                      else
                      { %>
                    <div class="order_flow_right_wait">
                        <asp:LinkButton ID="lbtnComplete" runat="server" Text="完成" CssClass="order_flow_input"
                            ToolTip="點擊完成訂單" Enabled="False" OnClick="lbtnComplete_Click" />
                        <span>
                            <p class="name">
                                完成訂單</p>
                        </span>
                    </div>
                    <%} %>
                    <div class="clear">
                    </div>
                </div>
                <%}
                  else if (model.status == 4)
                  { %>
                <div style="text-align: center; line-height: 50px; font-size: 20px; color: Red;">
                    該訂單已取消</div>
                <%}
                  else if (model.status == 5)
                  { %>
                <div style="text-align: center; line-height: 50px; font-size: 20px; color: Red;">
                    該訂單已作廢</div>
                <%} %>
            </div>
            <!--訂單狀態操作.結束-->
            <div class="line10">
            </div>
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table"
                        style="border-bottom: 0;">
                        <tr>
                            <th align="left">
                                商品名稱
                            </th>
                            <th width="12%" align="left">
                                銷售價
                            </th>
                            <th width="12%" align="left">
                                市場價
                            </th>
                            <th width="10%" align="left">
                                數量
                            </th>
                            <th width="12%" align="left">
                                金額合計
                            </th>
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
                        <td>
                            <%#Eval("quantity")%>
                        </td>
                        <td>
                            <%#Convert.ToDecimal(Eval("goods_price")) * Convert.ToInt32(Eval("quantity"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table"
                style="border-bottom: 0;">
                <tr>
                    <th width="50%" colspan="2">
                        &gt;&gt;收貨人資料
                    </th>
                    <th width="50%" colspan="2">
                        &gt;&gt;會員資料
                    </th>
                </tr>
                <tr>
                    <td width="5%" class="col">
                        收貨人：
                    </td>
                    <td>
                        <%=model.accept_name %>
                    </td>
                    <td width="5%" class="col">
                        會員帳號：
                    </td>
                    <td>
                        <%=model.user_name %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        收貨地址：
                    </td>
                    <td>
                        <%=model.address %>
                    </td>
                    <td class="col">
                        會員組別：
                    </td>
                    <td>
                        <%=groupModel.title %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        郵遞區號：
                    </td>
                    <td>
                        <%=model.post_code %>
                    </td>
                    <td class="col">
                        手機：
                    </td>
                    <td>
                        <%=model.mobile %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        電話：
                    </td>
                    <td>
                        <%=model.telphone %>
                    </td>
                    <td class="col">
                        帳戶點數：
                    </td>
                    <td>
                        <%=userModel.point %>點
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table">
                <tr>
                    <th width="50%" colspan="2">
                        &gt;&gt;付款配送資料
                    </th>
                    <th width="50%" colspan="2">
                        &gt;&gt;訂單統計資料
                    </th>
                </tr>
                <tr>
                    <td width="5%" class="col">
                        付款方式：
                    </td>
                    <td>
                        <%=new DTcms.BLL.payment().GetTitle(model.payment_id) %>
                    </td>
                    <td width="5%" class="col">
                        商品總金額：
                    </td>
                    <td>
                        <%=model.real_amount-model.real_freight %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        付款狀態：
                    </td>
                    <td>
                        <%if (payModel.type == 2)
                          { %>
                        <%=model.payment_status == 3 ? "已付款" : "未付款" %>
                        <%}
                          else
                          { %>
                        線下付款
                        <%} %>
                    </td>
                    <td class="col">
                        配送費用：
                    </td>
                    <td>
                        <%=model.real_freight %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        配送方式：
                    </td>
                    <td>
                        <%=new DTcms.BLL.distribution().GetTitle(model.distribution_id) %>
                    </td>
                    <td class="col">
                        付款手續費：
                    </td>
                    <td>
                        <%=model.payment_fee %>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        發貨狀態：
                    </td>
                    <td>
                        <%=model.distribution_status == 2 ? "已發貨" : "未發貨" %>
                    </td>
                    <td class="col">
                        積分總額：
                    </td>
                    <td>
                        <%=model.point > 0 ? "+" + model.point.ToString() : model.point.ToString()%>
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        用戶留言：
                    </td>
                    <td>
                        <%=model.message %>
                    </td>
                    <td class="col">
                        訂單總金額：
                    </td>
                    <td>
                        <%=model.order_amount %>
                    </td>
                </tr>
            </table>
            <div class="line10">
            </div>
        </div>
        <div class="foot_btn_box">
            <asp:Button ID="btnCancel" runat="server" Text="取消訂單" CssClass="btnSubmit" Visible="false"
                OnClick="btnCancel_Click" />&nbsp;
            <asp:Button ID="btnInvalid" runat="server" Text="作廢訂單" CssClass="btnSubmit" Visible="false"
                OnClick="btnInvalid_Click" />&nbsp;
            <input type="button" id="btnPrint" value="列印訂單" class="btnSubmit" onclick="parent.openDialog('商品列印預覽', 'orders/order_print.aspx?id=<%=model.id %>', 850, 500);printview();" />&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
