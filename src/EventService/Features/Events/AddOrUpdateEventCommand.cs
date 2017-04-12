using EventService.Data;
using EventService.Data.Model;
using EventService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

using static EventService.Features.Geolocation.GetLongLatCoordinatesQuery;

namespace EventService.Features.Events
{
    public class AddOrUpdateEventCommand
    {
        public class AddOrUpdateEventRequest : IRequest<AddOrUpdateEventResponse>
        {
            public EventApiModel Event { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateEventResponse { }

        public class AddOrUpdateEventHandler : IAsyncRequestHandler<AddOrUpdateEventRequest, AddOrUpdateEventResponse>
        {
            public AddOrUpdateEventHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateEventResponse> Handle(AddOrUpdateEventRequest request)
            {
                var entity = await _context.Events
                    .Include(x => x.Tenant)
                    .Include(x=>x.EventLocation)
                    .SingleOrDefaultAsync(x => x.Id == request.Event.Id && x.Tenant.UniqueId == request.TenantUniqueId);

                var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);

                if (entity == null) {                    
                    _context.Events.Add(entity = new Event() { TenantId = tenant.Id });
                }

                entity.EventLocation = entity.EventLocation ?? new EventLocation() { TenantId = tenant.Id };

                var longLatResponse = await _mediator.Send(new GetLongLatCoordinatesRequest() { Address = $"{request.Event.Address},{request.Event.City},{request.Event.Province},{request.Event.PostalCode}" });
                
                entity.Name = request.Event.Name;

                entity.Description = request.Event.Description;

                entity.Abstract = request.Event.Abstract;

                entity.Start = request.Event.Start;

                entity.End = request.Event.End;

                entity.ImageUrl = request.Event.ImageUrl;
                
                entity.EventLocation.Longitude = longLatResponse.Longitude;

                entity.EventLocation.Latitude = longLatResponse.Latitude;

                entity.EventLocation.Address = request.Event.Address;

                entity.EventLocation.City = request.Event.City;

                entity.EventLocation.Province = request.Event.Province;

                entity.EventLocation.PostalCode = request.Event.PostalCode;


                await _context.SaveChangesAsync();

                return new AddOrUpdateEventResponse();
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
            protected readonly IMediator _mediator;
        }
    }
}
