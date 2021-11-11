using System;
using System.Collections.Generic;
using System.Linq;
using AMS.Model.Model;
namespace AMS.Models.Requests.Holiday
{
    public class GetHolidayRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
            public int Id { get; set; }
            public Object RunRequest(GetHolidayRequest request)
            {
                var response = new GetHolidayResponse();
            try { 
                }
                catch (Exception e)
                {
                    response.IsSuccessful = false;
                    response.ValidationErrors.Add(e.Message);
                }
                return response;
            }
        }
        public class GetHolidayResponse
        {
            public bool IsSuccessful { get; set; }
            public List<string> ValidationErrors { get; set; } = new List<string>();
        }
    }