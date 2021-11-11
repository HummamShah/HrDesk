app.controller('HolidayCtrl',
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
            console.log("Connected to Holiday App");

            // ==================================================== INIT INDEX ==========================================================
            $scope.InitIndex = function () {
               
                $scope.Holiday = {};
                $scope.AjaxGet("/api/HolidayApi/GetHolidayList").then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            $scope.HolidaysList = response.data.HolidaysList;
                            console.log($scope.HolidaysList);
                        } else {
                            toaster.pop('error', "error", "Could Not Find Holiday List, try again!");
                        }
                    }
                );
            }
            // =================================================== DELETE HOLIDAY ====================================================
            $scope.DeleteHoliday = function (Id) {
                console.log(Id);
                $scope.AjaxPost('/Api/HolidayApi/DeleteHoliday', { Id: Id}).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            if (response.data.Success) {
                                toaster.pop('success', "success", "Holiday has been Deleted!");
                                $timeout(function () { window.location.href = '/Holiday'; }, 2000);
                            }
                            else if (!response.data.Success) {
                                toaster.pop('error', "error", response.data.ValidationErrors[0]);
                            }
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Delete Holiday, TRY AGAIN!");
                        }
                    }
                );
            }
            $scope.SaveId = function (Id) {
                $scope.DeleteId = Id;
            }
            $scope.ModalOptions = {};
            $scope.SetHolidayModal = function (action) {
                $scope.ModalOptions = {};
                $scope.ModalOptions.Action = action;
                if (action == "Edit") {
                    $scope.ModalOptions.Heading = "Edit Holiday";
                    //$scope.UpdateHoliday(data);
                }if (action == "Add") {
                    $scope.ModalOptions.Heading = "Add Holiday";
                }
            }
            // =================================================== EDIT HOLIDAY ====================================================
            $scope.UpdateHoliday = function (Holiday) {
                $scope.ActionType = "Edit";
                $scope.Holiday = Holiday;
                $scope.HolidayId = Holiday.Id;
                $scope.Holiday.Date = new Date(Holiday.Date);
                $scope.SetHolidayModal("Edit");
                console.log($scope.Holiday);
            }
            $scope.EditHoliday = function (Holiday) {
                var Data = {
                    Id: $scope.HolidayId,
                    Date: $scope.GetDatePostFormat($scope.Holiday.Date),
                    Remarks: Holiday.Remarks
                }
                if ($scope.Holiday.Date == null) {
                    toaster.pop('error', "error", "Please choose date");
                    return;
                }
                if ($scope.Holiday.Date < new Date()) {
                    toaster.pop('error', "error", "You can't add holiday for passed dates");
                    return;
                }
                if ($scope.Holiday.Date.getDay() == 0 || $scope.Holiday.Date.getDay() == 6) {
                    toaster.pop('error', "error", "Please choose weekdays");
                    return;
                }
                
                console.log(Data)
                $scope.AjaxPost('/Api/HolidayApi/EditHoliday', Data).then(
                    
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                           
                            if (response.data.Success) {
                                toaster.pop('success', "success", "Holiday has been Edited!");
                                $timeout(function () { window.location.href = '/Holiday'; }, 2000);
                            }
                            else if (!response.data.Success) {
                                toaster.pop('error', "error", response.data.ValidationErrors[0]);
                            }
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Delete Holiday, TRY AGAIN!");
                        }
                    }
                );
            }
            // =================================================== ADD HOLIDAY ====================================================

            $scope.AddHoliday = function () {
                console.log('inside add holiday function', $scope.Holiday);
                if ($scope.Holiday.Date == null) {
                    toaster.pop('error', "error", "Please choose date");
                    return;
                }
                if ($scope.Holiday.Date < new Date()) {
                    toaster.pop('error', "error", "You can't add holiday for passed dates");
                    return;
                }
                if ($scope.Holiday.Date.getDay() == 0 || $scope.Holiday.Date.getDay() == 6) {
                    toaster.pop('error', "error", "Please choose weekdays");
                    return;
                }
                console.log($scope.Holiday.Date);
                $scope.Holiday.Date = $scope.GetDatePostFormat($scope.Holiday.Date);
                console.log($scope.Holiday.Date);
                $scope.AjaxPost('/Api/HolidayApi/AddHoliday', $scope.Holiday).then(
                    function (response) {
                        if (response.status == 200) {
                            console.log(response);
                            if (response.data.IsSuccessful) {
                                toaster.pop('success', "success", "Holiday has been added!");
                                $timeout(function () { window.location.href = '/Holiday'; }, 2000);
                            }
                            else if (!response.data.IsSuccessful) {
                                toaster.pop('error', "error", response.data.ValidationErrors[0]);
                            }
                        }
                        else {
                            toaster.pop('error', "error", "Could Not Add Holiday, TRY AGAIN!");
                        }
                    }
                );
            }
        }
    ]
);