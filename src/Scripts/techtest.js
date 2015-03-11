(function ($) {
    $(function () {
        JS.require('JS.Class', function (Class) {
            var UserDetail = new Class({
                initialize: function (data, label) {
                    var self = this;
                    var key = 'is' + label;
                    self.label = label;
                    self.data = data;
                    self.value = ko.observable(data[key]);
                    self.value.subscribe(function (value) {
                        self.data[key] = value;
                    });

                    self.toggle = function () {
                        var newValue = self.value() ? false : true;
                        self.value(newValue);
                    };
                }
            });

            var Colour = new Class({
                initialize: function (colour, parent, isFavourite) {
                    var self = this;
                    self.colour = colour;
                    self.parent = parent;
                    self.isFavourite = ko.observable(isFavourite);
                    self.isFavourite.subscribe(function (selected) {
                        if (selected) {
                            self.parent.colours.push(self.colour);
                        } else {
                            var matches = _.findWhere(self.parent.colours, { colourId: self.colour.colourId });
                            self.parent.colours = _.without(self.parent.colours, matches);
                        }
                    });

                    self.toggle = function () {
                        var newValue = self.isFavourite() ? false : true;
                        self.isFavourite(newValue);
                    }
                }
            });

            var Person = new Class({
                initialize: function (data) {
                    var self = this;
                    self.data = data;
                    self.details = ko.observableArray();
                    self.colours = ko.observableArray();

                    self.save = function (callback) {
                        $.ajax({
                            url: '/api/people/' + self.data.personId,
                            method: 'PUT',
                            data: self.data,
                            success: function () {
                                if (callback) callback();
                            }
                        });
                    };

                    self._setData(data, self);
                },

                _setData: function (data, self) {

                    self.details([
                        new UserDetail(data, 'Authorised'),
                        new UserDetail(data, 'Enabled')
                    ]);

                    _.each(viewModel.colours, function (x) {
                        var isFavourite = _.some(data.colours, function (y) {
                            return x.colourId == y.colourId;
                        });

                        var colour = new Colour(x, data, isFavourite);
                        self.colours.push(colour);
                    });
                }
            });

            var Page = new Class({
                initialize: function (parent) {
                    var self = this;
                    self.parent = parent;

                    self.isVisible = ko.computed(function () {
                        if (!self.parent.activePage()) return false;
                        return self.parent.activePage().id == self.id;
                    });


                    self.activate = function (model) {
                        self.parent.activePage(self);
                        if (self.onActivate) self.onActivate(model);
                    };
                }
            });

            var PeoplePage = new Class(Page, {
                initialize: function (parent) {
                    var self = this;
                    self.callSuper(parent);
                    self.id = 'peoplePage';
                    self.title = ko.observable('People');
                    self.list = ko.observable();
                    self.onActivate = function () {
                        $.get('/api/people', function (data) {
                            self.list(data);
                        });
                    };
                }
            });

            var UpdatePage = new Class(Page, {
                initialize: function (parent) {
                    var self = this;
                    self.callSuper(parent);
                    self.id = 'updatePage';
                    self.title = ko.observable('');
                    self.person = ko.observable({});
                    self.onActivate = function (model) {
                        var self = this;
                        $.get(model.href, function (data) {
                            self.person(new Person(data));
                            self.title('Update' + " " + data.name);
                        });
                    };

                    self.cancel = function () {
                        self.reset();
                    };

                    self.save = function () {
                        self.person().save(function () {
                            self.reset();
                        });
                    };

                    self.reset = function () {
                        self.person({});
                        self.title('');
                        self.parent.people.activate();
                    };
                }
            });

            var Pages = new Class({
                initialize: function (parent) {
                    var self = this;
                    self.parent = parent
                    self.activePage = ko.observable();
                    self.people = new PeoplePage(self);
                    self.update = new UpdatePage(self);
                }
            });

            var ViewModel = new Class({
                initialize: function () {
                    var self = this;
                    self.pages = new Pages(self);
                    self.pages.people.activate();
                    $.get('/api/colours', function (data) {
                        self.colours = data;
                    });
                },
            });

            var viewModel = new ViewModel();
            ko.applyBindings(viewModel);
        });
    });
})(jQuery);