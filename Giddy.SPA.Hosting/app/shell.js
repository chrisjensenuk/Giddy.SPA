define(['plugins/router', 'knockout', 'durandal/app', 'module/server', 'login/login'], function (router, ko, app, server) {

    var _activate = function () {
        var self = this;

        //check if we are loggged in
        server.checkIsLoggedIn();

        return router.map([
            { route: [''], moduleId: 'home/index', title: 'Home', nav: true }
        ]).buildNavigationModel()
          .activate();
    }

    var _login = function (loginModel) {

        var loginBox = require('login/login');
        loginBox.show();
    }

    var _logout = function () {
        server.logOut();
    }

    return {
        router: router,
        isLoggedIn: server.isLoggedIn,
        login: _login,
        logout: _logout,
        activate: _activate
    }
});