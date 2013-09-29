define(['ko'], function(ko){
	return function ToolbarViewModel() {

		this.name = ko.observable('toolbar');

		this.template = 'toolbar';
		
	};
});