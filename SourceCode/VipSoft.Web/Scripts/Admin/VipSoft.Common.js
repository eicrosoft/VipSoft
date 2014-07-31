$(document).ready(function () {
    tableListRowColor();
});
//Tab控制函数
function tabs(tabId, tabNum) {
    //设置点击后的切换样式
    $(tabId + " .tab_nav li").removeClass("selected");
    $(tabId + " .tab_nav li").eq(tabNum).addClass("selected");
    //根据参数决定显示内容
    $(tabId + " .tab_con").hide();
    $(tabId + " .tab_con").eq(tabNum).show();
}
 

function tableListRowColor() {
    if ($(".tableList").length > 0) {
        $(".tableList tr:even").addClass("evenRowColor");
        $(".tableList tr").hover(
            function () { $(this).addClass("mouseOver"); },
            function () { $(this).removeClass("mouseOver"); }
        );
    }
}

function successed() {
    alert("删除成功！");
}