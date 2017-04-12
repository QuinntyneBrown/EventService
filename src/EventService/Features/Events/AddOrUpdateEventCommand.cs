using MediatR;
using EventService.Data;
using EventService.Data.Model;
using EventService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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
                    .SingleOrDefaultAsync(x => x.Id == request.Event.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Events.Add(entity = new Event() { TenantId = tenant.Id });
                }

                var longLatResponse = await _mediator.Send(new GetLongLatCoordinatesRequest() { Address = $"{request.Event.Address},{request.Event.City},{request.Event.Province},{request.Event.PostalCode}" });

                entity.Longitude = longLatResponse.Longitude;

                entity.Latitude = longLatResponse.Latitude;

                entity.Name = request.Event.Name;

                entity.Address = request.Event.Address;

                entity.City = request.Event.City;

                entity.Province = request.Event.Province;

                entity.PostalCode = request.Event.PostalCode;

                entity.Description = request.Event.Description;

                await _context.SaveChangesAsync();

                return new AddOrUpdateEventResponse();
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
            protected readonly IMediator _mediator;
        }

    }

}
