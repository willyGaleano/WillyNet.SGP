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
using WillyNet.SGP.Core.Application.Specifications.AreaSpecification;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Areas.Queries.GetAllAreas
{
    public class GetAllAreasQuery : IRequest<PagedResponse<IEnumerable<AreaDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
    }
    public class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, PagedResponse<IEnumerable<AreaDto>>>
    {
        private readonly IRepositoryAsync<Area> _repositoryArea;
        private readonly IMapper _mapper;
        public GetAllAreasQueryHandler(IRepositoryAsync<Area> repositoryArea, IMapper mapper)
        {
            _repositoryArea = repositoryArea;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<AreaDto>>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var listaAreas = await _repositoryArea.ListAsync(
                                new PagedAreasSpecification(request.PageNumber, request.PageSize, request.Nombre)
                                );
            var listaAreasDto = _mapper.Map<IEnumerable<AreaDto>>(listaAreas);

            var count = await _repositoryArea
                   .CountAsync(new PagedAreasSpecification(request.PageNumber, request.PageSize, request.Nombre));
            return
                new PagedResponse<IEnumerable<AreaDto>>(listaAreasDto, request.PageNumber, request.PageSize, count, "¡Consulta exitosa!");
        }
    }

}
