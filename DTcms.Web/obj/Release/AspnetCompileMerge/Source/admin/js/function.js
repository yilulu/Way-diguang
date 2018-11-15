//*DTcms後臺管理頁JS函數，Jquery擴展
//*作者：一些事情
//*時間：2012年02月20日

//=============================切換驗證碼======================================
function ToggleCode(obj, codeurl) {
    $(obj).attr("src", codeurl + "?time=" + Math.random());
}

//表格隔行變色
$(function () {
    $(".msgtable tr:nth-child(odd)").addClass("tr_odd_bg"); //隔行變色
    $(".msgtable tr").hover(
			    function () {
			        $(this).addClass("tr_hover_col");
			    },
			    function () {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
});
//==========================頁面載入時JS函數結束===============================

//===========================系統管理JS函數開始================================

//Tab控制函數
function tabs(tabId, tabNum) {
    //設置點擊後的切換樣式
    $(tabId + " .tab_nav li").removeClass("selected");
    $(tabId + " .tab_nav li").eq(tabNum).addClass("selected");
    //根據參數決定顯示內容
    $(tabId + " .tab_con").hide();
    $(tabId + " .tab_con").eq(tabNum).show();
}

//可以自動關閉的提示
function jsprint(msgtitle, url, msgcss, callback) {
    $("#msgprint").remove();
    var cssname = "";
    switch (msgcss) {
        case "Success":
            cssname = "pcent success";
            break;
        case "Error":
            cssname = "pcent error";
            break;
        default:
            cssname = "pcent warning";
            break;
    }
    var str = "<div id=\"msgprint\" class=\"" + cssname + "\">" + msgtitle + "</div>";
    $("body").append(str);
    $("#msgprint").show();
    var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
    var curriframe = "";
    $(itemiframe).each(function () {
        if ($(this).css("display") != "none") {
            curriframe = $(itemiframe).index($(this));
            return false;
        }
    });
    if (url == "back" && curriframe != "") {
        frames[curriframe].history.back(-1);
    } else if (url != "" && curriframe != "") {
        frames[curriframe].location.href = url;
    }
    //3秒後清除提示
    setTimeout(function () {
        $("#msgprint").fadeOut(500);
        //如果動畫結束則刪除節點
        if (!$("#msgprint").is(":animated")) {
            $("#msgprint").remove();
        }
    }, 3000);
    //執行回呼函數
    if (typeof (callback) == "function") callback();
}

//全選取消按鈕函數，調用樣式如：
function checkAll(chkobj) {
    if ($(chkobj).find("span b").text() == "全選") {
        $(chkobj).find("span b").text("取消");
        $(".checkall input").attr("checked", true);
    } else {
        $(chkobj).find("span b").text("全選");
        $(".checkall input").attr("checked", false);
    }
}

//執行回傳函數
function ExePostBack(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        $.ligerDialog.warn("對不起，請選中您要操作的記錄！");
        return false;
    }
    var msg = "刪除記錄後不可恢復，您確定嗎？";
    if (arguments.length == 2) {
        msg = objmsg;
    }
    $.ligerDialog.confirm(msg, "提示訊息", function (result) {
        if (result) {
            __doPostBack(objId, '');
        }
    });
    return false;
}

//執行回傳函數
function UpPostBack(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        $.ligerDialog.warn("對不起，請選中您要操作的記錄！");
        return false;
    }
    var msg = "你確定要此次操作嗎？";
    if (arguments.length == 2) {
        msg = objmsg;
    }
    $.ligerDialog.confirm(msg, "提示訊息", function (result) {
        if (result) {
            __doPostBack(objId, '');
        }
    });
    return false;
}
//關閉提示窗口
function CloseTip(objId) {
    $("#" + objId).hide();
}

