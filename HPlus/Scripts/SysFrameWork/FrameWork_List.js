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
    BTable: function (options) {//文档地址： http://bootstrap-table.wenzhixin.net.cn/zh-cn/documentation/
        var defaults = {
            domid: "btable",
            height: $(window).height() - 80,
            striped: true,
            method: "post",
            classes: "table table-hover",
            url: "",
            idField: "_ukid",
            pageSize: 20,
            pageNumber: 1,
            pageList: [10, 25, 50, 100, 1000],
            pagination: true,
            showColumns: false,
            detailView: false,
            clickToSelect: true,
            sortable: true,
            sortName: "_ukid",//定义排序列,通过url方式获取数据填写字段名，否则填写下标 
            sortOrder: "asc",//定义排序方式 'asc' 或者 'desc'
            columns: [],
            data: [],
            onClickRow: null,
            onDblClickRow: null,
            onCheck: null,
            onCheckAll: null,
        };
        var options = $.extend({}, defaults, options);
        $btList = $('#' + options.domid);
        var jsonConfig = {
            height: options.height,
            striped: options.striped,
            method: options.method,
            classes: options.classes,
            url: options.url,
            columns: options.columns,
            data: options.data,
            idField: options.idField,
            pageSize: options.pageSize,
            pageNumber: options.pageNumber,
            pageList: options.pageList,
            pagination: options.pagination,
            showColumns: options.showColumns,
            detailView: options.detailView,
            clickToSelect: options.clickToSelect,
            sortable: options.sortable,
            sortName: options.sortName,
            sortOrder: options.sortOrder,
            onClickRow: function (row, dom, field) {
                $btList.bootstrapTable('uncheckAll');
                if (options.onClickRow != null) {
                    options.onClickRow(row, dom, field);
                } else {

                }
            },
            onDblClickRow: function (row, dom, field) {
                if (options.onDblClickRow != null) {
                    if (fun != null && fun != "")
                        options.onDblClickRow(row, dom, field);
                } else {
                    if (fun != null && fun != "")
                        $.FindBack.JqGridBindDbClick(row._ukid);
                }
            },
            onCheck: function (row, dom) {
                if (options.onCheck != null) {
                    options.onCheck(row, dom);
                } else {

                    $List.ChekeBtnState();
                }
            },
            onUncheck: function () {

                $List.ChekeBtnState();
            },
            onCheckAll: function (row) {
                if (options.onCheckAll != null) {
                    options.onCheckAll(row);
                } else {
                    $List.ChekeBtnState();
                }
            }
        };
        $btList.bootstrapTable(jsonConfig);
        $(window).resize(function () {
            $btList.bootstrapTable('resetView');
        });
    },
    ChekeBtnState: function () {//检查button 状态
        //下面是控制 修改和删除的按钮状态
        $KeyValue = $btList.BTRowValue();
        if ($KeyValue.length > 0) {
            if ($KeyValue.length == 1) {
                $btnedit.removeAttr("disabled");
                $btndel.removeAttr("disabled");
            } else {
                $btndel.removeAttr("disabled");
                $btnedit.attr("disabled", "disabled");
            }
        }
    },
    //删除数据
    Del: function (options) {
        var defaults = {
            url: ""
        };
        var options = $.extend({}, defaults, options);
        var KeyValue = $btList.BTRowValue();
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
    ExportExcel: function (url, data) {//到处excel
        $(event.srcElement).attr("href", url + "?" + $formsearch.serialize() + (data ? data : ""));
    },
}

//检索隐藏、显示
function ShowSearch(t) {
    $PanelSearch.stop().animate({
        height: "toggle"
    }, 200);
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

//bootstraptable 的 扩展类  用来获取用户选中的信息  返回值： 如果是选中的多条 则返回的是一个数组里面包含了每行的信息  如果是选中的单条则返回的是一行的信息
$.fn.BTRowValue = function () {
    var $BTable = $(this);
    var rows = $BTable.bootstrapTable('getSelections');
    if (rows.length > 0) {
        var json = [];
        for (var i = 0; i < rows.length ; i++) {
            var row = {};
            for (item in rows[i]) {
                row[item] = rows[i][item];
            }
            json.push(row);
        }
        return json;
    }
    return [];
}