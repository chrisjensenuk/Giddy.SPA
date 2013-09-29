require.config({
    baseUrl: '/Scripts',
    paths: {
        jquery: 'jquery-1.8.2.min',
        infuser : 'infuser-amd',
        TrafficCop : 'TrafficCop-amd',
        ko : 'knockout-2.3.0.debug',
        koext: 'koExternalTemplateEngine-amd',
        app: 'app/application',
        vm: 'viewmodel'
    }
});

require(['app', 'vm/Toolbar'], function (app, ToolbarViewModel) {
    var vm = new ToolbarViewModel();
    app.apply($(document.body), vm);
});

