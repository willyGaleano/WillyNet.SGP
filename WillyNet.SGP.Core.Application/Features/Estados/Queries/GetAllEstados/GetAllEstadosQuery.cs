using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.DTOs;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Specifications.EstadoSpecification;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Estados.Queries.GetAllEstados
{
    public class GetAllEstadosQuery : IRequest<PagedResponse<IEnumerable<EstadoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }

        public class GetAllEstadosQueryHandler : IRequestHandler<GetAllEstadosQuery, PagedResponse<IEnumerable<EstadoDto>>>
        {
            private readonly IRepositoryAsync<Estado> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetAllEstadosQueryHandler(IRepositoryAsync<Estado> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<IEnumerable<EstadoDto>>> Handle(GetAllEstadosQuery request, CancellationToken cancellationToken)
            {
                var listaEstados = await _repositoryAsync
                        .ListAsync(new PagedEstadosSpecification(request.PageNumber, request.PageSize, request.Nombre));
                var listaEstadosDto = _mapper.Map<IEnumerable<EstadoDto>>(listaEstados);
                var count = await _repositoryAsync
                    .CountAsync(new PagedEstadosSpecification(request.PageNumber, request.PageSize, request.Nombre));
                return 
                    new PagedResponse<IEnumerable<EstadoDto>>(listaEstadosDto, request.PageNumber, request.PageSize, count, "¡Consulta exitosa!");
            }
        }
    }
}
