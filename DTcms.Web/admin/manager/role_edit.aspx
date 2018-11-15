<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.role_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>系統參數設置</title>
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
    //超級管理員
    $(function () {
        $("#ddlRoleType").change(function () {
            if ($(this).find("option:selected").attr("value") == 1) {
                $(".cball,.config,.member,.item_view,.item_add,.item_edit,.item_delete").attr("disabled", "disabled");
            } else {
                $(".cball,.config,.member,.item_view,.item_add,.item_edit,.item_delete").attr("disabled", "");
            }
        });
    });
    //選中相關的checkbox
    function SelectedItems(obj, className) {
        if ($(obj).attr("checked") == true) {
            $("." + className).attr("checked", true);
        } else {
            $("." + className).attr("checked", false);
        }
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">後台</a>首頁 &gt; 控制桌面 &gt; 角色管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯角色資料</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>名稱：</th>
                <td><asp:TextBox ID="txtRoleName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>類型：</th>
                <td>
                    <asp:DropDownList ID="ddlRoleType" runat="server" CssClass="select2 required"/>
                </td>
            </tr>
            <tr>
                <th>權限：</th>
                <td>
                    <label style="margin:0;"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'config');" /><b>系統設置</b></label>
                </td>
            </tr>
            <tr>
                <th></th>
                <td class="item_list">
                    <ul>
                        <asp:Repeater ID="rptList1" runat="server">
                        <ItemTemplate>
                        <li>
                            <asp:HiddenField ID="hidId" Value="0" runat="server" />
                            <asp:HiddenField ID="hidName" Value='<%#Eval("name")%>' runat="server" />
                            <label><input type="checkbox" runat="server" id="cblNavName" value='<%#Eval("value")%>' class="config" /><%#Eval("text")%></label>
                        </li>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </td>
            </tr>
            <%if (siteConfig.memberstatus == 1)
              { %>
            <tr>
                <th></th>
                <td>
                    <label style="margin:0;"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'member');" /><b>會員設置</b></label>
                </td>
            </tr>
            <tr>
                <th></th>
                <td class="item_list">
                    <ul>
                        <asp:Repeater ID="rptList2" runat="server">
                        <ItemTemplate>
                        <li>
                            <asp:HiddenField ID="hidId" Value="0" runat="server" />
                            <asp:HiddenField ID="hidName" Value='<%#Eval("name")%>' runat="server" />
                            <label><input type="checkbox" runat="server" id="cblNavName" value='<%#Eval("value")%>' class="member" /><%#Eval("text")%></label>
                        </li>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </td>
            </tr>

            <tr>
                <th></th>
                <td>
                    <label style="margin:0;"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'order');" /><b>銷售設置</b></label>
                </td>
            </tr>
            <tr>
                <th></th>
                <td class="item_list">
                    <ul>
                        <asp:Repeater ID="rptList21" runat="server">
                        <ItemTemplate>
                        <li>
                            <asp:HiddenField ID="hidId" Value="0" runat="server" />
                            <asp:HiddenField ID="hidName" Value='<%#Eval("name")%>' runat="server" />
                            <label><input type="checkbox" runat="server" id="cblNavName" value='<%#Eval("value")%>' class="order" /><%#Eval("text")%></label>
                        </li>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </td>
            </tr>
            <%} %>
            <tr>
                <th></th>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" class="border_table">
                        <thead>
                        <tr>
                            <th width="150">頻道設置</th>
                            <th width="70"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'item_view');"/>查看</th>
                            <th width="70"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'item_add');"/>添加</th>
                            <th width="70"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'item_edit');"/>修改</th>
                            <th width="70"><input type="checkbox" class="cball" onclick="SelectedItems(this, 'item_delete');"/>刪除</th>
                        </tr>
                        </thead>
                        <tbody>
                        <asp:Repeater ID="rptList3" runat="server">
                        <ItemTemplate>
                        <tr class="td_c">
                            <td><asp:HiddenField ID="hidChannelId" Value='<%#Eval("id")%>' runat="server" /><%#Eval("title")%></td>
                            <td><asp:HiddenField ID="hidViewId" Value="0" runat="server" /><input type="checkbox" runat="server" id="cbView" class="item_view" /></td>
                            <td><asp:HiddenField ID="hidAddId" Value="0" runat="server" /><input type="checkbox" runat="server" id="cbAdd" class="item_add" /></td>
                            <td><asp:HiddenField ID="hidEditId" Value="0" runat="server" /><input type="checkbox" runat="server" id="cbEdit" class="item_edit" /></td>
                            <td><asp:HiddenField ID="hidDeleteId" Value="0" runat="server" /><input type="checkbox" runat="server" id="cbDelete" class="item_delete" /></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </tbody>
                    </table>
                </td>
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
