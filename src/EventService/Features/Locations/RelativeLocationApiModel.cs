using EventService.Data.Model;
using System.Device.Location;

namespace EventService.Features.Locations
{
    public class RelativeLocationApiModel: LocationApiModel
    {                
        public string Distance { get; set; }

        public double OriginLongitude { get; set; }

        public double OriginLatitude { get; set; }

        public static TModel FromLocationAndOrigin<TModel>(Location location, double originLongitude, double originLatitude) where
            TModel : RelativeLocationApiModel, new()
        {
            var model = LocationApiModel.FromLocation<TModel>(location);

            var origin = new GeoCoordinate(originLatitude, originLongitude);

            var destination = new GeoCoordinate(location.Latitude, location.Longitude);

            model.Distance = $"{string.Format("{0:0.#}", origin.GetDistanceTo(destination) / 1000)} KM";

            return model;
        }

        public static RelativeLocationApiModel FromLocationAndOrigin(Location location, double originLongitude, double originLatitude)
            => FromLocationAndOrigin<RelativeLocationApiModel>(location,originLongitude,originLatitude);
    }
}
