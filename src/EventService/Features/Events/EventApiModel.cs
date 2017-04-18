using EventService.Data.Model;
using EventService.Features.Locations;
using System;
using System.Device.Location;

namespace EventService.Features.Events
{
    public class EventApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }
        
        public string Description { get; set; }

        public string Abstract { get; set; }
        
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public LocationApiModel EventLocation { get; set; }

        public static TModel FromEvent<TModel>(Event entity) where
            TModel : EventApiModel, new()
        {
            var model = new TModel();

            model.Id = entity.Id;

            model.TenantId = entity.TenantId;

            model.Name = entity.Name;

            model.DisplayName = entity.DisplayName;

            model.Description = entity.Description;

            model.Abstract = entity.Abstract;

            model.Start = entity.Start;

            model.End = entity.End;

            model.Url = entity.Url;

            model.EventLocation = LocationApiModel.FromLocation(entity.EventLocation);
            
            return model;
        }

        
        public static EventApiModel FromEvent(Event entity)
            => FromEvent<EventApiModel>(entity);

    }
}
