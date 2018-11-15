<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"><head><meta http-equiv="Content-Type" content="text/html; charset=gb2312">

<!--#include file="inc.asp"-->
<link type="text/css" rel="stylesheet" href="css/reset.css">
<link type="text/css" rel="stylesheet" href="css/common.css">

<script type="text/javascript" async src="js/ga.js"></script>
<script type="text/javascript" src="js/t.js"></script>
<script type="text/javascript" async src="js/mixpanel-2.2.min.js">
</script><script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/tools.js"></script>
<script type="text/javascript" src="js/pzoomindex.js"></script>
<!--[if IE 6]>
<script type="text/javascript" src="js/DD_belatedPNG_0.0.8a.js"></script>
<script type="text/javascript">
$(function(){
	DD_belatedPNG.fix('.navlist li a');
})

</script>
<![endif]-->
<!-- start Mixpanel --><script type="text/javascript">(function(e,b){if(!b.__SV){var a,f,i,g;window.mixpanel=b;a=e.createElement("script");a.type="text/javascript";a.async=!0;a.src=("https:"===e.location.protocol?"https:":"http:")+'//cdn.mxpnl.com/libs/mixpanel-2.2.min.js';f=e.getElementsByTagName("script")[0];f.parentNode.insertBefore(a,f);b._i=[];b.init=function(a,e,d){function f(b,h){var a=h.split(".");2==a.length&&(b=b[a[0]],h=a[1]);b[h]=function(){b.push([h].concat(Array.prototype.slice.call(arguments,0)))}}var c=b;"undefined"!==
typeof d?c=b[d]=[]:d="mixpanel";c.people=c.people||[];c.toString=function(b){var a="mixpanel";"mixpanel"!==d&&(a+="."+d);b||(a+=" (stub)");return a};c.people.toString=function(){return c.toString(1)+".people (stub)"};i="disable track track_pageview track_links track_forms register register_once alias unregister identify name_tag set_config people.set people.increment people.append people.track_charge people.clear_charges people.delete_user".split(" ");for(g=0;g<i.length;g++)f(c,i[g]);b._i.push([a,
e,d])};b.__SV=1.2}})(document,window.mixpanel||[]);
mixpanel.init("d2b8d596184d9e538eabab71730ac65a");</script><!-- end Mixpanel -->
</head>
<body>
<div class="wrapper">
 <div class="header_top">
    <div class="logowrapper"><a href="index.asp" title="九度网络"><img src="images/logo.png" alt="九度网络" width="650" border="0"></a></div>
    <p class="title_list">
<!--<a href="javascript:addCookie();">收藏本站</a>  |  <a href="javascript:setHomepage();">设为首页</a><br>
<span class="tel">投诉卡号码：<strong>18049013980</strong><br>
咨询热线：<strong>029-88816879</strong></span>-->
<table width="220" border="0" align="right" cellpadding="0" cellspacing="0" >
<tr>
    <td width="27" height="35">&nbsp;</td>
    <td width="193" align="right" style="font-family:'微软雅黑'; font-size:14px; padding-right:18px;"><a href="javascript:addCookie();">收藏本站</a> | <a href="javascript:setHomepage();">设为首页</a></td>
    </tr>
<tr>
    <td width="27" height="25"><img src="images/tel.jpg" width="18" height="15"></td>
    <td width="193" style="font-family:'微软雅黑'; font-size:14px;">投诉电话：<span style="color:#B20000; font-weight:bold;">18049013980</span></td>
    </tr>
  <tr>
    <td height="20"><img src="images/tel.jpg" width="18" height="15"></td>
    <td style="font-family:'微软雅黑'; font-size:14px;">咨询热线：<span style="color:#B20000; font-weight:bold;">029-88816879</span></td>
    </tr>
</table>


</p>
  </div>
  <div id="header">
    <div class="navlistout">
	   <ul class="navlist clearfix">
          <li class="pz_nav01"><a href="index.asp">九度首页</a></li>     			 		         			 
		  	    <li class="pz_nav02"><a href="about.asp">关于我们</a></li>               
      			 <li class="pz_nav08"><a href="zh.asp" style="width:98px;">账户托管</a></li> 
                 <li class="pz_nav09"><a href="sem.asp">服务标准</a></li> 				 
		  	    <li class="pz_nav03"><a href="al.asp">托管案例</a></li>               
      			         			 
		  	    <li class="pz_nav04"><a href="xueyuan.asp">托管学院</a></li>               
      			         			 
		  	    <li class="pz_nav05"><a href="newslist.asp"> 公司动态</a></li>               
      			         			 
		  	    <!--<li class="pz_nav06"><a href="#">人才招聘</a></li>               
      			         			 
		  	    <li class="pz_nav07"><a href="#">投诉中心</a></li>   -->             

        </ul>
     </div>
    <div class="nav_cont_out">
      <div class="nav_cont">
        <a href="javascript:void(0)" id="chang_list" title="切换图片"><img src="images/btn_circle.png" alt="切换图片"></a>
        <div class="change_nav_out">
          <div class="change_nav" style="left: 0px; ">
          	
          <%
	   set rs=server.CreateObject("adodb.recordset")
	       rs.open "select * from friendlink where l_pid=186 and l_ppid=409 " ,conn,1,1
		   do while not rs.eof
	        
	   %>
       <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="<%=rs("l_url")%>"><img src="<%=rs("l_pic")%>" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>
	   
	   <%
	       rs.movenext
		   loop
		   rs.close()
	   %>     
               
            
            
            
