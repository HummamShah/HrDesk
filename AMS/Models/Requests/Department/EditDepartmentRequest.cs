using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<DepartmentPositions> PositionsList = new List<DepartmentPositions>();
        public object RunRequest(EditDepartmentRequest req)
        {
            var response = new EditDepartmentResponse();
            var Department = db.Department.Where(x => x.Id == req.Id).FirstOrDefault();
            Department.Name = req.Name;
            Department.Description = req.Description;
            Department.UpdatedAt = DateTime.Now;

            // removing the previously added positions
            var Positions = db.DepartmentPositions.Where(x => x.DeptId == req.Id).ToList();
            foreach (var position in Positions)
            {
                db.DepartmentPositions.Remove(position);
                db.SaveChanges();
            }

            // adding department positions
            foreach (var deptPosition in req.PositionsList)
            {
                var DeptPosition = new DepartmentPositions();
                DeptPosition.DeptId = req.Id;
                DeptPosition.DeptName = req.Name;
                DeptPosition.PositionOrder = deptPosition.PositionOrder;
                DeptPosition.PositionName = deptPosition.PositionName;
                DeptPosition.JobDescription = deptPosition.JobDescription;
                db.DepartmentPositions.Add(DeptPosition);
                db.SaveChanges();
            }

            db.SaveChanges();
            return response;
        }
    }
}
