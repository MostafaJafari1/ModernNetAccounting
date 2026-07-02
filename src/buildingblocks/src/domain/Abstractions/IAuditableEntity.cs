using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Abstractions
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
