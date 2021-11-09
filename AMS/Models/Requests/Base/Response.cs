using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Base
{
    public class Response
    {
        public bool Success { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}