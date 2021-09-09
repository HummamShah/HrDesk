using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Models.Requests.AgentAttendance
{
    public class GetSummaryRequest  
    {

        private AMSEntities _dbContext = new AMSEntities();
        public string UserId{ get; set; }
        public int Month{ get; set; }
        public int Year{ get; set; }
        public Object RunRequest(GetSummaryRequest request)
        {
            var response = new GetSummaryResponse();
            try
            {
                var id = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                var Attendances = _dbContext.AgentAttendance.Where(x => x.AgentId == id).ToList();
                if (request.Month == 1) {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 28)).ToList();
                }
                else if (request.Month == 0 || request.Month == 2|| request.Month == 4 || request.Month == 6 || request.Month == 7 || request.Month == 9 || request.Month == 11) {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 31)).ToList();
                }
                else if (request.Month == 3 || request.Month == 5 || request.Month == 8 || request.Month == 10)
                {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 30)).ToList();
                }
                if (Attendances.Count != 0)
                {
                    response.AbsentCount = Attendances.Where(x => x.IsAbsent == true).Count();
                    response.PresentCount = Attendances.Where(x => x.IsPresent == true).Count();
                    response.LateCount = Attendances.Where(x => x.IsLate == true).Count();
                    response.ExcuseCount = Attendances.Where(x => x.IsExcused == true).Count();
                    response.IsSuccessful = true;
                }
                else {
                    response.AttendanceIsEmpty = true;
                    response.IsSuccessful = true;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.ValidationErrors.Add(e.Message);
            }
            return response;
        }
    }
    public class GetSummaryResponse
    {
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int ExcuseCount { get; set; }
        public bool IsSuccessful { get; set; }
        public bool AttendanceIsEmpty { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}