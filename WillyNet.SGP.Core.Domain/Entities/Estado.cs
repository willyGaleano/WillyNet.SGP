using System.Collections.Generic;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Estado : AuditableBaseEntity
    {
        public int EstadId { get; set; }
        public string EstadNomb { get; set; }
        public ICollection<Flujo> Flujos { get; set; }
    }
}
