using System.Collections.Generic;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Modulo : AuditableBaseEntity
    {
        public int ModuId { get; set; }
        public string ModuNomb { get; set; }
        public ICollection<Flujo> Flujos { get; set; }
    }
}
