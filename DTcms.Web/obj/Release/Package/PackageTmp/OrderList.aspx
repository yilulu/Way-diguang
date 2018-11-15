<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="OrderList.aspx.cs" Inherits="DTcms.Web.OrderList" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../admin/images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>消費記錄</div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">消費記錄</span><%--<span class="vip_3_2_2"><img src="../images/vip_ico1.jpg"
                name="vip_ico1" id="vip_ico1" /><a href="#"><span class="vip_ico1_1">顯示中的資料</span></a><img
                    src="../images/vip_ico2.jpg" name="vip_ico1" id="vip_ico1" /><a href="#"><span class="vip_ico1_1">審核中的資料</span></a><img
                        src="../images/vip_ico3.jpg" name="vip_ico1" id="vip_ico1" /><a href="#"><span class="vip_ico1_1">已刪除的資料</span></a></span>--%></div>
        <div id="vip_3_3">
            <div id="login" style="width: 800px; height: 920px; border: 0px none">
                <asp:Repeater ID="rptList" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                            <tr>
                                <th width="6%">
                                    選擇
                                </th>
                                 <th width="12%" align="left">
                                    訂單號
                                </th>
                                <th width="20%" align="left">
                                    用戶名
                                </th>
                                <th width="10%" align="left">
                                    金額
                                </th>
                                <th align="left">
                                    備註
                                </th>
                                <th width="16%" align="left">
                                    產生時間
                                </th>
                                <th width="20%" align="left">
                                    完成狀態
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId"
                                    Value='<%#Eval("id")%>' runat="server" />
                            </td>
                            <td>
                                <%#Eval("order_no")%>
                            </td>
                            <td>
                                <%#GetNameByID(Eval("user_ID").ToString())%>
                            </td>
                            <td>
                                <%# Convert.ToDecimal(Eval("value")) > 0 ? "+" + Eval("value").ToString() : Eval("value").ToString()%>
                            </td>
                            <td>
                                <%#Eval("remark")%>
                            </td>
                            <td>
                                <%#string.Format("{0:g}", Eval("add_time"))%>
                            </td>
                            <td>
                                <%#Convert.ToInt32(Eval("status")) == 1 ? "已完成(" + string.Format("{0:g}", Eval("complete_time")) + ")" : "未完成"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暫無記錄</td></tr>" : ""%>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <!--列表展示.結束-->
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
</asp:Content>
