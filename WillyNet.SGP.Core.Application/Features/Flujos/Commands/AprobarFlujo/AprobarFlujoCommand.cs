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

namespace WillyNet.SGP.Core.Application.Features.Flujos.Commands.AprobarFlujo
{
    public class AprobarFlujoCommand : IRequest<Response<int>>
    {
        public int FlujoId { get; set; }
        public string FlujoEspecific { get; set; }               
        public bool FlujoPriori { get; set; }
        public int IniId { get; set; }
        public int ModuId { get; set; }
        public string NombreNextModulo { get; set; }
    }

    public class AprobarFlujoCommandHandler : IRequestHandler<AprobarFlujoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Flujo> _repositoryFlujo;
        private readonly IRepositoryAsync<Estado> _repositoryEstado;
        private readonly IRepositoryAsync<Modulo> _repositoryModulo;
        private readonly IRepositoryAsync<Iniciativa> _repositoryIniciativa;
        private readonly ITransactionDb _transactionDb;

        public AprobarFlujoCommandHandler(IRepositoryAsync<Flujo> repositoryFlujo,
                                       IRepositoryAsync<Estado> repositoryEstado,
                                       IRepositoryAsync<Modulo> repositoryModulo,
                                       IRepositoryAsync<Iniciativa> repositoryIniciativa,
                                    ITransactionDb transactionDb)

        {
            _repositoryFlujo = repositoryFlujo;
            _repositoryEstado = repositoryEstado;
            _repositoryModulo = repositoryModulo;
            _repositoryIniciativa = repositoryIniciativa;
            _transactionDb = transactionDb;
        }

        public async Task<Response<int>> Handle(AprobarFlujoCommand request, CancellationToken cancellationToken)
        {
            #region ACTUALIZAR FECHA, ESPECIFICAION Y ACTIVO DE LA FLUJO-INICIATIVA ACTUAL
            var iniFlujoActual = await _repositoryFlujo.GetByIdAsync(request.FlujoId);
            iniFlujoActual.FlujoFecAprob = DateTime.Now;
            iniFlujoActual.FlujoActivo = false;

            if (!string.IsNullOrEmpty(request.FlujoEspecific))
                iniFlujoActual.FlujoEspecific = request.FlujoEspecific;

            await _repositoryFlujo.UpdateAsync(iniFlujoActual);
            #endregion

            #region CREAR NUEVA INICIATIVA EN EL FLUJO CON EL SIGUIENTE MODULO         
            Modulo modulo;
            Estado estado;
            switch (request.NombreNextModulo.ToUpper())
            {
                case "INGENIERO":
                    modulo = await _repositoryModulo
                                    .GetBySpecAsync(new GetIdModuloByNombSpecification("Ingeniero"));
                    estado = await _repositoryEstado
                                                .GetBySpecAsync(new GetEstadoByNombSpecification("Procede"));
                    break;
                case "ESPECIALISTA":
                    modulo = await _repositoryModulo
                                    .GetBySpecAsync(new GetIdModuloByNombSpecification("Especialista"));
                    estado = await _repositoryEstado
                                                .GetBySpecAsync(new GetEstadoByNombSpecification("Análisis"));
                    break;

                default:
                    await _transactionDb.DbContextTransaction.RollbackAsync();
                    throw new Exception("gaaaa");

            }


            #region ACTUALIZAMOS LA PRIORIDAD DE LA INICIATIVA SI ES QUE LA HAY
            if (request.FlujoPriori)
            {
                var iniciativa = await _repositoryIniciativa.GetByIdAsync(request.IniId);
                iniciativa.IniPriori = request.FlujoPriori;
                await _repositoryIniciativa.UpdateAsync(iniciativa);
            }
            #endregion
            

            var newFlujo = new Flujo
            {
                IniId = request.IniId,
                ModuId = modulo.ModuId,
                EstadId = estado.EstadId,
                FlujoActivo = true
            };

            var flujo = await _repositoryFlujo.AddAsync(newFlujo);

            #endregion

            _transactionDb.DbContextTransaction.Commit();

            return new Response<int>(flujo.IniId);
        }
    }
}
