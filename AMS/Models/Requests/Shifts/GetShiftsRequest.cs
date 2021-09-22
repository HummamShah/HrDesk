using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Shifts
{
    public class GetShiftsRequest
    {

        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetShiftsRequest request)
        {
            var response = new GetShiftsResponse();
            try
            {
                response.ShiftsList = new List<Shift>();
                var Shifts = _dbContext.Shifts;
                foreach (var shift in Shifts) {
                    var Shift = new Shift();
                    Shift.Id = shift.Id;
                    Shift.Name = shift.Name;
                    Shift.StartTime = shift.StartTime;
                    Shift.EndTime = shift.EndTime;
                    response.ShiftsList.Add(Shift);
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
    public class GetShiftsResponse
    {
        public bool IsSuccessful { get; set; }
        public List<Shift> ShiftsList { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }

    public class Shift {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}