using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.Department
{
    public class AddDepartmentResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddDepartmentRequest
    {
        private AMSEntities db = new AMSEntities();
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; internal set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public object RunRequest(AddDepartmentRequest req)
        {
            var response = new AddDepartmentResponse();
            var Department = new AMS.Model.Model.Department();
            Department.Name = req.Name;
            Department.Description = Description;
            Department.CreatedBy = req.CreatedBy;
            Department.CreatedAt = DateTime.Now;
            Department.Date = DateTime.Today;
            db.Department.Add(Department);
            db.SaveChanges();
            return response;
        }
    }
}
