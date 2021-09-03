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

        // ================================================== INIT INDEX (Agent) ==========================================================

        $scope.initIndex = function () {
            var date = new Date();
            /* $scope.Date = new Date();*/
            $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
            $scope.DateTo = new Date();
            $scope.GetAgentAttendanceRecord($scope.DateFrom, $scope.DateTo);
        }

        // ================================================== INIT INDEX (HR: All Employess) ==========================================================

        $scope.initIndexHR = function () {
            var date = new Date();
            /* $scope.Date = new Date();*/
            $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
            $scope.DateTo = new Date();
            $scope.GetAllEmployeesAttendance($scope.DateFrom, $scope.DateTo);
        }

        // ================================================== GET All STAFF ATTENDANCE ==========================================================

        $scope.GetAllEmployeesAttendance = function (dateFrom, dateTo) {
            dateFrom = $scope.GetDatePostFormat(dateFrom);
            dateTo = $scope.GetDatePostFormat(dateTo);
            $scope.AjaxGet("/api/AgentAttendanceApi/GetListing", { DateFrom: dateFrom, DateTo: dateTo }).then(
                function (response) {
                    console.log(response);
                    /* $scope.NewDate = response.data.Data;*/
                    $scope.AttendanceList = response.data.Data;
                }
            );
        }

        // ================================================== AGENT ATTENDANCE ==========================================================

        $scope.GetAgentAttendanceRecord = function (dateFrom, dateTo) {
            dateFrom = $scope.GetDatePostFormat(dateFrom);
            dateTo = $scope.GetDatePostFormat(dateTo);
            $scope.AttendanceList = {};
            $scope.AjaxGet("/api/AgentAttendanceApi/GetAgentAttendance", { DateFrom: dateFrom, DateTo: dateTo }).then(
                function (response) {
                    console.log(response);
                    /* $scope.NewDate = response.data.Data;*/
                    $scope.AgentInfo = response.data;
                    $scope.AttendanceList = response.data.AgentAttandanceList;
                    for (var i = 0; i < $scope.AttendanceList.length; i++){
                        console.log($scope.AttendanceList[i].StartDate);
                        $scope.data[i] = $scope.AttendanceList[i].StartDate;
                    }
                }
            );
        }

        // ================================================== MARK EXCUSED ==========================================================

        $scope.MarkExcused = function (Id, remarks) {
            $scope.AjaxPost("/api/AgentAttendanceApi/AddAgentExcuse", { Id: Id, Remarks: remarks }).then(
                function (response) {
                    if (response.status == 200) {
                        $scope.GetAttendance($scope.DateFrom, $scope.DateTo);
                    }
                }
            );
        }

        // ================================================== GET USER ID (MARK EXCUSE) =============================================y
        $scope.SaveExcusedAgent = function (Agent) {
            $scope.ExcusedAgentId = Agent.Id;
            $scope.ExcusedAgentRemarks = "";
        }

        // ================================================= Bar Chart Start =========================================================

        $scope.labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30'];
        $scope.series = ['Series A'];

        $scope.data = [];
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
    }
]);