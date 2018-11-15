function killErrors() {
    return true;
}
window.onerror = killErrors;

var bgDivId = "bgDiv";
var smDivId = "smDiv";
var iframeId = "openIframe";

//显示弹窗
function openWin(url, top, width, height) {
    /******
    ***显示背景
    ******/
    var table = window.document.getElementById("bgTable");
    var bg_width = table.clientWidth + 2;
    var bg_height = table.scrollHeight + 2;
    var bg_top = table.offsetTop;
    var bg_left = table.offsetLeft - 1;

    var div1 = window.document.createElement("div");
    div1.id = bgDivId;
    div1.style.cssText = "width:" + bg_width + "px; height:" + bg_height + "px; position:absolute; top:" + bg_top + "px; left:" + bg_left + "px; background-color:#ccc; filter:alpha(opacity=50); -moz-opacity:0.5; opacity:0.5;";
    window.document.body.appendChild(div1);

    /******
    ***显示DIV窗口
    ******/
    var sm_width = width;
    var sm_top = top;
    var sm_left = (window.document.body.clientWidth / 2) - (sm_width / 2);

    var div2 = window.document.createElement("div");
    div2.id = smDivId;
    div2.style.cssText = "width:" + sm_width + "px; height:auto; position:absolute; top:" + sm_top + "px; left:" + sm_left + "px; background-color:#ccc; border:solid 5px #001;";
    window.document.body.appendChild(div2);

    /******
    ***显示關閉按钮
    ******/
    div2.innerHTML = "<img src=\"js/close.png\" onclick=\"closeAll();\" alt=\"關閉\" style=\"position:absolute; top:-15px; right:-15px;\" />";

    /******
    ***显示Iframe页面
    ******/
    var frame = window.document.createElement("iframe");
    frame.id = iframeId;
    frame.src = url;
    frame.width = "100%";
    frame.height = height + "px";
    frame.frameBorder = "no";
    window.document.getElementById(smDivId).appendChild(frame);
}

//關閉
function closeAll() {
    if (document.getElementById(bgDivId) != null) {
        document.getElementById(bgDivId).parentNode.removeChild(document.getElementById(bgDivId));
    }
    if (document.getElementById(iframeId) != null) {
        document.getElementById(iframeId).parentNode.removeChild(document.getElementById(iframeId));
    }
    if (document.getElementById(smDivId) != null) {
        document.getElementById(smDivId).parentNode.removeChild(document.getElementById(smDivId));
    }
}