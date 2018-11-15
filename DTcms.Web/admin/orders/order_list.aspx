<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="DTcms.Web.admin.orders.order_list" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>訂單列表</title>
    <link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 銷售管理 &gt; 訂單列表</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a>
            <asp:LinkButton ID="btnCancel" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnCancel','只有待確定的訂單才會被取消，您確定要這樣做？');"
                OnClick="btnCancel_Click"><span><b class="stop">取消訂單</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnInvalid" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnInvalid','只有已完成的訂單才會被作廢，您確定要這樣做？');"
                OnClick="btnInvalid_Click"><span><b class="remove">作廢訂單</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnDelete','只有取消狀態的訂單才會被刪除，您確定要這樣做？');"
                OnClick="btnDelete_Click"><span><b class="delete">刪除訂單</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">全部訂單狀態</asp:ListItem>
                <asp:ListItem Value="1">已生成</asp:ListItem>
                <asp:ListItem Value="2">已確認</asp:ListItem>
                <asp:ListItem Value="3">已完成</asp:ListItem>
                <asp:ListItem Value="4">已取消</asp:ListItem>
                <asp:ListItem Value="5">已作廢</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlPaymentStatus_SelectedIndexChanged">
                <asp:ListItem Value="0">全部付款狀態</asp:ListItem>
                <asp:ListItem Value="1">待付款</asp:ListItem>
                <asp:ListItem Value="2">已付款</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="ddlDistributionStatus" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlDistributionStatus_SelectedIndexChanged">
                <asp:ListItem Value="0">全部發貨狀態</asp:ListItem>
                <asp:ListItem Value="1">待發貨</asp:ListItem>
                <asp:ListItem Value="2">已發貨</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        </div>
    </div>
    <!--列表展示.開始-->
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        選擇
                    </th>
                    <th align="left">
                        訂單號
                    </th>
                    <th width="12%" align="left">
                        會員帳號
                    </th>
                    <th width="10%" align="left">
                        配送方式
                    </th>
                    <th width="10%" align="left">
                        付款方式
                    </th>
                    <th width="10%">
                        訂單狀態
                    </th>
                    <th width="10%">
                        總金額
                    </th>
                    <th width="15%" align="left">
                        下單時間
                    </th>
                    <th width="6%">
                        操作
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
                    <%#Eval("user_name")%>
                </td>
                <td>
                    <%#new DTcms.BLL.distribution().GetTitle(Convert.ToInt32(Eval("distribution_id")))%>
                </td>
                <td>
                    <%#new DTcms.BLL.payment().GetTitle(Convert.ToInt32(Eval("payment_id")))%>
                </td>
                <td align="center">
                    <%#GetOrderStatus(Convert.ToInt32(Eval("id")))%>
                </td>
                <td align="center">
                    <%#Eval("order_amount")%>
                </td>
                <td>
                    <%#string.Format("{0:g}",Eval("add_time"))%>
                </td>
                <td align="center">
                    <a href="order_edit.aspx?id=<%#Eval("id")%>">查看</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暫無記錄</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>
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
    </form>
</body>
</html>