//打開Dialog視窗
function openDialog(tit, sendUrl, w, h) {
    if (arguments.length == 3) {
        $.ligerDialog.open({ title: tit, url: sendUrl, width: w, isResize: true });
    } else if (arguments.length == 4) {
        $.ligerDialog.open({ title: tit, url: sendUrl, width: w, height: h, isResize: true });
    } else {
        $.ligerDialog.open({ title: tit, url: sendUrl, isResize: true });
    }
}

//只允許輸入數位
function checkNumber(e) {
    if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {  //FF 
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || (e.which == 8) || (e.which == 46)))
            return false;
    } else {
        if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 8) || (event.keyCode == 46)))
            event.returnValue = false;
    }
}
//===========================系統管理JS函數結束================================

//================上傳檔JS函數開始，需和jquery.form.js一起使用===============
//文件上傳
function Upload(action, repath, uppath, iswater, isthumbnail, filepath) {
    var sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判斷是否打浮水印
    if (arguments.length == 4) {
        sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    //判斷是否生成宿略圖
    if (arguments.length == 5) {
        sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //自訂上傳路徑
    if (arguments.length == 6) {
        sendUrl = filepath + "tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //開始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隱藏上傳按鈕
            $("#" + repath).nextAll(".files").eq(0).hide();
            //顯示LOADING圖片
            $("#" + repath).nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                $("#" + repath).val(data.msgbox);
            } else {
                alert(data.msgbox);
            }
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上傳失敗，錯誤資訊：" + e);
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        url: sendUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//附件上傳
function AttachUpload(repath, uppath) {
    var submitUrl = "../../tools/upload_ajax.ashx?action=AttachFile&UpFilePath=" + uppath;
    //開始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隱藏上傳按鈕
            $("#" + uppath).parent().hide();
            //顯示LOADING圖片
            $("#" + uppath).parent().nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var listBox = $("#" + repath + " ul");
                var newLi = '<li>'
                + '<input name="hidFileName" type="hidden" value="0|' + data.mstitle + "|" + data.msgbox + '" />'
                + '<b class="close" title="刪除" onclick="DelAttachLi(this);"></b>'
                + '<span class="right">下載積分：<input name="txtPoint" type="text" class="input2" value="0" onkeydown="return checkNumber(event);" /></span>'
                + '<span class="title">附件：' + data.mstitle + '</span>'
                + '<span>人氣：0</span>'
                + '<a href="javascript:;" class="upfile"><input type="file" name="FileUpdate" onchange="AttachUpdate(\'hidFileName\',this);" /></a>'
                + '<span class="uploading">正在更新...</span>'
                + '</li>';
                listBox.append(newLi);
                //alert(data.mstitle);
            } else {
                alert(data.msgbox);
            }
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上傳失敗，錯誤信息：" + e);
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        url: submitUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//更新附件上傳
function AttachUpdate(repath, uppath) {
    var btnOldName = $(uppath).attr("name");
    var btnNewName = "NewFileUpdate";
    $(uppath).attr("name", btnNewName);
    var submitUrl = "../../tools/upload_ajax.ashx?action=AttachFile&UpFilePath=" + btnNewName;
    //開始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隱藏上傳按鈕
            $(uppath).parent().hide();
            //顯示LOADING圖片
            $(uppath).parent().nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var ArrFileName = $(uppath).parent().prevAll("input[name='" + repath + "']").val().split("|");
                $(uppath).parent().prevAll("input[name='" + repath + "']").val(ArrFileName[0] + "|" + data.mstitle + "|" + data.msgbox);
                $(uppath).parent().prevAll(".title").html("附件：" + data.mstitle);
            } else {
                alert(data.msgbox);
            }
            $(uppath).parent().show();
            $(uppath).parent().nextAll(".uploading").eq(0).hide();
            $(uppath).attr("name", btnOldName);
        },
        error: function (data, status, e) {
            alert("上傳失敗，錯誤訊息：" + e);
            $(uppath).parent().show();
            $(uppath).parent().nextAll(".uploading").eq(0).hide();
            $(uppath).attr("name", btnOldName);
        },
        url: submitUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//===========================上傳檔JS函數結束================================
