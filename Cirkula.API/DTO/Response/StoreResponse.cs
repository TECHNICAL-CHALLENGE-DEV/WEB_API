using System.Globalization;
using System;
using System.Text.Json.Serialization;
using Cirkula.API.Data.Models;

namespace Cirkula.API.DTO.Response
{
    
    public class StoreResponse
    {
        public const string FORMAT_HOUR = "hh:mm tt";

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("bannerUrl")]
        public string BannerUrl { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("openTime")]
        public String OpenTime { get; set; }
        [JsonPropertyName("closeTime")]
        public string CloseTime { get; set; }
        [JsonPropertyName("distanceInKm")]
        public double? DistanceInKm { get; set; }
        [JsonPropertyName("isOpen")]
        public bool IsOpen { get; set; } = false;
        public class Builder
        {
            private StoreResponse dto;
            private List<StoreResponse> collection;

            public Builder()
            {
                this.dto = new StoreResponse();
                this.collection = new List<StoreResponse>();
            }
            public Builder(StoreResponse dto)
            {
                this.dto = dto;
                this.collection = new List<StoreResponse>();
            }
            public Builder(List<StoreResponse> collection)
            {
                this.dto = new StoreResponse();
                this.collection = collection;
            }

            public StoreResponse Build() => dto;
            public List<StoreResponse> BuildAll() => collection;

            public static Builder From(Store entity, TimeSpan? currentHour = null)
            {
                var dto = new StoreResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    BannerUrl = entity.BannerUrl,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                };
                var culture = new CultureInfo("en-US");
                dto.OpenTime = new DateTime().Add(entity.OpenTime).ToString(FORMAT_HOUR, culture).ToUpper();
                dto.CloseTime = new DateTime().Add(entity.CloseTime).ToString(FORMAT_HOUR, culture).ToUpper();
                if (currentHour != null)
                    if (entity.OpenTime <= currentHour && currentHour < entity.CloseTime)
                        dto.IsOpen = true;
                return new Builder(dto);
            }

            public static Builder From(IEnumerable<Store> entities)
            {
                var collection = new List<StoreResponse>();
                var currentHour = DateTime.Now.TimeOfDay;

                foreach (var entity in entities)
                    collection.Add(From(entity, currentHour).Build());

                return new Builder(collection);
            }
        }

    }
}
