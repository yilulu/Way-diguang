<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="viplist.aspx.cs" Inherits="DTcms.Web.viplist" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../../../admin/images/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/miniWin-min.js" type="text/javascript"></script>
    <script src="js/miniWin.js" type="text/javascript"></script>
    <script src="js/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openWin() {
            var DG = new miniWin();
            DG.options({
                filter: 1,
                title: "上傳檔案",
                isDrag: true,
                width: 700,
                height: 500,
                skinBorderColor: "#333",
                dataType: "frame",
                data: "upFile.aspx",
                frameScroll: "yes"
            });
            DG.init();
        }
    </script>
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>VIP</div>
    <span class="vip_1">歡迎您,<span class="vip_2"><asp:Label ID="lblUsername" runat="server" /></span><asp:Label
        ID="lblbUsertype" runat="server" />
        <a href="useredit.aspx">【修改資料】</a></span>
    <div id="vip_2">
    </div>
    <div style="clear: both; height: 10px">
    </div>
    <div id="vip_3">
        <!--會員左邊菜單-->
        <uc1:usermenu ID="usermenu1" runat="server" />
        <div id="vip_3_2">
            <span class="vip_3_2_1">下載文件列表</span><span style="float: right; margin-right: 30px;"><a
                href="javascript:;" onclick="openWin()">點擊上傳檔案</a></span>
        </div>
        <div id="vip_3_3">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 35%;">
                        文件標題
                    </td>
                    <td style="width: 35%;">
                        文件大小
                    </td>
                    <td>
                        操作
                    </td>
                </tr>
                <asp:Literal ID="VIPList" runat="server"></asp:Literal>
            </table>
            <div id="fenye">
                <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                    <tr>
                        <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage" ShowPageIndexBox="Never"
                            runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                            NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage_PageChanged"
                            SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                        </webdiyer:AspNetPager>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
