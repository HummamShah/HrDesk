app.controller('DeductionCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Deduction App");

            // ================================================== INIT INDEX ==========================================================
            $scope.initIndex = function () {
                console.log("inisde init index");
                $scope.GetDeductionList();
            }

            // ================================================== ADD INIT INDEX ==========================================================
            $scope.addInit = function () {
                console.log("inisde add init index");
                $scope.Deduction = {};
                $scope.Deduction.Type = "Percentage";
            }

            // ================================================== EDIT INIT INDEX ==========================================================
            $scope.editInit = function () {
                console.log("inisde edit init index");
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/DeductionApi/GetDeduction", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Deduction = response.data;
                    }
                );
            }

            // =================================================== GET DEDUCTION LIST =========================================================
            $scope.GetDeductionList = function () {
                $scope.AjaxGet("/api/DeductionApi/GetDeductionList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.DeductionsList = response.data.DeductionsList;
                            console.log($scope.DeductionsList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Deduction List, try again!");
                        }
                    }
                );
            }

            // =================================================== ADD DEDUCTION =========================================================
            $scope.AddDeduction = function () {
                if (!$scope.Deduction.Name) {
                    toaster.pop('error', "error", "Please fill the NAME field!");
                    return;
                }
                if (!$scope.Deduction.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE field!");
                    return;
                }
                if (!$scope.Deduction.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT field!");
                    return;
                }

                $scope.AjaxPost("/api/DeductionApi/AddDeduction", $scope.Deduction).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Deduction Added Successfully!");
                            $timeout(function () { window.location.href = '/Deduction'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Deduction, try again!");
                        }
                    }
                );
            }

            // =================================================== EDIT DEDUCTION =========================================================
            $scope.EditDeduction = function () {
                if (!$scope.Deduction.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Deduction.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Deduction.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/DeductionApi/EditDeduction", $scope.Deduction).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Deduction Edited Successfully!");
                            $timeout(function () { window.location.href = '/Deduction'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Edit Deduction, try again!");
                        }
                    }
                );
            }
        }
    ]
);