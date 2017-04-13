using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using static EventService.Features.Geolocation.GetAddressFromLatitudeAndLongitudeQuery;
using EventService.Features.Geolocation;
using static EventService.Features.Geolocation.GetLongLatCoordinatesQuery;

namespace EventService.Features.Events
{
    public class GetClosestEventsQuery
    {
        public class GetClosestEventsRequest : IRequest<GetClosestEventsResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public string Address { get; set; }
        }

        public class GetClosestEventsResponse
        {
            public ICollection<ClosetEventApiModel> Events { get; set; } = new HashSet<ClosetEventApiModel>();
        }

        public class GetClosestEventsHandler : IAsyncRequestHandler<GetClosestEventsRequest, GetClosestEventsResponse>
        {
            public GetClosestEventsHandler(EventServiceContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<GetClosestEventsResponse> Handle(GetClosestEventsRequest request)
            {
                var utcNow = DateTime.UtcNow;
                
                var events = await _context.Events
                    .Include(x => x.Tenant)
                    .Include(x=>x.EventLocation)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId && x.Start > utcNow)
                    .ToListAsync();

                var longitudeAndLatitude = await _mediator.Send(
                    new GetLongLatCoordinatesRequest() { Address = request.Address });

                return new GetClosestEventsResponse()
                {
                    Events = events.Select(x => ClosetEventApiModel.FromEventAndOriginCoordinates(x, longitudeAndLatitude.Longitude, longitudeAndLatitude.Latitude))
                    .OrderBy( x => x.EventLocation.Distance)
                    .ToList()
                };
            }

            protected readonly IMediator _mediator;
            protected readonly EventServiceContext _context;
        }

    }

}
