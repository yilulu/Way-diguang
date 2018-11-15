<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_edit.aspx.cs" Inherits="DTcms.Web.admin.users.group_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯會員組</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        //表單驗證
        $(function () {
            $("#form1").validate({
                errorPlacement: function (lable, element) {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 會員管理 &gt; 編輯會員組</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯會員組</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            組別名稱：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2"
                                MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否隱藏：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                                *隱藏後，用戶將無法升級或顯示該組別。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            註冊默認會員組：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsDefault" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                                *用戶註冊成功後自動默認成為該會員組，如果存在多條，則以等級值最小的爲準。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            參與自動升級：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsUpgrade" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <label>
                                *如果是否，在滿足升級條件下系統則不會自動升級為該會員組。</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            等級值：
                        </th>
                        <td>
                            <asp:TextBox ID="txtGrade" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*升級順序，取值範圍-100，等級值越大，會員等級越高。</label>
                        </td>
                    </tr>
                    <%--  <tr>
                <th>升級所需積分：</th>
                <td><asp:TextBox ID="txtUpgradeExp" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*自動升級所需要的積分。</label></td>
            </tr>
            <tr>
                <th>初始金額：</th>
                <td><asp:TextBox ID="txtAmount" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*自動到該會員組贈送的金額，負數則減扣。</label></td>
            </tr>
            <tr>
                <th>初始積分：</th>
                <td><asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*自動到該會員組贈送的積分，負數則減扣。</label></td>
            </tr>--%>
                    <tr>
                        <th>
                            回饋折扣：
                        </th>
                        <td>
                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*下次購買可用點數折抵現金</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
