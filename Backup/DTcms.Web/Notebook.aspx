<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Notebook.aspx.cs" Inherits="DTcms.Web.Notebook" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../../../admin/images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
    <script src="js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/miniWin-min.js" type="text/javascript"></script>
    <script src="js/miniWin.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openWin(v1) {
            var DG = new miniWin();
            DG.options({
                filter: 1,
                title: "查看留言",
                isDrag: true,
                width: 700,
                height: 550,
                skinBorderColor: "#333",
                dataType: "frame",
                data: "Feedback.aspx?id=" + v1,
                frameScroll: "yes"
            });
            DG.init();
        }
    </script>
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>留言列表
    </div>
    <div style="clear: both; height: 10px">
    </div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_3">
            <!--列表展示.开始-->
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                        <tr>
                            <th width="16%" align="left">
                                用户
                            </th>
                            <th width="16%" align="left">
                                留言時間
                            </th>
                            <th width="20%">
                                狀態
                            </th>
                            <th width="40%">
                                操作
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#GetNameByID(Eval("UserID").ToString())%>
                        </td>
                        <td>
                            <%#string.Format("{0:g}",Eval("add_time"))%>
                        </td>
                        <td align="center">
                            <%#Eval("reply_content").ToString() == "" ? "未回覆" : "已回覆"%>
                        </td>
                         <td align="center">
                            <a href="javascript:;" onclick="openWin('<%#Eval("ID") %>')">點擊查看詳細信息</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <!--列表展示.结束-->
            <div class="line15">
            </div>
            <div class="page_box">
            </div>
            <div class="line10">
            </div>
        </div>
    </div>
</asp:Content>
