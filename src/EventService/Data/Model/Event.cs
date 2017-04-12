using EventService.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Event: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public string Abstract { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
        
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
