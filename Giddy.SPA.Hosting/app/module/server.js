define(['jquery', 'knockout', 'plugins/router'], function ($, ko, router) {

    //Are we logged in or not (Boolean) 
    var _isLoggedIn = ko.observable();

    //RequestVerificationToken - used for AntiForgery
    var _rvt = ko.observable();

    _isLoggedIn.subscribe(function (isLoggedIn) {
        //we are changing from anonymous session to an authenticated session or vice versa.  We will need a new Request Verification Token and the user routes
        $.when($.getJSON('api/rvt'),
            $.getJSON('api/userroutes'))
        .done(function (rvt, routes) {
            //Set the request verification token
            _rvt(rvt[0]);

            //set the routes
            router.routes = [];
            router.map(routes[0]).buildNavigationModel();
            return router.activate();
        });
    });

    //Check user is authenticated on the server
    var _checkIsLoggedIn = function () {
        //check if the user is authorized. returns ajax promise
        return $.get('/api/checkloggedin')
            .then(function (isLoggedIn) {
                _isLoggedIn(isLoggedIn);
            });
    }

    //try logging in using the supplied username and passsword
    var _logIn = function (loginModel) {

        var dfd = new $.Deferred();

        _ajaxJson('api/login', ko.toJS(loginModel), loginModel)
            .then(function (routerConfig) {
                _isLoggedIn(true);
                dfd.resolve();
            })
            .fail(function (err, loginModel) {
                _isLoggedIn(false);
                dfd.reject(err, loginModel);
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
    //context = the context. This object will be passed through to the promise then, fail, etc functions
    //return = a jQuery promise
    var _ajaxJson = function (url, obj, context) {

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

        if (context != null) request.context = context;

        request.done(function (data) {
            dfd.resolve(data, context);
        });

        request.fail(function (xhr, msg) {
            //xhr responseText contains the json ModelState message
            var err = JSON.parse(xhr.responseText);

            dfd.reject(err, context);
        });

        return dfd.promise();
    }

    //Err = error sent from the server (a string or the ModelState object)
    //model = the observable model what was sent to the server
    //obsServerMessage = the observable to write the server error message to.
    var _mapServerErrorToValidation = function (err, model, obsServerMessage) {

        if (typeof err == 'string') {
            obsServerMessage(err);
            return;
        }

        obsServerMessage(err.Message);

        var m = model();

        //for each observable in the model see if a modelStateProperty has been defined
        for (var obs in m) {
            var observable = m[obs];
            var errors = err.errors[obs];

            if (errors !== undefined) {
                //manually set the knockout validation error
                observable.setError(errors[0]);
            }
            
        }
    }

    return {
        ajaxJson: _ajaxJson,
        isLoggedIn: _isLoggedIn,
        checkIsLoggedIn: _checkIsLoggedIn,
        logIn: _logIn,
        logOut: _logOut,
        mapServerErrorToValidation: _mapServerErrorToValidation
    }

});