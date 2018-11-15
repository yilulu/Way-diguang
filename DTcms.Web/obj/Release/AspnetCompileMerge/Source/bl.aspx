<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="bl.aspx.cs" Inherits="DTcms.Web.bl" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/cs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i); /* zzjs1 */
                var con = document.getElementById("con_" + name + "_" + i); /* con_zzjs_1 */
                menu.className = i == cursel ? "hover" : ""; /*三目運算 等號優先*/
                con.style.display = i == cursel ? "block" : "none";

                var pagecon = document.getElementById("fenye_con_" + name + "_" + i); /* con_zzjs_1 */
                pagecon.style.display = i == cursel ? "block" : "none";
            }
        }

    </script>
    <style type="text/css">
        #Layer1
        {
            position: absolute;
            left: 6px;
            top: 3109px;
            width: 354px;
            height: 65px;
            z-index: 1;
            background-color: #C3C3C3;
        }
        a:link
        {
            text-decoration: none;
            color: #000000;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: underline;
            color: #57B227;
        }
        a:active
        {
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><a href="productdsgx.aspx?mid=6">便利生活</a></div>
    <div id="ziye_middle">
        <div id="ziye_middle_left_cs">
            <div style="clear: both">
            </div>
            <%--<div id="cs_2">
                <span class="cs_2_bt">地區搜索:</span>
                <%=GetTypeWhereString() %>
            </div>--%>
            <div id="main">
                <div id="menubox">
                    <!--樣式1 滑動選項卡-->
                    <ul>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <li id="zzjs2"><a href="productdsgx.aspx?mid=6&cid=<%#Eval("id") %>">
                                    <%#Eval("title")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <!--樣式2 點擊選項卡 -->
                </div>
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_2">
                        <iframe width="750" height="460" src="<%=FileUpUrl %>" frameborder="0" allowfullscreen>
                        </iframe>
                    </div>
                </div>
            </div>
        </div>
        <%--<div id="cs_right">
            <span class="tjfy">便利生活展示</span>
            <asp:Repeater ID="repDateTuijian" runat="server">
                <ItemTemplate>
                    <span class="right_tu"><a href="productViewbl.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>"
                        >
                        <img src="../images/right_tu.jpg" name="ziye_right_tu" border="0" id="cs_ziye_right_tu" /></a>
                    </span><a href="productViewbl.aspx?id=<%#Eval("id")%>&mid=<%#Eval("channel_id") %>"
                        ><span class="tbsq_biaoti">
                            <%# ToSubstring(Eval("title"),7)%></span></a> <span class="tbsq_nritong2">面積:<%#GetTypleWhereTilte(Convert.ToInt32(Eval("mianji")),null)%></span>
                    <span class="tbsq_nritong2">社區:<%#Eval("shequ")%></span> <span class="tbsq_nritong2">
                        總價:<%#GetTypleWhereTilte(Convert.ToInt32(Eval("jiaqianQJ")),null)%></span> <span
                            class="tbsq_nritong2">格局：<%#GetTypleWhereTilte(Convert.ToInt32(Eval("huxing")), null)%></span>
                    <span class="tbsq_nritong2">樓層：<%#Eval("louceng")%></span> <span class="ziye_right_xian">
                    </span>
                </ItemTemplate>
            </asp:Repeater>
        </div>--%>
    </div>
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
