using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
            return response;
        }
    }
}
