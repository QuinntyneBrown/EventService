﻿using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EventService.Features.Users
{
    public class GetUserByUsernameQuery
    {
        public class GetUserByUsernameRequest : IRequest<GetUserByUsernameResponse>
        {
            public string Username { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetUserByUsernameResponse
        {
            public UserApiModel User { get; set; }
        }

        public class GetUserByUsernameHandler : IAsyncRequestHandler<GetUserByUsernameRequest, GetUserByUsernameResponse>
        {
            public GetUserByUsernameHandler(IEventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetUserByUsernameResponse> Handle(GetUserByUsernameRequest request)
            {
                return new GetUserByUsernameResponse()
                {
                    User = UserApiModel.FromUser(await _context.Users.SingleAsync(x=>x.Username == request.Username && x.TenantId == request.TenantId))
                };
            }

            private readonly IEventServiceContext _context;
            private readonly ICache _cache;
        }
    }
}