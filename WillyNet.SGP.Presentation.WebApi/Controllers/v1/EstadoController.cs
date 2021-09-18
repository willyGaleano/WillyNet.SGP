using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Estados.Commands.CreateEstado;
using WillyNet.SGP.Core.Application.Features.Estados.Queries.GetAllEstados;

namespace WillyNet.SGP.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstadoController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEstadosParameters filters)
        {
            return Ok(await Mediator.Send(
                    new GetAllEstadosQuery
                    {
                        PageNumber = filters.PageNumber,
                        PageSize = filters.PageSize,
                        Nombre = filters.Nombre
                    }
                ));
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> Create([FromBody] CreateEstadoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
