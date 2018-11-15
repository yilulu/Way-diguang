
//載入編輯器
$(function () {
    var editor = KindEditor.create('textarea[name="ctl00$ContentPlaceHolder1$note"]', {
        resizeType: 1,
        uploadJson: 'tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
        fileManagerJson: 'tools/upload_ajax.ashx?action=ManagerFile',
        allowFileManager: true
    });

});
function valdata() {
    var username = $("#<%=txtusername.ClientID %>").val();
    var pass1 = $("#<%=txtpassword.ClientID %>").val();
    var pass2 = $("#txtpassword2").val();
    var emall = $("#<%=txtemall.ClientID %>").val();
    var regStr = /^[_a-zA-Z0-9\-]+(\.[_a-zA-Z0-9\-]*)*@[a-zA-Z0-9\-]+([\.][a-zA-Z0-9\-]+)+$/;

    if (username == "") {
        alert("請輸入用戶名!");
        return false;
    }
    if (pass1 == "") {
        alert("請輸入密碼!");
        return false;
    }
    if (emall == "") {
        alert("請輸入郵箱!");
        return false;
    }
    else {
        var chk = regStr.test(emall);
        if (!chk) {
            alert("郵箱格式錯誤!");
            return false;
        }

    }
    if (pass1 != pass2) {
        alert("重複密碼不一致!");
        return false;
    }
}

function select(obj) {
    var i = $(obj).val();
    if (i == 1) {
        $("#zhifuModel").css("display", "none");
    }
    else {
        $("#zhifuModel").css("display", "block");
    }
}
function chkUserName(v1) {
    $.ajax({
        type: "get",
        url: "tools/regChk.ashx",
        data: "action=userName&name=" + v1 + "",
        cache: false,
        timeout: 15000,
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        success: function (result) {
            if (result == 0) {
                alert("你輸入的帳號" + v1 + "系統中不存在！");
            }
        }
    });
}
function chkRegUserName(v1) {
    $.ajax({
        type: "get",
        url: "tools/regChk.ashx",
        data: "action=userName&name=" + v1 + "",
        cache: false,
        timeout: 15000,
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        success: function (result) {
            if (result == 1) {
                alert("你輸入的帳號" + v1 + "系統中已經存在！");
            }
        }
    });
}
function chkUserEmail(v1) {
    $.ajax({
        type: "get",
        url: "tools/regChk.ashx",
        data: "action=Email&name=" + v1 + "",
        cache: false,
        timeout: 15000,
        dataType: 'html',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        success: function (result) {
            if (result == 1) {
                alert("你輸入的Email" + v1 + "系統中已經存在！");
            }
        }
    });
}
