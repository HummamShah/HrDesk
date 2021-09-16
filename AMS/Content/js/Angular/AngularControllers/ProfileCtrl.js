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

            // ==================================================== INIT INDEX ==========================================================
            $scope.InitIndex = function () {
                $scope.charts();
                $scope.User = {};
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/UserApi/GetUser", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.User = response.data;
                        $scope.TempUser = response.data;
                    }
                );
            }

            // ==================================================== EDIT PROFILE ==========================================================

            $scope.EditProfile = function (User) {
                console.log(User);
                if (User.Address == "") {
                    toaster.pop('error', "error", "Please Enter Address");
                    return;
                }
                if (User.Contact1 == "") {
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                $scope.AjaxPost("/api/UserApi/EditUserByUser", User).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Profile Has Been Updated Successfully!");
                            $scope.InitIndex();
                        } else {
                            toaster.pop('error', "error", "Could Not Update Profile!");
                        }
                    }
                );
            }

            // ==================================================== UPLOAD IMAGE ==========================================================

            $scope.UploadUserImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);
                        $scope.User[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    }
                );
            }

            // ==================================================== RESET ==========================================================
            // we can use initIndex() directly on RESET button click, but we might need to add something in future. If no need, delete the function and use initIndex() directly
            $scope.Reset = function (User) {
                $scope.InitIndex();
            }
        }
    ]
)
    ;