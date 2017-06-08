$(function () {
    $("body").removeClass("gray-bg");
});
var SaveUrl = "";
var $Form = {
    Load: function (options) {
        var defaults = {
            id: "",
            url: "",
            data: null,
            success: null,
            callBack: null,
            async: true,
            title: null,
        };
        var options = $.extend({}, defaults, options);
        KeyId = options.id;
        var index = $.GetFrameIndex();
        if (KeyId == null || KeyId == "")
            top.$("#layui-layer" + index + ">.layui-layer-title").text(options.title == null ? "新增" : options.title);
        else
            top.$("#layui-layer" + index + ">.layui-layer-title").text(options.title == null ? "修改" : options.title);
        //初始化表单
        FW.Ajax({
            url: options.url,
            data: options.data == null ? { ID: KeyId } : options.data,
            //NotLoding: false,
            async: options.async,
            success: function (r) {
                if (r.status == 1) {
                    if (options.success == null) {
                        ko.cleanNode(document.getElementById("form"));
                        ko.utils.extend(vModel, ko.mapping.fromJS(r));//更新vModel
                        ko.applyBindings(vModel, document.getElementById("form"));//注册vModel
                    }
                    else {
                        options.success(r);
                    }
                    if (options.callBack != null)
                        options.callBack(r);
                }
            }
        });
    },
    Save: function (options) {
        var defaults = {
            isClose: false,
            url: "",
            data: null,
            success: null,
            callBack: null,
            msg: null,
        };
        var options = $.extend({}, defaults, options);
        FW.Ajax({
            type: "Post",
            url: (SaveUrl ? SaveUrl : options.url),
            data: (options.data ? options.data : ko.toJS(vModel)),
            success: function (r) {
                if (options.success == null) {
                    if (r.status == 1) {
                        FW.MsgBox((options.msg == null ? "保存成功!" : options.msg), "成功");
                        $.ModalClose(options.isClose);
                        $Form.ResetUrl(r);
                    }
                }
                else {
                    options.success(r);
                }
                if (options.callBack != null)
                    options.callBack(r);
            }
        });
    },
    //刷新重置URL
    ResetUrl: function (r) {
        //开启遮罩层
        FW.Loading.Open();
        var url = window.location.href;
        if (r) {
            if (url.indexOf('?ID') == -1 && url.indexOf('&ID') == -1) {
                if (url.indexOf('?') != -1) {
                    url = url + '&ID=' + r.ID + '';
                }
                else {
                    url = url + '?ID=' + r.ID + '';
                }
            }
            else {
                var obj = GetRequest();
                if (!obj["ID"]) {
                    url = url.replace("ID=", "ID=" + r.ID);
                }
            }
        }
        else {
            var obj = GetRequest();
            if (obj) {
                url = url.substring(0, url.indexOf('?'));
                url = url + '?1=1';
                for (var item in obj) {
                    if (item != 'ID' && item != '1') {
                        url = url + '&' + item + '=' + obj[item] + '';
                    }
                }
            }
        }
        top.$("iframe[name=" + $.GetFrameName() + "]").attr("src", url).load(function () {
            FW.Loading.Close();
        });
        return url;
    }
};