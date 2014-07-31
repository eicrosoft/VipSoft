/// <reference path="../jquery.min.js" />
/// <reference path="../jquery.form.js" /> 
/// <reference path="~/Scripts/layer/layer.js" />

var editor;
$(document).ready(function () {
    try { 
      editor = UE.getEditor('UEContent');
    }catch(e)
    {
    }
    submit(); 
});
//Tab控制函数
function submit() {
    //设置点击后的切换样式
    // $("#btnSave").click(function (event) {
    //     alert("This ");
    //     $.ajax({
    //         type: "POST",
    //         url: "/Admin/Article/Save/",
    //         dataType: "json",
    //         success: function (data) {
    //             alert(data);
    //             alert("This user has be deleted");
    //         },
    //         error: function (data) {
    //             alert(data);
    //             alert("This user cannot be deleted");
    //         }
    //     });
    // }
    //);

    //$('#btnSave').click(function () {
    //    alert("Thank you for your comment!");
    //});


    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/Article/Save/?'+new Date().getMilliseconds(),  // override for form's 'action' attribute 
        //target:        '#output2',   // target element(s) to be updated with server response 
        // beforeSubmit: showRequest,  // pre-submit callback 
        type: 'post',    // 'get' or 'post', override for form's 'method' attribute 
        data: { mid: $('#mid').val(), CategoryId: $('#cid').val(), id: $('#id').val() },
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
        success: function () {
            layer.msg('保存成功！');
        }
    };
    
    if ($('#frmArticle').length > 0) {
        // bind to the form's submit event 
        $('#frmArticle').submit(function() {
            // inside event callbacks 'this' is the DOM element so we first 
            // wrap it in a jQuery object and then invoke ajaxSubmit 
            
            editor.sync();
            $(this).ajaxSubmit(options);

            // !!! Important !!! 
            // always return false to prevent standard browser submit and page navigation 
            return false;
        });
    }
}


function search() {

    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/VipSoft/Article/List/' + $('#mid').val() + '/' + $('#cid').val() + '/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
        //target:        '#output2',   // target element(s) to be updated with server response 
        // beforeSubmit: showRequest,  // pre-submit callback 
        type: 'post',    // 'get' or 'post', override for form's 'method' attribute 
        //data: { mid: $('#mid').val(), CategoryId: $('#cid').val(), id: $('#id').val() },
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
        //success: function () {
        //    layer.msg('保存成功！'); 
        //}
    }; 
    if ($('#frmArticleList').length > 0) {
        
        // bind to the form's submit event 
        $('#frmArticleList').submit(function () {
            // inside event callbacks 'this' is the DOM element so we first 
            // wrap it in a jQuery object and then invoke ajaxSubmit 
            editor.sync();
            $(this).ajaxSubmit(options);

            // !!! Important !!! 
            // always return false to prevent standard browser submit and page navigation 
            return false;
        });
    } 
}

function successed() {
    alert("删除成功！");
}

function deleteArticle(sender, id) {
    if (id > 0) {
        layer.confirm('是否删除该数据！', function () {
            {
                var url = "/VipSoft/Article/Delete/" + mid + "/" + cid + "/" + id;
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
