<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="down.aspx.cs" Inherits="DTcms.Web.down" %>

<%@ Register Src="Usercontrol/TuiJian.ascx" TagName="TuiJian" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <link href="style/cs_sj.css" rel="stylesheet" type="text/css" />
    <script>
<!--
        function setTab1(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs11 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_11 */
                menu.className = i == cursel ? "hover" : ""; /*三目運算 等號優先*/
                con.style.display = i == cursel ? "block" : "none";
            }
        }
//-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：帝光地產聯盟>>資料下載</div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_sj" style="width: 100%">
            <div id="cs_sj_xia1" style="width: 100%">
                <table width="100%" style="text-align: center;">
                    <tr style="height: 30px">
                        <td>
                            文件名稱
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            存證信函範例.doc
                        </td>
                        <td>
                            <a href="txt/存證信函範例.doc">點擊下載</a>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            法院版簡易版.doc
                        </td>
                        <td>
                            <a href="txt/法院版簡易版.doc">點擊下載</a>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            內政部詳細版.doc
                        </td>
                        <td>
                            <a href="txt/內政部詳細版.doc">點擊下載</a>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            英文版.doc
                        </td>
                        <td>
                            <a href="txt/英文版.doc">點擊下載</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
