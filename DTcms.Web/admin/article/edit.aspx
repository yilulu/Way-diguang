<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.admin.article.edit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯文章資料</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type='text/javascript' src="../../scripts/swfupload/swfupload.js"></script>
    <script type='text/javascript' src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script language="javascript" type="text/javascript" src="../js/getdate/WdatePicker.js"></script>
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
        //初始化上傳控制項
        $(function () {
            InitSWFUpload("../../tools/upload_ajax.ashx", "Filedata", "<%=siteConfig.attachimgsize%> KB", "../../scripts/swfupload/swfupload.swf", 1, 1);
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 文章管理 &gt; 編輯資料</div>
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
                            <asp:DropDownList ID="ddlCategoryId" CssClass="select2" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            標題名稱：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" Width="350"
                                MaxLength="100" />
                        </td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server">
                        <tr>
                            <th>
                                推薦類型：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="1">允許評論</asp:ListItem>
                                    <asp:ListItem Value="1">置頂</asp:ListItem>
                                    <asp:ListItem Value="1">推薦</asp:ListItem>
                                    <asp:ListItem Value="1">熱點</asp:ListItem>
                                    <asp:ListItem Value="1">幻燈</asp:ListItem>
                                    <asp:ListItem Value="1">隱藏</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                文章作者：
                            </th>
                            <td>
                                <asp:TextBox ID="txtAuthor" runat="server" CssClass="txtInput normal" MaxLength="100">管理員</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                文章來源：
                            </th>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="txtInput normal" MaxLength="100">本站</asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <th>
                            添加時間：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTime" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                runat="server" CssClass="txtInput " MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            文章摘要：
                        </th>
                        <td>
                            <asp:TextBox ID="txtZhaiyao" runat="server" MaxLength="255" TextMode="MultiLine"
                                CssClass="small" /><label>不填將自動截取內容255個字元</label>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel2" runat="server">
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
                            <th>
                                自定義封面：
                            </th>
                            <td>
                                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" MaxLength="255"></asp:TextBox>
                                <a href="javascript:;" class="files">
                                    <input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                                <span class="uploading">正在上傳，請稍後...</span>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top" style="padding-top: 10px;">
                                圖片相冊：
                            </th>
                            <td>
                                <input type="text" class="txtInput normal left" />
                                <div class="upload_btn">
                                    <span id="upload"></span>
                                </div>
                                <label>
                                    可以上傳多張圖片。</label>
                                <div class="clear">
                                </div>
                                <!--封面隱藏值.開始-->
                                <!--
                    <input type="hidden" name="focus_photo" id="focus_photo" value=""/>
                    -->
                                <asp:HiddenField ID="focus_photo" runat="server" />
                                <!--封面隱藏值.結束-->
                                <!--上傳提示.開始-->
                                <div id="show">
                                </div>
                                <!--上傳提示.結束-->
                                <!--圖片清單.開始-->
                                <div id="show_list">
                                    <ul>
                                        <asp:Literal ID="LitAlbumList" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                                <!--圖片清單.結束-->
                            </td>
                        </tr>
                    </asp:Panel>
                    <!--
            <tr>
                <th>擴展屬性：</th>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" class="border_table">
                        <tbody>
                        <col width="80px"><col>
                        <tr>
                            <th>屬性一</th>
                            <td><input name="nav_url" type="text" value="" class="txtInput middle" /></td>
                        </tr>
                        <tr>
                            <th>屬性二</th>
                            <td><input name="nav_url" type="text" value="" class="txtInput middle" /></td>
                        </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            -->
                    <!--擴展屬性.開始-->
                    <asp:Literal ID="LitAttributeList" runat="server"></asp:Literal>
                    <!--擴展屬性.結束-->
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
                        <th valign="top">
                            詳細描述：
                        </th>
                        <td>
                            <textarea id="txtContent" cols="100" rows="8" style="width: 99%; height: 350px; visibility: hidden;
                                word-wrap: break-word;" runat="server"></textarea>
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
    </form>
</body>
</html>
