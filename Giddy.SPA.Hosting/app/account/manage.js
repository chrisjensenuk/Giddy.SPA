define(['knockout', 'module/server', 'plugins/router'], function (ko, server, router) {

    //knockout model
    var _model = ko.validatedObservable({
        oldPassword: ko.observable(""),
        newPassword: ko.observable(""),
        confirmPassword: ko.observable("")
    });

    //add validation
    _model().oldPassword.extend({ required: true });
    _model().newPassword.extend({ required: true });
    _model().confirmPassword.extend({ required: true });

    //server error message
    var _serverError = ko.observable();

    var _manage = function () {

        server.ajaxJson("/api/manage", ko.toJS(_model), _model)
            .then(function () {
                router.navigate('');
            })
            .fail(function (err, model) {
                server.mapServerErrorToValidation(err, model, _serverError);
            });
    }

    return {
        model: _model,
        manage: _manage,
        serverError: _serverError
    };

});