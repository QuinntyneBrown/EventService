using System;
using System.Collections.Generic;
using EventService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class EventTagRef: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Event")]
        public int? EventId { get; set; }

        public int? TagId { get; set; }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Event Event { get; set; }
    }
}
