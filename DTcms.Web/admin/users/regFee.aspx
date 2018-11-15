<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regFee.aspx.cs" Inherits="DTcms.Web.admin.users.regFee" %>

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
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 會員管理 &gt; 會員繳費列表</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a><asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"
                    OnClientClick="return UpPostBack('btnDelete');" OnClick="btnDelete_Click"><span><b class="delete">更新狀態</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnCancel" runat="server" CssClass="tools_btn" OnClientClick="return UpPostBack('btnCancel');"
                OnClick="btnCancel_Click"><span><b class="delete">取消介紹人點數</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlGroupId" runat="server" CssClass="select2" AutoPostBack="True"
                OnSelectedIndexChanged="ddlGroupId_SelectedIndexChanged">
                <asp:ListItem Value="0">未繳費</asp:ListItem>
                <asp:ListItem Value="1">已繳費</asp:ListItem>
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
                    <th width="12%" align="center">
                        會員組
                    </th>
                    <th width="12%">
                        入會費
                    </th>
                    <th width="8%">
                        介紹人帳號
                    </th>
                    <th width="8%">
                        註冊日期
                    </th>
                    <th width="8%">
                        是否繳費
                    </th>
                    <%-- <th width="6%">
                        操作
                    </th>--%>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId"
                        Value='<%#Eval("id")%>' runat="server" />
                </td>
                <td width="50">
                    <a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                        <img width="48" height="48" src="../../upload/user/<%#Eval("avatar").ToString() != "" ? Eval("avatar") : "../../images/default_user_avatar.gif" %>" /></a>
                </td>
                <td class="user_box">
                    <h4>
                        <b>
                            <%#Eval("user_name")%></b> (暱稱：<%#Eval("nick_name")%>)</h4>
                    <i>註冊時間：<%#string.Format("{0:g}",Eval("reg_time"))%></i> <span><a class="amount"
                        href="javascript:parent.f_addTab('amount_log','會員消費記錄','users/amount_log.aspx?keywords=<%#Eval("user_name")%>');"
                        title="消費記錄">餘額</a> <a class="point" href="javascript:parent.f_addTab('point_log','會員積分記錄','users/point_log.aspx?keywords=<%#Eval("user_name")%>');"
                            title="積分記錄">積分</a> <a class="msg" href="javascript:parent.f_addTab('user_message','會員訊息管理','users/message_list.aspx?keywords=<%#Eval("user_name")%>');"
                                title="訊息記錄">訊息</a> </span>
                </td>
                <td>
                    <%#new DTcms.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%>
                </td>
                <td align="center">
                    <%#Eval("amount")%>
                </td>
                <td align="center">
                    <%#Eval("dianming")%>
                </td>
                <td align="center">
                    <%#Eval("reg_time")%>
                </td>
                <td align="center">
                    <%#GetUserStatus(Convert.ToInt32(Eval("isFee")))%>
                </td>
                <%-- <td align="center">
                    <a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
                </td>--%>
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
