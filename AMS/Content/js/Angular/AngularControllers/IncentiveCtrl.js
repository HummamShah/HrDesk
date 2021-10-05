app.controller('IncentiveCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Incentive App");

            // ================================================== INIT INDEX ==========================================================
            $scope.initIndex = function () {
                console.log("inisde init index");
                $scope.GetIntensiveList();
            }

            // ================================================== ADD INIT INDEX ==========================================================
            $scope.addInit = function () {
                console.log("inisde add init index");
                $scope.Intensive = {};
            }

            // ================================================== EDIT INIT INDEX ==========================================================
            $scope.editInit = function () {
                console.log("inisde edit init index");
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/IncentiveApi/GetIncentive", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Incentive = response.data;
                    }
                );
            }

            // =================================================== GET INCENTIVE LIST =========================================================
            $scope.GetIncentiveList = function () {
                $scope.AjaxGet("/api/IncentiveApi/GetIncentiveList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.IncentivesList = response.data.IncentivesList;
                            console.log($scope.IncentivesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Incentive List, try again!");
                        }
                    }
                );
            }

            // =================================================== ADD INCENTIVE =========================================================
            $scope.AddIncentive = function () {
                if (!$scope.Incentive.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Incentive.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Incentive.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/IncentiveApi/AddIncentive", $scope.Incentive).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Incentive Added Successfully!");
                            $timeout(function () { window.location.href = '/Incentive'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Incentive, try again!");
                        }
                    }
                );
            }

            // =================================================== EDIT INCENTIVE =========================================================
            $scope.EditIncentive = function () {
                if (!$scope.Incentive.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Incentive.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Incentive.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/IncentiveApi/EditIncentive", $scope.Incentive).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Incentive Edited Successfully!");
                            $timeout(function () { window.location.href = '/Incentive'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Edit Incentive, try again!");
                        }
                    }
                );
            }
        }
    ]
);