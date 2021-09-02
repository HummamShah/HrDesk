app.controller('AgentAttendanceCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to AgentAttendanceCtrl App");
            $scope.initIndex = function () {
                $scope.Date = new Date();
                $scope.DateFrom = new Date();
                $scope.DateTo = new Date();
                // below line
                $scope.TodaysDate = $scope.GetDatePostFormat(new Date());
                console.log($scope.TodaysDate);
                $scope.GetAttendance($scope.DateFrom, $scope.DateTo);

            }

            $scope.GetAttendance = function (dateFrom, dateTo) {
                dateFrom = $scope.GetDatePostFormat(dateFrom);
                dateTo = $scope.GetDatePostFormat(dateTo);
                $scope.AjaxGet("/api/AgentAttendanceApi/GetListing", { DateFrom: dateFrom, DateTo: dateTo }).then(
                    function (response) {
                        console.log(response);
                        /* $scope.NewDate = response.data.Data;*/
                        $scope.AttendanceList = response.data.Data;
                    });
            }

            $scope.AttendanceRecord = function (dateFrom, dateTo) {
                dateFrom = $scope.GetDatePostFormat(dateFrom);
                dateTo = $scope.GetDatePostFormat(dateTo);
                $scope.AttendanceList = {};
                $scope.AjaxGet("/api/AgentAttendanceApi/GetAgentAttendance", { DateFrom: dateFrom, DateTo: dateTo }).then(
                    function (response) {
                        console.log(response);
                        /* $scope.NewDate = response.data.Data;*/
                        $scope.AttendanceList = response.data.AgentAttandanceList;
                    });
            }

            //Monthly Attendance Report
            /* $scope.GetAttendanceReport = function (dateFrom, dateTo) {
                 dateFrom = $scope.GetDatePostFormat(dateFrom);
                 dateTo = $scope.GetDatePostFormat(dateTo);
                 $scope.AjaxGet("/api/ReportApi/GetAttendanceReport", { DateFrom: dateFrom, DateTo: dateTo }).then(
                     function (response) {
                         console.log(response);
                         $scope.AttendanceList = response.data.Data;
                     });
             }*/
            // mark excused function
            $scope.MarkExcused = function (Id, remarks) {
                $scope.AjaxPost("/api/AgentAttendanceApi/AddAgentExcuse", { Id: Id, Remarks: remarks }).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.GetAttendance($scope.DateFrom, $scope.DateTo);
                        }
                    });
            }

            // getting user ID to mark excuse 
            $scope.SaveExcusedAgent = function (Agent) {
                $scope.ExcusedAgentId = Agent.Id;
                $scope.ExcusedAgentRemarks = "";
            }
        }
    ]);