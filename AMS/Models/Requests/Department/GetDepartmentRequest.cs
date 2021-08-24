using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.Department
{
    public class GetDepartmentResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class GetDepartmentRequest
    {
        private AMSEntities db = new AMSEntities();
        public int Id { get; set; }
        public object RunRequest(GetDepartmentRequest req)
        {
            var response = new GetDepartmentResponse();
            var Department = db.Department.Where(x => x.Id == req.Id).FirstOrDefault();
            response.Name = Department.Name;
            response.Description = Department.Description;
            return response;
        }
    }
}
