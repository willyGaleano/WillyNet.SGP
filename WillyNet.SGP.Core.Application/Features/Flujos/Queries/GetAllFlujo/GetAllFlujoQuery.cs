using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.DTOs;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Specifications.FlujoSpecification;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Flujos.Queries.GetAllFlujo
{
    public class GetAllFlujoQuery : IRequest<PagedResponse<IEnumerable<FlujoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string NombreEstado { get; set; }
        public string NombreModulo { get; set; }
        public string NombreIniciativa { get; set; }
        public string IniCodi { get; set; }
        public int EstadoId { get; set; }
    }

    public class GetAllFlujoQueryHandler : IRequestHandler<GetAllFlujoQuery, PagedResponse<IEnumerable<FlujoDto>>>
    {
        private readonly IRepositoryAsync<Flujo> _repositoryFlujo;
        private readonly IMapper _mapper;
        public GetAllFlujoQueryHandler(IRepositoryAsync<Flujo> repositoryFlujo, IMapper mapper)
        {
            _repositoryFlujo = repositoryFlujo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<FlujoDto>>> Handle(GetAllFlujoQuery request, CancellationToken cancellationToken)
        {
            
            var listaFlujoSpec = await _repositoryFlujo
                           .ListAsync(
                            new PagedFlujoSpecification(
                                request.PageNumber, request.PageSize, request.NombreEstado,request.NombreModulo,
                                request.NombreIniciativa, request.IniCodi, request.EstadoId
                                ));
          
            var listaFlujoDto = _mapper.Map<IEnumerable<FlujoDto>>(listaFlujoSpec);
            var count = await _repositoryFlujo
                .CountAsync(new PagedFlujoSpecification(
                                request.PageNumber, request.PageSize, request.NombreEstado, request.NombreModulo,
                                request.NombreIniciativa, request.IniCodi, request.EstadoId
                                ));
            return
                new PagedResponse<IEnumerable<FlujoDto>>(listaFlujoDto, request.PageNumber, request.PageSize, count, "¡Consulta exitosa!");            
        }
    }
}
