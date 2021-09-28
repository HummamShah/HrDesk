using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.AgentAttendance
{
    public class GetEmployessListRequest
    {

        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetEmployessListRequest request)
        {
            var response = new GetEmployessListResponse();
            try
            {
                var Employees = _dbContext.Agent;
                response.EmployeesList = new List<Employees>();

                foreach (var employee in Employees) {
                    var Employee = new Employees();
                    Employee.Id = employee.Id;
                    Employee.Name = employee.FisrtName + " " + employee.LastName;
                    response.EmployeesList.Add(Employee);
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
    public class GetEmployessListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<Employees> EmployeesList { get; set; }
        public List<string> ValidationErrors { get; set; }
    }

    public class Employees {
        public int Id{ get; set; }
        public string Name{ get; set; }
    }
}