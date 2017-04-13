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
    public class GetEventByIdQuery
    {
        public class GetEventByIdRequest : IRequest<GetEventByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetEventByIdResponse
        {
            public EventApiModel Event { get; set; } 
        }

        public class GetEventByIdHandler : IAsyncRequestHandler<GetEventByIdRequest, GetEventByIdResponse>
        {
            public GetEventByIdHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetEventByIdResponse> Handle(GetEventByIdRequest request)
            {                
                return new GetEventByIdResponse()
                {
                    Event = EventApiModel.FromEvent(await _context.Events
                    .Include(x => x.Tenant)
                    .Include(x => x.EventLocation)
                    .SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
