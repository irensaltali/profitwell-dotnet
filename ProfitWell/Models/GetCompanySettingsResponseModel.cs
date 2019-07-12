using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProfitWell.Enum;

namespace ProfitWell.Models
{
    public class GetCompanySettingsResponseModel : BaseResponseModel
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencySymbol CurrencySymbol { get; set; }
    }
}
