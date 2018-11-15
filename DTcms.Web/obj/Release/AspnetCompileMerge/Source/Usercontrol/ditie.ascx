<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ditie.ascx.cs" Inherits="DTcms.Web.ditie" %>
<div id="tabbox">
    <ul class="tabs" id="tabs2">
        <li><a href="#">區域/地標</a></li>
        <li><a href="#">捷運沿線</a></li>
    </ul>
    <ul class="tab_conbox" id="tab_conbox2">
        <li class="tab_con">
        <span class="cs_huan1"><span class="cs_huan2">區域:</span>
       <%=Htmlquyu %></span>
        <span class="cs_huan1"><span class="cs_huan2">總價:</span>
        <%=HtmljiaqianQJ %></span>
        <span class="cs_huan1"><span class="cs_huan2">面積:</span>
       <%=Htmlmianji %></span>
        <span class="cs_huan1"><span class="cs_huan2">戶型:</span>
       <%=Htmlhuxing %></span>
        <span class="cs_huan1"><span class="cs_huan2">方式:</span>
        <%=Htmlfangshi %></span>
        </li>
        <li class="tab_con">
        <span class="cs_huan1">
           <%=Htmlditie%>
        </span> 
        <%--<span class="cs_huan1">ss</span>
        <span class="cs_huan1">ss</span>
        <span class="cs_huan1">ss</span>
        <span class="cs_huan1"> ss</span> --%>
        </li>
    </ul>
</div>
<div id="cs_2">
    <span class="cs_2_bt">推薦小區:</span><%=GetTypeWhereString() %></div>
