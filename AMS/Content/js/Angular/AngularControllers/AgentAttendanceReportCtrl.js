app.controller('AgentAttendanceReportCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to AgentAttendanceReportCtrl App");
            $scope.initIndex = function () {
                $scope.Date = new Date();
                $scope.DateFrom = new Date();
                $scope.DateTo = new Date();
                // below line
                $scope.TodaysDate = $scope.GetDatePostFormat(new Date());
                console.log($scope.TodaysDate);
                $scope.GetAttendanceReport($scope.DateFrom, $scope.DateTo);

            }
            //Monthly Attendance Report
            $scope.GetAttendanceReport = function (dateFrom, dateTo) {
                $scope.AttendanceList = [];
                $scope.Date = [];
                $scope.Details = [];
                dateFrom = $scope.GetDatePostFormat(dateFrom);
                dateTo = $scope.GetDatePostFormat(dateTo);
                $scope.AjaxGet("/api/ReportApi/GetAttendanceReport", { DateFrom: dateFrom, DateTo: dateTo }).then(
                    function (response) {
                        console.log(response);
                        $scope.AgentName = response.data.AgentName;
                        $scope.Details = response.data.Details;
                        $scope.AttendanceList = response.data.Data;
                        $scope.AttendanceDates = response.data.Dates;

                    });
            }
            // mark excused function
            /* $scope.MarkExcused = function (Id, remarks) {
                 $scope.AjaxPost("/api/AgentAttendanceApi/AddAgentExcuse", { Id: Id, Remarks: remarks }).then(
                     function (response) {
                         if (response.status == 200) {
                             $scope.GetAttendance($scope.DateFrom, $scope.DateTo);
                         }
                     });
             }
 */


        }
    ]);