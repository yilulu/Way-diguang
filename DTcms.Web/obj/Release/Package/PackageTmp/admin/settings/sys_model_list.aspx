<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_model_list.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_model_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>系統模型清單</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 控制桌面 &gt; 房屋類型</div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <div class="search_box">
             類型：
             <asp:DropDownList id="ddlTypleID" CssClass="select2 required" runat="server" AutoPostBack="false">
                <asp:ListItem Value="1">區域</asp:ListItem>
                <asp:ListItem Value="2">總價</asp:ListItem>
                <asp:ListItem Value="3">面積</asp:ListItem>
                <asp:ListItem Value="4">戶型</asp:ListItem>
                <asp:ListItem Value="5">方式</asp:ListItem>
                <asp:ListItem Value="6">捷運沿線</asp:ListItem>
                <asp:ListItem Value="7">房型用途</asp:ListItem>
                <asp:ListItem Value="8">金額範圍</asp:ListItem>
                </asp:DropDownList>&nbsp;
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 尋" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
		    <a href="sys_model_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全選</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete','刪除模型也將刪除下屬的所有頻道，確定要刪除嗎？');"><span><b class="delete">批次刪除</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">選擇</th>
        <th align="left">標題</th>
        <th align="12%">類型</th>
        <th width="12%">排序</th>
        <th width="12%">推薦</th>
        <th width="10%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Enabled='<%#bool.Parse((Convert.ToInt32(Eval("is_sys"))==0 ).ToString())%>' /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><a href="sys_model_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        
        <td align="center"><%#GetTypeName(Eval("inherit_index"))%></td>
        <td align="center"><%#Eval("sort_id")%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_sys")) == 1 ? "是" : "否"%></td>
        <td align="center"><a href="sys_model_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暫無記錄</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
