//公共配置
//layer.config({
//    extend: ['extend/layer.ext.js', 'skin/moon/style.css'],
//    skin: 'layer-ext-moon'
//});

$.ajaxSetup({
    //异步请求
    async: true,
    //缓存设置
    cache: false
});

var Lay;
var To;
$(function () {
    SetPower();
    setInterval("SetPower()", 1000);
    //判断当前文档是否是子集
    if (window.top !== window.self) {
        Lay = window.top.layer;
        To = window.top.toastr;
    }
    else {
        Lay = layer;
        To = toastr;
    }
    $.AjaxFilter();
    To.options = {
        "closeButton": true,
        "debug": true,
        "progressBar": true,
        "preventDuplicates": true,
        "positionClass": "toast-top-center",
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
});

//AJAX过滤器
$.AjaxFilter = function () {
    //ajax请求全局设置
    $(document).ready(function () {
    }).ajaxStart(function () {
    }).ajaxSend(function () {
        //发送请求
    }).ajaxSuccess(function (event, xhr, settings) {
        //请求成成功
    }).ajaxComplete(function (evt, request, settings) {
        var text = request.responseText;
        if (text) {
            try {
                var json = $.parseJSON(text);
                if (json.Success) {
                    switch (json.ErrorCode) {
                        //消息提醒
                        case "01":
                            FW.MsgBox(json.ErrorMessage, "警告");
                            break;
                            //登陆超时或违禁操作
                        case "02":
                            FW.OutLogin(json.ErrorMessage, json.JumpUrl);
                            return false;
                            break;
                            //系统错误显示错误页面
                        default:
                            FW.Ajax({
                                type: "post",
                                url: "/Admin/Error/Index",
                                data: json,
                                dataType: "html",
                                success: function (h) {
                                    $("html").html(h);
                                }
                            });
                            break;
                    }
                }
            } catch (e) {
                console.log(e);
            }
        }
    }).ajaxError(function (event, xhr, settings) {
        //请求出错
        //if (xhr.status != 200)
        //    $.ModalMsg("系统错误,请联系管理员！[" + xhr.status + " " + xhr.statusText + "]", "error");
    }).ajaxStop(function () {
    });


}

var FW = {
    MsgBox: function (content, type) {
        if (type) {
            switch (type) {
                case "成功":
                    //成功消息提示，默认背景为浅绿色
                    To.success(content);
                    break;
                case "警告":
                    //警告消息提示，默认背景为橘黄色 
                    To.warning(content);
                    break;
                case "错误":
                    //错误消息提示，默认背景为浅红色 
                    To.error(content);
                    break;
            }
        } else {
            //常规消息提示，默认背景为浅蓝色
            To.info(content);
        }
        //另一种调用方法
        //toastr["info"]("你有新消息了!", "消息提示");
    },
    ConfirmBox: function (content, callBack) {
        Lay.confirm(content, {
            icon: "fa-exclamation-circle",
            title: "消息提醒",
            btn: ['确认', '取消'],
            btnclass: ['btn btn-primary', 'btn btn-danger'],
        }, function (index) {
            callBack(true, index);
        }, function (index) {
            callBack(false, index);
        });
    },
    Loading: {
        ix: 0,
        Open: function () {
            this.ix = Lay.load(1, { shade: [0.5, "#000"], time: 50 * 1000 }); //Lay.msg('请稍候...', { icon: 16, shade: [0.5, "#000"], time: 0 });
        },
        Close: function () {
            Lay.close(this.ix);
        }
    },
    OutLogin: function (Msg, JumpUrl) {
        Lay.msg("消息提醒：" + Msg + "! [ 3s ]", {
            time: 3 * 1000,
            shade: [0.3, "#393D49"],
            success: function (layero, index) { //提示框成功弹出
                var i = 3;
                setInterval(function () {//动态时间
                    i--;
                    $(layero).find(".layui-layer-content").text("消息提醒：" + Msg + "! [ " + i + "s ]");
                }, 1000);
            }
        }, function () {
            //提示框关闭后
            window.top.location = JumpUrl;
        });
    },
    //Ajax请求
    Ajax: function (options) {
        var defaults = {
            type: "post",
            url: "",
            dataType: "json",
            data: {},
            success: null,
            async: true,
            NotLoding: true
        };
        var options = $.extend(defaults, options);
        if (options.url == "")
            return false;
        if (options.NotLoding)
            FW.Loading.Open();
        window.setTimeout(function () {
            $.ajax({
                type: options.type,
                url: options.url,
                dataType: options.dataType,
                data: options.data,
                async: options.async,
                success: function (r) {
                    if (options.NotLoding)
                        FW.Loading.Close();
                    options.success(r);
                },
                beforeSend: function () {
                },
                complete: function () {

                }
            });
        }, 200);

    },
    //Ajax上传文件到服务器
    AjaxUpFile: function (options) {
        var defaults = {
            elid: "",
            url: "",
            dataType: "json",
            data: null,
            success: null,
            NotLoding: true
        };
        var options = $.extend(defaults, options);
        if (options.url == "")
            return false;
        if (options.NotLoding)
            FW.Loading.Open();
        window.setTimeout(function () {
            $.ajaxFileUpload({
                url: options.url, //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: options.elid, //文件上传域的ID
                dataType: options.dataType, //返回值类型 一般设置为json
                data: options.data, //服务器成功响应处理函数
                success: function (data, status) {
                    if (options.NotLoding)
                        FW.Loading.Close();
                    if (options.success != null) {
                        options.success(data, status);
                    }
                }, //服务器响应失败处理函数
                error: function (data, status, e) {
                    return false;
                },
                complete: function () {

                }
            });
        }, 200);
    },
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return "";
    },
    //建立一個可存取到該file的url  用于上传图片，，可通过该地址浏览图片
    GetObjectURL: function (file) {
        var url = "";
        if (window.createObjectURL != undefined) { // basic
            url = window.createObjectURL(file);
        } else if (window.URL != undefined) { // mozilla(firefox)
            url = window.URL.createObjectURL(file);
        } else if (window.webkitURL != undefined) { // webkit or chrome
            url = window.webkitURL.createObjectURL(file);
        }
        return url;
    },
    //设置cookie
    setCookie: function (name, value) {
        var Days = 30;
        var exp = new Date();
        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    },
    //获取cookie值
    getCookie: function (name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return null;
    },
    delCookie: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = getCookie(name);
        if (cval != null)
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    },
    isPositiveNum: function (s) {//是否为正整数  
        var re = /^[0-9]*[1-9][0-9]*$/;
        return re.test(s);
    },
    jdmoney: function (money) {//判断是否为正实数。
        var t = /^\d+(\.\d+)?$/;
        return t.test(money);
    },
    Mapping: function (json, obj, objName) {
        for (var prop in json) {
            if (obj.hasOwnProperty(prop))
                eval(objName + "." + prop + "('" + (json[prop] ? json[prop] : "") + "')");
        }
    }
}

