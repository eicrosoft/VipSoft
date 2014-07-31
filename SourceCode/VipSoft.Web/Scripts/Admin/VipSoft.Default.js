// 要清空这里的JS，将 unicorn.js 的内容移值过来。


var tab = null;
var accordion = null;
var tree = null;
$(function () {
    //页面布局
    $("#global_layout").ligerLayout({ leftWidth: 180, height: '100%', topHeight: 65, bottomHeight: 24, allowTopResize: false, allowBottomResize: false, allowLeftCollapse: true, onHeightChanged: f_heightChanged });

    var height = $(".l-layout-center").height();

    //Tab
    $("#framecenter").ligerTab({ height: height });

    //左边导航面板
    $("#global_left_nav").ligerAccordion({ height: height - 25, speed: null });

    $(".l-link").hover(function () {
        $(this).addClass("l-link-over");
    }, function () {
        $(this).removeClass("l-link-over");
    });

    //设置频道菜单
    $("#global_channel_tree").ligerTree({
        url: '../tools/admin_ajax.ashx?action=sys_channel_load',
        checkbox: false,
        nodeWidth: 112,
        //attribute: ['nodename', 'url'],
        onSelect: function (node) {
            if (!node.data.url) return;
            var tabid = $(node.target).attr("tabid");
            if (!tabid) {
                tabid = new Date().getTime();
                $(node.target).attr("tabid", tabid)
            }
            f_addTab(tabid, node.data.text, node.data.url);
        }
    });

    //加载插件菜单
    //loadPluginsNav();

    //快捷菜单
    var menu = $.ligerMenu({
        width: 120, items:
    [
        { text: '管理首页', click: itemclick },
        { text: '修改密码', click: itemclick },
        { line: true },
        { text: '关闭菜单', click: itemclick }
    ]
    });
    $("#tab-tools-nav").bind("click", function () {
        var offset = $(this).offset(); //取得事件对象的位置
        menu.show({ top: offset.top + 27, left: offset.left - 120 });
        return false;
    });

    tab = $("#framecenter").ligerGetTabManager();
    accordion = $("#global_left_nav").ligerGetAccordionManager();
    tree = $("#global_channel_tree").ligerGetTreeManager();
    //tree.expandAll(); //默认展开所有节点
    $("#pageloading_bg,#pageloading").hide();
});

//频道菜单异步加载函数，结合ligerMenu.js使用
//function loadChannelTree() {
//    if (tree != null) {
//        tree.clear();
//        tree.loadData(null, "tools/admin_ajax.ashx?action=sys_channel_load");
//    }
//}

//加载插件管理菜单
//function loadPluginsNav() {
//    $.ajax({
//        type: "POST",
//        url: "tools/admin_ajax.ashx?action=plugins_nav_load&time=" + Math.random(),
//        timeout: 20000,
//        beforeSend: function (XMLHttpRequest) {
//            $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">正在加载，请稍候...</div>");
//        },
//        success: function (data, textStatus) {
//            $("#global_plugins").html(data);
//        },
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">加载插件菜单出错！</div>");
//        }
//    });
//}

//快捷菜单回调函数
function itemclick(item) {
    switch (item.text) {
        case "管理首页":
            f_addTab('home', '管理中心', 'center.aspx');
            break;
        case "快捷导航":
            //调用函数
            break;
        case "修改密码":
            f_addTab('manager_pwd', '修改密码', 'manager/manager_pwd.aspx');
            break;
        default:
            //关闭窗口
            break;
    }
}
function f_heightChanged(options) {
    if (tab)
        tab.addHeight(options.diff);
    if (accordion && options.middleHeight - 24 > 0)
        accordion.setHeight(options.middleHeight - 24);
}
//添加Tab，可传3个参数
function f_addTab(tabid, text, url, iconcss) {
    if (arguments.length == 4) {
        tab.addTabItem({ tabid: tabid, text: text, url: url, iconcss: iconcss });
    } else {
        tab.addTabItem({ tabid: tabid, text: text, url: url });
    }
}
//提示Dialog并关闭Tab
function f_errorTab(tit, msg) {
    $.ligerDialog.open({
        isDrag: false,
        allowClose: false,
        type: 'error',
        title: tit,
        content: msg,
        buttons: [{
            text: '确定',
            onclick: function (item, dialog, index) {
                //查找当前iframe名称
                var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
                var curriframe = "";
                $(itemiframe).each(function () {
                    if ($(this).css("display") != "none") {
                        curriframe = $(this).attr("tabid");
                        return false;
                    }
                });
                if (curriframe != "") {
                    tab.removeTabItem(curriframe);
                    dialog.close();
                }
            }
        }]
    });
}

//Tab控制函数
function tabs(tabId, tabNum) {
    //设置点击后的切换样式
    $(tabId + " .tab_nav li").removeClass("selected");
    $(tabId + " .tab_nav li").eq(tabNum).addClass("selected");
    //根据参数决定显示内容
    $(tabId + " .tab_con").hide();
    $(tabId + " .tab_con").eq(tabNum).show();
}
