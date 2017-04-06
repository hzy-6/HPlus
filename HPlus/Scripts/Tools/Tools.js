//--------------------------------------Ajax请求全局方法------------------------------//
var index;
var MyLayer;
var $Messager;
var Mydialog;
$.ajaxSetup({
    //异步请求
    async: true,
    //缓存设置
    cache: false
});
//ajax请求全局设置
$(document).ready(function () {
    //判断当前文档是否是子集
    if (window.top !== window.self) {
        $Messager = window.top.$.messager;
        MyLayer = window.top.layer;
    }
    else {
        $Messager = $.messager;
        MyLayer = layer;
    }
}).ajaxStart(function () {
    //加载层-风格4
    Tools.Loading.Open();
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
                        Tools.Alert(json.ErrorMessage, json.JumpUrl);
                        break;
                        //登陆超时或违禁操作
                    case "02":
                        Tools.OutLogin(json.ErrorMessage, json.JumpUrl);
                        break;
                        //登陆超时，打开登陆窗口
                    case "03":
                        OpenLoginWindow(json.ErrorMessage, json.JumpUrl);
                        break;
                        //系统错误显示错误页面
                    default:
                        $.ajax({
                            type: "post",
                            url: "/Index/ErrorPage/",
                            data: json,
                            dataType: "html",
                            success: function (html) {
                                $("html").html(html);
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
    //    Tools.MessgesBox("系统错误,请联系管理员！(" + xhr.status + " " + xhr.statusText + ")", "");
}).ajaxStop(function () {
    //移除遮罩层
    Tools.Loading.Close();
});

var timer;
//工具类
var Tools = {
    //
    Loading: {
        Open: function () {
            index = MyLayer.load(0, {
                shade: [0.2, '#000']
            });
        },
        Close: function () {
            MyLayer.close(index);
        }
    },
    //计时后退出
    OutLogin: function (Msg, JumpUrl) {
        MyLayer.msg("消息提醒：" + Msg + "! [ 5s ]", {
            time: 5 * 1000,
            shade: [0.3, "#393D49"],
            //提示框成功弹出
            success: function (layero, index) {
                var i = 5;
                //动态时间
                setInterval(function () {
                    i--;
                    $(layero).find(".layui-layer-content").text("消息提醒：" + Msg + "! [ " + i + "s ]");
                }, 1000);
            }
        }, function () {
            //提示框关闭后
            window.top.location = JumpUrl;
        });
    },
    //打开登陆窗口
    OpenLoginWindow: function (Msg, JumpUrl) {
        var index = MyLayer.open({
            type: 2,
            title: Msg,
            shadeClose: false,
            shade: [0.5, '#393D49'], //遮罩层
            maxmin: false, //开启最大化最小化按钮
            area: ['300px', '400px'],
            content: JumpUrl
        });
    },
    //弹消息框
    MessgesBox: function (Msg, JumpUrl) {
        if (JumpUrl == "" || JumpUrl == null) {
            MyLayer.alert(Msg + " !", {
                title: "消息提醒",
                skin: 'layui-layer-molv'
            });
        }
        else {
            MyLayer.alert(Msg + " !", {
                title: "消息提醒",
                skin: 'layui-layer-molv'
            }, function () {
                window.location = JumpUrl;
            });
        }
    },
    //弹消息提醒框（常用）
    Alert: function (Msg, JumpUrl) {
        if (JumpUrl == "" || JumpUrl == null) {
            MyLayer.msg(Msg + " !", {
                icon: 6,
                time: 3500,
                offset: 0,
                shift: 6
            }, function () {
            });
            //Tools.Messager.alert("提醒", Msg);
        }
        else {
            MyLayer.msg(Msg + " !", {
                icon: 6,
                time: 3500,
                offset: 0,
                shift: 6
            }, function () {
                window.location = JumpUrl;
            });
        }
    },
    Messager:
        {
            alert: function (t, msg) {
                $Messager.model = {
                    ok: { text: "关闭", classed: 'btn-default' },
                    cancel: { text: "取消", classed: 'btn-danger' }
                };
                $Messager.alert(t, msg);
            },
            confirm: function () {
                $Messager.alert("该方法未完成！");
                //$Messager.alert(Msg, t);
                //$.messager.confirm("提醒!", "您确定要退出吗?", function () {
                //    window.location.href = '@Url.Action("Index","Login")';
                //});
            }
        },
    //进度条
    ProgressBar: {
        //加载动画,返回动画的html，并且开始执行动画
        loading: function () {
            var html = "";
            html += "<div id=\"Tools-ProgressBar\" class=\"progress progress-striped active\">";
            html += "<div style=\"width: 1%\" aria-valuemax=\"100\" aria-valuemin=\"0\" aria-valuenow=\"75\" role=\"progressbar\" class=\"progress-bar progress-bar-danger\">";
            html += "<span class=\"sr-only\">40% Complete (success)</span>";
            html += "</div>";
            html += "</div>";
            var i = 2;
            timer = setInterval(function () {
                if (i >= 98)
                    clearInterval(timer);
                $("#Tools-ProgressBar>.progress-bar.progress-bar-danger").css({ "width": "" + i + "" + "%" });
                i += 1;
            }, 100);
            return html;
        },
        //进度条加载完成
        over: function () {
            clearInterval(timer);
            $("#Tools-ProgressBar>.progress-bar.progress-bar-danger").css({ "width": "100%" });
        }
    },
    //查找带回
    FindBack: {
        //绑定双击事件
        BindDbClick: function () {
            if (Tools.GetQueryString("isUse") == 1) {
                $("tbody>tr").dblclick(function () {
                    //var index = parent.layer.getFrameIndex(window.name);
                    //eval("parent." + Tools.GetQueryString("fun") + "($(this).attr('data-id'))");
                    //parent.layer.close(index);
                    //var data = "" + Tools.GetQueryString("fun") + "($(this).attr('data-id'))";
                    //eval("parent." + Tools.GetQueryString("fun") + "($(this).attr('data-id'))");
                    var json = "fun=" + Tools.GetQueryString("fun") + "," + "id=" + $(this).attr('data-id');
                    Mydialog = top.dialog.get(window);
                    Mydialog.close(json);
                    Mydialog.remove();
                });
            }
        },
        //打开查找带回页面
        OpenFindBackPage: function (title, OpeUrl) {
            //var index = layer.open({
            //    type: 2,
            //    title: title,
            //    shadeClose: true,
            //    shade: [0.9, '#393D49'], //遮罩层
            //    maxmin: true, //开启最大化最小化按钮
            //    area: ['900px', '500px'],
            //    content: OpeUrl  //' /Admin/MemberGrade?isUse=1&fun=backFindMemberGrade'
            //});
            //layer.full(index);
            //= top.dialog.get(window);
            Mydialog = top.dialog({
                id: "FindBack",
                title: title,
                width: 1200,
                height: 500,
                url: OpeUrl,
                onclose: function () {
                    var result = this.returnValue;
                    var json = { fun: result.split(',')[0].split('=')[1], id: result.split(',')[1].split('=')[1] };
                    //alert(json.fun);
                    eval(json.fun + "('" + json.id + "')");
                }
            }).showModal();
        }
    },
    //获取地址栏参数
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
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
    //建立一個可存取到該file的url  用于上传图片，，可通过该地址浏览图片
    getObjectURL: function (file) {
        var url = null;
        if (window.createObjectURL != undefined) { // basic
            url = window.createObjectURL(file);
        } else if (window.URL != undefined) { // mozilla(firefox)
            url = window.URL.createObjectURL(file);
        } else if (window.webkitURL != undefined) { // webkit or chrome
            url = window.webkitURL.createObjectURL(file);
        }
        return url;
    }
};