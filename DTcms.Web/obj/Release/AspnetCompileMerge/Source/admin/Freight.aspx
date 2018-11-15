<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Freight.aspx.cs" Inherits="DTcms.Web.admin.Freight" %>

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
    <script type="text/javascript">
        function SetNull(v1) {

        }
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 運費管理 &gt; 編輯資料</div>
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
                            類型1：
                        </th>
                        <td>
                            <asp:RadioButton ID="RadOne" Text="1" GroupName="Fee" runat="server" />:全店免運費
                        </td>
                    </tr>
                    <tr>
                        <th>
                            類型2：
                        </th>
                        <td>
                            <asp:RadioButton ID="RadTwo" Text="2" GroupName="Fee" runat="server" />:<asp:TextBox
                                ID="txtTotal" Text="" runat="server"></asp:TextBox>元(含)以上免運費,
                            <asp:TextBox ID="txtLow" Text="" runat="server"></asp:TextBox>元以下收運費<asp:TextBox ID="txtFee"
                                Text="" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            類型3：
                        </th>
                        <td>
                            <asp:RadioButton ID="Rad3" Text="3" GroupName="Fee" runat="server" />:每筆訂單固定收取運費,
                            運費金額<asp:TextBox ID="txtMasterFee" runat="server" Text=""></asp:TextBox>
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
