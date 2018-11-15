<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="productdsgx.aspx.cs" Inherits="DTcms.Web.WebForm5" %>

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
        .navxinzen
        {
            margin: 10px;
            font-size: 14px;
            line-height: 20px;
            padding: 10px;
            height: 20px;
            text-align: center;
            float: left;
            border: 1px solid #CCC;
            width: 100px;
            background-color: #F6F6F6;
        }
        .navxinzen a
        {
            display: block;
        }
        #ancxxx
        {
            width: 1100px;
            margin-right: auto;
            margin-left: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="kong" style="height: 10px">
    </div>
    <div id="ziye_ymx">
        當前位置：<a href="index.aspx">帝光地產聯盟</a>>><a href="productdsgx.aspx?mid=6">便利生活</a></div>
    <div id="ancxxx">
        <!--樣式1 滑動選項卡-->
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <li class="navxinzen"><a href="<%#Eval("link_url") %>" target="_blank">
                    <%#Eval("title")%></a></li>
            </ItemTemplate>
        </asp:Repeater>
        <div class="clear">
        </div>
        <!--樣式2 點擊選項卡 -->
    </div>
    <!--<div id="ziye_middle"> 
        <div id="ziye_middle_left_cs">
            <div style="clear: both">
            </div>
            <%--<div id="cs_2">
                <span class="cs_2_bt">地區搜索:</span>
                <%=GetTypeWhereString() %>
            </div>--%>
            <div id="main">
               
                <div id="conten">
                    <div class="www_zzjs_net_show" id="con_zzjs_2" style="display: ">
                        <asp:Repeater ID="repdate2" runat="server">
                            <ItemTemplate>
                                <ul class="cs_tu">
                                    <li class="cs_tu1"><a href="bl.aspx?ID=<%#Eval("ID") %>">
                                        <%#Eval("title")%></a></li><a href="bl.aspx?ID=<%#Eval("ID") %>">
                                            <img class=" cs_tu11111" src="<%#Eval("img_url")%>" name="dsgx_tu1" border="0" width="142"
                                                height="129" /></a>
                                    <ul class="cs_dsgx_zhongjian">
                                        <span class="dsgx_mingcheng2">地址：<%#ToSubstring(Eval("dizhi").ToString(), 18)%></span><span
                                            class="dsgx_mingcheng2">服務項目：<%#ToSubstring(Eval("fuwuxiangju").ToString(), 200)%></span>
                                        <span class="dsgx_mingcheng2">官方網站：<a href="<%#Eval("link_url").ToString()%>" target="_blank"><%#Eval("link_url").ToString()%></a></span></ul>
                                    <ul class="cs_tu3">
                                        <li class="cs_tu3_1">聯絡電話</li>
                                        <li class="cs_tu3_1"><span class="cs_tu2_1_1">
                                            <%#Eval("dianhua")%></span></li>
                                        <li class="cs_tu3_1"></li>
                                    </ul>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div id="fenye_con_zzjs_2" style="display: none" class="fenye">
                        <table class="pager" border="0" cellpadding="0" cellspacing="0" style="height: 23px;">
                            <tr>
                                <webdiyer:AspNetPager NumericButtonCount="7" ID="aspPage2" ShowPageIndexBox="Never"
                                    runat="server" ShowFirstLast="true" FirstPageText="首頁" LastPageText="末頁" PrevPageText="上一頁"
                                    NextPageText="下一頁" ShowInputBox="Always" OnPageChanged="aspPage2_PageChanged"
                                    SubmitButtonText="GO" TextAfterInputBox=" " SubmitButtonClass="button" ShowDisabledButtons="False">
                                </webdiyer:AspNetPager>
                            </tr>
                        </table>
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
    </div>-->
    <div style="clear: both; height: 29px;">
    </div>
</asp:Content>
