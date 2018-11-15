<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RePw.aspx.cs" Inherits="DTcms.Web.admin.users.RePw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        //計算用戶組價格
        $(function () {
            $("#txtSellPrice").change(function () {
                var sprice = $(this).val();
                if (sprice > 0) {
                    $(".groupprice").each(function () {
                        //$(this).val($(this).attr("discount") * sprice / 100);
                        var num = $(this).attr("discount") * sprice / 100;
                        $(this).val(ForDight(num, 2));
                        //$(this).val(num);
                    });
                }
            });
        });
        //四捨五入函數
        function ForDight(Dight, How) {
            Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
            return Dight;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        請再一次輸入登錄密碼:<asp:TextBox ID="TextBox1" CssClass="txtInput normal required" TextMode="Password"
            runat="server"></asp:TextBox>
        <%--<asp:Button ID="Button1" runat="server" Text="儲存送出" OnClick="Button1_Click" />--%>
        <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
