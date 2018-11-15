<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="message_list.aspx.cs" Inherits="DTcms.Web.admin.users.message_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>會員訊息管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    function messageview(obj) {
        var m = '<div class="message_box" style="display:block;">' + $(obj).next(".message_box").html() + '</div>';
        $.ligerDialog.open({ type: "", title: "訊息內容", content: $(m), width: 480, isResize: true });
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 會員管理 &gt; 會員訊息列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
            <a href="message_send.aspx" class="tools_btn"><span><b class="add">發送訊息</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlType" runat="server" CssClass="select2" AutoPostBack="True" onselectedindexchanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">全部類型...</asp:ListItem>
                <asp:ListItem Value="1">系統消息</asp:ListItem>
                <asp:ListItem Value="2">收件箱</asp:ListItem>
                <asp:ListItem Value="3">發件箱</asp:ListItem>
            </asp:DropDownList>&nbsp;
	    </div>
    </div>

    <!--列表展示.開始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">選擇</th>
        <th width="10%" align="left">類型</th>
        <th width="12%" align="left">收件人</th>
        <th align="left">標題</th>
        <th width="12%">狀態</th>
        <th width="16%" align="left">發送時間</th>
        <th width="6%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><%#GetMessageType(Convert.ToInt32(Eval("type")))%></td>
        <td><%#Eval("accept_user_name")%></td>
        <td><%#Eval("title")%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_read")) == 1 ? "已閱讀" : "未閱讀"%></td>
        <td><%#Eval("post_time")%></td>
        <td align="center"><a href="javascript:;" onclick="messageview(this);">查看</a>
        <div class="message_box">
            <dl>
                <dt>訊息類型：</dt>
                <dd><%#GetMessageType(Convert.ToInt32(Eval("type")))%></dd>
            </dl>
            <dl>
                <dt>發件人：</dt>
                <dd><%#Eval("post_user_name").ToString() == "" ? "-" : Eval("post_user_name").ToString()%></dd>
            </dl>
            <dl>
                <dt>收件人：</dt>
                <dd><%#Eval("accept_user_name")%></dd>
            </dl>
            <dl>
                <dt>發送時間：</dt>
                <dd><%#Eval("post_time")%></dd>
            </dl>
            <dl>
                <dt>閱讀狀態：</dt>
                <dd><%#Convert.ToInt32(Eval("is_read")) == 1 ? "已閱讀" : "未閱讀"%></dd>
            </dl>
            <dl>
                <dt>閱讀時間：</dt>
                <dd><%#Eval("read_time")%></dd>
            </dl>
            <dl>
                <dt>標題：</dt>
                <dd><%#Eval("title")%></dd>
            </dl>
            <dl>
                <dt>內容：</dt>
                <dd><%#Eval("content")%></dd>
            </dl>
            <div class="clear"></div>
        </div>
        </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暫無記錄</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.結束-->

   
    <div class="line15"></div>
    <div class="page_box">
      <div id="PageContent" runat="server" class="flickr right"></div>
      <div class="left">
         顯示<asp:TextBox ID="txtPageNum" runat="server" CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" 
             ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox>條/頁
      </div>
    </div>
    <div class="line10"></div>
</form>
</body>
</html>
