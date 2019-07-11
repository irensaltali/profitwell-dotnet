using Newtonsoft.Json;

namespace ProfitWell.Models
{
    public class Meta
    {
        [JsonProperty("data_provider_user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DataProviderUserId { get; set; }

        [JsonProperty("churn_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ChurnType { get; set; }
    }
}
