<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="needlist.aspx.cs" Inherits="DTcms.Web.admin.needlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯市場需求</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript">

        //表單驗證

        //刪除附件Li節點
        function DelAttachLi(obj) {
            $.ligerDialog.confirm("您確定要刪除嗎？", "提示訊息", function (result) {
                if (result) {
                    $(obj).parent().remove(); //刪除節點
                }
            });
        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 市場需求 &gt; 編輯資料</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a>基本資料</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="150px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            買/租：
                        </th>
                        <td>
                            <asp:TextBox ID="txtType" runat="server" CssClass="txtInput normal required" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            地區：
                        </th>
                        <td>
                            <asp:TextBox ID="txtArea" runat="server" CssClass="txtInput normal required" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            類型：
                        </th>
                        <td>
                            <asp:TextBox ID="txtCata" runat="server" CssClass="txtInput small required " MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            坪數：
                        </th>
                        <td>
                            <asp:TextBox ID="txtMianJi" runat="server" CssClass="txtInput small required digits"
                                MaxLength="10">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            備註：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSpec" runat="server" TextMode="MultiLine" CssClass="txtInput small required"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="foot_btn_box">
            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
            &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
        </div>
    </div>
    </form>
</body>
</html>
