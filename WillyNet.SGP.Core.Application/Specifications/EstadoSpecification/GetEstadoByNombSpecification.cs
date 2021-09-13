using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.EstadoSpecification
{
    public class GetEstadoByNombSpecification : Specification<Estado>, ISingleResultSpecification
    {
        public GetEstadoByNombSpecification(string nombre)
        {            
            Query.Where(x => x.EstadNomb.ToLower() == nombre.ToLower());   
        }
    }
}
