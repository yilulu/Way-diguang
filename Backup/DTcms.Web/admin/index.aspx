<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.admin.index" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=siteConfig.webname %>
        - 後台管理</title>
    <link href="../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="images/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/ui/js/ligerBuild.min.js" type="text/javascript"></script>
    <script src="js/function.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tab = null;
        var accordion = null;
        var tree = null;
        $(function () {
            //頁面佈局
            $("#global_layout").ligerLayout({ leftWidth: 180, height: '100%', topHeight: 65, bottomHeight: 24, allowTopResize: false, allowBottomResize: false, allowLeftCollapse: true, onHeightChanged: f_heightChanged });

            var height = $(".l-layout-center").height();

            //Tab
            $("#framecenter").ligerTab({ height: height });

            //左邊導航面板
            $("#global_left_nav").ligerAccordion({ height: height - 25, speed: null });

            $(".l-link").hover(function () {
                $(this).addClass("l-link-over");
            }, function () {
                $(this).removeClass("l-link-over");
            });

            //設置頻道功能表
            $("#global_channel_tree").ligerTree({
                url: '../tools/admin_ajax.ashx?action=sys_channel_load',
                checkbox: false,
                nodeWidth: 112,
                //attribute: ['nodename', 'url'],
                onSelect: function (node) {
                    if (!node.data.url) return;
                    var tabid = $(node.target).attr("tabid");
                    if (!tabid) {
                        tabid = new Date().getTime();
                        $(node.target).attr("tabid", tabid)
                    }
                    f_addTab(tabid, node.data.text, node.data.url);
                }
            });
            //            //加载插件菜单
            //            loadPluginsNav();

            //載入外掛程式菜單
            loadPluginsNav();
            //快顯功能表
            var menu = $.ligerMenu({ width: 120, items:
		[
			{ text: '管理首頁', click: itemclick },
			{ text: '修改密碼', click: itemclick },
			{ line: true },
			{ text: '關閉選單', click: itemclick }
		]
            });
            $("#tab-tools-nav").bind("click", function () {
                var offset = $(this).offset(); //取得事件對象的位置
                menu.show({ top: offset.top + 27, left: offset.left - 120 });
                return false;
            });

            tab = $("#framecenter").ligerGetTabManager();
            accordion = $("#global_left_nav").ligerGetAccordionManager();
            tree = $("#global_channel_tree").ligerGetTreeManager();
            //tree.expandAll(); //默認展開所有節點
            $("#pageloading_bg,#pageloading").hide();
        });

        //頻道功能表非同步載入函數，結合ligerMenu.js使用
        function loadChannelTree() {
            if (tree != null) {
                tree.clear();
                tree.loadData(null, "../tools/admin_ajax.ashx?action=sys_channel_load");
            }
        }

        //載入外掛程式管理功能表
        function loadPluginsNav() {
            $.ajax({
                type: "POST",
                url: "../tools/admin_ajax.ashx?action=plugins_nav_load&time=" + Math.random(),
                timeout: 20000,
                beforeSend: function (XMLHttpRequest) {
                    $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">正在載入中，請稍候...</div>");
                },
                success: function (data, textStatus) {
                    $("#global_plugins").html(data);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">載入外掛程式選單出錯！</div>");
                }
            });
        }
        //加载插件管理菜单
        //        function loadPluginsNav() {
        //            $.ajax({
        //                type: "POST",
        //                url: "../tools/admin_ajax.ashx?action=plugins_nav_load&time=" + Math.random(),
        //                timeout: 20000,
        //                beforeSend: function (XMLHttpRequest) {
        //                    $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">正在加載，請稍後...</div>");
        //                },
        //                success: function (data, textStatus) {
        //                    $("#global_plugins").html(data);
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">加載插件菜單出錯！</div>");
        //                }
        //            });
        //        }
        //快顯功能表回呼函數
        function itemclick(item) {
            switch (item.text) {
                case "管理首頁":
                    f_addTab('home', '管理中心', 'center.aspx');
                    break;
                case "快捷導航":
                    //調用函數
                    break;
                case "修改密碼":
                    f_addTab('manager_pwd', '修改密碼', 'manager/manager_pwd.aspx');
                    break;
                default:
                    //關閉窗口
                    break;
            }
        }
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }
        //添加Tab，可傳3個參數
        function f_addTab(tabid, text, url, iconcss) {
            if (arguments.length == 4) {
                tab.addTabItem({ tabid: tabid, text: text, url: url, iconcss: iconcss });
            } else {
                tab.addTabItem({ tabid: tabid, text: text, url: url });
            }
        }
        //提示Dialog並關閉Tab
        function f_errorTab(tit, msg) {
            $.ligerDialog.open({
                isDrag: false,
                allowClose: false,
                type: 'error',
                title: tit,
                content: msg,
                buttons: [{
                    text: '確定',
                    onclick: function (item, dialog, index) {
                        //查找當前iframe名稱
                        var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
                        var curriframe = "";
                        $(itemiframe).each(function () {
                            if ($(this).css("display") != "none") {
                                curriframe = $(this).attr("tabid");
                                return false;
                            }
                        });
                        if (curriframe != "") {
                            tab.removeTabItem(curriframe);
                            dialog.close();
                        }
                    }
                }]
            });
        }
    </script>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
    <div class="pageloading_bg" id="pageloading_bg">
    </div>
    <div id="pageloading">
        資料載入中，請稍候...</div>
    <div id="global_layout" class="layout" style="width: 100%">
        <!--頭部-->
        <div position="top" class="header">
            <div class="header_box">
                <div class="header_right">
                    <span><b>
                        <%=admin_info.user_name %>（<%=new DTcms.BLL.manager_role().GetTitle(admin_info.role_id) %>）</b>您好，歡迎光臨</span>
                    <br />
                    <a href="javascript:f_addTab('home','管理中心','center.aspx')">管理中心</a> | <a target="_blank"
                        href="../">預覽網站</a> |
                    <asp:LinkButton ID="lbtnExit" runat="server" OnClick="lbtnExit_Click">安全退出</asp:LinkButton>
                </div>
                <a class="logo">DTcms Logo</a>
            </div>
        </div>
        <!--左邊-->
        <div position="left" title="管理菜單" id="global_left_nav">
            <div title="頻道管理" iconcss="menu-icon-model" class="l-scroll">
                <ul id="global_channel_tree" style="margin-top: 3px;">
                    <%-- <li isexpand="false"><span>基礎設置</span>
                        <ul>
                            <li url="settings/sys_model_list.aspx"><span>房屋類型</span></li>
                            <li url="Area_list.aspx"><span>縣市鄉鎮</span></li>
                        </ul>
                    </li>--%>
                </ul>
            </div>
            <%if (siteConfig.memberstatus == 1)
              { %>
            <div title="會員管理" iconcss="menu-icon-member">
                <ul class="nlist">
                    <%if (IsAdminLevel("users", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('user_list','會員資料管理','users/user_list.aspx')">會員資料管理</a></li>
                    <%}
                      if (IsAdminLevel("users", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('shop_list','商家會員資料管理','users/shop_list.aspx')">商家會員資料管理</a></li>
                    <%}
                      if (IsAdminLevel("regFee", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('regFee.aspx','確認註冊繳費','users/regFee.aspx')">確認註冊繳費</a></li>
                    <%}
                  if (IsAdminLevel("user_groups", DTEnums.ActionEnum.View.ToString()))
                  { %>
                    <li><a href="javascript:f_addTab('user_groups','會員組別管理','users/group_list.aspx')">會員組別管理</a></li>
                    <%}

                        if (IsAdminLevel("amount_log", DTEnums.ActionEnum.View.ToString()))
                        { %>
                    <li><a href="javascript:f_addTab('amount_log','會員消費記錄','users/amount_log.aspx')">會員消費記錄</a></li>
                    <%}
                      if (IsAdminLevel("point_log", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('point_log','會員積分記錄','users/point_log.aspx')">會員積分記錄</a></li>
                    <%}
                   if (IsAdminLevel("mail_template", DTEnums.ActionEnum.View.ToString()))
                   { %>
                    <li><a href="javascript:f_addTab('mail_template','郵件範本管理','users/mail_template_list.aspx')">
                        郵件範本管理</a></li>
                    <%}
                      if (IsAdminLevel("app_oauth", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('app_oauth','OAuth平台設置','users/oauth_list.aspx')">OAuth平台設置</a></li>
                    <%}
                      if (IsAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('user_config','會員參數配置','users/user_config.aspx')">
                        會員參數配置</a></li>
                    <%} %>
                </ul>
            </div>
            <div title="銷售管理" iconcss="menu-icon-order">
                <ul class="nlist">
                    <%if (IsAdminLevel("orders", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('orders','商品訂單列表','orders/order_list.aspx')">商品訂單列表</a></li>
                    <%}
                      if (IsAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('order_payment','付款方式設置','orders/payment_list.aspx')">
                        付款方式設置</a></li>
                    <%}%>
                </ul>
            </div>
            <%} %>
            <%if (IsAdminLevel("sys_plugin", DTEnums.ActionEnum.View.ToString()))
              { %>
            <div title="其他管理" iconcss="menu-icon-plugins">
                <ul id="global_plugins" class="nlist">
                    <!--
                    <li><a class="l-link" href="javascript:f_addTab('listpage21','廣告管理','demos/case/listpage21.htm')">廣告管理</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage22','新聞採集','demos/case/listpage22.htm')">新聞採集</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage23','問卷調查','demos/case/listpage23.htm')">問卷調查</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage24','自訂表單','demos/case/listpage24.htm')">自訂表單</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage25','線上留言','demos/case/listpage25.htm')">線上留言</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage26','友情連結','demos/case/listpage25.htm')">友情連結</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage27','Tag標籤','demos/case/listpage25.htm')">Tag標籤</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage28','整合介面','demos/case/listpage25.htm')">整合介面</a></li>
                    -->
                </ul>
            </div>
            <%} %>
            <div title="控制桌面" iconcss="menu-icon-setting">
                <ul class="nlist">
                    <%if (IsAdminLevel("sys_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_config','系統參數設置','settings/sys_config.aspx')">
                        系統參數設置</a></li>
                    <%} if (IsAdminLevel("sys_model", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_model','房屋類型設置','settings/sys_model_list.aspx')">
                        房屋類型設置</a></li>
                    <%} if (IsAdminLevel("sys_channel", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_channel','系統頻道設置','settings/sys_channel_list.aspx')">
                        系統頻道設置</a></li>
                    <%} if (IsAdminLevel("sys_plugin", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('plugin_list','系統插件管理','settings/plugin_list.aspx')">
                        系統插件管理</a></li>
                    <%}
                      if (IsAdminLevel("sys_log", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('manager_log','系統日誌管理','manager/manager_log.aspx')">
                        系統日誌管理</a></li>
                    <%} %>
                    <li><a class="l-link" href="javascript:f_addTab('comment_list','物件提問管理','comment/list.aspx')">
                        物件提問管理</a></li>
                    <%if (IsAdminLevel("sys_manager", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_manager','管理員管理','manager/manager_list.aspx')">
                        管理員管理</a></li>
                    <%}%>
                    <%if (IsAdminLevel("sys_nav", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_nav','網站導航','manager/nav_list.aspx')">
                        網站導航</a></li>
                    <%}%>
                </ul>
            </div>
        </div>
        <div position="center" id="framecenter" toolsid="tab-tools-nav">
            <div tabid="home" title="管理中心" iconcss="tab-icon-home" style="height: 300px">
                <iframe frameborder="0" name="sysMain" src="center.aspx"></iframe>
            </div>
        </div>
        <div position="bottom" class="footer">
            <div class="copyright">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
