using ProfitWell.Enum;
using System;

namespace ProfitWell.Models
{
    public class ChurnSubscriptionRequestModel
    {
        public string SubscriptionId { get; set; }

        public string SubscriptionAlias { get; set; }

        public DateTime EffectiveDate { get; set; }

        public ChurnType ChurnType { get; set; }
    }
}
