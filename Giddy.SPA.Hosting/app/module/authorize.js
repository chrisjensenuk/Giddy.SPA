define(['module/server', 'knockout', 'knockout.validation', ], function (server, ko, validation) {

    var _login = function (loginModel) {
        return server.ajaxJson('api/login', ko.toJS(loginModel))
    }

    var _logout = function () {
        return server.ajaxJson('api/logout')
    }

    return {
        login: _login,
        logout: _logout
    }

});