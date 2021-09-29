﻿app.controller('UserCtrl',
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
            console.log("Connected to User App");

            // ====================================================== INIT INDEX ============================================================

            $scope.initIndex = function () {
                /*$scope.AjaxGet("/api/UserApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Users = response.data.Data;
                    });*/
                var Filters = ["FirstName", "LastName", "Email", "DepartmentName", "CreatedBy"];
                $scope.SetSearchColoumns(Filters);
                $scope.Assignment = {};
                $scope.AssignmentTypes = ["FirstName", "Email", "DepartmentName", "CreatedBy"];
                var ListingOptions = {
                    CurrentPage: 1,
                    PageSize: 20
                }
                $scope.ListingOptions.Url = '/api/UserApi/GetListData';
                $scope.InitListing();
            }

            // ====================================================== ADD USER ============================================================

            $scope.AddUser = function (user) {
                console.log(user);
                /*if (user.DepartmentId == null || user.DepartmentId == 0) {
                    //alert("Please Select Department");
                    toaster.pop('error', "error", "Please Select Department");
                    return;
                }*/
                if (user.FirstName == null || user.FirstName == "") {
                    //alert("Please Enter First Name");
                    toaster.pop('error', "error", "Please Enter First Name");
                    return;
                }
                if (user.Email == null || user.Email == "") {
                    //alert("Please Enter Email!");
                    toaster.pop('error', "error", "Please Enter Email!");
                    return;
                }
                if (user.Contact1 == null) {
                    //alert("Please Enter Primary Contact");
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                if (user.Password == null || user.Password == "") {
                    //alert("Enter Password");
                    toaster.pop('error', "error", "Please Enter Password!");
                    return;
                }
                if (user.RemainingLeaves == null) {
                    toaster.pop('error', "error", "Please Select Remaining Leaves!");
                    return;
                }
                if (user.AnnualLeaves == null) {
                    toaster.pop('error', "error", "Please Select Annual Leaves!");
                    return;
                }
                if (user.MedicalLeaves == null) {
                    toaster.pop('error', "error", "Please Select Medical Leaves!");
                    return;
                }
                if ((user.AnnualLeaves + user.MedicalLeaves) != user.RemainingLeaves) {
                    toaster.pop('error', "error", "The total of Annual & Medical Leaves should be equal to Remaining Leaves!");
                    return;
                }
                if (user.ShiftId == null) {
                    toaster.pop('error', "error", "Please select shift");
                    return;
                }
                if (user.Password != user.ConfirmPassword) {
                    //alert("Password and Confirm Password shoul match");
                    toaster.pop('error', "error", "Password and Confirm Password should match!");
                    return;
                }
                    //Loader need to make it generic so we could use this in a function
                $scope.IsServiceRunning = true;

                $scope.AjaxPost("/api/UserApi/RegisterUser", user).then(
                    function (response) {
                        console.log(response);
                        
                        if (response.status == 200) {
                            if (response.data.Success) {
                                //alert("User has been Added Successfully!");
                                toaster.pop('success', "success", "User Added Successfully!");
                                $timeout(function () { window.location.href = '/User'; }, 2000);
                            }
                            else {
                                angular.forEach(response.data.ValidationErrors, function (error, key) {
                                    toaster.pop('error', "error", error);}
                                );
                            }

                        } else {
                            toaster.pop('error', "error", "Could Not Add User!");
                        }
                    }
                );
            }

            // ====================================================== ADD ADMIN ============================================================

            $scope.AddAdmin = function (user) {
                console.log(user);
                $scope.AjaxPost("/api/UserApi/RegisterAdmin", user).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Admin Added Successfully!");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Admin!");
                        }
                    }
                );
            }

            // ====================================================== EDIT USER ============================================================

            $scope.EditUser = function (user) {
                console.log(user);
                if ((user.AnnualLeaves + user.MedicalLeaves) != user.RemainingLeaves) {
                    toaster.pop('error', "error", "The total of Annual & Medical Leaves should be equal to Remaining Leaves!");
                    return;
                }
                $scope.AjaxPost("/api/UserApi/EditUser", user).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "User Edited Successfully!");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Update User!");
                        }
                    }
                );
            }

            // ====================================================== ADD ADMIN INIT ============================================================

            $scope.AddAdminInit = function () {
                $scope.Admin = {};
            }
            function GetDepartments() {
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    }
                );
            }

            // ====================================================== EDIT INIT ============================================================

            $scope.EditInit = function () {
                $scope.User = {};
                GetDepartments();
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/UserApi/GetUser", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.User = response.data;
                        console.log("inside edit init");
                        $scope.User.Docs.forEach(x => {
                            x.ChooseInput = false;
                            console.log(x.ChooseInput);
                        });
                        console.log($scope.User.Docs);
                        /*$scope.resume = $scope.User.Docs.find(x => x.Title == 'Resume').Url;*/
                        //$scope.GetUsersByDepartmentId($scope.User.DepartmentId);                        
                    }
                );
                $scope.AjaxGet("/api/ShiftApi/GetShifts", null).then(
                    function (response) {
                        console.log(response);
                        $scope.ShiftsList = response.data.ShiftsList;
                    }
                );
            }

            // ====================================================== ADD INIT ============================================================

            $scope.AddInit = function () {
                $scope.User = {};
                $scope.Document = {};
                $scope.User.Docs = [{ "Title": "Resume" }, { "Title": "CNIC front" }, { "Title": "CNIC back" }, { "Title": "Appointment Letter" }];
                $scope.User.EductaionalDocs = [];
                $scope.User.Certificates = [];
                $scope.AddEdcuationalDocRow();
                $scope.AddCertificateRow();
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    }
                );
                $scope.AjaxGet("/api/ShiftApi/GetShifts", null).then(
                    function (response) {
                        console.log(response);
                        $scope.ShiftsList = response.data.ShiftsList;
                    }
                );
            }

            // =================================================== ADD EDUCATIONAL DOC ROW ===================================================
            $scope.AddEdcuationalDocRow = function () {
                var row = {Id: 0, Title: "", SubTitle: "", Url : ""};
                $scope.User.EductaionalDocs.push(row);
            }

            // =================================================== ADD CERTIFICATE ROW ===================================================
            $scope.AddCertificateRow = function () {
                var row = {Id: 0, Title: "", SubTitle: "", Url: ""};
                $scope.User.Certificates.push(row);
            }

            // ==================================================== UPLOAD USER IMAGE ===========================================================
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

            // =========================================================== UPLOAD DOCUMENT ================================================================
            $scope.UploadFile = function (files, Title) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        if (response.data.Success) {
                            $scope.RemoveUploadedFile(Title);
                            $scope.Document = {};
                            //$scope.Document.Id = Doc.Id;
                            $scope.Document.Title = Title;
                            $scope.Document.SubTitle = files[0].name;
                            $scope.Document.Url = response.data.Urls[0];
                            $scope.Document.ChooseInput = false;
                            $scope.User.Docs.forEach((doc, key) => {
                                console.log("inside forEach");
                                if (doc.Title == Title) {
                                    $scope.User.Docs.splice(key, 1, $scope.Document);
                                }
                            });
                            console.log($scope.User.Docs);
                        } else {

                        }
                    }
                );
            }

            // =========================================================== EDIT DOCUMENT ================================================================
            $scope.EditFile = function (files, Title, index) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        if (response.data.Success) {
                            $scope.Document = {};
                            //$scope.Document.Id = Doc.Id;
                            $scope.Document.Title = Title;
                            $scope.Document.SubTitle = files[0].name;
                            $scope.Document.Url = response.data.Urls[0];
                            $scope.User.Docs.splice(index, 1, $scope.Document);
                            console.log($scope.User.Docs);
                        } else {

                        }
                    }
                );
            }

            // =========================================================== UPLOAD EDUCATIONAL DOCUMENT ================================================================
            $scope.UploadEducationalFile = function (files, Title, index) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        if (response.data.Success) {
                            $scope.Document = {};
                            //$scope.Document.Id = Doc.Id;
                            $scope.Document.Title = Title;
                            $scope.Document.SubTitle = files[0].name;
                            $scope.Document.Url = response.data.Urls[0];
                            $scope.User.EductaionalDocs.splice(index, 1, $scope.Document);
                            console.log($scope.User.EductaionalDocs);
                        } else {

                        }
                    }
                );
            }

            // =========================================================== UPLOAD CERTIFICATE ================================================================
            $scope.UploadCertificate = function (files, Title, index) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        if (response.data.Success) {
                            $scope.Document = {};
                            //$scope.Document.Id = Doc.Id;
                            $scope.Document.Title = Title;
                            $scope.Document.SubTitle = files[0].name;
                            $scope.Document.Url = response.data.Urls[0];
                            $scope.User.Certificates.splice(index, 1, $scope.Document);
                            console.log($scope.User.Certificates);
                        } else {

                        }
                    }
                );
            }

            // ========================================================== REMOVE UPLOADED FILE ============================================================
            $scope.RemoveUploadedFile = function (Title) {
                var Doc = { Id: 0, Title: Title, SubTitle: "", Url: "", ChooseInput: false };
                $scope.User.Docs.forEach((doc, key) => {
                    console.log("inside forEach");
                    if (doc.Title == Title) {
                        $scope.User.Docs.splice(key, 1, Doc);
                    }
                });
                console.log($scope.User.Docs);
            }

            // ========================================================== REMOVE UPLOADED EDUCATIONAL FILE ============================================================
            $scope.RemoveUploadedEducationalFile = function (index) {
                console.log("inside edu doc delete")
                $scope.User.EductaionalDocs.splice(index, 1);
                console.log($scope.User.EductaionalDocs);
            }

            // ========================================================== REMOVE UPLOADED CERTIFICATE ============================================================
            $scope.RemoveUploadedCertificate = function (index) {
                console.log("inside certificate delete")
                $scope.User.Certificates.splice(index, 1);
                console.log($scope.User.Certificates);
            }

            // ========================================================== ENABLE INPUT FIELD ===============================================================
            $scope.EnableInputField = function (Doc) {
                console.log(Doc.ChooseInput);
                Doc.ChooseInput = true;
                console.log(Doc.ChooseInput);
            }

            // ========================================================== DISABLE INPUT FIELD ===============================================================
            $scope.DisableInputField = function (Doc) {
                console.log(Doc.ChooseInput);
                Doc.ChooseInput = false;
                console.log(Doc.ChooseInput);
            }


            $scope.UploadAdminImage = function (files, prop) {
                $scope.GetSingleImageUploadUrl(files).then(
                    function (response) {
                        console.log(response);
                        $scope.Admin[prop] = response.data.Urls[0];
                        if (response.Success) {

                        } else {

                        }
                    }
                );
            }

            $scope.HiringAddInit = function () {
                $scope.Id = $scope.GetUrlParameter("Id");
            }
            $scope.AddHiring = function(user)
            {
                $scope.AjaxPost("/api/HiringApi/AddHiring", { Id: $scope.Id, User: user }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Hiring Process Add Successfully!");
                            $timeout(function () { window.location.href = '/Hiring'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Hiring Process!");
                        }
                    }
                );
            }
        }
    ]
);