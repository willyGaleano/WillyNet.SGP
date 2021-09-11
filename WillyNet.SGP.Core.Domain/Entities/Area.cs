using System.Collections.Generic;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Area : AuditableBaseEntity
    {
        public int AreaId { get; set; }
        public string AreaNomb { get; set; }
        public string UserResponsId { get; set; }
        public UserApp UserAppRespons { get; set; }
        public ICollection<Iniciativa> Iniciativas { get; set; }
    }
}
