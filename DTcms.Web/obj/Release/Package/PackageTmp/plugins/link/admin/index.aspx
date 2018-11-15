<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.Plugin.Link.admin.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>友情連結管理</title>
<link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../../../<%=siteConfig.webmanagepath %>/images/style.css" />
<link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../../../<%=siteConfig.webmanagepath %>/js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 外掛程式管理 &gt; 友情連結</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" />
		    </div>
            <a href="link_edit.aspx?action=Add" class="tools_btn"><span><b class="add">增加連結</b></span></a>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn" onclick="btnSave_Click"><span><b class="send">儲存排序</b></span></asp:LinkButton>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="lbtnUnLock" runat="server" CssClass="tools_btn"
                OnClientClick="return ExePostBack('lbtnUnLock','審核後前台將顯示該資料，確定繼續嗎？');" onclick="lbtnUnLock_Click"><span><b class="stop">批次審核</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>

    <!--列表展示.開始-->
    <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">選擇</th>
        <th align="left">標題</th>
        <th width="16%" align="left">是否圖片</th>
        <th width="16%" align="left">發佈時間</th>
        <th width="60" align="left">排序</th>
        <th width="80">屬性</th>
        <th width="8%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><%# Convert.ToInt32(Eval("is_lock")) == 1 ? "<img src=\"../../../" + siteConfig.webmanagepath + "/images/icon_audit.gif\" title=\"未審核\" />" : ""%> <a href="link_edit.aspx?action=Edit&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><%# Convert.ToInt32(Eval("is_image")) == 0 ? "文字連結" : "<img src=\"" + Eval("img_url") + "\" width=\"50\" height=\"20\" />"%></td>
        <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
        <td align="center"><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" /></td>
        <td align="center">
          <asp:ImageButton ID="ibtnRed" CommandName="ibtnRed" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "../../../" + siteConfig.webmanagepath + "/images/ico-2.png" : "../../../" + siteConfig.webmanagepath + "/images/ico-2_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "取消推薦" : "設置推薦"%>' />
        </td>
        <td align="center"><a href="link_edit.aspx?&action=Edit&id=<%#Eval("id")%>">修改</a></td>
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
