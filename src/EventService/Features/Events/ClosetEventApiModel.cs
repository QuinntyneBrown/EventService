using EventService.Data.Model;
using EventService.Features.Locations;
using System;

namespace EventService.Features.Events
{
    public class ClosetEventApiModel
    {

        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Abstract { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public RelativeLocationApiModel EventLocation { get; set; }

        public static TModel FromClosetEvent<TModel>(Event entity, double originLongitude, double originLatitude) where
            TModel : ClosetEventApiModel, new()
        {
            var model = new TModel();

            model.Id = entity.Id;

            model.TenantId = entity.TenantId;

            model.Name = entity.Name;

            model.Description = entity.Description;

            model.Abstract = entity.Abstract;

            model.Start = entity.Start;

            model.End = entity.End;

            model.EventLocation = RelativeLocationApiModel.FromLocationAndOrigin(entity.EventLocation,originLongitude,originLatitude);

            return model;
        }

        public static ClosetEventApiModel FromEventAndOriginCoordinates(Event entity, double originLongitude, double originLatitude)
            => FromClosetEvent<ClosetEventApiModel>(entity, originLongitude, originLatitude);

    }
}