//移除数组某一项值
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};


//获取当前Iframe的Name
$.GetFrameName = function () {
    return "layui-layer-iframe" + $.GetFrameIndex();
}

//获取当前iFrame 的 Index
$.GetFrameIndex = function () {
    return top.layer.getFrameIndex(window.name);
}

//获取父级 iFrame Name
$.GetParentFrameName = function () {
    return top.$("#" + $.GetFrameName()).attr("data-parentiframename");
}

//获取H+窗口选中的Iframe的Name
$.GetWindowIframe = function () {
    return top.$("#content-main>iframe:visible").attr("name");
};


//根据Iframe的name 获取Ifram对象 dx对象可不用传递
$.GetIFrame = function (name, dx) {
    var f;
    if (dx == "" || dx == null || dx == undefined) {
        dx = "top"; f = eval(dx + ".frames");
    }
    else {
        f = eval(dx);
    }
    for (var i = 0; i < f.length; i++) {
        if (f[i].length > 0) {
            return $.GetIFrame(name, dx + ".frames['" + f[i].name + "']");
        }
        else {
            if (f[i].name == name) {
                return dx + ".frames['" + name + "']";
            }
        }
    }
}

//执行某个Iframe中的函数 funName：函数名（参数1，参数2，...）  iframeName：iframe名字
$.ExFun = function (funName, iframeName) {
    try {
        eval($.GetIFrame(iframeName) + "." + funName + ";");
        return true;
    } catch (e) {
        throw new Error(e.message);
        return false;
    }
}

