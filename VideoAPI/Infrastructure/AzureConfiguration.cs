using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAPI.Infrastructure
{
    public class AzureConfiguration
    {
        public string AadClientId { get; set; }
        public string AadEndpoint { get; set; }
        public string AadSecret { get; set; }
        public string AadTenantId { get; set; }
        public string AccountName { get; set; }
        public string ArmAadAudience { get; set; }
        public string ArmEndpoint { get; set; }
        public string Region { get; set; }
        public string ResourceGroup { get; set; }
        public string SubscriptionId { get; set; }
        public string StreamingEndPointName { get; set; }
    }
}
