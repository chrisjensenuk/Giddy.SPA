define(['plugins/dialog', 'knockout', 'module/server', 'validation'], function (dialog, ko, server) {

    var _model = ko.validatedObservable({
        username: ko.observable(""),
        password: ko.observable(""),
        rememberMe: ko.observable(false)
    });

    //add validation
    _model().username.extend({ required: true });
    _model().password.extend({ required: true });

    //The name of the property in the server ModelState
    _model().username.modelStateProperty = 'model.UserName';
    _model().password.modelStateProperty = 'model.Password';

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
        serverError: ko.observable()
    }
});