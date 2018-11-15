function Del(ID) {
    if (confirm("你確定要刪除嗎？")) {
        $.ajax({
            type: "post",
            dataType: "html",
            data: "Sid=" + ID,
            url: "Common/Vip_Down.ashx?action=DelSummary", //请求地址
            error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { /*alert(XmlHttpRequest.responseText);*/alert("获取数据出错!"); } },
            success: function (result) {
                if (result == 1) {
                    alert("刪除成功!");
                    window.location.href = "viplist.aspx";
                } else {
                    alert("刪除失敗!");
                    window.location.href = "viplist.aspx";
                }

            }
        });
    }
}