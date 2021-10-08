using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.AgentIncentives
{
    public class GetAgentIncentivesRequest
    {
    }

    public class AgentIncentives {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int IncentiveId { get; set; }
        public string AgentName { get; set; }
        public string IncentiveName { get; set; }
    }
}