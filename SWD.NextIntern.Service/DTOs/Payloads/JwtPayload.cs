using Newtonsoft.Json;

namespace SWD.NextIntern.Service.DTOs.Payloads
{
    public class JwtPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
