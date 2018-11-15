<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="helper.aspx.cs" Inherits="DTcms.Web.admin.helper" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>商品管理</title>
    <link type="text/css" rel="stylesheet" href="../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="images/style.css" />
    <link type="text/css" rel="stylesheet" href="../css/pagination.css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 幫助管理 &gt; 管理列表</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="helperedit.aspx?action=<%=DTEnums.ActionEnum.Add %>&channel_id=<%=this.channel_id %>"
                class="tools_btn"><span><b class="add">添加幫助資料</b></span></a>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn" OnClick="btnSave_Click"><span><b class="send">儲存排序</b></span></asp:LinkButton>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnDelete');"
                OnClick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlCategoryId" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlCategoryId_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp; 添加類型:<asp:DropDownList ID="ddlProperty" runat="server" CssClass="select2"
                AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                <asp:ListItem Value="" Selected="True">請選擇</asp:ListItem>
                <asp:ListItem Value="0">系統管理員</asp:ListItem>
                <asp:ListItem Value="1">商家</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        </div>
    </div>
    <!--列表展示.開始-->
    <asp:Repeater ID="rptList1" runat="server" OnItemCommand="rptList_ItemCommand">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        選擇
                    </th>
                    <th align="left">
                        標題
                    </th>
                    <th width="13%" align="left">
                        所屬類別
                    </th>
                    <th width="16%" align="left">
                        發佈時間
                    </th>
                    <th width="60" align="left">
                        排序
                    </th>
                    <th width="110">
                        狀態
                    </th>
                    <th width="8%">
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
                    <a href="helperedit.aspx?channel_id=<%#this.channel_id %>&action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        <%#Eval("title")%></a>
                </td>
                <td>
                    <%#new DTcms.BLL.category().GetTitle(Convert.ToInt32(Eval("category_id")))%>
                </td>
                <td>
                    <%#string.Format("{0:g}",Eval("add_time"))%>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="txtInput2 small2"
                        onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" />
                </td>
                <td align="center">
                    <%#Eval("Status").ToString().Equals("0") ? "未審核" : Eval("Status").ToString().Equals("1") ? "上架" : Eval("Status").ToString().Equals("2") ? "下架" : ""%>
                </td>
                <td align="center">
                    <a href="helperedit.aspx?channel_id=<%#this.channel_id %>&action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList1.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暫無記錄</td></tr>" : ""%>
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
