define(['jquery', 'infuser', 'ko', 'koext'], function ($, infuser, ko, koext) {

    infuser.defaults.templateUrl = "Scripts/templates";

    var apply = function ($element, vm) {

        var $containerlesstemplate = $("<!-- ko template: { name: template } --><!-- /ko -->");
        $element.append($containerlesstemplate);

        ko.applyBindings(vm, $containerlesstemplate[0]);
    }

    return {
        apply : apply
    }
});