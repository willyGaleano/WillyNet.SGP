using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Parameters;

namespace WillyNet.SGP.Core.Application.Features.Estados.Queries.GetAllEstados
{
    public class GetAllEstadosParameters : RequestParameter
    {
        public string Nombre { get; set; }
    }
}
