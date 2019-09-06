using Newtonsoft.Json;

namespace ProfitWell.Models
{
    public class UpdateUserRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public string UserAlias { get; set; }
        /// <summary>
        /// Email - The new email address of the user. (This can actually be any sort of text, not necessarily an email address.
        /// Some prefer to store a name here instead.) This will be the display text that is used on the Customers tab.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
