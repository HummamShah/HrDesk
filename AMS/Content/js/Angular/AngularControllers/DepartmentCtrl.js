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
            $scope.initIndex = function () {
                var promise = $http.get("/api/DepartmentApi/GetListData", { params: null, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
            $scope.AddInit = function () {
                $scope.Department = {};
            }
            $scope.EditInit = function () {
                $scope.Company = {};
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
                    });

            }
            $scope.AddDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }

                var promise = $http.post("/api/DepartmentApi/AddDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("New Department Added Successfully!");
                            toaster.pop('success', "success", "New Department added Successfully!");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        }
                        else {
                            //alert("Could not add new department");
                            toaster.pop('error', "error", "Could not add successfully!");
                        }

                    });
            }

            $scope.EditDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }

                var promise = $http.post("/api/DepartmentApi/EditDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("Department has been updated Successfully!");
                            toaster.pop('success', "success", "Department has been added succesfully");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        } else {
                            //alert("Could Not Update Department");
                            toaster.pop('error', "error", "Could not add department!");
                        }

                    });
            }

        }
    ]);