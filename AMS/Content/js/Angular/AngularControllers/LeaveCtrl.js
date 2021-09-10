app.controller('LeaveCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, toaster) {
            console.log("Connected to Leave App");

            // ================================================== INIT INDEX ==========================================================
            $scope.initIndex = function () {
                console.log("inisde init index");
                var date = new Date();
                $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
                $scope.DateTo = new Date();
                $scope.searchedMonth = $scope.selectedMonth;
                $scope.AjaxPost("/api/LeaveApi/GetLeaveRecord", { a: $scope.DateTo}).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.LeavesList = response.data.LeaveRecordList;
                            console.log($scope.LeavesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Leave Record, try again!");
                        }
                    }
                );
            }
            // ================================================================ VARIABLES =========================================================

            $scope.labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30'];
            $scope.series = ['Time'];
            $scope.data = [
                [1, 2, 3, 4, 5, 6]
            ];
            var date = new Date();
            $scope.selectedMonth = date.getMonth();
            $scope.selectedYear = date.getFullYear();
            $scope.months = [{ id: 0, name: "January" }, { id: 1, name: "February" }, { id: 2, name: "March" }, { id: 3, name: "April" }, { id: 4, name: "May" }, { id: 5, name: "June" }, { id: 6, name: "July" }, { id: 7, name: "August" }, { id: 8, name: "September" }, { id: 9, name: "October" }, { id: 10, name: "November" }, { id: 11, name: "December" }];
            $scope.years = ["2021"];        //["2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021"];
        }
    ]
);