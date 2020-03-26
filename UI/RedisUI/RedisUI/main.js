var ViewModel = function() {
    self = this;

    self.items = ko.observableArray([]);

    $.getJSON("https://localhost:44317/api/Values", function(allData) {
        var mappedTasks = $.map(allData, function(item) { return item });
        self.items(mappedTasks);
    });  

    $.ajax({
        url: "https://localhost:44317/api/Values",
     
        // The name of the callback parameter, as specified by the YQL service
        jsonp: "callback",
     
        // Tell jQuery we're expecting JSONP
        dataType: "jsonp",
     
        // Work with the response
        success: function( allData ) {
            var mappedTasks = $.map(allData, function(item) { return item });
            self.items(mappedTasks);
        }
    });
};
 
ko.applyBindings(new ViewModel()); // This makes Knockout get to work