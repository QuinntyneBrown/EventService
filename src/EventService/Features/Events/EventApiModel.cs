using EventService.Data.Model;

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

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public static TModel FromEvent<TModel>(Event e) where
            TModel : EventApiModel, new()
        {
            var model = new TModel();
            model.Id = e.Id;
            model.TenantId = e.TenantId;
            model.Name = e.Name;
            return model;
        }

        public static EventApiModel FromEvent(Event e)
            => FromEvent<EventApiModel>(e);

    }
}
