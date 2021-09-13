using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Estados.Commands.CreateEstado
{
    public class CreateEstadoCommand : IRequest<Response<int>>
    {
        public string EstadNomb { get; set; }

        public class CreateEstadoCommandHandler : IRequestHandler<CreateEstadoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Estado> _repositoryAsync;
            private readonly IMapper _mapper;

            public CreateEstadoCommandHandler(IRepositoryAsync<Estado> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateEstadoCommand request, CancellationToken cancellationToken)
            {
                var nuevoRegistro = _mapper.Map<Estado>(request);
                var data = await _repositoryAsync.AddAsync(nuevoRegistro);

                return new Response<int>(data.EstadId, "¡Consulta exitosa!");
            }
        }
    }
}
