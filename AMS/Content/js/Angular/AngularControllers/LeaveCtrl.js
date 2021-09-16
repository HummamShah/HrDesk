app.controller('LeaveCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",


        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Leave App");

            // ================================================== INIT INDEX ==========================================================
            $scope.initIndex = function () {
                console.log("inisde init index");
                var date = new Date();
                $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
                $scope.DateTo = new Date();
                $scope.searchedMonth = $scope.selectedMonth;
                /*$scope.getLeaveRecord();*/
                $scope.getLeaveSummary();
            }

            // =================================================== LEAVE SUMMARY =========================================================
            $scope.getLeaveSummary = function () {
                $scope.AjaxPost("/api/LeaveApi/GetSummary", { Month: $scope.selectedMonth, Year: $scope.selectedYear }).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.Summary = response.data;
                            $scope.LeavesList = response.data.LeaveRecordList;
                            $scope.searchedMonth = $scope.selectedMonth;
                            console.log($scope.LeavesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Leave Summary, try again!");
                        }
                    }
                );
            }

            // ====================================================== LEAVE RECORD =========================================================
            $scope.getLeaveRecord = function () {
                $scope.AjaxPost("/api/LeaveApi/GetLeaveRecord", { Month: $scope.selectedMonth, Year: $scope.selectedYear }).then(
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

            // ====================================================== GET PENDING LEAVE REQUESTS (HR) =========================================================
            $scope.getPendingLeaves = function () {
                $scope.AjaxGet("/api/LeaveApi/GetPendingLeaves", {}).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.PendingLeavesList = response.data.PendingLeavesList;
                            console.log($scope.PendingLeavesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Pending Leave Requests, try again!");
                        }
                    }
                );
            }

            // ====================================================== ACCEPT LEAVE (HR) =========================================================
            $scope.acceptLeave = function (Leave) {
                $scope.AjaxPost("/api/LeaveApi/AccpetLeave", { LeaveId: Leave.Id, DaysCount: Leave.DaysCount }).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.getPendingLeaves();
                        } else {
                            toaster.pop('error', "error", "Could Not Proceed Accpet Leave, try again!");
                        }
                    }
                );
            }

            // ====================================================== REJECT LEAVE (HR) =========================================================
            $scope.rejectLeave = function (Leave) {
                $scope.AjaxPost("/api/LeaveApi/RejectLeave", { LeaveId: Leave.Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.getPendingLeaves();
                        } else {
                            toaster.pop('error', "error", "Could Not Proceed Reject Leave, try again!");
                        }
                    }
                );
            }

            // ===================================================== APPLY LEAVE =========================================================
            $scope.applyLeave = function () {
                //console.log($scope.Leave.LeaveFrom < new Date().today);
                //var a = new Date();
                //console.log(a);
                //console.log($scope.Leave);
                
                if ($scope.Leave.LeaveFrom == null) {
                    toaster.pop('error', "error", "Please choose date");
                    return;
                }
                if ($scope.Leave.LeaveFrom < new Date()) {
                    toaster.pop('error', "error", "You can't apply for passed dates");
                    return;
                }
                if ($scope.Leave.singleMultiple == 'single') {
                    $scope.Leave.LeaveTo = $scope.Leave.LeaveFrom;
                }
                else if ($scope.Leave.singleMultiple == 'multiple') {
                    if ($scope.Leave.LeaveFrom == null) {
                        toaster.pop('error', "error", "Please choose all dates");
                        return;
                    }
                    if ($scope.Leave.LeaveFrom >= $scope.Leave.LeaveTo) {
                        toaster.pop('error', "error", "Please choose correct dates ");
                        return;
                    }
                }
                console.log($scope.Leave.LeaveFrom.getDay());
                if ($scope.Leave.LeaveFrom.getDay() == 0 || $scope.Leave.LeaveFrom.getDay() == 6 || $scope.Leave.LeaveTo.getDay() == 0 || $scope.Leave.LeaveTo.getDay() == 6)  {
                    toaster.pop('error', "error", "Please choose weekdays");
                    return;
                }

                $scope.LeaveTemp = {};
                $scope.LeaveTemp.Type = $scope.Leave.Type;
                $scope.LeaveTemp.Reason = $scope.Leave.Reason;
                $scope.LeaveTemp.LeaveFrom = $scope.GetDatePostFormat($scope.Leave.LeaveFrom);
                $scope.LeaveTemp.LeaveTo = $scope.GetDatePostFormat($scope.Leave.LeaveTo);
                //$scope.Leave.LeaveFrom = $scope.GetDatePostFormat($scope.Leave.LeaveFrom);
                //$scope.Leave.LeaveTo = $scope.GetDatePostFormat($scope.Leave.LeaveTo);
                $scope.AjaxPost("/api/LeaveApi/ApplyLeave", $scope.LeaveTemp).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            if (response.data.IsSuccessful) {
                                toaster.pop('success', "success", "HR will check your request soon!");
                                $timeout(function () { window.location.href = '/Leave'; }, 2000);

                            }
                            else if (!response.data.IsSuccessful)
                            {
                                toaster.pop('error', "error", response.data.ValidationErrors[0]);
                            }
                        } else {
                            toaster.pop('error', "error", "Could Not Apply Leave, TRY AGAIN!");
                        }
                    }
                );
            }

            // ======================================================= VARIABLES =========================================================

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
            $scope.Leave = { "singleMultiple": 'single', "Type": 0, "Reason": "", "LeaveFrom": null, "LeaveTo": null};
        }
    ]
);