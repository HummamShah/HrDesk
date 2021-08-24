using AMS.Model.Requests.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class DepartmentApiController : ApiController
    {
        [HttpPost]
        public object AddDepartment([FromBody] AddDepartmentRequest req) //If not working remove frombody
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetDepartment([FromUri] GetDepartmentRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditDepartment([FromBody] EditDepartmentRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetDepartmentsDropdown([FromUri] GetDepartmentDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}