using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitWell.Models
{
    public class GetPlanIdsResponseModel : BaseResponseModel
    {
        [JsonProperty("plan_ids")]
        public List<string> PlanIds { get; set; }
    }
}
