using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.EstadoSpecification
{
    public class ExistsEstadoSpecification : Specification<Estado>
    {
        public ExistsEstadoSpecification(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
                Query.Where(x => x.EstadNomb.ToLower() == nombre.ToLower());
        }
    }
}
