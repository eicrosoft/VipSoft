/// <reference path="jquery-1.7.1.js" />
/// <reference path="jquery.form.js" />
/// <reference path="~/Scripts/layer/layer.js" />
var editor;
$(document).ready(function () {    
    submit();
    roleAccess();
});
//Tab控制函数
function submit() { 
    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/Role/SaveRole/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
        //target:        '#output2',   // target element(s) to be updated with server response 
        // beforeSubmit: showRequest,  // pre-submit callback 
        type: 'post',    // 'get' or 'post', override for form's 'method' attribute 
        //data: {},
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
        success: function () {
            layer.alert('保存成功！');
        }
    };
     

    // bind to the form's submit event 
    $('#frmRole').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit 
       
        $(this).ajaxSubmit(options); 
        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });
     
}

function deleteRole(sender, id) {
    if (id > 0) {
        layer.confirm('是否删除该数据！', function () {
            {
                var url = "/VipSoft/Role/Delete/" + mid + "/" + cid + "/" + id;
                console.log(url);
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




//Tab控制函数
function roleAccess() {
    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/Role/SaveRoleAccess/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
        //target:        '#output2',   // target element(s) to be updated with server response 
        // beforeSubmit: showRequest,  // pre-submit callback 
        type: 'post',    // 'get' or 'post', override for form's 'method' attribute 
        //data: {},
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
        success: function () {
            layer.alert('保存成功！');
        }
    };
     
    $('#frmRoleAccess').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit 

        $(this).ajaxSubmit(options);
        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });
}


function successed() {
    layer.alert('保存成功！');
}


