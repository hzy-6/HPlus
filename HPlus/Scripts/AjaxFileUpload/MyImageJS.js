function dwzImageUpLoad(name) {
    var url = $("[name='" + name + "']").attr("dwzSugessUrl") + "?dwzaction=dwzImage&dwzcontrolname=" + name + "&dwzeventname=upload";
    var jsonstr = '{' + $("[name='" + name + "']").attr("JsonData") + ',savepath: ' + $("[name='" + name + "']").attr("savepath") + ' }';
    //var buff = href.replace('')
    if ($("#" + name + "_inputFile").val() == '') {
        alert('请选择需要上传的文件');
        return false;
    }
    // var quer = '?knowkey=' + knowkey + '&knowflag=' + escape(knowflag) + '&know=1&pageindex=' + pager + '&href=' + escape(href) + '&text=' + escape(text) + '';
    /*$box.ajaxUrl({
    type: "POST", url: url, data: form, callback: function (a) {
    //$box.find("[layoutH]").layoutH();
    dwzMsgPager(0);
    }
    });*/
    var a = eval("(" + jsonstr + ")");
    $.ajaxFileUpload
            (
                {
                    url: url, //用于文件上传的服务器端请求地址
                    secureuri: false, //是否需要安全协议，一般设置为false
                    fileElementId: name + '_inputFile', //文件上传域的ID
                    dataType: 'text', //返回值类型 一般设置为json
                    data: a,
                    success: function (data, status)  //服务器成功响应处理函数
                    {
                        $("[name='" + name + "_dwzp2']").html(data);
                        /*$("#img1").attr("src", data.imgurl);
                        if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                        alert(data.error);
                        } else {
                        alert(data.msg);
                        }
                        }*/
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                }
            )

            }

            function dwzImageDel(name) {
                var $box = $("[name='" + name + "_dwzp2']");
                var url = $("[name='" + name + "']").attr("dwzSugessUrl") + "?dwzaction=dwzImage&dwzcontrolname=" + name + "&dwzeventname=del";
                var buff = ",DWZImageDataKey:'" + $box.find("[selected='selected']").attr("DWZImageDataKey") + "',DWZDelImageName:'" + $box.find("[selected='selected']").attr("DWZDelImageName") + "'";
                if ($box.find("[selected='selected']").length == 0) {
                    alert('请选择一个要删除文件');
                    return false;
                }
                var jsonstr = '{' + $("[name='" + name + "']").attr("JsonData") + ',savepath: ' + $("[name='" + name + "']").attr("savepath") + '' + buff + '}';
                $box.ajaxUrl({
                    type: "POST", url: url, data: eval("(" + jsonstr + ")"), callback: function () {
                        //$box.find("[layoutH]").layoutH();
                    }
                });
            }
            function dwzImageSelect(name) {
                var $box = $("[name='" + name + "_dwzp2']");
                //border:1px solid #000
                $box.find('img').css("border", "");
                $box.removeAttr("selected");
                $obj = $(event.srcElement);
                if ($obj.attr("selected") == null) {
                    $obj.css("border", "1px solid red");
                    $obj.attr("selected", "true");
                }
                else {
                    $obj = $(event.srcElement);

                    $obj.removeAttr("selected");
                }
                
            }