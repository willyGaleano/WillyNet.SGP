using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.SGP.Core.Application.DTOs
{
    public class FlujoDto
    {
        public int FlujoId { get; set; }                                        
        public IniciativaDto Iniciativa { get; set; }
        public ModuloDto Modulo { get; set; }
        public EstadoDto Estado { get; set; }
    }
}
