<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="orderadd.aspx.cs" Inherits="DTcms.Web.WebForm11" %>

<%@ Register Assembly="DTcms.Web" Namespace="Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/order.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ff() {
            var username = $("#<%=txtusername.ClientID %>").val();
            var txtphone = $("#<%=txtphone.ClientID %>").val();
            var txtAddress = $("#<%=txtdizhi.ClientID %>").val();
            if (username == "") {
                alert("請輸入收貨人");
                return false;
            }
            if (txtphone == "") {
                alert("請輸入聯繫手機");
                return false;
            }
            if (txtAddress == "") {
                alert("請輸入收貨地址");
                return false;
            }
        }

    </script>
    <script>
        function window.onhelp() {//屏蔽F1帮助
            return false}
        function document.onkeydown() {
            if (event.keyCode==116) {//屏蔽F5键
                event.keyCode = 0;
                event.cancelBubble = true;
                return false;
            }
            if (event.keyCode==122) {//屏蔽F11键
                event.keyCode = 0;
                event.cancelBubble = true;
                return false;
            }
            if ((event.ctrlKey && event.keyCode==82)) {//屏蔽Ctrl+R
                event.keyCode = 0;
                event.cancelBubble = true;
                return false;
            }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>送出訂單
    </div>
    <div id="login_1">
        <span class="login_1_1">訂單資料</span>
    </div>
    <div id="order" style="height: auto;">
        <div id="order_right">
            <h3 style="text-align: center;">商品資料：</h3>
            <div id="vip_3_3">
                <table width="100%" border="0" align="center" cellpadding="00" cellspacing="0" class="gouwuche">
                    <asp:Repeater ID="repddata" runat="server">
                        <HeaderTemplate>
                            <tr>
                                <td width="40%" align="center" bgcolor="#FFFFFF">商品名稱
                                </td>
                                <td width="15%" align="center" bgcolor="#FFFFFF">商品圖片
                                </td>
                                <td width="15%" align="center" bgcolor="#FFFFFF">價格
                                </td>
                                <td width="10%" align="center" bgcolor="#FFFFFF">數量
                                </td>
                                <td width="20%" align="center" bgcolor="#FFFFFF">小計
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" bgcolor="#FFFFFF">
                                    <div style="width: 99%; height: 20px; line-height: 20px; overflow: hidden;">
                                        <a title="<%# Eval("title") %>">
                                            <%# Eval("title")%></a>
                                    </div>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <div style="padding-top: 5px; padding-bottom: 5px;">
                                        <img src="<%# Eval("img_url")%>" />
                                    </div>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">$<%# Eval("price", "{0:0.00}")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%# Eval("quantity")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#double.Parse(Eval("price").ToString()) * double.Parse(Eval("quantity").ToString())%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--   <footertemplate>--%>
                    <tr>
                        <td style="text-align: right; margin-right: 30px;">
                            <input type="checkbox" name="chkPoint" value="总价" onclick="check(this,<%=pointMoney%>)" />使用點數兌換
                            <input type="hidden" id="hiddenPrice" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="text-align: right; margin-right: 30px;">可抵現金$
                            <%=pointMoney%><input type="hidden" id="hidePoint" runat="server" /><input type="hidden"
                                id="hideNo" runat="server" /><input id="hideFee" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="right" bgcolor="#FFFFFF">
                            <div style="padding: 10px;">
                                <table width="0" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <%-- <a href="shopCat.aspx">&lt;&lt; 返回購物車修改商品</a>--%>
                                        </td>
                                        <td width="100">&nbsp;
                                        </td>
                                        <td>商品總金額：$<span id="TotalPrice"><%=cartModel.payable_amount%></span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <%-- //</footertemplate>--%>
            </div>
        </div>
        <div id="order_left" style="border-right-style: none">
            <ul class="login_left1">
                <li><span class="login_left_1_1">收貨人:</span><span class="login_left_1_2"><input name="logins1"
                    type="text" id="txtusername" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">聯繫電話:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtlianxidianhua" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">郵遞區號:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" id="txtyoubian" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">收貨地址:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" id="txtdizhi" class="inputtext" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">訂單留言:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtliuyan" runat="server" /><input
                        type="hidden" id="UserEmail" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">聯繫手機:</span><span class="login_left_1_2"><input
                    name="logins1" type="text" class="inputtext" id="txtphone" runat="server" />
                </span></li>
                <li><span class="login_left_1_1">付款方式:</span><span class="login_left_1_2">
                    <cc1:MyDropDownList ID="ddlzhifu" runat="server" Where=" is_lock=0" Table_ID_Name="dt_payment*id*title">
                    </cc1:MyDropDownList>
                </span></li>
                <li><span class="login_left_1_1">配送方式:</span><span class="login_left_1_2">
                    <cc1:MyDropDownList ID="ddlpeisong" runat="server" Where=" is_lock=0" Table_ID_Name="dt_distribution*id*title">
                    </cc1:MyDropDownList>
                </span></li>
                <li>
                    <asp:ImageButton ID="btnlogin" runat="server" ImageUrl='../images/tjdd.jpg' OnClientClick="return ff()"
                        OnClick="btnlogin_Click" />
                </li>
            </ul>
        </div>
    </div>
    <script type="text/javascript">
        function check(obj, value) {
            if (obj.checked) 
            {
                var shengyu=<%=shengyu %>;
                $("#TotalPrice").text(shengyu);
                $("#hiddenPrice").val(shengyu);
            }  else{
                $("#TotalPrice").text(<%=PousePrice %>);
                $("#hiddenPrice").val(<%=PousePrice %>);
            }     
        }
    </script>
</asp:Content>
