define(['plugins/dialog', 'knockout', 'module/server', 'validation'], function (dialog, ko, server) {

    var _model = ko.validatedObservable({
        username: ko.observable("").extend({ required: true}),
        password: ko.observable("").extend({ required: true }),
        rememberMe: ko.observable(false)
    });

    var _login = function (dialogResult) {
        var self = this;

        server.logIn(_model)
            .done(function () {
                alert("close");
                self.close();
            });
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
    }
});