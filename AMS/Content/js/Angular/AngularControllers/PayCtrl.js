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

            /**
             * initializes the main index page */
            $scope.initIndex = function () {
                $scope.GetPayListing();
            }

            /** initializes the edit page by binding the values of the generated salary first
             * */
            $scope.editInit = async function () {
                console.log("inside edit init");
                await $scope.GeneratePaySlip();
            }

            /** calls the function to generating PaySlip of a particular agent
             * */
            $scope.paySlipInit = function () {
                console.log("inside payslip init");
                $scope.GeneratePaySlip();
            }

            /** gets the basic info of the agents (related to pay e.g. gross salary etc)
             * */
            $scope.GetPayListing = function () {
                $scope.AjaxGet("/api/PayApi/GetPayRollList", null).then(
                    function (response) {
                        console.log(response);
                        $scope.PayRollList = response.data.PayRollList;
                        console.log($scope.PayRollList);
                    }
                );
            }

            /**
             * generates pay slips for the given agent id (getting from url parameter)
             * */
            $scope.GeneratePaySlip = function () {
                console.log("inside generate payslip");
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxPost("/api/PayApi/GeneratePaySlip", { 'Id': Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.PaySlip = response.data;
                    }
                );
            }

            /**
            * sets the title of add row modal
            * @param {string} title
            */
            $scope.SetModalTitle = function (title) {
                $scope.ModalTitle = title;
                $scope.Row = { "Description": null, "Amount": null, "Remarks": null };
            }

            /**
             * adds a row 
             * @param {any} type
             */
            $scope.AddRow = function (type, row) {
                if (type === 'Incentive') { $scope.PaySlip.IncentiveAddition.push(row); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Tax') { $scope.PaySlip.TaxDeductions.push(row); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Deduction') { $scope.PaySlip.DeductionsDeductions.push(row); $scope.CalculateTotalAmount(type); return;}
            }

            /**
             * removes the entire entry from the given 'type' list by 'index'
             * @param {number} index
             * @param {string} type
             */
            $scope.RemoveRow = function (index, type) {
                if (type === 'Incentive') { $scope.PaySlip.IncentiveAddition.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Tax') { $scope.PaySlip.TaxDeductions.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
                else if (type === 'Deduction') { $scope.PaySlip.DeductionsDeductions.splice(index, 1); $scope.CalculateTotalAmount(type); return;}
            }

            /**
             * calculates the total on the change of amount
             * @param {string} type
             */
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

            /**
             * it calls the API to save the generated payslip in DB
             * @param {object} paySlip
             */
            $scope.SavePaySlip = function (paySlip) {
                $scope.AjaxPost("/api/PayApi/SavePaySlip", paySlip).then(
                    function (response) {
                        console.log(response);
                        if (response.data.IsSuccessful) {
                            console.log("inside success");
                            $timeout(function () { window.location.href = '/Pay'; }, 2000);
                        }
                        else {
                            toaster.pop(response.data.ValidationErrors[0]);
                        }
                    }
                );
            }
        }
    ]
);