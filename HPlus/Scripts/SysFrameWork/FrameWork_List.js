//初始值
$.jgrid.defaults.styleUI = "Bootstrap";
var $btList = $("#btable");//bootstraptable对象
var $gridList = $("#jqtable");
var $PanelSearch = $("#Panel_Search");
var $btnsearch = $("#btn_search");
var $formsearch = $("#form_search");
var $btndel = $("button[data-power=Del]");
var $btnedit = $("button[data-power=Edit]");
var fun = FW.GetQueryString("fun");
var $KeyValue = "";
$(function () {
    // Add responsive to jqGrid  表格自适应
    $(window).bind('resize', function () {
        var width = $('.jqGrid_wrapper').width();
        var height = $('.jqGrid_wrapper').height();
        $gridList.setGridWidth(width);
        $gridList.setGridHeight($(window).height() - 180);
    });
});
var $List = {
    //表格初始化
    TableInit: function (options) {
        var defaults = {
            id: "jqtable",
            url: "",
            pager: "#pager",
            datatype: "json",
            rowNum: 20,
            rowList: [5, 10, 20, 50, 100, 1000, 2000],
            width: $('.jqGrid_wrapper').width(),
            height: $(window).height() - 186,//-150
            colNames: [],
            colModel: [],
            postData: null,
            sortname: "_ukid",
            multiselect: null,
            sortorder: "desc",
            jsonReader: null,
            ondblClickRow: null,
            onSelectRow: null,
            onSelectAll: null,
        };
        var options = $.extend({}, defaults, options);
        $gridList = $('#' + options.id);
        var jsonConfig = {
            url: options.url,
            rowNum: options.rowNum,
            rowList: options.rowList,
            pager: options.pager,
            datatype: options.datatype,
            width: options.width,
            height: options.height,//-150
            colNames: options.colNames,
            colModel: options.colModel,
            sortname: options.sortname,//排序名称
            mtype: "post",
            loadComplete: function (r) {
                $KeyValue = $gridList.jqGridRowValue();
            },
            viewrecords: true,
            reloadAfterSubmit: true,//提交完重新加载
            sortorder: options.sortorder,
            rownumbers: true,
            multiselect: ((options.multiselect == null) ? ((fun == null || fun == "") ? true : false) : options.multiselect),//如果是查找带回，去除复选框
            multiboxonly: true,
            jsonReader: options.jsonReader,
            gridComplete: function () {
                //$(".J_menuItem").on("click", top.menuItem);
            },
            onSelectRow: function (rowid, e) {
                if (options.onSelectRow == null) {
                    $KeyValue = $gridList.jqGridRowValue();
                    if ($KeyValue.length == 1) {
                        $btnedit.removeAttr("disabled");
                    }
                    else {
                        $btnedit.attr("disabled", "disabled");
                    }
                    if ($KeyValue.length > 0) {
                        $btndel.removeAttr("disabled");
                    }
                    else {
                        $btndel.attr("disabled", "disabled");
                    }
                }
                else {
                    options.onSelectRow(rowid);
                }
            },
            ondblClickRow: function (rowid, iRow, iCol, e) {
                if (options.ondblClickRow == null) {
                    if (fun != null && fun != "")
                        $.FindBack.JqGridBindDbClick($gridList.jqGrid('getRowData', rowid)._ukid);
                }
                else {
                    if (fun != null && fun != "")
                        options.ondblClickRow(rowid, iRow, iCol, e);
                }
            },
            onSelectAll: function (aRowids, status) {
                if (options.onSelectAll == null) {
                    $KeyValue = $gridList.jqGridRowValue();
                    if ($KeyValue.length == 1) {
                        $btnedit.removeAttr("disabled");
                    }
                    else {
                        $btnedit.attr("disabled", "disabled");
                    }
                    if ($KeyValue.length > 0) {
                        $btndel.removeAttr("disabled");
                    }
                    else {
                        $btndel.attr("disabled", "disabled");
                    }
                }
                else {
                    options.onSelectAll(aRowids);
                }
            }
        };
        if (options.postData != null) {
            jsonConfig.postData = options.postData;
        }

        $gridList.jqGrid(jsonConfig);

        //检索
        $btnsearch.click(function () {
            var postdata = $gridList.jqGrid('getGridParam').postData;
            var datas = $formsearch.serialize().split("&");
            for (var i = 0; i < datas.length; i++) {
                postdata[datas[i].split("=")[0]] = decodeURI(datas[i].split("=")[1]);
            }
            $gridList.jqGrid('setGridParam', {
                postData: postdata,
                datatype: "json",
                page: 1,
            }).trigger('reloadGrid', { fromServer: true, page: 1 });
            $("#Btn_Power_Search").click();

            $KeyValue = $gridList.jqGridRowValue();
            if ($KeyValue.length == 1) {
                $btnedit.removeAttr("disabled");
            }
            else {
                $btnedit.attr("disabled", "disabled");
            }
            if ($KeyValue.length > 0) {
                $btndel.removeAttr("disabled");
            }
            else {
                $btndel.attr("disabled", "disabled");
            }

        });
    },
    BTable: function (options) {
        var defaults = {
            domid: "btable",
            height: $(window).height() - 74,
            striped: false,
            method: "post",
            url: "",
            pageSize: 20,
            pageNumber: 1,
            pageList: [10, 25, 50, 100, 1000],
            pagination: true,
            showColumns: false,
            detailView: false,
            clickToSelect: true,
            columns: [],
            data: [],
            onClickRow: null,
        };
        var options = $.extend({}, defaults, options);
        $btList = $('#' + options.domid);
        var jsonConfig = {
            height: options.height,
            striped: options.striped,
            method: options.method,
            url: options.url,
            pageSize: options.pageSize,
            pageNumber: options.pageNumber,
            pageList: options.pageList,
            pagination: options.pagination,
            showColumns: options.showColumns,
            detailView: options.detailView,
            clickToSelect: options.clickToSelect,
            onClickRow: function (row, dom, field) {
                $btList.bootstrapTable('uncheckAll');
                if (options.onClickRow != null)
                    options.onClickRow(row, dom, field);
            },
            columns: options.columns,
            data: options.data,
        };
        $btList.bootstrapTable(jsonConfig);
    },
    //删除数据
    Del: function (options) {
        var defaults = {
            url: ""
        };
        var options = $.extend({}, defaults, options);
        var KeyValue = $gridList.jqGridRowValue();
        if (KeyValue.length < 1) {
            FW.MsgBox("请选择要移除的数据!", "警告");
            return false;
        }
        FW.ConfirmBox("确认删除吗?", function (r, ix) {
            if (r) {
                var arr = Array();
                for (var i = 0; i < KeyValue.length; i++) {
                    arr.push(KeyValue[i]._ukid);
                }
                FW.Ajax({
                    type: "post",
                    url: options.url,
                    data: { ID: JSON.stringify(arr) },
                    success: function (r) {
                        if (r.status == 1) {
                            Lay.close(ix);
                            Refresh();
                            FW.MsgBox("操作成功!", "成功");
                        }
                    }
                });
            }
        });
    },
    ExportExcel: function (url, data) {
        $(event.srcElement).attr("href", url + "?" + $formsearch.serialize() + (data ? data : ""));
    },
}

