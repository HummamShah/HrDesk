using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Model.Requests.Department
{
    public class GetDepartmentResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DepartmentPositions> PositionsList = new List<DepartmentPositions>();
    }
    public class GetDepartmentRequest
    {
        private AMSEntities db = new AMSEntities();
        public int Id { get; set; }
        public object RunRequest(GetDepartmentRequest req)
        {
            var response = new GetDepartmentResponse();
            var Department = db.Department.Where(x => x.Id == req.Id).FirstOrDefault();
            response.Id = Department.Id;
            response.Name = Department.Name;
            response.Description = Department.Description;
            response.PositionsList = new List<DepartmentPositions>();
            var Positions = db.DepartmentPositions.Where(x => x.DeptId == req.Id).ToList();
            foreach (var Position in Positions)
            {
                var position = new DepartmentPositions();
                position.Id = Position.Id;
                position.DeptId = Position.DeptId;
                position.DeptName = Position.DeptName;
                position.PositionOrder = Position.PositionOrder;
                position.PositionName = Position.PositionName;
                position.JobDescription = Position.JobDescription;
                position.CreatedBy = Position.CreatedBy;
                position.CreatedAt = Position.CreatedAt;
                response.PositionsList.Add(position);
            }
            return response;
        }
    }
}
