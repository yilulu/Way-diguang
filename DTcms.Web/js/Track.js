var T = {
    AddTrack: function (ID, uid, IsDel) {
        $.ajax({
            type: "get",
            url: "/tools/submit_ajax.ashx?t=" + Math.random(),
            data: "action=AddTrack&IsDel=" + IsDel + "&uid=" + uid + "&CID=" + ID,
            cache: false,
            timeout: 15000,
            dataType: 'html',
            error: function (XMLHttpRequest, textStatus, errorThrown) { alert("出错了"); },
            success: function (result) {
                eval("var json=" + result);
                if (json.msg == 1) {
                    alert(json.msgbox);
                } else {
                    alert(json.msgbox);
                }
                window.location.reload();
            }
        });
    }
}