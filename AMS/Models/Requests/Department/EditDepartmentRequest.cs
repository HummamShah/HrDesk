using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.Department
{
    public class EditDepartmentResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class EditDepartmentRequest
    {
        private AMSEntities db = new AMSEntities();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; internal set; }

        public object RunRequest(EditDepartmentRequest req)
        {
            var response = new EditDepartmentResponse();
            var Department = db.Department.Where(x => x.Id == req.Id).FirstOrDefault();
            Department.Name = req.Name;
            Department.Description = req.Description;
            Department.UpdatedAt = DateTime.Now;
            db.SaveChanges();
            return response;
        }
    }
}
