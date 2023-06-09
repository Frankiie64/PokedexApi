using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Domain.Commons
{
    public abstract class AuditableBaseEntity
    {
        public virtual Guid  Id { get; set; }
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set;}

    }
}
