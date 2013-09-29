(function (app) {

    //constructor function
    var HelloViewModel = function () {

        var vm = this;
        vm.helloModel = new APP.HelloModel();
        
        //subscribe to the changing event of name.
        vm.helloModel.name.subscribe(function (name) {

            var model = this;
            
            //check if the model is valid (e.g. greater than 3 chars)
            if (model.name.isValid()) {
                
                //add Foo to the text via ajax
                app.ajaxJson('/api/addfoo', name, function (n) {
                    //on succcess update the fooName
                    model.fooName(n);
                });
            }
        }, vm.helloModel);
    }

    app.HelloViewModel = HelloViewModel;

}(window.APP));