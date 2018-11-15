
var setTimer, blinkTimer; //倒计时
//构造函数
miniWin = function() {
    this.options = function(opt) {
        setting = opt;
        miniID = opt.miniID || "HQMini"; 				//对象或实例ID，通过设置不同的ID可产生多窗口并存模式
        back = {
            isShow: opt.isShow == null ? true : opt.isShow, //是否展示背景
            bgColor: opt.bgColor || "#000000", 			//背景颜色
            filter: opt.filter || 20, 					//背景透明度
            showType: opt.showType || "fade"			//背景展示方式[show:直接显示或关闭，fade:淡入淡出，设置为fade时请将filter值设置大一点] 
        };
        miniBox = {
            skinPath: opt.skinPath || "http://images.sneduyun.com/base/mini/HQ.miniWin.default.gif", //皮肤路径
            skinBorderColor: opt.skinBorderColor || "#CCCCCC", //边框颜色
            skinTitleColor: opt.skinTitleColor || "#333333", //title颜色
            isDrag: opt.isDrag == null ? true : opt.isDrag, //是否允许拖拽[注意当pos!="md"时设置拖拽无效，即只有窗口在页面中间时才可以拖拽]
            isScroll: opt.isScroll == null ? true : opt.isScroll, //是否跟随页面滚动
            auto: opt.auto == null ? false : opt.auto, 		//是否自动关闭窗口
            autoTimes: opt.autoTimes || 3, 				//自动关闭时间,秒
            isBlink: opt.isBlink == null ? true : opt.isBlink,
            show: opt.show || "show", 					//窗口关闭或打开方式[win7:模拟win7效果,fade:淡入淡出,show:直接打开]
            width: opt.width || $(window).width() - 50, 	//窗口宽度，必须参数 默认全屏-50
            height: opt.height || $(window).height() - 50, //窗口高度，默认全屏-50，auto为自适应,iframe不支持自适应
            pos: opt.pos || "md", 						//窗口出现位置[lt:左上角,rt:右上角,md:中间,lb:左下角,rb:右下角,mouse:鼠标处]
            title: opt.title || "系统提示",
            dataType: opt.dataType || "html", 			//内容类型[html:html代码,frame:框架]
            data: opt.data || "data or frameUrl is null", //内容[当dataType为html时此处输入html代码，为html时请转码:escape(html)，为frame时输入iframeUrl]
            frameScroll: opt.frameScroll || "auto", 	//dataType="frame"时，是否出现滚动条[no,yes,auto]
            frameCache: opt.frameCache == null ? false : opt.frameCache, //内容为框架时是否开启缓存
            loaderImg: opt.loaderImg || "http://images.sneduyun.com/base/hengqian.loading.gif" //loadingUrl，请根据实际设置该地址
        };
    };
    //相关常量的值
    this.constVal = function() {
        return {
            size: {
                winWdt: $(window).width(),
                winHgt: $(window).height(),
                bodyWdt: $("body").width(),
                bodyHgt: $("body").height(),
                miniWdt: $("#" + miniID + "_Boxer").width(),
                miniHgt: $("#" + miniID + "_Boxer").height(),
                osLeft: $(window).scrollLeft(),
                osTop: $(window).scrollTop(),
                clientWdt: document.documentElement.clientWidth,
                clientHgt: document.documentElement.clientHeight
            },
            browser: {
                ie: /msie/.test(window.navigator.userAgent.toLowerCase()),
                moz: /gecko/.test(window.navigator.userAgent.toLowerCase()),
                opera: /opera/.test(window.navigator.userAgent.toLowerCase()),
                safari: /safari/.test(window.navigator.userAgent.toLowerCase()),
                ie6: !!window.ActiveXObject && !window.XMLHttpRequest,
                ie7: /msie/.test(window.navigator.userAgent.toLowerCase()) && navigator.appVersion.match(/7./i) == '7.',
                ie8: /msie/.test(window.navigator.userAgent.toLowerCase()) && navigator.appVersion.match(/8./i) == '8.'
            }
        }
    };
    //html append to body
    this.appendMini = function() {
        var HQBox = "";
        var posBox;
        var offSetTop = 0;
        if (this.constVal().browser.ie6) {
            offSetTop = this.constVal().size.osTop;
        }
        switch (miniBox.pos) {
            case "lt":
                posBox = "left:0;top:" + offSetTop + "px;";
                break;
            case "rt":
                posBox = "right:0;top:" + offSetTop + "px;";
                break;
            case "lb":
                posBox = "left:0;bottom:0;";
                break;
            case "rb":
                posBox = "right:0;bottom:0;";
                break;
            default:
                posBox = "left:0;top:0;";
                break;
        }
        HQBox += "<div id=\"" + miniID + "_Boxer\" style=\"display:none;z-index:1003;overflow:hidden;" + posBox
        HQBox += "width:" + miniBox.width + "px;height:" + (miniBox.height == "auto" ? "auto" : miniBox.height + "px") + ";position:" + (miniBox.isScroll && !this.constVal().browser.ie6 ? "fixed" : "absolute") + ";\">";

        HQBox += "<div style=\"width:" + parseInt(miniBox.width - 6) + "px;height:" + (miniBox.height == "auto" ? "auto" : parseInt(miniBox.height - 6) + "px") + ";position:relative;padding:3px;\">";
        //半透明背景
        HQBox += "<div id=\"" + miniID + "_opacity\" style=\"width:" + miniBox.width + "px;height:" + (miniBox.height == "auto" ? "auto" : miniBox.height + "px") + ";position:absolute;z-index:-1;left:0;top:0;background:#000;filter:alpha(opacity=20);opacity:0.20;-moz-border-radius:5px;border-radius:5px;\"></div>";
        //内容区
        HQBox += "<div style=\"width:" + parseInt(miniBox.width - 8) + "px;height:" + (miniBox.height == "auto" ? "auto" : parseInt(miniBox.height - 8) + "px") + ";border:1px " + miniBox.skinBorderColor + " solid;background:#fff;\">";

        //标题
        HQBox += "<div id=\"" + miniID + "_Drager\" style=\"width:" + parseInt(miniBox.width - 13) + "px;height:22px;padding:8px 0 0 5px;font-weight:bold;";
        HQBox += "color:" + miniBox.skinTitleColor + ";overflow:hidden;position:relative;background:url(" + miniBox.skinPath + ") repeat-x 0 0;cursor:" + (miniBox.isDrag && miniBox.pos == "md" ? "move" : "normal") + ";\" " + (miniBox.isDrag && miniBox.pos == "md" ? "onmousedown=HQDrag(" + obj2Str(setting) + ",event)" : "") + ">" + unescape(miniBox.title);
        if (miniBox.auto) {
            HQBox += "<span style=\"position:absolute;right:5px;top:7px;\" id=\"" + miniID + "_Timer\">" + miniBox.autoTimes + "</span>";
        } else {
            HQBox += "<span style=\"display:inline;cursor:pointer;width:16px;height:16px;position:absolute;right:5px;top:7px;background:url(" + miniBox.skinPath + ") no-repeat 0 bottom;\"";
            HQBox += " onmouseover=\"this.style.background='url(" + miniBox.skinPath + ") no-repeat right bottom'\" onmouseout=\"this.style.background='url(" + miniBox.skinPath + ") no-repeat 0 bottom'\"";
            HQBox += " title=\"关闭\" id=\"" + miniID + "_Closer\" onclick=HQcallback(" + obj2Str(setting) + ",\"closeMini()\")></span>";
        }
        HQBox += "</div>";
        //内容
        HQBox += "<div id=\"" + miniID + "_Contenter\" style=\"width:" + parseInt(miniBox.width - 8) + "px;height:" + (miniBox.height == "auto" ? "auto" : (miniBox.height - 38) + "px") + ";background:fff;\"></div>";

        HQBox += "</div>";
        HQBox += "</div>";
        HQBox += "</div>";

        //bg
        var clkBlink = "";
        if (miniBox.isBlink) {
            clkBlink = "onclick=\"new miniWin().winBlink(0);\"";
        }
        HQBox += "<div id=\"" + miniID + "_Backer\" " + clkBlink + " style=\" "
        HQBox += "background:" + back.bgColor + ";filter:alpha(opacity=" + back.filter + ");"
        HQBox += "opacity:" + Math.round(back.filter / 100 * Math.pow(10, 2)) / Math.pow(10, 2) + ";";
        HQBox += "width:100%;position:absolute;left:0px;top:0px;display:none;z-index:1002;\"";
        HQBox += "></div>";
        this.destroy();
        $("body").append(HQBox);
    };
    //对象销毁
    this.destroy = function() {
        if ($("#" + miniID + "_Boxer").length > 0) {
            $("#" + miniID + "_Boxer").remove();
            if ($("#" + miniID + "_Backer").length > 0) {
                $("#" + miniID + "_Backer").remove();
            }
            if ($("#" + miniID + "_Framer").length > 0) {
                $("#" + miniID + "_Framer").remove();
            }
        }
    }
    //背景打开/关闭
    this.openBg = function(isOpen) {
        if (back.isShow) {
            var opacityVal = Math.round(back.filter / 100 * Math.pow(10, 2)) / Math.pow(10, 2);
            if (isOpen) {
                var backHeight = this.constVal().size.bodyHgt > this.constVal().size.winHgt ? this.constVal().size.bodyHgt : this.constVal().size.winHgt;
                $("#" + miniID + "_Backer").height(backHeight);
                if (back.showType == "fade") {
                    $("#" + miniID + "_Backer").animate({ opacity: "0" }, "fast", function() {
                        $(this).show().animate({ opacity: opacityVal }, 1000);
                    });
                } else {
                    $("#" + miniID + "_Backer").show();
                }
            } else {
                if (back.showType == "fade") {
                    $("#" + miniID + "_Backer").animate({ opacity: "0" }, 1000, function() {
                        $(this).hide().remove();
                    });
                } else {
                    $("#" + miniID + "_Backer").hide().remove();
                }
            }
        }
    };
    this.debugIE = function(index) {
        if (this.constVal().browser.ie6 || this.constVal().browser.ie7 || this.constVal().browser.ie8) {
            arrIndex = index == 0 ? 1 : 0;
            var skinArr = new Array("", "none")
            $("#" + miniID + "_opacity").css({ "display": "" + skinArr[arrIndex] + "" });
        }
    };
    //打开窗口
    this.openMini = function() {
        if (miniBox.dataType == "frame") {
            this.addIframe();
        } else {
            $("#" + miniID + "_Contenter").html(unescape(miniBox.data));
        }
        var consWidth = (this.constVal().size.winWdt - this.constVal().size.miniWdt) / 2;
        var consHeight = (this.constVal().size.winHgt - this.constVal().size.miniHgt) / 2;
        if (!miniBox.isScroll || this.constVal().browser.ie6) {
            //ie6不支持position:fixed，非fixed需重新计算
            consHeight = (this.constVal().size.clientHgt - this.constVal().size.miniHgt) / 2 + this.constVal().size.osTop;
        }
        if (miniBox.show == "win7") {
            this.debugIE(0);
            if (miniBox.pos == "md") {
                $("#" + miniID + "_Boxer").css({
                    top: consHeight - 30 + "px",
                    left: consWidth + "px"
                }).animate({ opacity: "show", top: consHeight }, "slow", function() {
                    new miniWin().debugIE(1);
                });
            } else {
                $("#" + miniID + "_Boxer").show("fast", function() {
                    new miniWin().debugIE(1);
                });
            }
        } else {
            if (miniBox.pos == "md") {
                $("#" + miniID + "_Boxer").css({
                    top: consHeight + "px",
                    left: consWidth + "px"
                });
            }
            if (miniBox.show == "fade") {
                $("#" + miniID + "_Boxer").fadeIn(1000, function() {
                    new miniWin().debugIE(1);
                });
            } else {
                $("#" + miniID + "_Boxer").show();
            }
        }
        //自动关闭窗口
        if (miniBox.auto) {
            clearTimeout(setTimer);
            this.autoClose(miniBox.autoTimes);
        }
        //ie6窗口跟随滚动方法
        if (miniBox.isScroll && this.constVal().browser.ie6) {
            $(window).bind("scroll", function() {
                //在ie6下如果同时创建多个实例且设置多窗口滚动的话，后绑定的事件会覆盖之前绑定的事件
                HQcallback(setting, "ScrollIE6()");
                //new miniWin().ScrollIE6();
            })
        }
    };
    //关闭窗口
    this.closeMini = function() {
        var consHeight = (this.constVal().size.winHgt - this.constVal().size.miniHgt) / 2;
        if (!miniBox.isScroll || this.constVal().browser.ie6) {
            consHeight = $("#" + miniID + "_Boxer").offset().top;
        }
        if (miniBox.show == "win7") {
            this.debugIE(0);
            if (miniBox.pos == "md") {
                $("#" + miniID + "_Boxer").animate({ opacity: "hide", top: consHeight - 40 }, "slow", function() {
                    new miniWin().destroy();
                });
            } else {
                $("#" + miniID + "_Boxer").hide("fast", function() {
                    new miniWin().destroy();
                });
            }
        } else {
            if (miniBox.show == "fade") {
                this.debugIE(0);
                $("#" + miniID + "_Boxer").fadeOut(1000, function() {
                    new miniWin().destroy();
                });
            } else {
                new miniWin().destroy();
            }
        }
        this.openBg(false);
    };
    //窗口自动关闭，多实例时后创建的会覆盖前面的倒计时事件
    this.autoClose = function(t) {
        $("#" + miniID + "_Timer").html(t)
        if (t == 0) {
            this.closeMini();
        }
        t--;
        setTimer = setTimeout("HQcallback(" + obj2Str(setting) + ",\"autoClose(" + t + ")\")", 1000);
    };
    //闪烁
    this.winBlink = function(t) {
        if ($("#" + miniID + "_Drager").length == 0) {
            clearTimeout(blinkTimer);
            return;
        }
        var skin = $("#" + miniID + "_Drager").attr("style");
        if (skin.indexOf(miniBox.skinPath) > -1) {
            $("#" + miniID + "_Drager").css({ "background-image": "none" });
        } else {
            $("#" + miniID + "_Drager").css({ "background-image": "url(" + miniBox.skinPath + ")" });
        }
        t++;
        blinkTimer = setTimeout("HQcallback(" + obj2Str(setting) + ",\"winBlink(" + t + ")\")", 100);
        if (t == 6) {
            $("#" + miniID + "_Drager").css({ "background-image": "url(" + miniBox.skinPath + ")" });
            clearTimeout(blinkTimer);
        }
    }
    //ie6滚动
    this.ScrollIE6 = function() {
        var offSetTop;
        switch (miniBox.pos) {
            case "lt":
            case "rt":
                offSetTop = this.constVal().size.osTop;
                break;
            case "lb":
            case "rb":
                offSetTop = this.constVal().size.winHgt + this.constVal().size.osTop - this.constVal().size.miniHgt;
                break;
            default:
                offSetTop = (this.constVal().size.clientHgt - this.constVal().size.miniHgt) / 2 + this.constVal().size.osTop
                break;
        }
        $("#" + miniID + "_Boxer").css({
            top: offSetTop + "px"
        });
    };
    //拖拽
    this.drag = function(e) {
        var pop = document.getElementById(miniID + "_Boxer");
        e = e || window.event;
        var posX = e.clientX - parseInt(pop.style.left);
        var posY = e.clientY - parseInt(pop.style.top);
        if (this.constVal().browser.ie) {
            document.getElementById(miniID + "_Drager").setCapture();
        } else {
            window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP); //非ie下捕获鼠标
        }
        var dragConst = this.constVal(); //事件绑定中使用
        var dragID = miniID + "_Drager"; //事件绑定中使用
        var dragScroll = miniBox.isScroll; //事件绑定中使用
        document.onmousemove = function(ev) {
            ev = ev || window.event;
            var dragLeft = ev.clientX - posX;
            if (dragLeft <= 0) {
                dragLeft = 0;
            }
            var dragRig = dragConst.size.winWdt - dragConst.size.miniWdt;
            if (dragLeft >= dragRig) {
                dragLeft = dragRig;
            }
            var dragTop = ev.clientY - posY;
            var dragBot, dragHead;
            if (dragConst.browser.ie6 || !dragScroll) {
                dragBot = dragConst.size.clientHgt - dragConst.size.miniHgt + dragConst.size.osTop
                if (dragTop >= dragBot) {
                    dragTop = dragBot;
                }
                dragHead = dragConst.size.osTop;
                if (dragTop <= dragHead) {
                    dragTop = dragHead;
                }
            } else {
                dragBot = dragConst.size.winHgt - dragConst.size.miniHgt;
                if (dragTop >= dragBot) {
                    dragTop = dragBot;
                }
            }
            if (dragTop <= 0) { dragTop = 0; }
            pop.style.left = dragLeft + "px";
            pop.style.top = dragTop + "px";
        }
        document.getElementById(dragID).onmouseup = function() {
            document.onmousemove = null;
            if (dragConst.browser.ie) {
                document.getElementById(dragID).releaseCapture();
            } else {
                window.releaseEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            }
        }
    };
    //创建框架
    this.addIframe = function() {
        var _iframe = document.createElement('iframe');
        _iframe.setAttribute('id', miniID + "_Framer");
        _iframe.setAttribute('width', '100%');
        _iframe.setAttribute('height', miniBox.height - 38);
        _iframe.setAttribute('scrolling', miniBox.frameScroll);
        _iframe.setAttribute('frameborder', '0', 0);
        _iframe.setAttribute('src', 'about:blank');
        $("#" + miniID + "_Contenter").html(_iframe);
        var frameURL = miniBox.data;
        if (!miniBox.frameCache) {
            if (frameURL.indexOf('?') > -1) {
                frameURL += "&t=" + Math.random();
            } else {
                frameURL += "?t=" + Math.random();
            }
        }
        $("#" + miniID + "_Framer").attr("src", frameURL);
        this.showLoader(true);
        if (this.constVal().browser.ie) {
            _iframe.onreadystatechange = function() {
                if (this.readyState == 'loaded' || this.readyState == 'complete') {
                    new miniWin().showLoader(false);
                }
            };
        } else if (this.constVal().browser.moz) {
            _iframe.onload = function() {
                new miniWin().showLoader(false);
            };
        } else {
            new miniWin().showLoader(false);
        }
    };
    //loading
    this.showLoader = function(isShow) {
        if (isShow) {
            if ($("#HQLoader").length > 0) {
                $("#HQLoader").show();
            } else {
                var loader = "<div id=\"HQLoader\" style=\"position:absolute;top:50%;right:50%;padding:10px;margin:-27px -27px 0 0;";
                loader += "background:#fff;z-index:9999;width:32px;height:32px;"
                loader += "border:1px #ddd solid;filter:alpha(opacity=80);opacity:0.80;-webkit-box-shadow:0 0 6px #C2C2C2;-moz-box-shadow:0 0 6px #C2C2C2;moz-border-radius:6px;\"><img src=\"" + miniBox.loaderImg + "\" /></div>";
                $("#" + miniID + "_Contenter").append(loader);
            }
        } else {
            $("#HQLoader").hide().remove();
        }
    };
    //调用方法
    this.init = function() {
        try {
            this.appendMini();
            this.openMini();
            this.openBg(true);
        } catch (e) {
            alert("hengqian.miniWin_v2.0 error:\n" + e.message);
        }
    };

}
//用于执行绑定事件或回调函数
function HQcallback(setting, events) {
    var createObject = new miniWin();
    createObject.options(setting);
    eval("createObject." + events);
}
//用于执行拖拽事件绑定
function HQDrag(setting, e) {
    var createObject = new miniWin();
    createObject.options(setting);
    createObject.drag(e);
}
//object to string
function obj2Str(obj) {
    switch (typeof (obj)) {
        case 'object':
            var ret = [];
            if (obj instanceof Array) {
                for (var i = 0, len = obj.length; i < len; i++) {
                    ret.push(obj2Str(obj[i]));
                }
                return '[' + ret.join(',') + ']';
            } else if (obj instanceof RegExp) {
                return obj.toString();
            } else {
                for (var a in obj) {
                    ret.push(a + ':' + obj2Str(obj[a]));
                }
                return '{' + ret.join(',') + '}';
            }
        case 'function':
            return 'function() {}';
        case 'number':
            return obj.toString();
        case 'string':
            return "\"" + obj.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g, function(a) { return ("\n" == a) ? "\\n" : ("\r" == a) ? "\\r" : ("\t" == a) ? "\\t" : ""; }) + "\"";
        case 'boolean':
            return obj.toString();
        default:
            return obj.toString();
    }
}