//检索隐藏、显示
function ShowSearch(t) {
    $PanelSearch.stop().animate({
        height: "toggle"
    }, 300);
    if ($PanelSearch.height() < 2) {
        $(t).find("i").removeClass("fa fa-chevron-down").addClass("fa fa-chevron-up");
        //fa fa-chevron-down
    }
    else {
        $(t).find("i").removeClass("fa fa-chevron-up").addClass("fa fa-chevron-down");
    }
}

//刷新列表
function Refresh(data) {
    if (data) {
        var postdata = $gridList.jqGrid('getGridParam').postData;
        var datas = ($formsearch.serialize() + data).split("&");
        for (var i = 0; i < datas.length; i++) {
            postdata[datas[i].split("=")[0]] = decodeURI(datas[i].split("=")[1]);
        }
        $gridList.jqGrid('setGridParam', {
            postData: postdata,
            datatype: "json",
            page: 1,
        }).trigger('reloadGrid', { fromServer: true, page: 1 });
    }
    else {
        $gridList.trigger("reloadGrid", { fromServer: true, page: 1 });
    }

    $KeyValue = $gridList.jqGridRowValue();
    if ($KeyValue.length == 1) {
        $btnedit.removeAttr("disabled");
    }
    else {
        $btnedit.attr("disabled", "disabled");
    }
    if ($KeyValue.length > 0) {
        $btndel.removeAttr("disabled");
    }
    else {
        $btndel.attr("disabled", "disabled");
    }

}