<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DTcms.Web.admin.content.list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>內容管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 內容管理 &gt; 內容列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
            <a href="edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&channel_id=<%=this.channel_id %>" class="tools_btn"><span><b class="add">添加內容</b></span></a>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn" onclick="btnSave_Click"><span><b class="send">儲存排序</b></span></asp:LinkButton>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            請選擇：<asp:DropDownList ID="ddlCategoryId" runat="server" CssClass="select2" AutoPostBack="True" onselectedindexchanged="ddlCategoryId_SelectedIndexChanged"></asp:DropDownList>&nbsp;
            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select2" AutoPostBack="True" onselectedindexchanged="ddlProperty_SelectedIndexChanged">
                <asp:ListItem Value="" Selected="True">所有屬性</asp:ListItem>
                <asp:ListItem Value="isMsg">評論</asp:ListItem>
                <asp:ListItem Value="isRed">推薦</asp:ListItem>
            </asp:DropDownList>
	    </div>
    </div>

    <!--列表展示.開始-->
    <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">選擇</th>
        <th align="left">標題</th>
        <th width="13%" align="left">所屬類別</th>
        <th width="16%" align="left">發佈時間</th>
        <th width="5%" align="left">排序</th>
        <th width="110">屬性</th>
        <th width="8%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><a href="edit.aspx?channel_id=<%#this.channel_id %>&action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><%#new DTcms.BLL.category().GetTitle(Convert.ToInt32(Eval("category_id")))%></td>
        <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
        <td align="center"><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" /></td>
        <td align="center">
          <asp:ImageButton ID="ibtnMsg" CommandName="ibtnMsg" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "../images/ico-0.png" : "../images/ico-0_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "取消評論" : "設置評論"%>' />
          <asp:ImageButton ID="ibtnRed" CommandName="ibtnRed" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "../images/ico-2.png" : "../images/ico-2_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "取消推薦" : "設置推薦"%>' />
        </td>
        <td align="center"><a href="edit.aspx?channel_id=<%#this.channel_id %>&action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暫無記錄</td></tr>" : ""%>
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
