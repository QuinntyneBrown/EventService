using EventService.Data.Model;
using System;
using System.Device.Location;

namespace EventService.Features.Events
{
    public class EventApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }
        
        public string Description { get; set; }

        public string Abstract { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public double? Distance { get; set; }

        public static TModel FromEvent<TModel>(Event entity) where
            TModel : EventApiModel, new()
        {
            var model = new TModel();

            model.Id = entity.Id;

            model.TenantId = entity.TenantId;

            model.Name = entity.Name;

            model.Address = entity.Address;

            model.City = entity.City;

            model.PostalCode = entity.PostalCode;

            model.Description = entity.Description;

            model.Abstract = entity.Abstract;

            model.Longitude = entity.Longitude;

            model.Latitude = entity.Latitude;

            model.Start = entity.Start;

            model.End = entity.End;

            return model;
        }

        public static EventApiModel FromEventAndOrigin(Event entity, double originLongitude, double originLatitude) {
            var model = FromEvent(entity);
            var origin = new GeoCoordinate(originLatitude, originLongitude);
            var destination = new GeoCoordinate(entity.Latitude, entity.Longitude);
            model.Distance = origin.GetDistanceTo(destination);
            return model;
        }

        public static EventApiModel FromEvent(Event entity)
            => FromEvent<EventApiModel>(entity);

    }
}
