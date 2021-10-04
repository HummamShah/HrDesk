app.controller('IntensiveCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Intensive App");

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
                $scope.AjaxGet("/api/IntensiveApi/GetIntensive", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Intensive = response.data;
                    }
                );
            }

            // =================================================== GET INTENSIVE LIST =========================================================
            $scope.GetIntensiveList = function () {
                $scope.AjaxGet("/api/IntensiveApi/GetIntensiveList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.IntensivesList = response.data.IntensivesList;
                            console.log($scope.IntensivesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Tax List, try again!");
                        }
                    }
                );
            }

            // =================================================== ADD INTENSIVE =========================================================
            $scope.AddIntensive = function () {
                if (!$scope.Intensive.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Intensive.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Intensive.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/IntensiveApi/AddIntensive", $scope.Intensive).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Intensive Added Successfully!");
                            $timeout(function () { window.location.href = '/Insentive'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Intensive, try again!");
                        }
                    }
                );
            }

            // =================================================== EDIT INTENSIVE =========================================================
            $scope.EditIntensive = function () {
                if (!$scope.Intensive.Name) {
                    toaster.pop('error', "error", "Please fill the NAME filed!");
                    return;
                }
                if (!$scope.Intensive.Type) {
                    toaster.pop('error', "error", "Please fill the TYPE filed!");
                    return;
                }
                if (!$scope.Intensive.Amount) {
                    toaster.pop('error', "error", "Please fill the AMOUNT filed!");
                    return;
                }

                $scope.AjaxPost("/api/IntensiveApi/EditIntensive", $scope.Intensive).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            toaster.pop('success', "success", "Intensive Edited Successfully!");
                            $timeout(function () { window.location.href = '/Insentive'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Edit Intensive, try again!");
                        }
                    }
                );
            }
        }
    ]
);