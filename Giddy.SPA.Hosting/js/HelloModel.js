(function (app) {

    //constructor function
    var HelloModel = function () {

        var self = this;

        self.name = ko.observable("Yo!")
            .extend({
                required: true,
                minLength: 3
            });

        self.fooName = ko.observable("");
    }

    app.HelloModel = HelloModel;

}(window.APP));