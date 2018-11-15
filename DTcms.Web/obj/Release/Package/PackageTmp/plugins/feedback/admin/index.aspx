<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.plugins.feedback.admin.index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>線上留言管理</title>
    <link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../../../admin/images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../../../admin/js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 外掛程式管理 &gt; 線上留言</div>
    <div class="tools_box">
        <div class="tools_bar">
            <div class="search_box">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" />
            </div>
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b
                class="all">全選</b></span></a>
            <asp:LinkButton ID="lbtnUnLock" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('lbtnUnLock','審核後前台將顯示該資料，確定繼續嗎？');"
                OnClick="lbtnUnLock_Click"><span><b class="stop">批次審核</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnDelete');"
                OnClick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
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
                        標題
                    </th>
                    <th width="16%" align="left">
                        用戶
                    </th>
                    <th width="16%" align="left">
                        發佈時間
                    </th>
                    <th width="80">
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
                    <a href="reply.aspx?id=<%#Eval("id")%>">
                        <%#Eval("zhuzhi")%></a>
                </td>
                <td>
                    <%#Eval("user_name")%>
                </td>
                <td>
                    <%#string.Format("{0:g}",Eval("add_time"))%>
                </td>
                <td align="center">
                    <%#string.IsNullOrEmpty(Eval("reply_content").ToString()) == true ? "未回覆" : "已回覆"%>
                </td>
                <td align="center">
                    <a href="../feedback.aspx?id=<%#Eval("id")%>">回覆</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暫無記錄</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.結束-->
    <div class="line15">
    </div>
    <div class="page_box">
        <webdiyer:AspNetPager NumericButtonCount="7" ID="AspNetPager1" ShowPageIndexBox="Never"
            runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
            NextPageText="下一頁" ShowInputBox="Always" OnPageChanging="AspNetPager1_PageChanging"
            SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
        </webdiyer:AspNetPager>
    </div>
    <div class="line10">
    </div>
    </form>
</body>
</html>
