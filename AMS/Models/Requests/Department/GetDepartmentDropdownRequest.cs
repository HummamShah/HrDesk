using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Model.Requests.Department
{
    public class GetDepartmentDropdownResponse
    {
        public List<DepartmentDropdown> Data { get; set; }
    }
    public class DepartmentDropdown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DepartmentPositions> PositionsList = new List<DepartmentPositions>();
    }
    public class GetDepartmentDropdownRequest
    {
        private AMSEntities db = new AMSEntities();
        public object RunRequest(GetDepartmentDropdownRequest req)
        {
            var response = new GetDepartmentDropdownResponse();
            response.Data = new List<DepartmentDropdown>();
            var Department = db.Department.ToList();
            foreach (var depart in Department)
            {
                var temp = new DepartmentDropdown();
                temp.Id = depart.Id;
                temp.Name = depart.Name;
                response.Data.Add(temp);

                var Positions = db.DepartmentPositions.Where(x => x.DeptId == depart.Id).ToList();
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
                    temp.PositionsList.Add(position);
                }
            }
            return response;
        }
    }
}
