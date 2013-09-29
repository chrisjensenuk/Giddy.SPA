define(['jquery', 'knockout'], function ($, ko) {

    //Are we logged in or not (Boolean) 
    var _isLoggedIn = ko.observable();

    //RequestVerificationToken - used for AntiForgery
    var _rvt = ko.observable();

    _isLoggedIn.subscribe(function (isLoggedIn) {
        //we are changing from anonymous session to an authenticated session or vice versa.  We will need a new Request Verification Token
        $.getJSON('api/rvt')
            .then(function (rvt) {
                _rvt(rvt);
            });
    });

    //Check user is authenticated on the server
    var _checkIsLoggedIn = function () {
        //check if the user is authorized
        $.get('/api/checkloggedin').then(function (isLoggedIn) {
            _isLoggedIn(isLoggedIn);
        });
    }

    //try logging in using the supplied username and passsword
    var _logIn = function (loginModel) {

        var dfd = new $.Deferred();

        _ajaxJson('api/login', ko.toJS(loginModel))
            .then(function () {
                _isLoggedIn(true);
                dfd.resolve();
            })
            .fail(function (err) {
                _isLoggedIn(false);
                dfd.reject();
            });

        return dfd.promise();
    }

    var _logOut = function () {
        _ajaxJson('api/logout')
        .then(function () {
            _isLoggedIn(false);
        });
    }


    //url = the location of the [WebMethod]
    //obj = the object to stringify and submit
    //done = the callback function to call after a successful post.  function is passed the returned data. 
    //return = a jQuery promise
    var _ajaxJson = function (url, obj) {

        // Create a new Deferred.
        var dfd = new $.Deferred();

        var json = JSON.stringify(obj);

        var request = $.ajax({
            type: "POST",
            url: url,
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                "__RequestVerificationToken": _rvt()
            }
        });

        request.done(function (data) {
            dfd.resolve(data);
        });

        request.fail(function (xhr, msg) {
            //xhr responseText contains the json ModelState message
            var err = JSON.parse(xhr.responseText);

            dfd.reject(err);
        });

        return dfd.promise();
    }

    return {
        ajaxJson: _ajaxJson,
        isLoggedIn: _isLoggedIn,
        checkIsLoggedIn: _checkIsLoggedIn,
        logIn: _logIn,
        logOut: _logOut
    }

});