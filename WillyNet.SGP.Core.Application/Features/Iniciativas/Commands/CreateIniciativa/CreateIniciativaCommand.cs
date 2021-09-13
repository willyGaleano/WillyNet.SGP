using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Specifications.EstadoSpecification;
using WillyNet.SGP.Core.Application.Specifications.ModuloSpecification;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Iniciativas.Commands.CreateIniciativa
{
    public class CreateIniciativaCommand : IRequest<Response<int>>
    {
        public string IniNomb { get; set; }
        public string IniDescrip { get; set; }
        public int CompId { get; set; }
        public int AreaId { get; set; }
        public string UserCreaId { get; set; }
        public string ArchiUbicImg { get; set; }
        public List<string> ArchiUbicOtros { get; set; }
    }

    public class CreateIniciativaCommandHandler : IRequestHandler<CreateIniciativaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Iniciativa> _repositoryIniciativa;
        private readonly IRepositoryAsync<Archivo> _repositoryArchivo;
        private readonly IRepositoryAsync<Flujo> _repositoryFlujo;
        private readonly IRepositoryAsync<Estado> _repositoryEstado;
        private readonly IRepositoryAsync<Modulo> _repositoryModulo;
        private readonly ITransactionDb _transactionDb;
        private readonly IMapper _mapper;
        public CreateIniciativaCommandHandler(IRepositoryAsync<Iniciativa> repositoryAsync,
                                    IRepositoryAsync<Archivo> repositoryArchivo,
                                    IRepositoryAsync<Flujo> repositoryFlujo,
                                    IRepositoryAsync<Estado> repositoryEstado,
                                    IRepositoryAsync<Modulo> repositoryModulo,
                                    ITransactionDb transactionDb,
                                    IMapper mapper)
        {
            _repositoryIniciativa = repositoryAsync;
            _repositoryArchivo = repositoryArchivo;
            _repositoryFlujo = repositoryFlujo;
            _repositoryEstado = repositoryEstado;
            _repositoryModulo = repositoryModulo;
            _transactionDb = transactionDb;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateIniciativaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var iniciativaMap = _mapper.Map<Iniciativa>(request);
                var iniciativa = await _repositoryIniciativa.AddAsync(iniciativaMap);
                iniciativa.IniCodi = "INI00" + iniciativa.IniId.ToString();
                await _repositoryIniciativa.UpdateAsync(iniciativa);

                if (!string.IsNullOrEmpty(request.ArchiUbicImg))
                {
                    var archivoImg = new Archivo
                    {
                        ArchiNomb = iniciativa.IniCodi,
                        ArchiUbic = request.ArchiUbicImg,
                        IniId = iniciativa.IniId
                    };

                    await _repositoryArchivo.AddAsync(archivoImg);
                }

                if (request.ArchiUbicOtros.Count > 0)
                {
                    foreach (var item in request.ArchiUbicOtros)
                    {
                        var archivoOtros = new Archivo
                        {
                            ArchiNomb = iniciativa.IniCodi,
                            ArchiUbic = item,
                            IniId = iniciativa.IniId
                        };
                        await _repositoryArchivo.AddAsync(archivoOtros);
                    }
                }

                var EstadoRegistrada = await _repositoryEstado
                                        .GetBySpecAsync(new GetEstadoByNombSpecification("Registrada"));
                var ModuloEspecialista = await _repositoryModulo
                                        .GetBySpecAsync(new GetIdModuloByNombSpecification("Especialista"));

                var newFlujo = new Flujo
                {
                    IniId = iniciativa.IniId,
                    EstadId = EstadoRegistrada.EstadId,
                    ModuId = ModuloEspecialista.ModuId
                };

                await _repositoryFlujo.AddAsync(newFlujo);
                await _transactionDb.DbContextTransaction.CommitAsync();

                return new Response<int>(iniciativa.IniId, "Se creo exitosamente la iniciativa");

            }
            catch(Exception ex)
            {
                await _transactionDb.DbContextTransaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            
        }
    }
}
