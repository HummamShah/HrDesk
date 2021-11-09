﻿using AMS.Model.Model;
using AMS.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Holiday
{
    public class EditHolidayResponse : Response { }

    public class EditHolidayRequest
    {
        private AMSEntities _dbchanges = new AMSEntities();
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int Id { get; set; }
        
        public Object RunRequest(EditHolidayRequest request)
        {
            var response = new EditHolidayResponse();
            response.ValidationErrors = new List<string>();
            try
            {
                var Holiday = _dbchanges.Holidays.Where(x => x.Id == request.Id).FirstOrDefault();
                Holiday.Date = request.Date;
                Holiday.Remarks = request.Remarks;
                _dbchanges.SaveChanges();
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