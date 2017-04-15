using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using EventService.Data.Clients;

namespace EventService.Features.Taxonomy
{
    public class GetTagsQuery
    {
        public class GetTagsRequest : IRequest<GetTagsResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class GetTagsResponse
        {
            public List<TagApiModel> Tags { get; set; }
        }

        public class GetTagsHandler : IAsyncRequestHandler<GetTagsRequest, GetTagsResponse>
        {
            public GetTagsHandler(ITaxonomyServiceClient client, ICache cache)
            {
                _client = client;
                _cache = cache;
            }

            public async Task<GetTagsResponse> Handle(GetTagsRequest request)
            {
                var tags = await _client.GetTags(request.TenantUniqueId);

                return new GetTagsResponse()
                {
                    Tags = tags
                };
            }

            private readonly ITaxonomyServiceClient _client;
            private readonly ICache _cache;

        }

    }

}
