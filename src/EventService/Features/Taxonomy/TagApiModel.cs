namespace EventService.Features.Taxonomy
{
    public class TagApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
    }
}
