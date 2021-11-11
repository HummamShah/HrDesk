using AMS.Model.Model;
using System;
using System.Collections.Generic;

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

        public List<DepartmentPosition> PositionsList = new List<DepartmentPosition>();
        public object RunRequest(AddDepartmentRequest req)
        {
            var response = new AddDepartmentResponse();
            var Department = new Model.Department();
            Department.Name = req.Name;
            Department.Description = Description;
            Department.CreatedBy = req.CreatedBy;
            Department.CreatedAt = DateTime.Now;
            Department.Date = DateTime.Today;
            var Dept = db.Department.Add(Department);
            db.SaveChanges();

            // adding department positions
            foreach (var deptPosition in req.PositionsList)
            {
                var DeptPosition = new DepartmentPositions();
                DeptPosition.DeptId = Dept.Id;
                DeptPosition.DeptName = Dept.Name;
                DeptPosition.PositionOrder = deptPosition.PositionOrder;
                DeptPosition.PositionName = deptPosition.PositionName;
                DeptPosition.JobDescription = deptPosition.JobDescription;
                DeptPosition.CreatedAt= DateTime.Now;
                DeptPosition.CreatedBy = req.CreatedBy;
                db.DepartmentPositions.Add(DeptPosition);
                db.SaveChanges();
            }

            return response;
        }
    }
    public class DepartmentPosition {
        public int PositionOrder { get; set; }
        public string PositionName { get; set; }
        public string JobDescription { get; set; }
    }
}
