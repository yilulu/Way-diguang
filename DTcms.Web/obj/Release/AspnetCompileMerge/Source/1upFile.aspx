<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="1upFile.aspx.cs" Inherits="DTcms.Web.upFile" %>

<%@ Register Src="Usercontrol/usermenu.ascx" TagName="usermenu" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/vip.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../admin/images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../../admin/js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript">
        //載入編輯器
        $(function () {
            var editor = KindEditor.create('textarea[name="txtContent"]', {
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });

        });
        //表單驗證
        $(function () {
            $("#form1").validate({
                invalidHandler: function (e, validator) {
                    parent.jsprint("有 " + validator.numberOfInvalids() + " 項填寫有誤，請檢查！", "", "Warning");
                },
                errorPlacement: function (lable, element) {
                    //可見元素顯示錯誤提示
                    if (element.parents(".tab_con").css('display') != 'none') {
                        element.ligerTip({ content: lable.html(), appendIdTo: lable });
                    }
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
        //刪除附件Li節點
        function DelAttachLi(obj) {
            $.ligerDialog.confirm("您確定要刪除嗎？", "提示訊息", function (result) {
                if (result) {
                    $(obj).parent().remove(); //刪除節點
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <span class="vip_3_2_1">上傳檔案</span>
        </div>
        <div id="vip_3_3">
            <div id="contentTab">
                <ul class="tab_nav">
                    <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本資料</a></li>
                    <li><a onclick="tabs('#contentTab',1);" href="javascript:;">詳細描述</a></li>
                    <li><a onclick="tabs('#contentTab',2);" href="javascript:;">SEO選項</a></li>
                </ul>
                <div class="tab_con" style="display: block;">
                    <table class="form_table">
                        <col width="150px">
                        <col>
                        <tbody>
                            <tr>
                                <th>
                                    所屬類別：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlCategoryId" CssClass="select2 required" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    標題名稱：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" MaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    推薦類型：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1">允許評論</asp:ListItem>
                                        <asp:ListItem Value="1">推薦</asp:ListItem>
                                        <asp:ListItem Value="1">隱藏</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    排序數字：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits"
                                        MaxLength="10">99</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    瀏覽次數：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtClick" runat="server" CssClass="txtInput small required digits"
                                        MaxLength="10">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th valign="top" style="padding-top: 10px;">
                                    上傳附件：
                                </th>
                                <td>
                                    <a href="javascript:;" class="files">
                                        <input type="file" id="FileUpload2" name="FileUpload2" onchange="AttachUpload('AttachList','FileUpload2');" /></a>
                                    <span class="uploading">正在上傳，請稍候...</span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    <div id="AttachList" class="attach_list">
                                        <ul>
                                            <asp:Repeater ID="rptAttach" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <input name="hidFileName" type="hidden" value="<%#Eval("id")%>|<%#Eval("title")%>|<%#Eval("file_path")%>" />
                                                        <b class="close" title="刪除" onclick="DelAttachLi(this);"></b><span class="right">下載積分：<input
                                                            name="txtPoint" type="text" class="input2" value="<%#Eval("point") %>" onkeydown="return checkNumber(event);" /></span>
                                                        <span class="title">附件：<%#Eval("title")%></span><span><%#Convert.ToInt32(Eval("file_size")) < 1024 ? Eval("file_size").ToString() + "KB" : Convert.ToInt32(Eval("file_size"))/1024 + "MB"%></span><span>人氣：<%#Eval("down_num")%></span><a
                                                            href="javascript:;" class="upfile"><input type="file" name="FileUpdate" onchange="AttachUpdate('hidFileName',this);" /></a><span
                                                                class="uploading">正在更新...</span> </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="tab_con">
                    <table class="form_table">
                        <col width="150px">
                        <col>
                        <tbody>
                            <tr>
                                <th>
                                    贊成人數：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtDiggGood" runat="server" CssClass="txtInput small required digits"
                                        MaxLength="10">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    反對人數：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtDiggBad" runat="server" CssClass="txtInput small required digits"
                                        MaxLength="10">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    URL連結：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="txtInput normal" MaxLength="255"></asp:TextBox><label>URL跳轉地址</label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    顯示圖片：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" MaxLength="255"></asp:TextBox>
                                    <a href="javascript:;" class="files">
                                        <input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                                    <span class="uploading">正在上傳，請稍候...</span>
                                </td>
                            </tr>
                            <tr>
                                <th valign="top">
                                    詳細描述：
                                </th>
                                <td>
                                    <textarea id="txtContent" cols="100" rows="8" style="width: 99%; height: 350px; visibility: hidden;"
                                        runat="server"></textarea>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="tab_con">
                    <table class="form_table">
                        <col width="150px">
                        <col>
                        <tbody>
                            <tr>
                                <th>
                                    SEO標題：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="255" CssClass="txtInput normal" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    SEO關鍵字：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtSeoKeywords" runat="server" MaxLength="255" TextMode="MultiLine"
                                        CssClass="small" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    SEO描述：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtSeoDescription" runat="server" MaxLength="255" TextMode="MultiLine"
                                        CssClass="small" />
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
        </div>
    </div>
</asp:Content>
