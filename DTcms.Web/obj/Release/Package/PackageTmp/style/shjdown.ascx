<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="shjdown.ascx.cs" Inherits="DTcms.Web.shjdown" %>
<asp:Repeater ID="repdata" runat="server">
    <ItemTemplate>
        <li class="cllm3"><a href="userlook.aspx?id=<%#Eval("id") %>">
            <img src="<%#Eval("avatar") %>" border="0" width="155" height="165" /></a>
            <div class="djbt">
                <%#Eval("user_name") %></div>
        </li>
    </ItemTemplate>
</asp:Repeater>
<script type="text/javascript">
$(document).ready(function(){

	$(".side ul li").hover(function(){
		$(this).find(".sidebox").stop().animate({"width":"124px"},200).css({"opacity":"1","filter":"Alpha(opacity=100)","background":"#ae1c1c"})	
	},function(){
		$(this).find(".sidebox").stop().animate({"width":"54px"},200).css({"opacity":"0.8","filter":"Alpha(opacity=80)","background":"#000"})	
	});
	
});

//回到顶部
function goTop(){
	$('html,body').animate({'scrollTop':0},600);
}
</script>
<div class="side">
	<ul>
		
		<li><a href="http://www.empro.com.tw/Notibook.aspx?mid=10"><div class="sidebox"><img src="/img/side_icon02.png">留言諮詢</div></a></li>
		
		<li style="border:none;"><a href="javascript:goTop();" class="sidetop"><img src="/img/side_icon05.png"></a></li>
	</ul>
</div>