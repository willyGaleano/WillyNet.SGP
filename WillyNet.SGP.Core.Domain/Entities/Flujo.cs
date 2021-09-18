using System;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class Flujo : AuditableBaseEntity
    {
        public int FlujoId { get; set; }
        public string FlujoEspecific { get; set; }
        public DateTime FlujoFecAprob { get; set; }
        public DateTime FlujoFecRechaz { get; set; }
        public bool FlujoActivo { get; set; }
        public int IniId { get; set; }
        public int ModuId { get; set; }
        public int EstadId { get; set; }
        public Iniciativa Iniciativa { get; set; }
        public Modulo Modulo { get; set; }
        public Estado Estado { get; set; }
    }
}
