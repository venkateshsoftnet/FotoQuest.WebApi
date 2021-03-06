using System;

namespace FotoQuest.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
