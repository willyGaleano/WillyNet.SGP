using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.DTOs;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Specifications.ComponenteSpecification;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Componentes.Queries.GetAllComponentes
{
    public class GetAllComponentesQuery : IRequest<PagedResponse<IEnumerable<ComponenteDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
    }
    public class GetAllComponentesQueryHandler : IRequestHandler<GetAllComponentesQuery, PagedResponse<IEnumerable<ComponenteDto>>>
    {
        private readonly IRepositoryAsync<Componente> _repositoryComponente;
        private readonly IMapper _mapper;
        public GetAllComponentesQueryHandler(IRepositoryAsync<Componente> repositoryComponente, IMapper mapper)
        {
            _repositoryComponente = repositoryComponente;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<ComponenteDto>>> Handle(GetAllComponentesQuery request, CancellationToken cancellationToken)
        {
            var listaAreas = await _repositoryComponente.ListAsync(
                                new PagedComponentesSpecification(request.PageNumber, request.PageSize, request.Nombre)
                                );
            var listaAreasDto = _mapper.Map<IEnumerable<ComponenteDto>>(listaAreas);

            var count = await _repositoryComponente
                   .CountAsync(new PagedComponentesSpecification(request.PageNumber, request.PageSize, request.Nombre));
            return
                new PagedResponse<IEnumerable<ComponenteDto>>(listaAreasDto, request.PageNumber, request.PageSize, count, "¡Consulta exitosa!");
        }
    }
}
