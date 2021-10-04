using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.AgentTaxes
{
    public class GetAgentTaxesRequest
    {
    }

    public class AgentTaxes {
        public int Id{ get; set; }
        public int AgentId{ get; set; }
        public int TaxId{ get; set; }
        public string AgentName{ get; set; }
        public string TaxName{ get; set; }
    }
}