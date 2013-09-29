//http://www.knockmeout.net/2012/08/thatconference-2012-session.html

//if you want the user to 'accept changes' then you need to save the initial and current values of model. This will pollute the model with temporary data. So creating an extension to handle this

//extending ko to add editable functionality
(function (ko) {
    ko.observable.fn.editable = function () {
        this.editValue = ko.observable(this());
        this.accept = function () {
            this(this.editValue());
        }.bind(this);

        return this;
    }
}(ko));

(function (app) {

    var EditableViewModel = function () {
        this.name = ko.observable("");

        this.name.editable();
    };

    app.EditableViewModel = EditableViewModel;
}(window.APP));
