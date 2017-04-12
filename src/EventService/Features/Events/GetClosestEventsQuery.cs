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
        }

        public class GetClosestEventsResponse
        {
            public GetClosestEventsResponse()
            {

            }
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
                throw new System.NotImplementedException();
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
