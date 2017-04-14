//h+扩展js
var App = function () {
    var isFullScreen = false;
    var requestFullScreen = function () {//全屏
        var de = document.documentElement;
        if (de.requestFullscreen) {
            de.requestFullscreen();
        } else if (de.mozRequestFullScreen) {
            de.mozRequestFullScreen();
        } else if (de.webkitRequestFullScreen) {
            de.webkitRequestFullScreen();
        }
        else {
            alert("该浏览器不支持全屏");
        }
    };

    //var requestFullScreen2 = function (element) {
    //    // 判断各种浏览器，找到正确的方法
    //    var requestMethod = element.requestFullScreen || //W3C
    //        element.webkitRequestFullScreen ||    //Chrome等
    //        element.mozRequestFullScreen || //FireFox
    //        element.msRequestFullScreen; //IE11
    //    if (requestMethod) {
    //        requestMethod.call(element);
    //    }
    //    else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
    //        var wscript = new ActiveXObject("WScript.Shell");
    //        if (wscript !== null) {
    //            wscript.SendKeys("{F11}");
    //        }
    //    }
    //};

    //退出全屏 判断浏览器种类
    var exitFull = function () {
        // 判断各种浏览器，找到正确的方法
        var exitMethod = document.exitFullscreen || //W3C
            document.mozCancelFullScreen ||    //Chrome等
            document.webkitExitFullscreen || //FireFox
            document.webkitExitFullscreen; //IE11
        if (exitMethod) {
            exitMethod.call(document);
        }
        else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
            var wscript = new ActiveXObject("WScript.Shell");
            if (wscript !== null) {
                wscript.SendKeys("{F11}");
            }
        }
    };

    return {
        handleFullScreen: function () {
            if (isFullScreen) {
                exitFull();
                isFullScreen = false;
            } else {
                requestFullScreen();
                isFullScreen = true;
            }
        },
    };

}();

