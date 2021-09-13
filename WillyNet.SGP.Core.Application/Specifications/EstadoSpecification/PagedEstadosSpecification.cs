using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.EstadoSpecification
{
    public class PagedEstadosSpecification : Specification<Estado>
    {
        public PagedEstadosSpecification(int pageNumber, int pageSize, string nombre)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.EstadNomb, "%" + nombre + "%");
        }
    }
}
