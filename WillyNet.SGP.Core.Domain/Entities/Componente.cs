using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Componente : AuditableBaseEntity
    {
        public int CompId { get; set; }
        public string CompNomb { get; set; }
        public ICollection<Iniciativa> Iniciativas { get; set; }
    }
}
