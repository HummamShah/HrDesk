app.controller('TaxCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Tax App");

            // ================================================== INIT INDEX ==========================================================
            $scope.initIndex = function () {
                console.log("inisde init index");
                $scope.GetTaxList();
            }

            // ================================================== ADD INIT INDEX ==========================================================
            $scope.addInit = function () {
                console.log("inisde add init index");
                $scope.Tax = {};
            }

            // ================================================== EDIT INIT INDEX ==========================================================
            $scope.editInit = function () {
                console.log("inisde edit init index");
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/TaxApi/GetTax", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Tax = response.data;
                    }
                );
            }

            // =================================================== GET TAX LIST =========================================================
            $scope.GetTaxList = function () {
                $scope.AjaxGet("/api/TaxApi/GetTaxList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.TaxesList = response.data.TaxesList;
                            console.log($scope.TaxesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Tax List, try again!");
                        }
                    }
                );
            }

            // =================================================== ADD TAX =========================================================
            $scope.AddTax = function () {
                if (!$scope.Tax.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Tax.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Tax.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/TaxApi/AddTax", $scope.Tax).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Tax Added Successfully!");
                            $timeout(function () { window.location.href = '/Tax'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Tax, try again!");
                        }
                    }
                );
            }

            // =================================================== EDIT TAX =========================================================
            $scope.EditTax = function () {
                if (!$scope.Tax.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Tax.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Tax.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/TaxApi/EditTax", $scope.Tax).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Tax Edited Successfully!");
                            $timeout(function () { window.location.href = '/Tax'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Edit Tax, try again!");
                        }
                    }
                );
            }
        }
    ]
);