//查找带回
$.FindBack = {
    JqGridBindDbClick: function (id) {
        var fun = FW.GetQueryString("fun");
        if (fun != null) {
            top.FW.setCookie("FindBack_Json", id);
            Lay.close($.GetFrameIndex());
        }
    },
    OpenPage: function (options) {
        var defaults = {
            type: 2,
            title: "查找带回",
            shadeClose: true,
            shade: [0.3, '#393D49'],
            maxmin: true,
            width: "1000px",
            height: "500px",
            content: '',
            parentIframeName: "",
            IsFull: false,
            callBack: null
        };
        var options = $.extend(defaults, options);
        var _width = top.$("html").width() > parseInt(options.width.replace('px', '')) ? options.width : top.$("html").width() + 'px';
        var _height = top.$("html").height() > parseInt(options.height.replace('px', '')) ? options.height : top.$("html").height() + 'px';
        //获取父级的Iframe名称  data-parentiframename
        //var parentIframeName = $.GetParentFrameName();
        var index = Lay.open({
            type: options.type,
            title: options.title,
            fix: false,
            shadeClose: options.shadeClose,
            shade: options.shade, //遮罩层
            shift: 5,
            maxmin: options.maxmin, //开启最大化最小化按钮
            //area: [options.width, options.height],
            area: [_width, _height],
            content: options.content,  //' /Admin/MemberGrade?isUse=1&fun=backFindMemberGrade'
            //zIndex: Lay.zIndex, //重点1
            success: function (layero) {
                //Lay.setTop(layero); //重点2
                $(layero).find(".layui-layer-setwin>.layui-layer-min").remove();
                $(layero).find("iframe").attr("data-parentiframename", options.parentIframeName == "" ? $.GetFrameName() : options.parentIframeName);
            },
            end: function () {
                var FindBack_Json = top.FW.getCookie("FindBack_Json");
                if (options.callBack != null) {
                    if (FindBack_Json != null && FindBack_Json != "") {
                        options.callBack(FindBack_Json);
                    }
                }
                top.FW.setCookie("FindBack_Json", "");
            }
        });
        if (options.IsFull) {
            Lay.full(index);
        }
    }
};

//打开Iframe窗口
$.ModalOpen = function (options) {
    var defaults = {
        id: "",
        url: "",
        title: "系统窗口",
        width: "100px",
        height: "100px",
        btn: ["保存", "关闭"],
        btnClass: ['btn btn-primary', 'btn btn-danger'],
        shade: 0.3,
        parentIframeName: "",
        callBack: null,
        IsFull: false
    };
    var options = $.extend({}, defaults, options);
    var _width = top.$("html").width() > parseInt(options.width.replace('px', '')) ? options.width : top.$("html").width() + 'px';
    var _height = top.$("html").height() > parseInt(options.height.replace('px', '')) ? options.height : top.$("html").height() + 'px';
    var json = $.extend({}, {
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        shift: 0,
        btn: options.btn,
        btnClass: options.btnClass,
        maxmin: true,
        //zIndex: Lay.zIndex, //重点1
        success: function (layero) {
            //Lay.setTop(layero); //重点2
            top.$(layero).find(".layui-layer-min").remove();
            top.$(layero).find("iframe").attr("data-parentiframename", options.parentIframeName);
        },
        yes: function () {
            options.callBack(options.id);
        }, cancel: function () {
            return true;
        }
    }, options);
    var index = Lay.open(json);
    if (options.IsFull) {
        Lay.full(index);
    }
}

$.ModalConfirm = function (content, callBack) {
    Lay.confirm(content, {
        icon: "fa-exclamation-circle",
        title: "消息提醒",
        btn: ['确认', '取消'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
    }, function (index) {
        callBack(true, index);
    }, function (index) {
        callBack(false, index);
    });
}

$.ModalAlert = function (content, type) {
    var icon = "";
    if (type == 'success' || type == '成功') {
        icon = "1";//"fa-check-circle";
    }
    if (type == 'error' || type == '错误') {
        icon = "2";//"fa-times-circle";
    }
    if (type == 'warning' || type == '警告') {
        icon = "0"//"fa-exclamation-circle";
    }
    Lay.alert(content, {
        icon: icon,
        title: "消息提醒",
        btn: ['确认'],
        btnclass: ['btn btn-primary'],
    });
}


$.ModalMsg = function (content, type) {
    if (type != undefined) {
        var icon = "";
        if (type == 'success' || type == '成功') {
            icon = "1";
        }
        if (type == 'error' || type == '错误') {
            icon = "2";
        }
        if (type == 'warning' || type == '警告') {
            icon = "0";
        }
        Lay.msg(content, { icon: icon, time: 3500, shift: 0 });
    } else {
        Lay.msg(content);
    }
}

//关闭模态框
$.ModalClose = function (IsClose) {
    var parentName = $.GetParentFrameName();
    if (top.window.frames[parentName] == undefined) {
        parentName = $.GetWindowIframe();
    }
    try {
        if (parentName) {
            top.window.frames[parentName].Refresh();
        }
    } catch (e) {

    }
    if (IsClose) {
        Lay.close($.GetFrameIndex());
    }
    else {
    }
}

$.fn.jqGridRowValue = function () {
    var $grid = $(this);
    var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var json = [];
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
        return json;
    } else {
        if (!$grid.jqGrid('getGridParam', 'multiselect'))
            return $grid.jqGrid('getRowData', $grid.jqGrid('getGridParam', 'selrow'));
        else
            return new Array();
    }
}

//V1 method
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
}



