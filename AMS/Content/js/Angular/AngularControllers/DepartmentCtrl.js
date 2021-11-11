app.controller('DepartmentCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Department App");

            // ================================================= INIT INDEX ========================================================
            $scope.initIndex = function () {
                var promise = $http.get("/api/DepartmentApi/GetListData", { params: null, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    }
                );
            }

            // ================================================= ADD INIT ========================================================
            $scope.AddInit = function () {
                $scope.Department = {};
                $scope.DepartmentPosition = {};
                $scope.Department.PositionsList = [];
                $scope.Positions = [{ id: 1, name: "Manager" }, { id: 2, name: "Assistant Manager" }, { id: 3, name: "Executive" }, { id: 4, name: "Employee" }];
            }

            // ================================================= EDIT INIT ========================================================
            $scope.EditInit = function () {
                $scope.Department = {};
                $scope.DepartmentPosition = {};
                $scope.Department.PositionsList = [];
                console.log($scope.Department.PositionsList);
                $scope.Positions = [{ id: 1, name: "Manager" }, { id: 2, name: "Assistant Manager" }, { id: 3, name: "Executive" }, { id: 4, name: "Employee" }];
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                var promise = $http.get("/api/DepartmentApi/GetDepartment", { params: data }, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Department = response.data;
                        console.log($scope.Department.PositionsList);
                    }
                );
            }

            // ================================================= ADD DEPARTMENT ========================================================
            $scope.AddDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                if (Department.Description == null || Department.Description == "") {
                    toaster.pop('error', "error", "Description Is Required!");
                    return;
                }
                var promise = $http.post("/api/DepartmentApi/AddDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            toaster.pop('success', "success", "New Department added Successfully!");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        }
                        else {
                            toaster.pop('error', "error", "Could not add successfully!");
                        }
                    }
                );
            }

            // ================================================= EDIT DEPARTMENT ========================================================
            $scope.EditDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                if (Department.Description == null || Department.Description == "") {
                    toaster.pop('error', "error", "Description Is Required!");
                    return;
                }

                var promise = $http.post("/api/DepartmentApi/EditDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Department has been edited succesfully");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not edit department!");
                        }
                    }
                );
            }

            // ========================================================= ADD POSITION =====================================================
            $scope.AddPosition = function (position) {
             
                if (position.PositionOrder == '' || position.PositionOrder == null) {
                    toaster.pop('error', 'error', "Please choose position!")
                    return;
                }
                if (position.JobDescription == '' || position.JobDescription == null) {
                    toaster.pop('error', 'error', "Please enter job description!")
                    return;
                }
                $scope.Department.PositionsList.push({ PositionOrder: position.PositionOrder, PositionName: position.PositionName, JobDescription: position.JobDescription });
                position = null;
                $scope.DepartmentPosition = {};
                console.log($scope.Department);
            }

            // ================================================= DELETE POSITION ========================================================
            $scope.RemovePosition = function (positionId) {
                console.log('before' + $scope.Department.PositionsList);
                $scope.Department.PositionsList.forEach((x, i) => {
                    if (x.PositionOrder == positionId) {
                        $scope.Department.PositionsList.splice(i, 1);
                    }
                })
                console.log('after' + $scope.Department.PositionsList);
            }

            // =================================================== SET POSITION NAME ON NG-CHANGE OF POSITION ========================
            $scope.SetPositionName = function (positionId) {
                console.log(positionId);
                $scope.Positions.forEach(x => {
                    if (x.id == positionId) {
                        $scope.DepartmentPosition.PositionName = x.name;
                    }
                5});
                console.log($scope.DepartmentPosition);
            }
        }
    ]);