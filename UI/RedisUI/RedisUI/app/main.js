var ViewModel = function() {
    self = this;

    self.movies = ko.observableArray([]);

    $.ajax({
        url: "https://localhost:44317/api/Movies",
        crossDomain: true,
        dataType: 'jsonp',
        method: "GET",

        success: function (data) { console.log(data); },
        error: function (jqXHR, textStatus, errorThrown) { console.log(errorThrown); console.log(textStatus); }
    });
};
 
ko.applyBindings(new ViewModel()); // This makes Knockout get to work