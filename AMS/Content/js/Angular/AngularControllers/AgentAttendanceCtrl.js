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
                var Id = $scope.UserIdentity;
            var date = new Date();
            $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
            $scope.DateTo = new Date();

            console.log("Id", Id)
            console.log("newVr", $scope.UserIdentityInit());
            /*$scope.GetSummary(null, $scope.selectedYear, $scope.selectedMonth,);*/
            $scope.searchedMonth = $scope.selectedMonth;
        }

        // ================================================== INIT INDEX (HR: All Employees) ==========================================================
        $scope.initIndexHRstaff = function () {
            var date = new Date();
            $scope.DateFrom = new Date(date.getFullYear(), date.getMonth(), 1);
            $scope.DateTo = new Date();
            $scope.GetAllEmployeesAttendance($scope.DateFrom, $scope.DateTo);
        }

        // ================================================== INIT INDEX (HR: Single Employee) ==========================================================
        $scope.initIndexHRagent = function () {
            $scope.GetAllEmployeesList();
            $scope.AgentAttendance = {};
        }

        // ================================================== GET All EMPLOYESS LIST ==========================================================
        $scope.GetAllEmployeesList = function () {
            $scope.AjaxGet("/api/AgentAttendanceApi/GetEmployessList").then(
                function (response) {
                    console.log(response);
                    $scope.EmployeesList = response.data.EmployeesList;
                    console.log($scope.EmployeesList);
                }
            );
        }

        // ================================================== GET All STAFF ATTENDANCE ==========================================================
        $scope.GetAllEmployeesAttendance = function (dateFrom, dateTo) {
            dateFrom = $scope.GetDatePostFormat(dateFrom);
            dateTo = $scope.GetDatePostFormat(dateTo);
            $scope.AjaxGet("/api/AgentAttendanceApi/GetListing", { DateFrom: dateFrom, DateTo: dateTo }).then(
                function (response) {
                    console.log(response);
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
                    $scope.AgentInfo = response.data;
                    $scope.AttendanceList = response.data.AgentAttandanceList;
                }
            );
        }

        // ================================================== GET USER ID (MARK EXCUSE) ===============================================
        $scope.SaveExcusedAgent = function (Agent) {
            $scope.ExcusedAgentId = Agent.Id;
            $scope.ExcusedAgentRemarks = "";
        }

        // ================================================= MARK EXCUSE =====================================================================
        $scope.MarkExcused = function (Id, remarks) {
            $scope.AjaxPost("/api/AgentAttendanceApi/AddAgentExcuse", { Id: Id, Remarks: remarks }).then(
                function (response) {
                    if (response.status == 200) {
                        $scope.GetAllEmployeesAttendance($scope.DateFrom, $scope.DateTo);
                    }
                }
            );
        }

        // ================================================= GET MONTH SUMMARY ============================================================
            $scope.GetSummary = function (Date,Employee) {
                console.log("EMployee And Date", Employee, Date)
                if (Employee != null) {
                    $scope.AjaxPost("/api/AgentAttendanceApi/GetSummary", { Date: $scope.GetDatePostFormat(Date), AgentsId: Employee.Id }).then(
                        function (response) {
                            if (response.status == 200) {
                                $scope.Summary = response.data;
                                $scope.AttendanceList = response.data.AgentAttandanceList;
                                $scope.searchedMonth = $scope.selectedMonth;
                                console.log($scope.Summary);
                            }
                        }
                    );
                }
                else {
                    $scope.AjaxPost("/api/AgentAttendanceApi/GetSummary", { Date: $scope.GetDatePostFormat(Date), AgentId: $scope.UserIdentity.Id }).then(
                        function (response) {
                            if (response.status == 200) {
                                $scope.Summary = response.data;
                                $scope.AttendanceList = response.data.AgentAttandanceList;
                                $scope.searchedMonth = $scope.selectedMonth;
                                console.log($scope.Summary);
                            }
                        }
                    );

                }
        }

        // ================================================================ VARIABLES =========================================================
        $scope.labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30'];
        $scope.series = ['Time'];
        $scope.data = [[1,2,3,4,5,6]];
        var date = new Date();
        $scope.selectedMonth = date.getMonth();
        $scope.selectedYear = date.getFullYear();
        $scope.months = [{ id: 0, name: "January" }, { id: 1, name: "February" }, { id: 2, name: "March" }, { id: 3, name: "April" }, { id: 4, name: "May" }, { id: 5, name: "June" }, { id: 6, name: "July" }, { id: 7, name: "August" }, { id: 8, name: "September" }, { id: 9, name: "October" }, { id: 10, name: "November" }, { id: 11, name: "December" }];
            // TODO: make the following list of 'Years' dynamic
            $scope.years = ["2021"];    //["2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021"];
        $scope.selectedEmployee;
        }
    ]
);