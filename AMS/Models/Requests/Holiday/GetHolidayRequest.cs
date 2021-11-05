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
                //{
                //    var Holiday = _dbContext.Tax.Where(x => x.Id == request.Id).FirstOrDefault();
                //    response.Id = Tax.Id;
                //    response.Name = Tax.Name;
                //    response.Type = Tax.Type;
                //    response.Amount = Tax.Amount;
                //    response.CreatedBy = Tax.CreatedBy;
                //    response.CreatedAt = Tax.CreatedAt;
                //    response.IsSuccessful = true;
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
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedAt { get; set; }
            public bool IsSuccessful { get; set; }
            public List<string> ValidationErrors { get; set; } = new List<string>();
        }
    }