using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProfitWell.Enum;
using ProfitWell.Helpers;
using System;
using System.Collections.Generic;

namespace ProfitWell.Models
{
    public class UpdateSubscriptionResponseModel : BaseResponseModel
    {
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("user_alias", NullValueHandling = NullValueHandling.Ignore)]
        public string UserAlias { get; set; }

        [JsonProperty("subscription_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SubscriptionId { get; set; }

        [JsonProperty("subscription_alias", NullValueHandling = NullValueHandling.Ignore)]
        public string SubscriptionAlias { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        [JsonProperty("plan_interval")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanInterval PlanInterval { get; set; }

        [JsonProperty("plan_currency", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencySymbol PlanCurrency { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(PriceConverter))]
        public decimal Price { get; set; }

        [JsonProperty("effective_date")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public Meta Meta { get; set; }
    }
}
