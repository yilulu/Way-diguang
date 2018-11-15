<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop_list.aspx.cs" Inherits="DTcms.Web.admin.users.shop_list" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>會員管理</title>
    <link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script src="../../js/openWin.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        $(function () {
        //            window.open("CheckPwd.aspx?CheckUrl=<%=Url %>", "", "width=400px,height=300px, top=300px,left=500px");
        //        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 商家會員管理 &gt; 商家會員列表</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="shop_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span>
                <b class="add">添加商家會員</b></span></a><asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn"
                    OnClick="btnSave_Click"><span><b class="send">儲存排序</b></span></asp:LinkButton>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnDelete');"
                OnClick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlGroupId" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlGroupId_SelectedIndexChanged">
                <asp:ListItem Text="商家會員" Value="5" />
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
                    <th align="left" colspan="2">
                        用戶名
                    </th>
                    <th width="12%" align="left">
                        會員組
                    </th>
                    <th width="12%" align="left">
                        郵箱
                    </th>
                    <th width="60" align="left">
                        排序
                    </th>
                    <th width="8%">
                        狀態
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
                <td width="50">
                    <a href="shop_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        <img width="48" height="48" src="<%#GetFace(Eval("avatar").ToString())%>" /></a>
                </td>
                <td class="user_box">
                    <h4>
                        <b>
                            <%#Eval("user_name")%></b> (暱稱：<%#Eval("nick_name")%>)</h4>
                    <i>註冊時間：<%#string.Format("{0:g}",Eval("reg_time"))%></i>
                </td>
                <td>
                    <%#new DTcms.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%>
                </td>
                <td>
                    <%#Eval("email")%>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("exp")%>' CssClass="txtInput2 small2"
                        onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" />
                </td>
                <td align="center">
                    <%#GetUserStatus(Convert.ToInt32(Eval("is_lock")))%>
                </td>
                <td align="center">
                    <a href="shop_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"11\">暫無記錄</td></tr>" : ""%>
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
