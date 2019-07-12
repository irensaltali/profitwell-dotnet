using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProfitWell.Models
{
    public class BaseResponseModel
    {
        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        [JsonProperty("non_field_errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> NonFieldErrors { get; set; }

        [JsonIgnore]
        public string Error { get; set; }
    }
}
