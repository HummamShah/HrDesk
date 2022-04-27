app.controller('PayCtrl',
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
            console.log("Connected to Pay App");

            // ============================================= INIT INDEX ===================================================
            $scope.initIndex = function () {
                // variables & constants
                var date = new Date();
                $scope.selectedMonth = date.getMonth();
                $scope.selectedYear = date.getFullYear();
                $scope.months = [{ id: 0, name: "January" }, { id: 1, name: "February" }, { id: 2, name: "March" }, { id: 3, name: "April" }, { id: 4, name: "May" }, { id: 5, name: "June" }, { id: 6, name: "July" }, { id: 7, name: "August" }, { id: 8, name: "September" }, { id: 9, name: "October" }, { id: 10, name: "November" }, { id: 11, name: "December" }];
                $scope.years = ["2021"];
                $scope.searchedMonth = $scope.selectedMonth;
                $scope.GetPayListing();
            }

           // ============================================= PAY SLIP INIT ===================================================
            $scope.paySlipInit = function () {
                console.log("inside payslip init");
                $scope.GeneratePaySlip();
            }

            // ============================================= EDIT INIT ======================================================
            $scope.editInit = async function () {
                console.log("inside edit init");
                await $scope.GeneratePaySlip();
            }

            // ============================================= GET PAY ROLL SUMMARY ===================================================
            $scope.GetPayRollSummary = function () {
                $scope.GetPayListing();
            }

            // ============================================= GET PAY LISTING ===================================================
            $scope.GetPayListing = function () {
                $scope.AjaxGet("/api/PayApi/GetPayRollList", { Month: $scope.selectedMonth, Year: $scope.selectedYear}).then(
                    function (response) {
                        console.log(response);
                        $scope.PayRollList = response.data.PayRollList;
                        console.log($scope.PayRollList);
                    }
                );
            }

            // ============================================= GENERATE PAY SLIP ==================================================
            $scope.GeneratePaySlip = function (Month, Year) {
                console.log("inside generate payslip");
                var Id = $scope.GetUrlParameter("Id");
                var Month = $scope.GetUrlParameter("Month");
                var Year = $scope.GetUrlParameter("Year");
                console.log(Month);
                console.log(Year);
                $scope.AjaxPost("/api/PayApi/GeneratePaySlip", { Id: Id, Month, Year}).then(
                    function (response) {
                        if (response.data.IsSuccessful) {
                            console.log(response);
                            $scope.PaySlip = response.data;
                        }
                        else {
                            toaster.pop('error', 'error', response.data.ValidationErrors[0]);
                            $timeout(function () { window.location.href = '/Pay'; }, 2000);
                        }
                    }
                );
            }

            // ============================================= SET MODAL TITLE ==================================================
            $scope.SetModalTitle = function (title) {
                $scope.ModalTitle = title;
                $scope.Row = { "Description": null, "Amount": null, "Remarks": null };
            }

            // ============================================= ADD ROW ==================================================
            $scope.AddRow = function (type, row) {
                if (type === 'Incentive') { $scope.PaySlip.IncentiveAddition.push(row); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Tax') { $scope.PaySlip.TaxDeductions.push(row); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Deduction') { $scope.PaySlip.DeductionsDeductions.push(row); $scope.CalculateTotalAmount(type); return;}
            }

            // ============================================= REMOVE ROW ==================================================
            $scope.RemoveRow = function (index, type) {
                if (type === 'Incentive') { $scope.PaySlip.IncentiveAddition.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Tax') { $scope.PaySlip.TaxDeductions.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Deduction') { $scope.PaySlip.DeductionsDeductions.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
            }

            // ============================================= CALCULATE TOTAL AMOUNT ==================================================
            $scope.CalculateTotalAmount = function (type) {
                if (type === 'Incentive') {
                    $scope.PaySlip.TotalIncentiveAddition = 0;
                    for (incentive of $scope.PaySlip.IncentiveAddition) {
                        $scope.PaySlip.TotalIncentiveAddition += incentive.Amount;
                    }
                }
                else if (type === 'Tax') {
                    $scope.PaySlip.TotalTaxDeduction = 0;
                    for (tax of $scope.PaySlip.TaxDeductions) {
                        $scope.PaySlip.TotalTaxDeduction += tax.Amount;
                    }
                }
                else if (type === 'Deduction') {
                    $scope.PaySlip.TotalDeductionsDeduction = 0;
                    for (deduction of $scope.PaySlip.DeductionsDeductions) {
                        $scope.PaySlip.TotalDeductionsDeduction += deduction.Amount;
                    }
                }
                $scope.PaySlip.FinalSalary = $scope.PaySlip.BasicSalary + $scope.PaySlip.TotalIncentiveAddition - $scope.PaySlip.TotalTaxDeduction - $scope.PaySlip.TotalDeductionsDeduction;
            }

            // ============================================= SAVE PAY SLIP ==================================================
            $scope.SavePaySlip = function (paySlip) {
                $scope.PaySlip.BasicPay = $scope.PaySlip.BasicSalary * 64.52 / 100;
                $scope.PaySlip.HouseRent = $scope.PaySlip.BasicSalary * 29.03 / 100;
                $scope.PaySlip.Utilities = $scope.PaySlip.BasicSalary * 6.45 / 100;
                $scope.AjaxPost("/api/PayApi/SavePaySlip", paySlip).then(
                    function (response) {
                        console.log(response);
                        if (response.data.IsSuccessful) {
                            console.log("inside success");
                            $timeout(function () { window.location.href = '/Pay'; }, 2000);
                        }
                        else {toaster.pop('error', 'error', response.data.ValidationErrors[0]);}
                    }
                );
            }
        }
    ]
);