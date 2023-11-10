using Microsoft.AspNetCore.Mvc;

namespace Cirkula.API.DTO.Request.Store
{
    public class GetStoreRequest
    {
        [FromQuery(Name = "latitude")]
        public double? Latitude { get; set; }

        [FromQuery(Name = "longitude")]
        public double? Longitude { get; set; }
    }
}
