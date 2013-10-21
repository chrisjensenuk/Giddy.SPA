define(['plugins/dialog', 'knockout', 'module/server', 'validation'], function (dialog, ko, server) {

    var _model = ko.validatedObservable({
        userName: ko.observable(""),
        password: ko.observable(""),
        rememberMe: ko.observable(false)
    });

    //add validation
    _model().userName.extend({ required: true });
    _model().password.extend({ required: true });

    //server error message
    var _serverError = ko.observable();

    var _login = function (dialogResult) {
        var self = this;

        //clear any server errors
        self.serverError(null);

        server.logIn(_model)
            .done(function () {
                self.close();
            })
            .fail(function (err, model) {
                server.mapServerErrorToValidation(err, model, self.serverError);
            });
        ;
    }


    var _close = function () {
        dialog.close(this, this.model);
    }

    var _show = function () {
        return dialog.show(this);
    }

    return {
        model: _model,
        login: _login,
        show: _show,
        close: _close,
        serverError: _serverError
    }
});