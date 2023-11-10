using System.Text.Json.Serialization;

namespace Cirkula.API.DTO.Request.Store
{
    public class PostStoreRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("bannerUrl")]
        public string BannerUrl { get; set; } = null!;
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("openTime")]
        public string OpenTime { get; set; }
        [JsonPropertyName("closeTime")]
        public string CloseTime { get; set; }
    }
}
