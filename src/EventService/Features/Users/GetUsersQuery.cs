using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EventService.Features.Users
{
    public class GetUsersQuery
    {
        public class GetUsersRequest : IRequest<GetUsersResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetUsersResponse
        {
            public ICollection<UserApiModel> Users { get; set; } = new HashSet<UserApiModel>();
        }

        public class GetUsersHandler : IAsyncRequestHandler<GetUsersRequest, GetUsersResponse>
        {
            public GetUsersHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetUsersResponse> Handle(GetUsersRequest request)
            {
                var users = await _context.Users
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetUsersResponse()
                {
                    Users = users.Select(x => UserApiModel.FromUser(x)).ToList()
                };
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
