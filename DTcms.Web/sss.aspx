<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sss.aspx.cs" Inherits="DTcms.Web.sss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯訂單資料</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
    <form name="form1" method="post" action="order_edit.aspx?id=195" id="form1">
    <div>
        <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKLTIwOTMxNDM4Nw9kFgICAQ9kFgQCAw8WAh4LXyFJdGVtQ291bnQCARYEAgEPZBYCZg8VBS3ohqDljp/om4vnmb3lvYjlipvkuq7nmb3pnaLohpwxMOeJh+ebkuijnTIybWwDNTUwAzk4MAExAzU1MGQCAg9kFgJmDxUBAGQCBA8PFgIeB1Zpc2libGVnZGRkwXH3k98lHiJ21UWcHJO587Pmlnk=" />
    </div>
    <div>
        <input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="308C109C" />
        <input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEWAgLZyof6AgKQ9M/rBWC77xN6+fOHVNUblGP5ASc/lRk7" />
    </div>
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 銷售管理 &gt; 編輯訂單資料</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯訂單資料</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <!--訂單狀態操作.開始-->
            <div class="order_box">
                <h3>
                    &gt;&gt;更改狀態（訂單號：20140722011326）</h3>
                <div class="order_flow" style="width: 460px;">
                    <div class="order_flow_left">
                        <a title="訂單已生成" class="order_flow_input">生成</a> <span>
                            <p class="name">
                                生成訂單</p>
                            <p>
                                2014/7/22 13:26:35</p>
                        </span>
                    </div>
                    <div class="order_flow_wait">
                        <a class="order_flow_input">付款</a> <span>
                            <p class="name">
                                等待付款</p>
                        </span>
                    </div>
                    <div class="order_flow_wait">
                        <a id="lbtnConfirm" disabled="disabled" title="點擊確認訂單" class="order_flow_input">確認</a>
                        <span>
                            <p class="name">
                                確認訂單</p>
                        </span>
                    </div>
                    <div class="order_flow_wait">
                        <a id="lbtnSend" disabled="disabled" title="點擊發貨" class="order_flow_input">發貨</a>
                        <span>
                            <p class="name">
                                商家發貨</p>
                        </span>
                    </div>
                    <div class="order_flow_right_wait">
                        <a id="lbtnComplete" disabled="disabled" title="點擊完成訂單" class="order_flow_input">完成</a>
                        <span>
                            <p class="name">
                                完成訂單</p>
                        </span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!--訂單狀態操作.結束-->
            <div class="line10">
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table"
                style="border-bottom: 0;">
                <tr>
                    <th align="left">
                        商品名稱
                    </th>
                    <th width="12%" align="left">
                        銷售價
                    </th>
                    <th width="12%" align="left">
                        市場價
                    </th>
                    <th width="10%" align="left">
                        數量
                    </th>
                    <th width="12%" align="left">
                        金額合計
                    </th>
                </tr>
                <tr>
                    <td>
                        膠原蛋白彈力亮白面膜10片盒裝22ml
                    </td>
                    <td>
                        550
                    </td>
                    <td>
                        980
                    </td>
                    <td>
                        1
                    </td>
                    <td>
                        550
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table"
                style="border-bottom: 0;">
                <tr>
                    <th width="50%" colspan="2">
                        &gt;&gt;收貨人資料
                    </th>
                    <th width="50%" colspan="2">
                        &gt;&gt;會員資料
                    </th>
                </tr>
                <tr>
                    <td width="5%" class="col">
                        收貨人：
                    </td>
                    <td>
                        htwyc
                    </td>
                    <td width="5%" class="col">
                        會員帳號：
                    </td>
                    <td>
                        htwyc
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        收貨地址：
                    </td>
                    <td>
                        Taiwan-Teipei
                    </td>
                    <td class="col">
                        會員組別：
                    </td>
                    <td>
                        尊榮卡
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        郵遞區號：
                    </td>
                    <td>
                        212
                    </td>
                    <td class="col">
                        手機：
                    </td>
                    <td>
                        0910-111222
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        電話：
                    </td>
                    <td>
                        02-11112222
                    </td>
                    <td class="col">
                        帳戶點數：
                    </td>
                    <td>
                        4945點
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table">
                <tr>
                    <th width="50%" colspan="2">
                        &gt;&gt;付款配送資料
                    </th>
                    <th width="50%" colspan="2">
                        &gt;&gt;訂單統計資料
                    </th>
                </tr>
                <tr>
                    <td width="5%" class="col">
                        付款方式：
                    </td>
                    <td>
                        超商支付
                    </td>
                    <td width="5%" class="col">
                        商品總金額：
                    </td>
                    <td>
                        467
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        付款狀態：
                    </td>
                    <td>
                        未付款
                    </td>
                    <td class="col">
                        配送費用：
                    </td>
                    <td>
                        200
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        配送方式：
                    </td>
                    <td>
                        宅急便
                    </td>
                    <td class="col">
                        付款手續費：
                    </td>
                    <td>
                        0
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        發貨狀態：
                    </td>
                    <td>
                        未發貨
                    </td>
                    <td class="col">
                        積分總額：
                    </td>
                    <td>
                        +83
                    </td>
                </tr>
                <tr>
                    <td class="col">
                        用戶留言：
                    </td>
                    <td>
                        test
                    </td>
                    <td class="col">
                        訂單總金額：
                    </td>
                    <td>
                        667
                    </td>
                </tr>
            </table>
            <div class="line10">
            </div>
        </div>
        <div class="foot_btn_box">
            <input type="submit" name="btnCancel" value="取消訂單" id="btnCancel" class="btnSubmit" />&nbsp;
            &nbsp;
            <input type="button" id="btnPrint" value="列印訂單" class="btnSubmit" onclick="parent.openDialog('商品列印預覽', 'orders/order_print.aspx?id=195', 850, 500);printview();" />&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
