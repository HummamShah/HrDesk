using AMS.Model.Model;
using AMS.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Holiday
{
    public class DeleteHolidayResponse : Response { }
    
    public class DeleteHolidayRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id { get; set; }

        public Object RunRequest(DeleteHolidayRequest request)
        {
            var response = new DeleteHolidayResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Holiday = _dbContext.Holidays.Where(x => x.Id == request.Id).FirstOrDefault();
                _dbContext.Holidays.Remove(Holiday);
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch (Exception e)
            {
                response.ValidationErrors.Add(e.Message.ToString());
                response.Success = false;
            }
            return response;
        }
       
    }
}