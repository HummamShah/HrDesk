app.controller('UserCtrl',
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
                var Filters = ["FirstName", "LastName", "Email", "DepartmentName", "CreatedBy"];
                $scope.SetSearchColoumns(Filters);
                $scope.Assignment = {};
                $scope.PositionDropDown = [];
                $scope.AssignmentTypes = ["FirstName", "Email", "DepartmentName", "CreatedBy"];
                var ListingOptions = {
                    CurrentPage: 1,
                    PageSize: 20
                }
                $scope.ListingOptions.Url = '/api/UserApi/GetListData';
                $scope.InitListing();
            }

            // ====================================================== ADD INIT ============================================================

            $scope.AddInit = function () {

                $scope.GetIncentiveList();
                $scope.GetTaxList();
                $scope.GetShiftList();
                $scope.GetDeductionList();
                $scope.User = {};
                $scope.Document = {};
                GetDepartments();
                $scope.PositionDropDown = [];
                $scope.User.Docs = [{ "Title": "Resume", "ChooseInput": false }, { "Title": "CNIC front", "ChooseInput": false }, { "Title": "CNIC back", "ChooseInput": false }, { "Title": "Appointment Letter", "ChooseInput": false }];
                $scope.User.EducationalDocs = [];
                $scope.User.Certificates = [];
                $scope.User.Taxes = [];
                $scope.User.Incentives = [];
                $scope.User.Deductions = [];
                $scope.AddEducationalDocRow();
                $scope.AddCertificateRow();
                console.log($scope.User.EducationalDocs);
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    }
                );
            }

            // ====================================================== EDIT INIT ============================================================

            $scope.EditInit = function () {
                $scope.GetIncentiveList();
                $scope.GetTaxList();
                $scope.GetDeductionList();
                $scope.GetShiftList();
                $scope.PositionDropDown = [];
                $scope.User = {};
                GetDepartments();
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/UserApi/GetUser", { Id: Id }).then(
                    function (response) {
                        $scope.User = response.data;
                        $scope.User.Docs.forEach(x => {
                            x.ChooseInput = false;
                        });
                        $scope.GetPositionsByDepartmentId($scope.User.DepartmentId);
                    }
                );
            }

            // ====================================================== ADD ADMIN INIT ============================================================

            $scope.AddAdminInit = function () {
                $scope.Admin = {};
            }
            // ====================================================== ADD USER ============================================================

            $scope.AddUser = function (user) {
                user.Position = JSON.parse(user.Position);

                console.log(user.Position);
                if (user.FirstName == null || user.FirstName == "") {
                    toaster.pop('error', "error", "Please Enter First Name");
                    return;
                }
                if (user.LastName == null || user.LastName == "") {
                    toaster.pop('error', "error", "Please Enter Last Name");
                    return;
                }
                if (user.Contact1 == null) {
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                if (user.Email == null || user.Email == "") {
                    toaster.pop('error', "error", "Please Enter Correct Email!");
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
                if (user.DepartmentId == null) {
                    toaster.pop('error', "error", "Please select department");
                    return;
                }
                if (user.ShiftId == null) {
                    toaster.pop('error', "error", "Please select shift");
                    return;
                }
                if (user.Password == null || user.Password == "") {
                    toaster.pop('error', "error", "Please Enter Password!");
                    return;
                }
                if (user.ConfirmPassword == null || user.ConfirmPassword == "") {
                    toaster.pop('error', "error", "Please Enter Confirm Password!");
                    return;
                }
                if (user.Password != user.ConfirmPassword) {
                    toaster.pop('error', "error", "Password and Confirm Password should match!");
                    return;
                }
                $scope.IsServiceRunning = true;
                $scope.AjaxPost("/api/UserApi/RegisterUser", user).then(
                    function (response) {
                        console.log(response);
                        
                        if (response.status == 200) {
                            if (response.data.Success) {
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

            // ====================================================== Reset Password ============================================================
            $scope.ResetPassword = function (Email) {
                $scope.AjaxPost("/api/UserApi/RegisterUser", { Email: Email}).then(
                    function (response) {
                        console.log("Success", response);
                        if (response.status == 200 && response.data.Success) {
                            toaster.pop('success', "success", "Password Reset Successfully!");
                            $timeout(function () { window.location.href = '/User'; }, 3000);
                        } else {
                            toaster.pop('error', "error", "Could Not Reset User Password!");
                        }
                    }
                );
            }
            // ====================================================== EDIT USER ============================================================

            $scope.EditUser = function (user) {
                if (user.Contact1 == null || user.Contact1 ==  "") {
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
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
                if (user.DepartmentId == null || user.DepartmentId == 0) {
                    toaster.pop('error', "error", "Please select department");
                    return;
                }
                if (user.ShiftId == null) {
                    toaster.pop('error', "error", "Please select shift");
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

            // ================================================== USER JD FOR MODAL ================================================
            $scope.ShowUserJD = function (userJD) {
                console.log(userJD);
                $scope.UserJD = userJD;
            }

            // ========================================================= GET DEPRATMENTS ================================================
            function GetDepartments() {
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    }
                );
            }

            // ==================================================== GET POSITIONS DROP DOWN =========================================================
            $scope.GetPositionsByDepartmentId = function (deptId) {
                $scope.Departments[deptId-1].PositionsList.forEach( (x) => { 
                    $scope.PositionDropDown.push(x);
                });
            }

            // =================================================== GET SHIFTS LIST =========================================================
            $scope.GetShiftList = function () {
                $scope.AjaxGet("/api/ShiftApi/GetShifts", null).then(
                    function (response) {
                        console.log(response);
                        $scope.ShiftsList = response.data.ShiftsList;
                    }
                );
            }

            // =================================================== GET TAXES LIST =========================================================
            $scope.GetTaxList = function () {
                $scope.AjaxGet("/api/TaxApi/GetTaxList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.TaxesList = response.data.TaxesList;
                            console.log($scope.TaxesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Tax List, try again!");
                        }
                    }
                );
            }

            // =================================================== GET INSENTIVES LIST =========================================================
            $scope.GetIncentiveList = function () {
                $scope.AjaxGet("/api/IncentiveApi/GetIncentiveList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.IncentivesList = response.data.IncentiveList;
                            console.log($scope.IncentivesList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Incentive List, try again!");
                        }
                    }
                );
            }

            // =================================================== GET DEDUCTIONS LIST =========================================================
            $scope.GetDeductionList = function () {
                $scope.AjaxGet("/api/DeductionApi/GetDeductionList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.DeductionsList = response.data.DeductionsList;
                            console.log($scope.DeductionsList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Deduction List, try again!");
                        }
                    }
                );
            }

            // =================================================== ADD EDUCATIONAL DOC ROW ===================================================
            $scope.AddEducationalDocRow = function () {
                var row = {Id: 0, Title: "", SubTitle: "", Url : ""};
                $scope.User.EducationalDocs.push(row);
                console.log($scope.User.EducationalDocs);
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
                            $scope.User.EducationalDocs.splice(index, 1, $scope.Document);
                            console.log($scope.User.EducationalDocs);
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
                $scope.User.EducationalDocs.splice(index, 1);
                console.log($scope.User.EducationalDocs);
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

            // =======================================================  VALIDATE CONTACT NUMBER 1 & 2 ====================================================
            // TODO: make a generic function for any number instead of the below specific functions
            $scope.IsContactNumber1 = function () {
                for (var i = 0; i < $scope.User.Contact1.length; i++) {
                    if (isNaN($scope.User.Contact1[i])) {
                        var temp = $scope.User.Contact1.split('');
                        temp.splice(i, 1);
                        $scope.User.Contact1 = temp.join('');
                        return;
                    }
                }
            }
            $scope.IsContactNumber2 = function () {
                for (var i = 0; i < $scope.User.Contact2.length; i++) {
                    if (isNaN($scope.User.Contact2[i])) {
                        var temp = $scope.User.Contact2.split('');
                        temp.splice(i, 1);
                        $scope.User.Contact2 = temp.join('');
                        return;
                    }
                }
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