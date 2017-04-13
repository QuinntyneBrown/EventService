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
    public class GetEventsQuery
    {
        public class GetEventsRequest : IRequest<GetEventsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetEventsResponse
        {
            public ICollection<EventApiModel> Events { get; set; } = new HashSet<EventApiModel>();
        }

        public class GetEventsHandler : IAsyncRequestHandler<GetEventsRequest, GetEventsResponse>
        {
            public GetEventsHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetEventsResponse> Handle(GetEventsRequest request)
            {
                var events = await _context.Events
                    .Include(x => x.Tenant)
                    .Include(x => x.EventLocation)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetEventsResponse()
                {
                    Events = events.Select(x => EventApiModel.FromEvent(x)).ToList()
                };
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
