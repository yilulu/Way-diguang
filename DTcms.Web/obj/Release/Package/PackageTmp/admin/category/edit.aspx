<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.admin.category.edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯類別資料</title>
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
    //載入編輯器
    $(function () {
        var editor = KindEditor.create('textarea[name="txtContent"]', {
            resizeType: 1,
            uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
            fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
            allowFileManager: true,
            items: [
						'source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'image', 'link']
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
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 類別管理 &gt; 編輯類別</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本資料</a></li>
        <li><a onclick="tabs('#contentTab',1);" href="javascript:;">擴展資料</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>所屬父類別：</th>
                <td><asp:DropDownList id="ddlParentId" CssClass="select2 required" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>排序數字：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits" maxlength="10">99</asp:TextBox></td>
            </tr>
            <tr>
                <th>調用別名：</th>
                <td><asp:TextBox ID="txtCallIndex" runat="server" CssClass="txtInput normal ime-disabled" maxlength="50"></asp:TextBox><label>該類別的調用別名，只允許字母、數字、底線</label></td>
            </tr>
            <tr>
                <th>類別名稱：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput required" maxlength="100" style="width:350px;" /></td>
            </tr>
            <tr>
                <th>SEO標題：</th>
                <td><asp:TextBox ID="txtSeoTitle" runat="server" CssClass="txtInput" maxlength="255" style="width:350px;" /></td>
            </tr>
            <tr>
                <th>SEO關鍵字：</th>
                <td><asp:TextBox ID="txtSeoKeywords" runat="server" maxlength="255" TextMode="MultiLine" CssClass="small" /></td>
            </tr>
            <tr>
                <th>SEO描述：</th>
                <td><asp:TextBox ID="txtSeoDescription" runat="server" maxlength="255" TextMode="MultiLine" CssClass="small" /></td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="tab_con">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>URL連結：</th>
                <td><asp:TextBox ID="txtLinkUrl" runat="server" CssClass="txtInput normal" maxlength="255"></asp:TextBox><label>URL跳轉地址</label></td>
            </tr>
            <tr>
                <th>顯示圖片：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="255"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                    <span class="uploading">正在上傳，請稍候...</span>
                </td>
            </tr>
            <tr>
                <th valign="top">類別介紹：</th>
                <td>
                    <textarea id="txtContent" cols="100" rows="8" style="width:99%;height:250px;visibility:hidden;" runat="server"></textarea>
                </td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="foot_btn_box">
    <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" onclick="btnSubmit_Click" />
    &nbsp;<input name="重設" type="reset" class="btnSubmit" value="重 設" />
    </div>
</div>
</form>
</body>
</html>
