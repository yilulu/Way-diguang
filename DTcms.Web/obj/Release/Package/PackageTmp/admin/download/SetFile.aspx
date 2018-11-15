<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetFile.aspx.cs" Inherits="DTcms.Web.admin.download.SetFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯下載資料</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript">
        function setCheckBox(powerL) {
            var checkobject = document.getElementsByName("uID");
            var values = powerL.split(",");

            for (var j = 0; j <= values.length - 1; j++) {
                for (var i = 0; i <= checkobject.length - 1; i++) {
                    if (checkobject[i].value == values[j]) {
                        checkobject[i].checked = true;
                    }
                }
            }
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        首頁 &gt; 下載管理 &gt; 把文件指派給會員</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">把文件指派給會員</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" name="uID" value="<%#Eval("ID")%>" /><%#Eval("user_name")%>
                </ItemTemplate>
                <FooterTemplate>
                    </td> </tr> </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="foot_btn_box">
            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
            &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
        </div>
    </div>
    </form>
</body>
</html>
