/// <reference path="jquery-1.7.1.js" />
/// <reference path="jquery.form.js" />

var editor;
$(document).ready(function () { 
    submit();
});
//Tab控制函数
function submit() { 
    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/FeedBack/Save/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
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
            
            art.dialog({
                title: '消息提示',
                content: '保存成功'
            });
            $("input[type='text']").val("");
            $("textarea").val("");
        }
    };
     

    // bind to the form's submit event 
    $('#frmFeedBack').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit  

        $(this).ajaxSubmit(options);

        
        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });
}

 
