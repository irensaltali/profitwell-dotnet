using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProfitWell.Enum;
using ProfitWell.Helpers;
using System;

namespace ProfitWell.Models
{
    public class UpdateSubscriptionRequestModel
    {
        [JsonIgnore]
        public string SubscriptionId { get; set; }

        [JsonIgnore]
        public string SubscriptionAlias { get; set; }

        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        [JsonProperty("plan_interval")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanInterval PlanInterval { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(PriceConverter))]
        public decimal Price { get; set; }

        [JsonProperty("effective_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime EffectiveDate { get; set; }
    }
}
