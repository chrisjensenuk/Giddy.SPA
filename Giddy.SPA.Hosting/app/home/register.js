define(['knockout', 'module/server', 'plugins/router'], function (ko, server, router) {
    
    //knockout model
    var _model = ko.validatedObservable({
        userName: ko.observable(""),
        fullName: ko.observable(""),
        password: ko.observable(""),
        confirmPassword: ko.observable("")
    });

    //add validation
    _model().userName.extend({ required: true });
    _model().fullName.extend({ required: true });
    _model().password.extend({ required: true });
    _model().confirmPassword.extend({ required: true });

    //server error message
    var _serverError = ko.observable();

    var _register = function () {

        server.ajaxJson("/api/register", ko.toJS(_model), _model)
            .then(function () {
                //we have succesfully registered and logged in.
                server.isLoggedIn(true);
                router.navigate('');
            })
            .fail(function (err, model) {
                server.mapServerErrorToValidation(err, model, _serverError);
            });
    }

    return {
        model: _model,
        register: _register,
        serverError: _serverError
    };

});