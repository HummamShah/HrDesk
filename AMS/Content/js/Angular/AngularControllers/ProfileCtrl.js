app.controller('ProfileCtrl',
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
            console.log("Connected to Profile App");

            $scope.InitIndex = function () {
                $scope.User = {};
                
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/UserApi/GetUser", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.User = response.data;
                        $scope.TempUser = response.data;
                    });
            }
            $scope.EditProfile = function (User) {
                console.log(User);
                if (User.Address == "") {
                    //alert("Please Enter Primary Contact");
                    toaster.pop('error', "error", "Please Enter Address");
                    return;
                }
                if (User.Contact1 == "") {
                    //alert("Please Enter Primary Contact");
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                
                $scope.AjaxPost("/api/UserApi/EditUser", User).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Profile has been Edited Successfully!");
                            $scope.InitIndex();
                        } else {
                            toaster.pop('error', "error", "Could Not  Update Profile!");
                        }
                    });
            }
        }
    ]
)
    ;