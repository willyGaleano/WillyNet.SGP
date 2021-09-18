﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.ComponenteSpecification
{
    public class PagedComponentesSpecification : Specification<Componente>
    {
        public PagedComponentesSpecification(int pageNumber, int pageSize, string nombre)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.CompNomb, "%" + nombre + "%");
        }
    }
}
