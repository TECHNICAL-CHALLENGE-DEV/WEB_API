using Cirkula.API.DTO.Response;
using System.Device.Location;

namespace Cirkula.API.UseCase
{
    public static class StoreUseCase
    {
        public static List<StoreResponse> OrderByPosition(double latitude, double longitude, List<StoreResponse> stores) {

            var origin = new GeoCoordinate(latitude, longitude);
            foreach (var store in stores)
            {
                var storePosition = new GeoCoordinate(store.Latitude, store.Longitude);
                // GetDistance (meters)
                store.DistanceInKm = Math.Round(origin.GetDistanceTo(storePosition) / 1000, 1);
            }
            return stores.OrderBy(x => x.DistanceInKm).ToList();
        }
    }
}
