using System.Text.Json.Serialization;

namespace Cirkula.API.DTO.Request.Store
{
    public class PutStoreRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("bannerUrl")]
        public string BannerUrl { get; set; }
    }
}
