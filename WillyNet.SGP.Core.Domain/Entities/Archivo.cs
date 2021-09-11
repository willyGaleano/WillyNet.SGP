using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Archivo : AuditableBaseEntity
    {
        public int ArchiId { get; set; }
        public string ArchiUbic { get; set; }
        public string ArchiNomb { get; set; }
        public int IniId { get; set; }
        public Iniciativa Iniciativa { get; set; }
    }
}
