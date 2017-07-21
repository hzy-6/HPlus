//初始值
var $btList = $("#btable");//bootstraptable对象
var $PanelSearch = $("#Panel_Search");
var $btnsearch = $("#btn_search");
var $formsearch = $("#form_search");
var $btndel = $("button[data-power=Del]");
var $btnedit = $("button[data-power=Edit]");
var fun = FW.GetQueryString("fun");
var $KeyValue = [];
$(function () {
});
var $List = {
    $index: null,
    BTable: function (options) {//文档地址： http://bootstrap-table.wenzhixin.net.cn/zh-cn/documentation/
        var defaults = {
            domid: "btable",
            height: $(window).height() - 74,
            striped: true,
            method: "post",
            classes: "table table-hover  table-condensed",
            url: "",
            idField: "_ukid",
            contentType: "application/x-www-form-urlencoded", //"multipart/form-data",//"application/json",
            dataType: "json",
            pageSize: 20,
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 1000],
            sidePagination: "server",
            paginationPreText: "上一页",
            paginationNextText: "下一页",
            pagination: true,
            showColumns: false,
            detailView: false,
            clickToSelect: true,
            sortable: true,
            silentSort: true,
            sortName: "_ukid",//定义排序列,通过url方式获取数据填写字段名，否则填写下标 
            sortOrder: "asc",//定义排序方式 'asc' 或者 'desc'
            maintainSelected: true,
            columns: [],
            data: [],
            queryParams: null,
            onClickRow: null,
            onDblClickRow: null,
            onCheck: null,
            onCheckAll: null,
        };
        var options = $.extend({}, defaults, options);
        //拼接 一下表格的默认 列
        var columns_def = [{
            field: 'number',
            title: '行号',
            width: '40px',
            align: 'center',
            formatter: function (value, row, index) {
                var page = $btList.bootstrapTable("getPage");
                return page.pageSize * (page.pageNumber - 1) + index + 1;
                //return index + 1;
            }
        }, {
            checkbox: true,
            field: 'ck',
        }];
        for (var i = 0; i < options.columns.length; i++) {
            columns_def.push(options.columns[i]);
        }
        options.columns = columns_def;

        $btList = $('#' + options.domid);
        var jsonConfig = {
            height: options.height,
            striped: options.striped,
            method: options.method,
            contentType: options.contentType,
            dataType: options.dataType,
            classes: options.classes,
            url: options.url,
            columns: options.columns,
            sidePagination: options.sidePagination,
            paginationPreText: options.paginationPreText,
            paginationNextText: options.paginationNextText,
            data: options.data,
            queryParamsType: 'limit_123',
            undefinedText: '',
            idField: options.idField,
            pageSize: options.pageSize,
            pageNumber: options.pageNumber,
            pageList: options.pageList,
            pagination: options.pagination,
            showColumns: options.showColumns,
            detailView: options.detailView,
            clickToSelect: options.clickToSelect,
            sortable: options.sortable,
            silentSort: options.silentSort,
            sortName: options.sortName,
            sortOrder: options.sortOrder,
            maintainSelected: options.maintainSelected,
            responseHandler: function (res) {
                res.total = res.records;//这里为了兼容以前的 表格插件  
                return res;
            },
            queryParams: function (params) {
                params = {
                    rows: params.pageSize,   //页面大小
                    page: params.pageNumber,  //页码
                    sortName: params.sortName,  //排序列名
                    sortOrder: params.sortOrder//排位命令（desc，asc）
                };
                //将检索的信息放入进去
                var datas = $formsearch.serialize().split("&");
                for (var i = 0; i < datas.length; i++) {
                    params[datas[i].split("=")[0]] = decodeURI(datas[i].split("=")[1]);
                }
                if (options.queryParams != null) {
                    params = options.queryParams(params);
                    //var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    //    pageSize: params.limit,   //页面大小
                    //    pageNumber: params.pageNumber,  //页码
                    //    minSize: $("#leftLabel").val(),
                    //    maxSize: $("#rightLabel").val(),
                    //    minPrice: $("#priceleftLabel").val(),
                    //    maxPrice: $("#pricerightLabel").val(),
                    //    Cut: Cut,
                    //    Color: Color,
                    //    Clarity: Clarity,
                    //    sort: params.sort,  //排序列名
                    //    sortOrder: params.order//排位命令（desc，asc）
                    //};
                }
                console.log(params);
                return params;
            },
            onClickRow: function (row, dom, field) {
                $btList.bootstrapTable('uncheckAll');
                if (options.onClickRow != null) {
                    options.onClickRow(row, dom, field);
                } else {
                    //$List.$index = dom.data('index'); //记录选中的id 用来 防止 info页保存后 列表刷新不能记住选中的数据
                    $List.$index = row._ukid;
                }
            },
            onDblClickRow: function (row, dom, field) {
                if (options.onDblClickRow != null) {
                    if (fun != null && fun != "")
                        options.onDblClickRow(row, dom, field);
                } else {
                    if (fun != null && fun != "")
                        $.FindBack.BindDbClick(row._ukid);
                }
            },
            onCheck: function (row, dom) {//选中复选框
                if (options.onCheck != null) {
                    options.onCheck(row, dom);
                } else {
                    $List.ChekeBtnState();
                }
            },
            onUncheck: function () {//取消复选框
                $List.ChekeBtnState();
            },
            onCheckAll: function (row) {//选中所有复选框
                if (options.onCheckAll != null) {
                    options.onCheckAll(row);
                } else {
                    $List.ChekeBtnState();
                }
            },
            onUncheckAll: function () {//取消选中所有复选框
                $List.ChekeBtnState();
            },
            onLoadSuccess: function () {
                //加载完成 检测一下是否有选中的行id 如果有将行 设置为选中状态
                if ($List.$index) {
                    $btList.bootstrapTable("checkBy", { field: "_ukid", values: [$List.$index] });
                }
            }
        };
        //生成表格
        $btList.bootstrapTable(jsonConfig);
        //表格自适应
        $(window).resize(function () {
            $btList.bootstrapTable('resetView', { height: $(window).height() - 80 });
        });
        //检索
        $btnsearch.click(function () {
            $List.Refresh();
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
        } else {
            $btnedit.attr("disabled", "disabled");
            $btndel.attr("disabled", "disabled");
        }
    },
    //删除数据
    Del: function (options) {
        var defaults = {
            url: ""
        };
        var options = $.extend({}, defaults, options);
        if ($KeyValue.length < 1) {
            FW.MsgBox("请选择要移除的数据!", "警告");
            return false;
        }
        FW.ConfirmBox("确认删除吗?", function (r, ix) {
            if (r) {
                var arr = Array();
                for (var i = 0; i < $KeyValue.length; i++) {
                    arr.push($KeyValue[i]._ukid);
                }
                FW.Ajax({
                    type: "post",
                    url: options.url,
                    data: { ID: JSON.stringify(arr) },
                    success: function (r) {
                        if (r.status == 1) {
                            Lay.close(ix);
                            $List.Refresh();
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
    Refresh: function () {
        $btList.bootstrapTable('refresh');  //refresh 刷新表格
    }
};

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

//bootstraptable 的 扩展方法  用来获取用户选中的信息  返回值： 如果是选中的多条 则返回的是一个数组里面包含了每行的信息  如果是选中的单条则返回的是一行的信息
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