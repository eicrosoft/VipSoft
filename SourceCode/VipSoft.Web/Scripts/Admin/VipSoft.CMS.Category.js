/// <reference path="jquery-1.7.1.js" />
/// <reference path="jquery.form.js" />
/// <reference path="~/Scripts/layer/layer.js" />
/*
列表
*/
var manager;
$(function () {
    loadCategoryList(); 
});

function loadCategoryList() { 
    if ($("#maingrid").length > 0) { 
        window['g'] =
            manager = $("#maingrid").ligerGrid({
                columns: [
                    { display: '编号', name: 'ID', id: 'id1', width: 30, type: 'int', align: 'left' },
                    { display: '分类名称', name: 'Name', align: 'left' },
                   // { display: '状态', name: 'Status', width: 35, align: 'center' }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '97%',
                allowHideColumn: false, rownumbers: true, checkbox: false, usePager: false,
                url: "/VipSoft/Category/CategoryListJson/" + mid + "/" + cid,
                alternatingRow: false, tree: { columnName: 'Name' }
            });
    }
}

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('请选择行'); return; }
    alert(JSON.stringify(row));

    alert(row.id);
}function getChecked() {
    var notes = manager.getSelectedRows();
    var text = "";
    for (var i = 0; i < notes.length; i++) {

        text += notes[i].id + ",";
    }
    alert('选择的节点数：' + text);
}

function addNote() {
    location.href = "/VipSoft/Category/Add/" + mid + "/" + cid ;
}function addChildNote() { 
    setChildNote("");
}

function editChildNote() { 
    setChildNote("?act=2");
}

function deleteNote(sender,id) { 
    if (id > 0) {
        layer.confirm('是否删除该数据！', function () {
            {
                var url = "/VipSoft/Category/Delete/" + mid + "/" + cid + "/" + id;
                $.post(url, function (data) {
                    if (data > 0) {
                        layer.msg('删除成功！');
                        $(sender).closest("tr").hide();
                    }
                });
                return false;
            } 
        }); 
    } else {
        layer.alert('请选择数据！');
    }
}

function setChildNote(act) {
    var row = manager.getSelectedRows();
    switch (row.length) {
        case 0: 
            layer.alert('请选择行！');
            break;
        case 1:
            location.href = "/VipSoft/Category/Add/" + mid + "/" + cid + "/" + row[0].ID + act;
            break;
        default:
            layer.alert('请不要选择多条数据进行操作！');
            break;
    }
}



/*
添加修改
*/

$(document).ready(function () {
    
    submit();
});

//Tab控制函数
function submit() { 

    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/Category/Save/?'+new Date().getMilliseconds(),  // override for form's 'action' attribute 
        //target:        '#output2',   // target element(s) to be updated with server response 
        // beforeSubmit: showRequest,  // pre-submit callback 
        type: 'post',    // 'get' or 'post', override for form's 'method' attribute 
        data: { mid: $('#mid').val(), CategoryId: $('#cid').val(), id: $('#id').val()},
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
        success: function () {
            layer.msg('保存成功！'); 
        }
    };
     

    // bind to the form's submit event 
    $('#frmCategory').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit     
        $(this).ajaxSubmit(options);

        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });
}
 