<!--          <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="#"><img src="images/group_four01.png" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>
          
          <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="#"><img src="images/group_four01.png" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>
          <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="#"><img src="images/group_four01.png" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>
          
          <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="#"><img src="images/group_four01.png" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>
          <div class="list_out_toppic">
            <dl>
              <dt class="navimg05"><a href="#"><img src="images/group_four01.png" alt="pic01" width="940" height="346"></a></dt>
              <dd>
              </dd>
            </dl>
          </div>-->
   </div>
         </div>
      </div>
    </div>
  </div>
  <!--头部结束-->
  <!--内容开始-->
   <div id="content">
    <div class="cont_top">
      <div class="cont_top_out">
          <div class="cont_top">
            <dl>
              <dt class="product"><a href="zh.asp" target="_blank"></a></dt>
              <dd>
                <h4>账户托管</h4>
                <a class="yellow">+</a>
                <p class="discription">主流搜索引擎广告全覆盖<br>高效广告管理平台- AdSight
</p>
              </dd>
            </dl>
            <dl>
              <dt class="service"><a href="sem.asp" target="_blank"></a></dt>
              <dd>
                <h4>服务标准 </h4>
                <a class="blue">+</a>
                <p class="discription">整合 SEM 投放服务<br>
各大搜索引擎广告托管服务<br>
SEM 优化师定向培养服务<br>
客制化广告管理工具开发
</p>
              </dd>
            </dl>
            <dl>
              <dt class="technology"><a href="al.asp"></a></dt>
              <dd>
                <h4>托管案例</h4>
                <a class="green">+</a>
                <p class="discription">Zoom 全媒体整合广告管理<br>
基于语义分析的自动优化算法
专利分布式算法支持百万级关键字帐户高效管理
</p>
              </dd>
            </dl>
            <dl class="optteacher">
           <dt class="optimization">
               <div class="opt_dis">
               		<div class="iconquotation quotation_left"></div>
                    <div class="cont_opt">
                    	 <p><em>九</em>度的SEM 广告服务帮助我们在合作的前几个月就突破瓶颈，在ROI 基本不变的基础上，月销售额又增长了 50%， 多谢你们细致专业的服务！</p>
                         <p class="btm_name">&mdash;&mdash;某知名电商市场总监</p>
                    </div>
					<div class="iconquotation quotation_right"></div>
               </div>
              </dt>
              <dd>
                <div class="opt_cont_top" style="display:none">
                  <h5>
				  <span class="img_opt"></span><a href="#" target="_blank"><span>品众互动官方微博</span></a><span class="little_logo"></span>
				 <a href="http:#" id="add_focus" target="_blank">+添加关注</a></h5>
                  <div style="clear:both"></div>
                </div>

              </dd>
            </dl>
            <div style="clear:both"></div>
          </div>
      </div>
      
    </div>
    <div class="cont_bottom">
      <div class="severtop">
        <h2>我的客户<span>our server</span></h2>
        <div class="square">
        <a href="al.asp">more</a> </div>
        <div style="clear:both"></div>
      </div>
       <%if 1>0 then%>
        <div class="xinzeng-liebiao">
         <%
             set rs=server.CreateObject("adodb.recordset")
             rs.open "select top 12  * from friendlink where l_pid=187 and l_ppid=410 ",conn,1,1
             do while not rs.eof
               lurl="alview.asp?name="&rs("l_title")&"&pid="&rs("l_pid")&"&ppid="&rs("l_ppid")&"&id="&rs("l_id")
			   %>
                <div class="liebiaox">
                    <a href="<%=lurl%>"><img src="<%if rs("l_pic")="" then%>images/zanwutupian.png<%else%><%=rs("l_pic")%><%end if%>" title="<%=rs("l_title")%>" width="215" height="145" border="0" /><span><%=left(rs("l_title"),12)%></span><%'if 1>2 then%><p><%=left(dropHtml(rs("xinghao")),68)%></p><%'end if%></a>
                    
                </div>
               <%
               rs.movenext
             loop
             rs.close()
             'set rs=nothing
         %> 
          
  
          <div class="clear"></div>
        </div>
      <%else%>
        <div class="severbtom">
          <ul class="clearfix" style="left: 0px; ">
         <%
         set rs=server.CreateObject("adodb.recordset")
             rs.open "select * from friendlink where l_pid=187 and l_ppid=410 " ,conn,1,1
             do while not rs.eof
         %>
       <li><a href="javascript:"><%if rs("l_pic")="" then%><img src="images/zanwutupian.png" width="200" height="103" ><%else%><img src="<%=rs("l_pic")%>" alt="pic01" width="200" height="103"><%end if%></a></li>
            
         
         <%
             rs.movenext
             loop
             rs.close()
         %>      
          
          
  <!--<li><a href="javascript:"><img src="images/1-121109120003551.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q555555b.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q555235c.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12112G54210b9.jpg" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q55423Y7.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q553412H.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q55311913.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q5524XA.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q5521X26.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q55152550.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q55129358.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q6341C18.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q61631G4.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q615131W.png" alt=""></a></li>
  <li><a href="javascript:"><img src="images/1-12110Q6123ba.png" alt=""></a></li>-->
        
          </ul>
        </div>
      <%end if%>
    </div>
  </div>
  <script type="text/javascript">
	$(function(){
		$(".close_tip").click(function(){
			$(".tip_online").hide();					   
		})   
	})
</script>
 <!--#include file="foot.asp"-->
</div>
</body></html>