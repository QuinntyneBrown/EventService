using EventService.Data.Model;

namespace EventService.Features.Taxonomy
{
    public class CategoryApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
    }
}
