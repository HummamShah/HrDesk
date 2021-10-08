app.controller('PayCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "Upload",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, Upload, toaster) {
            console.log("Connected to Pay App");

            // ====================================================== INIT INDEX ============================================================
            $scope.initIndex = function () {
                $scope.GetPayListing();
            }

            // ====================================================== PAY SLIP INIT ============================================================
            $scope.paySlipInit = function () {
                console.log("inside payslip init");
                var Id = $scope.GetUrlParameter("Id");
                console.log(Id);
                $scope.AjaxPost("/api/PayApi/GeneratePaySlip", {'Id' : Id}).then(
                    function (response) {
                        console.log(response);
                        $scope.PaySlip = response.data;
                        console.log($scope.PaySlip);
                    }
                );
            }

            // ====================================================== GET PAY LISTING ============================================================
            $scope.GetPayListing = function () {
                $scope.AjaxGet("/api/PayApi/GetPayRollList", null).then(
                    function (response) {
                        console.log(response);
                        $scope.PayRollList = response.data.PayRollList;
                        console.log($scope.PayRollList);
                    }
                );
            }

            // ===================================================== GENERATE PAY SLIP ==============================================================
            $scope.GeneratePaySlip = function (AgentPayDetail) {
                console.log(AgentPayDetail);
                $scope.AjaxGet("/api/PayApi/GeneratePaySlip", null).then(
                    function (response) {
                        console.log(response);
                        $scope.PaySlip = response.data;
                        console.log($scope.PaySlip);
                    }
                );
            }
        }
    ]
);