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
    public class GetCategoriesQuery
    {
        public class GetCategoriesRequest : IRequest<GetCategoriesResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class GetCategoriesResponse
        {
            public List<CategoryApiModel> Categories { get; set; }
        }

        public class GetCategoriesHandler : IAsyncRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
        {
            public GetCategoriesHandler(ITaxonomyServiceClient client, ICache cache)
            {
                _client = client;
                _cache = cache;
            }

            public async Task<GetCategoriesResponse> Handle(GetCategoriesRequest request)
            {
                List<CategoryApiModel> categories = await _client.GetCategories(request.TenantUniqueId);
                return new GetCategoriesResponse()
                {
                    Categories = categories
                };
            }

            private readonly ITaxonomyServiceClient _client;
            private readonly ICache _cache;
        }

    }

}
