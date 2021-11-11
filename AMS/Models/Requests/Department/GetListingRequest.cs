using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Model.Requests.Department
{
    public class GetListingResponse
    {
        public List<Department> Data { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string PositionsNames { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public class GetListingRequest
    {
        private AMSEntities db = new AMSEntities();
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<Department>();
            var Department = db.Department.ToList();
            foreach (var depart in Department)
            {
                var temp = new Department();
                temp.Id = depart.Id;
                temp.Name = depart.Name;
                temp.Description = depart.Description;
                temp.CreatedBy = depart.CreatedBy;
                temp.CreatedAt = depart.CreatedAt;
                temp.PositionsNames = String.Join(", ", db.DepartmentPositions.Where(x => x.DeptId == depart.Id).Select(y => y.PositionName));
                
                response.Data.Add(temp);
            }
            return response;
        }
    }
}
