using Newtonsoft.Json;

namespace ProfitWell.Models
{
    public class UpdateUserRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public string UserAlias { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
