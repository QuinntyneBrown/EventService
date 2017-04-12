using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EventService.Features.Events
{
    public class GetClosestEventsQuery
    {
        public class GetClosestEventsRequest : IRequest<GetClosestEventsResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string Address { get; set; }
        }

        public class GetClosestEventsResponse
        {
            public ICollection<EventApiModel> Events { get; set; } = new HashSet<EventApiModel>();
        }

        public class GetClosestEventsHandler : IAsyncRequestHandler<GetClosestEventsRequest, GetClosestEventsResponse>
        {
            public GetClosestEventsHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetClosestEventsResponse> Handle(GetClosestEventsRequest request)
            {
                var utcNow = DateTime.UtcNow;

                var events = await _context.Events
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId && x.Start > utcNow)
                    .ToListAsync();
                
                return new GetClosestEventsResponse()
                {
                    Events = events.Select(x => EventApiModel.FromEventAndOrigin(x,request.Longitude, request.Latitude))
                    .OrderBy(x=>x.Distance)
                    .ToList()
                };
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
