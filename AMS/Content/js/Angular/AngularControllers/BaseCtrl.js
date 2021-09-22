
angular.module('main', ['toaster'])

'use strict';
app.controller('baseCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        "Upload",
        "moment",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster, Upload, moment) {
            $scope.Search = {}; $scope.Search.searchText = 'a';
            $scope.filteredList = [];
            $scope.Filters = {};
            $scope.Search.FilterColoumns = [];
            //Url Parameter Start
            $scope.GetUrlParameter = function (param) {
                const queryString = window.location.search;
                const urlParams = new URLSearchParams(queryString);
                return urlParams.get(param);
            }
            //Url Parameter End

            //AjaxServices Start
            $scope.IsServiceRunning = false;
            $scope.ServiceClassBinder = "LoaderDeActivate"; // bydefualt loader is deactivated
            $scope.TotalNumberOfServicesRunning = 0;
            $scope.AjaxGet = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                        console.log(response);

                    }, function (resp) {
                        console.log(resp);
                        toaster.pop('error', "error", resp.status + "! Internal Error");
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }
            $scope.AjaxGetBackground = function (link, data) {
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        //success

                    }, function (resp) {
                        //error
                    }
                );
                return promise;
            }
            $scope.AjaxPost = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.post(link, data, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {

                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                        console.log(response);

                    }, function (resp) {
                        console.log(resp);
                        toaster.pop('error', "error", resp.status + "! Internal Error");
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }

            $scope.AjaxPostBackground = function (link, data) {
                var promise = $http.post(link, data, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {

                    }, function (resp) {
                    }
                );
                return promise;
            }
            //AjaxServices End




            // Get Notifications Start
            /* $scope.Notifications = {};
             $scope.Notifications.TotalNotification = 0;
             $timeout(function () {
                 getNotificaions();
             }, 60000);
             getNotificaions();
             function getNotificaions() {
                 $scope.AjaxGetBackground("/api/NotificationApi/GetTopFiveNotifications", null).then(
                     function (response) {
                         console.log(response);
                         $scope.Notifications = response.data;
                     });
             }*/
            //Get Notifications End


            //Listing Code Start
            $scope.ListingApiUrl = "";
            $scope.SetApiUrlForListing = function (ListingApiUrl) {
                $scope.ListingApiUrl = ListingApiUrl;
            }

            $scope.ListingOptions = {
                CurrentPage: 1,
                PageSize: 100,
                TotalRecords: 20,
                Url: '',
                Filters: {}
            }
            $scope.GridOptions = {
                CurrentPage: 1,
                PageSize: 100,
                TotalRecords: 20,
                Url: ''
            }
            $scope.NextPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage + 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.PreviousPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage - 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.UpdatePaginationSize = function (PaginationSize) {
                //$scope.ListingOptions.PageSize = PaginationSize;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.ResetList = function (data) {
                $scope.AjaxPost(data.Url, data).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.TotalRevenue = response.data.TotalRevenue; //TODO
                        $scope.filteredList = $scope.ListingData;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });
            }
            $scope.ApplyFilters = function (filters) {
                $scope.ListingOptions.Filters = filters;
                console.log($scope.ListingOptions);
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.SetSearchColoumns = function (Filters) {
                console.log(Filters);
                $scope.Search.FilterColoumns = Filters;
            }

            $scope.InitListing = function () {

                $scope.AjaxPost($scope.ListingOptions.Url, $scope.ListingOptions).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.filteredList = $scope.ListingData;
                        $scope.TotalRevenue = response.data.TotalRevenue; //TODO
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                        $scope.ListingOptions.TotalRevenue = response.data.TotalRevenue;
                    }
                );
            }


            //Listing Code End
            //search stsart
            $scope.resetAll = function () {
                $scope.filteredList = $scope.ListingData;
                $scope.Search.searchText = '';

            }
            $scope.search = function () {
                console.log("Serach Called");
                console.log($scope.ListingData);
                $scope.filteredList = _.filter($scope.ListingData,
                    function (item) {
                        console.log(item);
                        return searchUtil(item, $scope.Search.searchText);

                    });
                if ($scope.Search.searchText == '') {
                    $scope.filteredList = $scope.ListingData;
                }
            }
            $scope.resetAll();
            /* Search Text in all 3 fields */

            function searchUtil(item, toSearch) {
                /* Search Text in all 3 fields */
                console.log(item);
                var IsPresent = false;
                var FountCout = 0;
                angular.forEach($scope.Search.FilterColoumns, function (value, key) {
                    if (item[value] != null) {
                        IsPresent = (item[value].toLowerCase().indexOf(toSearch.toLowerCase()) > -1) ? true : false;
                        if (IsPresent) {
                            FountCout++
                        };
                    }

                }); console.log(IsPresent);
                if (FountCout == 0) {
                    return false;
                } else {
                    return true;
                }

            }
            $scope.ResetFilters = function () {
                $scope.Filters = {};
                $scope.ApplyFilters($scope.Filters);
            }
            $scope.SetFilters = function (filter) {
                console.log(filter);
                filter.AgentId = parseInt(filter.AgentId);
                if (filter.DateFrom != null) {
                    filter.DateFrom = $scope.GetDatePostFormat(filter.DateFrom);
                }
                if (filter.DateTo != null) {
                    filter.DateTo = $scope.GetDatePostFormat(filter.DateTo);
                }
                $scope.ApplyFilters(filter);
            }

            $scope.SelectedRows = [];
            $scope.UpdateSelectedGridRows = function () {
                angular.forEach($scope.filteredList, function (value, key) {
                    if (value.IsSelected) {
                        if ($scope.SelectedRows.indexOf(value) == -1)
                            $scope.SelectedRows.push(value);

                    }
                    else {
                        var index = $scope.SelectedRows.indexOf(value);
                        if (index != -1) {
                            $scope.SelectedRows.splice(index, 1);
                        }
                    }
                });

                console.log($scope.SelectedRows);
                return $scope.SelectedRows;
            }
            $scope.SelectAllRows = false;
            $scope.SelectAll = function (index) {
                $scope.SelectAllRows = !$scope.SelectAllRows;
                if ($scope.SelectAllRows) {
                    angular.forEach($scope.filteredList, function (value, key) {
                        value.IsSelected = true;
                        $scope.SelectedRows.push(value);
                    });

                }
                else {
                    angular.forEach($scope.filteredList, function (value, key) {
                        value.IsSelected = false;
                    });
                    $scope.SelectedRows = [];
                }

            }
            $scope.GetFiltersDropdown = function () {
                console.log("Filter Function Called")
                $scope.getAllAgentsDropdown();
                $scope.GetllCompaniesDropdown();
                $scope.getAccountDropdown();
                getCompaniesDropdown(0);
            }
            $scope.OnAgentChange = function (filter, variabletochange, variablesource) {
                if (filter[variablesource] == null) {
                    filter[variabletochange] = "";
                }
                else {
                    filter[variabletochange] = filter[variablesource].Id;
                }
            }

            //search end

            //Image Upload Code.
            $scope.GetSingleImageUploadUrl = function (files) {
                console.log("Here Start");
                $scope.SelectedFiles = files;

                if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                    var promise = Upload.upload({
                        url: '/FileUpload/UploadImages',
                        data: {
                            files: $scope.SelectedFiles
                        }
                    });
                    promise.then(function (response) {
                        $timeout(function () {
                            $scope.result = response.data;

                        })
                    }, function (response) {
                        if (response.status > 0) {
                            var errormessage = response.status + ":";

                        }
                    })

                }
                console.log("Here")
                return promise;
            }


            //Dashboard
            $scope.UserInfo = {};
            $scope.initDashboard = function () {
                $scope.AjaxGet("/api/AgentAttendanceApi/GetAgentAttendance", null).then(
                    function (response) {
                        if (response.status == 200) {

                            $scope.UserInfo = response.data;

                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could not Mark attendance");
                        }
                    });

                /* $scope.AjaxGet("/api/DashboardApi/GetUserReportData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.UserReportData = response.data;
                        $scope.data_SalesLeads = [
                            response.data.MonthlyLeadsData.MonthlyOpenLeadsData, //open
                            response.data.MonthlyLeadsData.MonthlyCompletedLeadsData, //completed
                            response.data.MonthlyLeadsData.MonthlyCancelledLeadsData, //Cancelled
                        ];
                        $scope.data_PMDLeads = [
                            response.data.MonthlyLeadsData.MonthlyPendingPMDLeadsData,
                            response.data.MonthlyLeadsData.MonthlyFeasiblePMDLeadsData,
                            response.data.MonthlyLeadsData.MonthlyNotFeasiblePMDLeadsData,
                        ];
 
                        $scope.data_QuotationLeads = [
                            response.data.MonthlyLeadsData.MonthlyPendingQuotationLeadsData,
                            response.data.MonthlyLeadsData.MonthlySubmittedQuotationLeadsData,
                            response.data.MonthlyLeadsData.MonthlyRTSQuotationLeadsData,
                            response.data.MonthlyLeadsData.MonthlyRTPQuotationLeadsData,
                        ];
 
                        $scope.data_ApprovedLeads = [
                            response.data.MonthlyLeadsData.MonthlyPendingApprovalLeadsData,
                            response.data.MonthlyLeadsData.MonthlyApprovedLeadsData,
                            response.data.MonthlyLeadsData.MonthlyNotApprovedLeadsData,
                        ]
 
                        $scope.data_Accounts = [
                            response.data.MonthlyAccountData.MonthlyActiveAccounts, //open
                            response.data.MonthlyAccountData.MonthlyInActiveAccount, //completed
                        ];
                    }
                );
                $scope.GetDataForCustomDate = function (date) {
                    var CustomDate = $scope.GetDatePostFormat(date);
                    console.log(CustomDate);
                }
 
                //Sales-Leads-Data Report
                $scope.labels_SalesLeads = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series_SalesLeads = ['Open', 'Completed', 'Cancelled'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
                //PMD-Leads-Data Report
                $scope.labels_PMDLeads = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series_PMDLeads = ['Pending', 'Feasible', 'NotFeasible'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
 
                //_QuotationLeads Report
                $scope.labels_QuotationLeads = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series_QuotationLeads = ['Pending', 'Subbmitted', 'RTS', 'RTP'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
 
                //ApprovedLeads report
                $scope.labels_ApprovedLeads = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series_ApprovedLeads = ['Pending', 'Approved', 'NotApproved'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
 
                //Accounts-Data Report
                $scope.labels_Accounts = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series_Accounts = ['Open', 'Completed', 'Cancelled'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
 
            }*/
            }
                //sorting and search
                $scope.Sorting = {
                    IsAscending: false,
                    Field: "",
                    AscClass: "active-sort",
                    DescClass: "inactive-sort"
                };
                $scope.SetSortingColoumn = function (Field) {
                    $scope.Sorting.Field = Field;
                    $scope.Sorting.IsAscending = !$scope.Sorting.IsAscending;
                    if ($scope.Sorting.IsAscending) {
                        $scope.Sorting.AscClass = "active-sort";
                        $scope.Sorting.DescClass = "inactive-sort";
                    }
                    if (!$scope.Sorting.IsAscending) {
                        $scope.Sorting.AscClass = "inactive-sort";
                        $scope.Sorting.DescClass = "active-sort";
                    }
                }

                //Payment Dropdown
                $scope.PaymentStatusDropdown = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Completed" }];
                $scope.PaymentTypeDropdown = [{ Id: 0, Name: "Cash" }, { Id: 1, Name: "Cheque" }, { Id: 2, Name: "Online" }];

                //Invoice Dropdown
                $scope.InvoiceStatusDropdown = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Completed" }];

                //Lead Dropdwons
                $scope.BusinessSegmentationDropDown = [{ id: 0, Name: "ISPs" }, { id: 1, Name: "Distributors" }, { id: 2, Name: "SolutionIntegreator" }, { id: 3, Name: "SoftwareHouse" }, { id: 5, Name: "GOVTSector" }, { id: 6, Name: "FinacialInstitues" }, { id: 7, Name: "FMCG" }, { id: 8, Name: "Telcos" }, { id: 9, Name: "CallCenter_BPO" }, { id: 10, Name: "Pharmaceutical" }, { id: 11, Name: "Textile" }]
                $scope.CityDropDown = [{ id: 0, Name: "Karachi" }, { id: 1, Name: "Lahore" }, { id: 2, Name: "Sialkot" }, { id: 3, Name: "Faisalabad" }, { id: 4, Name: "Rawalpindi" }, { id: 5, Name: "Peshawar" }, { id: 6, Name: "SaiduSharif" }, { id: 7, Name: "Multan" }, { id: 8, Name: "Gujranwala" }, { id: 9, Name: "Islamabad" }, { id: 10, Name: "Quetta " }, { id: 11, Name: "Bahawalpur" }, { id: 12, Name: "Sargodha" }, { id: 13, Name: "Mirpur" }, { id: 14, Name: "Chiniot" }, { id: 15, Name: "Sukkur" }, { id: 16, Name: "Larkana " }, { id: 17, Name: "Shekhupura " }, { id: 18, Name: "Jhang " }, { id: 19, Name: "RahimyarKhan" }, { id: 20, Name: "Gujrat" }]
                $scope.DomainDropDown = [{ Id: 0, Name: "SolutionSet" }, { Id: 1, Name: "Connectivity" }]

                //Ticketing Dropdowns; Open,Inprogress,Resolved,FollowUp,OnHold,ReOpen,
                $scope.TicketStatusList = [{ Id: 0, Name: "Open" }, { Id: 1, Name: "Inprogress" }, { Id: 2, Name: "Resolved" }, { Id: 3, Name: "FollowUp" }, { Id: 4, Name: "OnHold" }, { Id: 5, Name: "ReOpen" }];
                $scope.MediaTypes = ["LTE", "JAZZ_PMP", "Radio", "Fiber", "Zong", "Wimax", "Dual Fiber", "Fiber & Radio", "Bulk Bandwidth", "Colocation", "Bulk IP", "Spectrum", "Radio & Zong"];
                $scope.MediumsOfComplaint = ["Hot Line", "E-mail", "Alert", "KAM"];
                $scope.TicketProblems = ["Fiber Damaged", "BTS Flap", "BTS Outage", "CPE Faulty", "Low Stats", "Wi-Fi Issue", "Packet Loss", "Connector Faulty", "IP Issue", "SMTP Issue", "POE/Adaptor Faulty", "Configuration Issue", "Interference Issue", "Coverage Issue", "Slow Browsing", "LAN Cable Unplugged", "Connector Broken", "BTS Congested", "Cable Faulty", "Device Reboot", "Equipment Faulty", "Port Issue", "Latency Issue", "Power Issue", "Bandwidth Choked", "Billing/Limit Issue", "Service Request", "Deployment", "Info Required", "Status Required", "Outgoing blocked", "Link found Operational", "Backend Configuration", "Testing", "Relocation"];
                $scope.TicketStatus = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Resolved" }, { Id: 2, Name: "VisitScheduled" }, { Id: 3, Name: "OnHold" }];
                $scope.TicketRegions = ["Karachi", "Lahore", "Islamabad", "Hyderabad", "Sukkur", "Multan", "Peshawar", "Quetta", "Gujranwala", "Gujrat", "Sialkot", "Faisalabad", "Rahim Yar Khan", "Sargodha"];
                $scope.TicketOwnerShips = ["GCS", "Fariya", "Satcom", "Moblink", "Cybernet", "Brain Net", "Supernet", "Cactus", "Wasila", "Nexlinx", "Wateen", "Client", "Nayatel", "Vision Telecom", "ConnectTel", "Multinet", "Telecard", "Galaxy", "FiberCom", "Witribe", "SharpTel", "Connect", "FOP"];
                $scope.VisitCalls = ["On Call", "Faraz KHI", "Najeeb KHI", "Yousuf KHI", "Hasnain KHI", "Dawood PSW", "Jawad LHR", "Humayon LHR", "Ali Haider SLKT/GNW", "Azhar ISB", "Laraib HYD", "Noman MLT", "Waseem QTA", "Ejaz FSD"];
                $scope.LeadStatusDropdown = [{ Name: "Open", Id: 0 }, { Name: "Cancelled", Id: 1 }, { Name: "Completed", Id: 2 }, { Name: "Closed", Id: 3 }, { Name: "Reverted", Id: 4 }];
                $scope.AccountingCodes = [{ Name: "AIT", Id: "AIT001" }, { Name: "ADVANCE_ACTIVATION_CHARGES", Id: "AAC012" }, { Name: "BANK_CHARGES", Id: "BCR001" }, { Name: "Billing_FUT_Adjustments", Id: "RLR005" }, { Name: "Billing_Adjustment_Others", Id: "RLR003" }, { Name: "Billing_Late_Payment_Surcharge", Id: "RLP011" }, { Name: "Bounce_Cheque", Id: "BCC002" }, { Name: "Churn_Retention_Line_Rent", Id: "RLR005" }, { Name: "CUSTOMER_PAYMENT_NOT_POSTED_BY_DISTRIBUTOR", Id: "ERR001" }, { Name: "ADVANCEACTIVATIONCHARGES", Id: "AAC012" }, { Name: "CHARGE_BACK", Id: "CBK001" }, { Name: "CASH_REFND_FROM_STORE", Id: "SDX008" }, { Name: "ERROR_BANKS", Id: "ERR003" }, { Name: "Error", Id: "ERR002" }, { Name: "EQUIPMENT_PAYMENT_RECVD_AGAINST_LOST_DEVICES", Id: "EQP001" }, { Name: "FINAL_SETTLEMENT", Id: "FSD001" }, { Name: "Fraudulent_Activity", Id: "RLR012" }, { Name: "LINE_RENT", Id: "RLR001" }, { Name: "LINE_RENT_DISCOUNT_SALES_TAX", Id: "DIS001" }, { Name: "LINE_RENT_RECON_PROMO", Id: "RLR008" }, { Name: "Line_Rent_Refund", Id: "RLR004" }, { Name: "OTHER_CHARGES", Id: "OCR001" }, { Name: "OTHER_INCOM", Id: "OCR002" }, { Name: "Refund_Customer_Churn_Bal_Refunded", Id: "SDX007" }, { Name: "PACKAGE_CHANGE", Id: "RLR011" }, { Name: "REPAIR_CHARGES", Id: "REP003" }, { Name: "Security_To_Bill", Id: "SDX006" }, { Name: "TNA", Id: "TNA001" }, { Name: "TERMINATED_TNA", Id: "TNA002" }, { Name: "IIL_Adjustment", Id: "IIL001" }, { Name: "Sales Tax Charge 19.5%", Id: "STC001" }, { Name: "Sindh Sales Tax @ 19.5%  (Exempt due to second schedule of Sindh sales tax on service Act ,2011)", Id: "STC005" }, { Name: "Sales_Tax_Charge_17_on_Supplies", Id: "STC004" }, { Name: "Subscription_One_Time_Charges", Id: "RSC004" }, { Name: "PAYMENT_And_ADJUSTMENT", Id: "PAY002" }, { Name: "PAYMENT", Id: "PAY001" }];

                //Toasters test
                $scope.Pop = function () {
                    toaster.pop('success', "success", "text");
                    toaster.pop('error', "error", "Error in task");
                    toaster.pop('warning', "warning", "this is Warning");
                    toaster.pop('note', "note", "thhis is note");
                }
                $scope.Pop = function () {
                    toaster.pop('success', "success", "text");
                }

                //date moment test
                $scope.date = new moment().format("YYYY/MMM/DD");
                var date = new Date()
                console.log("Connected to lms App base ctrl " + moment(date).format("YYYY-MMM-DD"));

                $scope.GetDatePostFormat = function (date) {
                    return moment(date).format("YYYY-MMM-DD")
                }
                $scope.GetDateTimePostFormat = function (date) {
                    return moment(date).format("YYYY-MMM-DD hh:mm a")
                }
                $scope.MarkAgentSignOff = function () {
                    $scope.AjaxPost("/api/AgentAttendanceApi/SignOffAttendance", {}).then(
                        function (response) {
                            if (response.status == 200) {
                                toaster.pop('success', "success", "Sign Off Time Has been marked!");

                            } else {
                                //alert("Could Not Assign User");
                                toaster.pop('error', "error", "Could not Sign Off");
                            }
                        });
                }

                /* $scope.MarkedAsRead = function () {
                     $scope.AjaxPostBackground("/api/NotificationApi/MarkNotificationRead", null).then(function (response) {
                         console.log(response)
                         $scope.Notifications.TotalNotification = 0;
                     });
                 }*/

                //Excel Export
                $scope.Export = function (fileName, tableId) {
                    var wb = XLSX.utils.table_to_book(document.getElementById(tableId));
                    var sheet = wb.Sheets.Sheet1;
                    var wscols = [{ wch: 10 }, { wch: 15 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }, { wch: 12 }]
                    sheet["!cols"] = wscols;
                    console.log(sheet);
                    console.log(wb);

                    var wbout = XLSX.write(wb, { bookType: 'xlsx', bookSST: true, type: 'binary' });

                    saveAs(new Blob([s2ab(wbout)],
                        { type: "application/octet-stream" }), fileName + '.xlsx');
                }
                function s2ab(s) {
                    var buf = new ArrayBuffer(s.length);
                    var view = new Uint8Array(buf);
                    for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
                    return buf;
                }

                //Accounts Dropdown Filters
                $scope.getAccountDropdown = function () {
                    $scope.AjaxGet("/api/CompanyAccountsApi/GetCompanyAccountsDropdown", null).then(
                        function (response) {
                            console.log(response);
                            $scope.AccountDropdownList = response.data.Data;
                            console.log($scope.AccountDropdownList);
                        });
                }
                $scope.OnAccountChange = function (filter, variabletochange, variablesource, variableToFetch) {
                    if (filter[variablesource] == null) {
                        filter[variabletochange] = "";
                    }
                    else {
                        filter[variabletochange] = filter[variablesource][variableToFetch];
                        console.log(filter);
                    }
                }

                //User Identifier.
                $scope.UserIdentityInit = function () {
                    $scope.AjaxGet("/api/UserApi/GetUserInformation", null).then(
                        function (response) {

                        })
                }

                //Dropdowns For Filteration
                $scope.getAllAgentsDropdown = function () {
                    $scope.AjaxGet("/api/UserApi/GetAllUsersDropdown", null).then(
                        function (response) {
                            console.log(response);
                            $scope.AgentsDropdownList = response.data.Data;
                        });
                }
                $scope.GetllCompaniesDropdown = function () {
                    $scope.AjaxGet("/api/CompanyApi/GetAllCompaniesDropDown", null).then(
                        function (response) {
                            console.log(response);
                            $scope.AllCompaniesDropdownList = response.data.Data;
                        });
                }

                $scope.OnUiSelectedChange = function (object, variabletochange, variablesource, variablesourceField) {
                    object[variabletochange] = object[variablesource][variablesourceField];
                    console.log(object);
                }

                //User Identity
                $scope.UserIdentity = {};
                $scope.UserIdentityInit = function () {
                    $scope.AjaxGet("/api/UserApi/GetUserIdentity", null).then(
                        function (response) {
                            console.log(response);
                            $scope.UserIdentity = response.data;
                        });
                }
            }
            ]);