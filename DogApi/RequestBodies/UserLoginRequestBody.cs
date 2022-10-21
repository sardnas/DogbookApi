using Newtonsoft.Json;
using Sakur.WebApiUtilities.BaseClasses;

namespace DogApi.RequestBodies
{
    public class UserLoginRequestBody : RequestBody
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }

        public UserLoginRequestBody() { }
    }
}