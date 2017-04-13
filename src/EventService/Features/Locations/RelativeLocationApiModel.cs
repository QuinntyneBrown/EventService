using EventService.Data.Model;
using System.Device.Location;

namespace EventService.Features.Locations
{
    public class RelativeLocationApiModel: LocationApiModel
    {                
        public double Distance { get; set; }

        public double OriginLongitude { get; set; }

        public double OriginLatitude { get; set; }

        public static TModel FromLocationAndOrigin<TModel>(Location location, double originLongitude, double originLatitude) where
            TModel : RelativeLocationApiModel, new()
        {
            var model = LocationApiModel.FromLocation<TModel>(location);
            var origin = new GeoCoordinate(originLatitude, originLongitude);
            var destination = new GeoCoordinate(location.Latitude, location.Longitude);
            model.Distance = origin.GetDistanceTo(destination);
            return model;
        }

        public static RelativeLocationApiModel FromLocationAndOrigin(Location location, double originLongitude, double originLatitude)
            => FromLocationAndOrigin<RelativeLocationApiModel>(location,originLongitude,originLatitude);
    }
}
