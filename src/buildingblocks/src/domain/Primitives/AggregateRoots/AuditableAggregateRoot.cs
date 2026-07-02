using BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Primitives.AggregateRoots
{
    public abstract class AuditableAggregateRoot : AggregateRoot, IAuditableEntity
    {
        protected AuditableAggregateRoot() { }
        protected AuditableAggregateRoot(Guid id) : base(id) { }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
