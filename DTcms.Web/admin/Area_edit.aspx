<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Area_edit.aspx.cs" Inherits="DTcms.Web.admin.Area_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>編輯縣市鄉鎮資料</title>
<link href="../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script type="text/javascript">
    //表單驗證
    $(function () {
        $("#form1").validate({
            errorPlacement: function (lable, element) {
                element.ligerTip({ content: lable.html(), appendIdTo: lable });
            },
            success: function(lable){
                lable.ligerHideTip();
            }
        });
    });
    //功能表事件處理
    $(function () {
        //初始化按鈕事件
        $("#nav_box tr").each(function (i) {
            initButton(i);
        });
        //添加按鈕(點擊綁定)
        $("#navAddButton").click(function () {
            var navSize = $('#nav_box tr').size();
            var navRow = getTr(navSize);
            $("#nav_box").append(navRow);
            initButton(navSize);
        });
    });

    //表格行的功能表內容
    function getTr(indexValue) {
        var navRow = '<tr class="td_c">'
        + '<td><input name="nav_id" type="hidden" value="0" /><input name="nav_title" type="text" class="txtInput small" /></td>'
        + '<td><input name="nav_url" type="text" class="txtInput middle" /></td>'
        + '<td><input name="nav_sort" type="text" value="' + indexValue + '" class="txtInput" style="width:30px;" /></td>'
		+ '<td><img alt="刪除" src="../images/icon_del.gif" class="operator" /></td>'
		+ '</tr>';
        return navRow;
    }

    //初始化按鈕事件
    function initButton(indexValue) {
        //功能操作按鈕
        $("#nav_box tr:eq(" + indexValue + ") .operator").each(function (i) {
            switch (i) {
                //刪除                  
                case 0:
                    $(this).click(function () {
                        var obj = $(this);
                        $.ligerDialog.confirm("確定要刪除嗎？", "提示資料", function (result) {
                            if (result) {
                                obj.parent().parent().remove(); //刪除節點
                            }
                        });
                    });
                    break;
            }
        });
    }

</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 控制桌面 &gt; 縣市鄉鎮</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯縣市鄉鎮資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
             <tr>
                <th>上級：</th>
                <td>
                <asp:DropDownList id="ddlParent" CssClass="select2 required" runat="server">
              
                </asp:DropDownList>
                <label>必填</label></td>
            </tr>
            <tr>
                <th>標題：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits number"></asp:TextBox><label>*</label></td>
            </tr>
           <tr>
                <th>代 碼：</th>
                <td><asp:TextBox ID="txtCode" runat="server" CssClass="txtInput"></asp:TextBox></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" onclick="btnSubmit_Click" /></td>
            </tr>
            </tbody>
        </table>
    </div>
    
</div>
</form>
</body>
</html>
