using EventService.Data.Model;

namespace EventService.Features.Locations
{
    public class LocationApiModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public static TModel FromLocation<TModel>(Location location) where
            TModel : LocationApiModel, new()
        {
            var model = new TModel();

            model.Name = location.Name;

            model.Address = location.Address;

            model.City = location.City;

            model.Province = location.Province;

            model.PostalCode = location.PostalCode;

            model.Longitude = location.Longitude;

            model.Latitude = location.Latitude;

            return model;
        }

        public void ToLocation(Location location)
        {
            location.Name = Name;

            location.Address = Address;

            location.City = City;

            location.Province = Province;

            location.PostalCode = PostalCode;

            location.Longitude = Longitude;

            location.Latitude = Latitude;

        }

        public static LocationApiModel FromLocation(Location location)
            => FromLocation<LocationApiModel>(location);

    }
}
