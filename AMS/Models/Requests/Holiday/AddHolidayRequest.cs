using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Models.Requests.Holiday
{
    public class AddHolidayRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId{ get; set; }
        public DateTime Date{ get; set; }
        public string Remarks{ get; set; }
        public Object RunRequest(AddHolidayRequest request)
        {
            var response = new AddHolidayResponse();
            try
            {
                var ExistingHolidays = _dbContext.Holidays.ToList();
                foreach (var date in ExistingHolidays) {
                    if (request.Date == date.Date)
                    {
                        response.ValidationErrors.Add("This date has already been added!");
                        response.IsSuccessful = false;
                        return response;
                    }
                }
                var Holiday = new Holidays();
                Holiday.Date = request.Date;
                Holiday.Remarks = request.Remarks;
                Holiday.AddedAt = DateTime.Now;
                Holiday.AddedBy = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                _dbContext.Holidays.Add(Holiday);
                _dbContext.SaveChanges();
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
    public class AddHolidayResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
     
}