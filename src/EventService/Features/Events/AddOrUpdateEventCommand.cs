using MediatR;
using EventService.Data;
using EventService.Data.Model;
using EventService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

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

                entity.Name = request.Event.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateEventResponse();
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
