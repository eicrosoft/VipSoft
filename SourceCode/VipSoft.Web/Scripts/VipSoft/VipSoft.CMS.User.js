
//$('#frmUser').submit(function () {
//    // inside event callbacks 'this' is the DOM element so we first 
//    // wrap it in a jQuery object and then invoke ajaxSubmit  
//    url_path = "/User/Register";
//    $(this).ajaxSubmit(options);

//    // !!! Important !!! 
//    // always return false to prevent standard browser submit and page navigation 
//    return false;
//});

$(document).ready(function () {
    register();
});
//Tab控制函数
function register() {
    // prepare Options Object 
    var options = {
        // other available options:             
        url: '/User/Register/?' + new Date().getMilliseconds(),  // override for form's 'action' attribute 
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
                lock: true,
                icon: 'succeed',
                content: '添加成功',
                ok: function () {
                    this.title('消息提示').time(3);
                    return false;
                }
            });
            $("input[type='text']").val("");
            $("textarea").val("");
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