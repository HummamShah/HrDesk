using System;
using System.Collections.Generic;
using System.Linq;
using AMS.Model.Model;

namespace AMS.Models.Requests.Holiday
{
    public class GetHolidayListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();

        public string UserId { get; set; }


        public Object RunRequest(GetHolidayListRequest request)
            {
                var response = new GetHolidayListResponse();
                try
                {
                    response.HolidaysList = new List<Holiday>();
                    var Holidays = _dbContext.Holidays.ToList();
                    foreach (var holiday in Holidays)
                    {
                        var Holiday = new Holiday();
                        Holiday.Id = holiday.Id;
                        Holiday.Date = holiday.Date;
                        Holiday.Remarks = holiday.Remarks;
                        Holiday.AddedBy = holiday.AddedBy;
                        Holiday.AddedAt = holiday.AddedAt;
                        response.HolidaysList.Add(Holiday);
                    }
                    response.IsSuccessful = true;
                }
                catch (Exception e)
                {
                    response.IsSuccessful = false;
                    response.ValidationErrors.Add(e.Message);
                }
                return response;
            }
        }
        public class GetHolidayListResponse
    {
            public bool IsSuccessful { get; set; }
            public List<string> ValidationErrors { get; set; } = new List<string>();
            public List<Holiday> HolidaysList { get; set; }
        }
        public class Holiday
    {
            public int Id { get; set; }
            public DateTime? Date { get; set; }
            public string Remarks { get; set; }
            public string AddedBy { get; set; }
            public DateTime? AddedAt { get; set; }
        }
    }