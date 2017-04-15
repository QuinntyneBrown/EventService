using EventService.Features.Taxonomy;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventService.Data.Clients
{
    public interface ITaxonomyServiceClient
    {
        Task<List<CategoryApiModel>> GetCategories(Guid tenantUniqueId);
        Task<List<TagApiModel>> GetTags(Guid tenantUniqueId);
    }

    public class TaxonomyServiceClient: ITaxonomyServiceClient
    {
        public TaxonomyServiceClient(HttpClient client)
        {
            _client = client;
        }

        protected readonly HttpClient _client;

        public async Task<List<CategoryApiModel>> GetCategories(Guid tenantUniqueId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagApiModel>> GetTags(Guid tenantUniqueId)
        {
            throw new NotImplementedException();
        }
    }
}
