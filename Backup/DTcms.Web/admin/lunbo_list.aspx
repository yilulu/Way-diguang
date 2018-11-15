<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lunbo_list.aspx.cs" Inherits="DTcms.Web.admin.lunbo_list" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>圖片管理列表</title>
    <link type="text/css" rel="stylesheet" href="../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="images/style.css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 圖片管理</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                類型:
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="請選擇" Value="" />
                    <asp:ListItem Text="首頁廣告圖" Value="22" />
                    <asp:ListItem Text="首頁Banner" Value="1" />
                    <asp:ListItem Text="出售" Value="2" />
                    <asp:ListItem Text="出租" Value="3" />
                    <asp:ListItem Text="空間規劃" Value="4" />
                    <asp:ListItem Text="帝光精品" Value="5" />
                    <asp:ListItem Text="便利生活" Value="6" />
                    <asp:ListItem Text="知識分享" Value="7" />
                    <asp:ListItem Text="VIP" Value="8" />
                    <asp:ListItem Text="市場需求" Value="9" />
                    <asp:ListItem Text="留言諮詢" Value="10" />
                    <asp:ListItem Text="3D空間規劃中間輪轉圖" Value="11" />
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="lunbo_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span>
                <b class="add">添加</b></span></a> <a href="javascript:void(0);" onclick="checkAll(this);"
                    class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClick="btnDelete_Click"
                OnClientClick="return ExePostBack('btnDelete','您確定要刪除嗎？');"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        選擇
                    </th>
                    <th align="left">
                        圖片
                    </th>
                    <th align="12%">
                        標題
                    </th>
                    <th align="12%">
                        Link地址
                    </th>
                    <th width="12%">
                        排序
                    </th>
                    <th width="10%">
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
                <td align="center">
                    <a href="<%#Eval("img_url")%>" target="_blank">
                        <img src='<%#Eval("img_url")%>' height="100" width="100" />
                    </a>
                </td>
                <td>
                    <a href="Area_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        <%#Eval("title")%></a>
                </td>
                <td align="center">
                    <%#Eval("link_url")%>
                </td>
                <td align="center">
                    <%#Eval("sort")%>
                </td>
                <td align="center">
                    <a href="lunbo_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="line10">
    </div>
    </form>
</body>
</html>
