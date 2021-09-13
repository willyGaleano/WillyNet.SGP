using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.ModuloSpecification
{
    public class GetIdModuloByNombSpecification : Specification<Modulo>, ISingleResultSpecification
    {
        public GetIdModuloByNombSpecification(string nombre)
        {
            Query.Where(x => x.ModuNomb.ToLower() == nombre.ToLower());
        }
    }
}
