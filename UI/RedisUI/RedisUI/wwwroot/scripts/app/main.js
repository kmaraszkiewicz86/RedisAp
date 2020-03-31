var ViewModel = function() {
    self = this;

    self.showForm = ko.observable(false);

    self.details = ko.observable();

    self.movies = ko.observableArray([]);

    self.goToDetails = function(movie) {
        location.hash = '#Deatils/' + movie.id;
    }

    self.submitForm = function() {

    }

    Sammy(function () {
        

        this.get('#GetAll', function () {

            self.showForm(false);

            $.ajax({
                url: 'https://localhost:44307/api/Movies/All',
                dataType: 'json',
                type: 'POST',
                crossDomain: true,
                success: function (response) {
                    self.movies(response);
                },
                error: function (xhr, status, error) {
                    console.log(status + '; ' + error);
                }
            });
        });

        this.get('#Deatils/:id', function () {

            self.showForm(false);

            $.ajax({
                url: 'https://localhost:44307/api/Movies/GetDetails/' + this.params.id,
                dataType: 'json',
                type: 'POST',
                crossDomain: true,
                success: function (response) {
                    self.details(response);
                },
                error: function (xhr, status, error) {
                    console.log(status + '; ' + error);
                }
            });
        });

        this.get('#SendPost',
            function() {
                self.showForm(true);
            });

        this.get('', function () { this.app.runRoute('get', '#GetAll') });
    }).run();
}

ko.applyBindings(new ViewModel());