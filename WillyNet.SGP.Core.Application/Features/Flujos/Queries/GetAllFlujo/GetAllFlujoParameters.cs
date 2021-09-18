using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Parameters;

namespace WillyNet.SGP.Core.Application.Features.Flujos.Queries.GetAllFlujo
{
    public class GetAllFlujoParameters : RequestParameter
    {
        public string NombreEstado { get; set; }
        public string NombreModulo { get; set; }
        public string NombreIniciativa { get; set; }
        public string IniCodi { get; set; }
        public int EstadoId { get; set; }
    }
}
