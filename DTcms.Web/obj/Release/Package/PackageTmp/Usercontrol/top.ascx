<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top.ascx.cs" Inherits="DTcms.Web.Usercontrol.top" %>
<script  type="text/javascript">
    $(document).ready(function () {
        $(".sousuo_22").each(function () {
            var chushi = $(this).val();
            $(this).focus(function () {
                if ($(this).value == "") {
                    $(this).val(chushi);
                } else {
                    $(this).val("");
                }
            })
            $(this).blur(function () {
                if ($(this).val() == "") {
                    $(this).val(chushi);
                }
            })
        })
    })
</script>

<script type="text/javascript">
    function $$$$$(_sId) {
        return document.getElementById(_sId);
    }
    function hide(_sId)
    { $$$$$(_sId).style.display = $$$$$(_sId).style.display == "none" ? "" : "none"; }
    function pick(v) {
        document.getElementById('am').value = v;
        hide('HMF-1')
    }
    function pick2(v) {
        document.getElementById('bm2').value = v;
        hide('HMF-2')
    }
    function pick3(v) {
        document.getElementById('bm3').value = v;
        hide('HMF-3')
    }
    function bgcolor(id) {
        document.getElementById(id).style.background = "#F7FFFA";
        document.getElementById(id).style.color = "#000";
    }
    function nocolor(id) {
        document.getElementById(id).style.background = "";
        document.getElementById(id).style.color = "#788F72";
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
</style>
<style type="text/css">
    .pager
    {
        margin: 10px auto 0px auto;
    }

        .pager td
        {
            font-size: 12px;
            padding: 2px;
        }

            .pager td a
            {
                border: 1px solid #CECECE;
                float: left;
                font-size: 12px;
                font-weight: normal;
                height: 23px;
                line-height: 25px;
                margin: 20px 10px 0 0;
                padding: 0;
                text-align: center;
                width: 40px;
            }

    #aspPage_input
    {
        border: 1px;
        margin-bottom: 2px;
    }

    .curpage
    {
        border: 0px solid #CECECE;
        float: left;
        font-size: 12px;
        font-weight: normal;
        height: 23px;
        line-height: 25px;
        margin: 24px 10px 0 0;
        padding: 0;
        text-align: center;
        width: 40px;
    }

    .trunspage
    {
        border: 0px solid #CECECE;
        float: left;
        font-size: 12px;
        font-weight: normal;
        height: 23px;
        line-height: 25px;
        margin: 25px 10px 0 0;
        padding: 0;
        text-align: center;
        width: 60px;
    }

        .trunspage input
        {
            border: 1px solid #CECECE;
        }

    .loginbutton
    {
        width: 130px;
        height: 43px;
        border: 0;
        cursor: pointer;
        background-image: url(images/souxun.jpg);
        background-repeat: no-repeat;
        outline: none;
    }
</style>
<div id="top">
    <div id="top_1">
        <ul>
            <a href="userinfo.aspx">
                <li>會員中心</li>
            </a>
            <img src="images/top_ico3.jpg" name="top_ico1" id="top_ico1" />
            <%if (!Common.WEBUserCurrent.IsLogin)
              {%>
            <a href="mem.aspx">
                <li>註冊</li>
            </a>
            <img src="images/top_ico2.jpg" name="top_ico1" id="Img1" />
            <a href="login.aspx">
                <li>登入</li>
            </a>
            <img src="images/top_ico1.jpg" name="top_ico1" id="Img2" />
            <%}
              else
              { %>
            <asp:LinkButton ID="lbtnout" runat="server" OnClick="lbtnout_Click"> <li>&nbsp;退出&nbsp;</li></asp:LinkButton>
            <%} %>
            <a href="usercart.aspx">
                <li>&nbsp;購物車</li>
            </a>
        </ul>
    </div>
</div>
<div id="top_2">
    <a href="index.aspx">
        <img src="images/logo.jpg" name="logo" id="logo" /></a>
    <div id="daohang">
        <ul>
            <li class="daohang1"><a href="http://www.empro.com.tw/index.aspx">首頁</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/productlist.aspx?mid=2">出售</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/productlist.aspx?mid=3">出租</a></li>
            <li class="daohang2"><a href="http://empro3d.com.tw/">空間規劃</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/viplist.aspx?mid=8">VIP</a></li>
            <li class="daohang2"><a href="http://empro-shop.com.tw/">帝光精品</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/productdsgx.aspx?mid=6">便利生活</a></li>
            <li class="daohang3"><a href="http://www.empro.com.tw/news.aspx?mid=7">知識分享</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/UserNeed.aspx?mid=9">市場需求</a></li>
            <li class="daohang2"><a href="http://www.empro.com.tw/Notibook.aspx?mid=10">留言諮詢</a></li>
        </ul>
    </div>
</div>
