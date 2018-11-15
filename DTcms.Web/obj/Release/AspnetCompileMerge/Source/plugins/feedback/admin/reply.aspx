<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="DTcms.Web.Plugin.Feedback.admin.reply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>回覆留言訊息</title>
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
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 外掛程式管理 &gt; 線上留言</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">回覆留言</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px">
                <col />
                <tbody>
                    <tr>
                        <th>
                            連絡人：
                        </th>
                        <td>
                            <%=model.user_name %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            聯繫電話：
                        </th>
                        <td>
                            <%=model.user_tel %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性別：
                        </th>
                        <td>
                            <%=model.user_qq %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            電子郵箱：
                        </th>
                        <td>
                            <%=model.user_email %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            留言時間：
                        </th>
                        <td>
                            <%=model.add_time %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            留言標題：
                        </th>
                        <td>
                            <%=model.title %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            留言內容：
                        </th>
                        <td>
                            <%=model.content %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            回覆內容：
                        </th>
                        <td>
                            <asp:TextBox ID="txtReContent" runat="server" MaxLength="255" TextMode="MultiLine"
                                CssClass="small" />
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
