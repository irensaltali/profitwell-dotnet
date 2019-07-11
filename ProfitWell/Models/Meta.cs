using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitWell.Models
{
    public class Meta
    {
        [JsonProperty("data_provider_user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DataProviderUserId { get; set; }
    }
}
