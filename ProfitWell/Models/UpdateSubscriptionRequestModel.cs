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
        /// <summary>
        /// PlanId (string) - The ID of the plan that the user is switching to. 
        /// If the user has not switched plans, but has added or subtracted seats on his current plan, you should use the same plan_id as before.
        /// </summary>
        [JsonProperty("plan_id")]
        public string PlanId { get; set; }
        /// <summary>
        /// PlanInterval - The billing cycle for this plan. The two options are "month" and "year".
        /// </summary>
        [JsonProperty("plan_interval")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanInterval PlanInterval { get; set; }
        /// <summary>
        /// Status - The status of the subscription. Currently, the only valid status when upgrading/downgrading a subscription is "active".
        /// Down the line, we would like to add more statuses. (Note that the API may return statuses other than "active" when retrieving a customer's 
        /// subscription history, such as "trialing", "churned_voluntary" and "churned_delinquent".) - Optional)
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        /// <summary>
        /// Price - The new amount that you bill your user, per billing period, in cents. Keep the value of annual plans unmodified, meaning a $120.00 / 
        /// year plan should have a value of 12000. Note: This should always be the full value of the plan, even if the user changes plans mid-billing cycle and is
        /// charged/credited a prorated amount.
        /// </summary>
        [JsonProperty("value")]
        [JsonConverter(typeof(PriceConverter))]
        public decimal Price { get; set; }
        /// <summary>
        /// EffectiveDate - The date that this update takes effect, in UNIX timestamp format. (E.g. For 2018-01-1 00:00:00, the value would be 1514764800).
        /// </summary>
        [JsonProperty("effective_date")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))]
        public DateTime EffectiveDate { get; set; }
    }
}
