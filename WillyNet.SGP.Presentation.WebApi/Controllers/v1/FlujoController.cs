using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Flujos.Commands.AprobarFlujo;
using WillyNet.SGP.Core.Application.Features.Flujos.Queries.GetAllFlujo;

namespace WillyNet.SGP.Presentation.WebApi.Controllers.v1
{
    public class FlujoController : BaseApiController
    {
        [HttpPost("AprobarEtapa")]
        public async Task<IActionResult> Aprobar ([FromBody] AprobarFlujoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFlujoParameters parameters)
        {
            return Ok(
                   await Mediator.Send(
                        new GetAllFlujoQuery
                        {
                            PageNumber = parameters.PageNumber,
                            PageSize = parameters.PageSize,
                            NombreEstado = parameters.NombreEstado,
                            NombreModulo = parameters.NombreModulo,
                            NombreIniciativa = parameters.NombreIniciativa,
                            IniCodi = parameters.IniCodi,
                            EstadoId = parameters.EstadoId
                        }
                     ));
        }
    }
}
