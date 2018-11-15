<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="DTcms.Web.Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>留言訊息</title>
    <link href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../admin/images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../../../admin/js/function.js"></script>
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
    <div id="contentTab">
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px">
                <col />
                <tr>
                    <th>
                        姓名
                    </th>
                    <td>
                        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        性別
                    </th>
                    <td>
                        <asp:Label ID="lblsex" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        手機
                    </th>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        住家電話
                    </th>
                    <td>
                        <asp:Label ID="lbluser_tel" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        電子郵件
                    </th>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        主旨
                    </th>
                    <td>
                        <asp:Label ID="lblZhuZhi" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <asp:Panel ID="p1" runat="server">
                    <tr>
                        <th>
                            介紹物件已成交：
                        </th>
                        <td>
                            <input type="hidden" id="chkPointValue" runat="server" />
                            <asp:CheckBox ID="chkPoint" runat="server" /><input id="txtpoints" type="hidden"
                                runat="server" />
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <th>
                            屋主姓名
                        </th>
                        <td>
                            <asp:Label ID="lblMoney" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            屋主姓名聯絡電話
                        </th>
                        <td>
                            <asp:Label ID="lblMianJi" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <th>
                            地點
                        </th>
                        <td>
                            <asp:Label ID="lblAdress" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr class="xqtj">
                        <th>
                            類別
                        </th>
                        <td>
                            <asp:Label ID="lblClassName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <%-- <tr class="xqtj">
                    <th>
                        功能
                    </th>
                    <td>
                        <asp:Label ID="lblFunction" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <th>
                        內容 (必填)
                    </th>
                    <td>
                        <label for="textarea">
                        </label>
                        <asp:Label ID="lblContent" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        回覆內容
                    </th>
                    <td>
                        <asp:TextBox ID="lblReplayContent" Width="400px" Height="80px" TextMode="MultiLine"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
