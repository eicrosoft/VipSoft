/// <reference path="jquery-1.7.1.js" />
/// <reference path="jquery.form.js" />
/// <reference path="~/Scripts/layer/layer.js" />

var editor;
$(document).ready(function () {
    
    submit();
});
//Tab控制函数
function submit() { 
    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/User/SaveUser/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
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
    $('#frmUser').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit 
       
        $(this).ajaxSubmit(options); 
        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });
}

function successed() { 
    layer.alert('删除成功！');
}

 
function deleteUser(sender, id) {
    if (id > 0) {
        layer.confirm('是否删除该数据！', function () {
            {
                var url = "/VipSoft/User/Delete/" + mid + "/" + cid + "/" + id;
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