//V2 static
//string类的扩展
//var a = "I Love {0}, and You Love {1},Where are {0}! {4}";
//alert(String.format(a, "You", "Me"));
//alert(a.format("You", "Me"));

String.format = function () {
    if (arguments.length == 0)
        return null;

    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

function GetRequest() {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

//设置权限
function SetPower() {
    try {
        if (PowerModel) {
            for (var item in PowerModel) {
                if (!PowerModel[item]) {
                    $("*[data-power=" + item + "]").remove();
                    $("#Btn_Power_" + item).remove();
                }
                //if (PowerModel[item]) {
                //    $("*[data-power=" + item + "]").show();
                //    $("#Btn_Power_" + item).show();
                //}
                //else {
                //    $("*[data-power=" + item + "]").remove();
                //    $("#Btn_Power_" + item).remove();
                //}
            }
        }
    } catch (e) {

    }
}

function Arabia_to_Chinese(Num) {
    for (i = Num.length - 1; i >= 0; i--) {
        Num = Num.replace(",", "")//替换tomoney()中的“,”
        Num = Num.replace(" ", "")//替换tomoney()中的空格
    }
    Num = Num.replace("￥", "")//替换掉可能出现的￥字符
    if (isNaN(Num)) { //验证输入的字符是否为数字
        alert("请检查小写金额是否正确");
        return;
    }
    //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
    part = String(Num).split(".");
    newchar = "";
    //小数点前进行转化
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) { alert("位数过大，无法计算"); return ""; } //若数量超过拾亿单位，提示
        tmpnewchar = ""
        perchar = part[0].charAt(i);
        switch (perchar) {
            case "0": tmpnewchar = "零" + tmpnewchar; break;
            case "1": tmpnewchar = "壹" + tmpnewchar; break;
            case "2": tmpnewchar = "贰" + tmpnewchar; break;
            case "3": tmpnewchar = "叁" + tmpnewchar; break;
            case "4": tmpnewchar = "肆" + tmpnewchar; break;
            case "5": tmpnewchar = "伍" + tmpnewchar; break;
            case "6": tmpnewchar = "陆" + tmpnewchar; break;
            case "7": tmpnewchar = "柒" + tmpnewchar; break;
            case "8": tmpnewchar = "捌" + tmpnewchar; break;
            case "9": tmpnewchar = "玖" + tmpnewchar; break;
        }
        switch (part[0].length - i - 1) {
            case 0: tmpnewchar = tmpnewchar + "元"; break;
            case 1: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 2: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 3: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 4: tmpnewchar = tmpnewchar + "万"; break;
            case 5: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 6: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 7: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 8: tmpnewchar = tmpnewchar + "亿"; break;
            case 9: tmpnewchar = tmpnewchar + "拾"; break;
        }
        newchar = tmpnewchar + newchar;
    }
    //小数点之后进行转化
    if (Num.indexOf(".") != -1) {
        if (part[1].length > 2) {
            alert("小数点之后只能保留两位,系统将自动截段");
            part[1] = part[1].substr(0, 2)
        }
        for (i = 0; i < part[1].length; i++) {
            tmpnewchar = ""
            perchar = part[1].charAt(i)
            switch (perchar) {
                case "0": tmpnewchar = "零" + tmpnewchar; break;
                case "1": tmpnewchar = "壹" + tmpnewchar; break;
                case "2": tmpnewchar = "贰" + tmpnewchar; break;
                case "3": tmpnewchar = "叁" + tmpnewchar; break;
                case "4": tmpnewchar = "肆" + tmpnewchar; break;
                case "5": tmpnewchar = "伍" + tmpnewchar; break;
                case "6": tmpnewchar = "陆" + tmpnewchar; break;
                case "7": tmpnewchar = "柒" + tmpnewchar; break;
                case "8": tmpnewchar = "捌" + tmpnewchar; break;
                case "9": tmpnewchar = "玖" + tmpnewchar; break;
            }
            if (i == 0) tmpnewchar = tmpnewchar + "角";
            if (i == 1) tmpnewchar = tmpnewchar + "分";
            newchar = newchar + tmpnewchar;
        }
    }
    //替换所有无用汉字
    while (newchar.search("零零") != -1)
        newchar = newchar.replace("零零", "零");
    newchar = newchar.replace("零亿", "亿");
    newchar = newchar.replace("亿万", "亿");
    newchar = newchar.replace("零万", "万");
    newchar = newchar.replace("零元", "元");
    newchar = newchar.replace("零角", "");
    newchar = newchar.replace("零分", "");

    if (newchar.charAt(newchar.length - 1) == "元" || newchar.charAt(newchar.length - 1) == "角")
        newchar = newchar + "整"
    //  document.write(newchar);
    return newchar;

}

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

