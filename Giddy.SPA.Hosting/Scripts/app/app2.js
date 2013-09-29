window.APP = {};

(function(app) {
    app.showLogin = function () {
        alert("show login");
    }
}(window.APP));


//add ajax json helper
(function(app) {

    //url = the location of the [WebMethod]
    //obj = the object to stringify and submit
    //done = the callback function to call after a successful post.  function is passed the returned data. 
    //passthrough = [optional] is an object to remember on the client passed through as the second parameter to the callback
    //fail = [optional] the function to call on fail. function is passed the message and the passthrough object
    //always = [optional] called when request terminates. function is passed the passthrough object
    var ajaxJson = function (url, obj, done, passthrough, fail, always) {
 
        var json = JSON.stringify(obj);
 
        var request = $.ajax({
            type: "POST",
            url: url,
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
 
        request.done(function (data) {
            done(data, passthrough);
        });
 
        request.fail(function (xhr, msg) {
            if (xhr.status == 401) {
                //We are not logged in or session has expired
                app.showLogin();
                return;
            }
            if (fail !== undefined) {
                fail(msg, passthrough);
            }
            else {
                console.log("fail: " + msg);
            }
        });

        request.always(function (xhr, msg) {
            if (always !== undefined) {
                always(msg, passthrough);
            }
            else {
                console.log("always: " + msg);
            }
        });
    }

    app.ajaxJson = ajaxJson;
}(window.APP));