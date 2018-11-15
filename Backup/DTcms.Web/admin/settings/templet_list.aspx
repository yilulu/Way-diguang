<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>範本管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    //放大圖片
    $(function () {
        var x = 10;
        var y = 20;
        $(".imgtip").mouseover(function (e) {
            this.myTitle = this.title;
            this.title = "";
            var imgtip = "<div id='imgtip'><img src='" + $(this).attr("bigimg") + "' width='300' alt='預覽圖'/><\/div>"; //創建 div 元素
            $("body").append(imgtip); //把它追加到文檔中						 
            $("#imgtip")
			    .css({
			        "top": (e.pageY + y) + "px",
			        "left": (e.pageX + x) + "px"
			    }).show("fast");   //設置x座標和y座標，並且顯示
        }).mouseout(function () {
            this.title = this.myTitle;
            $("#imgtip").remove();  //移除 
        }).mousemove(function (e) {
            $("#imgtip")
			    .css({
			        "top": (e.pageY + y) + "px",
			        "left": (e.pageX + x) + "px"
			    });
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首頁 &gt; 控制桌面 &gt; 範本管理</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <asp:LinkButton ID="lbtnStart" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('lbtnStart', '啟用該範本將會全部生成範本檔，確定繼續嗎？');" 
                onclick="lbtnStart_Click"><span><b class="import">啟用範本</b></span></asp:LinkButton>
            <asp:LinkButton ID="lbtnRemark" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('lbtnRemark', '將全部生成範本檔，可能比較耗時，確定要這樣做嗎？');" 
                onclick="lbtnRemark_Click"><span><b class="refresh">全部生產</b></span></asp:LinkButton>
            <asp:LinkButton ID="lbtnManage" runat="server" CssClass="tools_btn" 
                onclick="lbtnManage_Click"><span><b class="common">管理</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">選擇</th>
        <th width="20%" align="left">範本名稱</th>
        <th width="13%">作者</th>
        <th width="16%">創建日期</th>
        <th width="12%">版本號</th>
        <th align="left">適用版本</th>
        <th width="12%">狀態</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" Checked='<%#Eval("skinname").ToString().ToLower() == siteConfig.templateskin ? true : false%>' runat="server" /><asp:HiddenField ID="hideSkinName" runat="server" Value='<%#Eval("skinname") %>' /></td>
        <td><%#Eval("name")%> <img src="../images/icon_view.gif" bigimg="<%#Eval("img")%>" title="查看預覽圖" class="imgtip" /></td>
        <td align="center"><%#Eval("author")%></td>
        <td align="center"><%#Eval("createdate")%></td>
        <td align="center"><%#Eval("version")%></td>
        <td><%#Eval("fordntver")%></td>
        <td align="center"><%#Eval("skinname").ToString().ToLower() == siteConfig.templateskin ? "<img src=\"../images/icon_correct.png\" title=\"正在使用中\" />" : "<img src=\"../images/icon_disable.png\" title=\"未啟用\" />"%></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
