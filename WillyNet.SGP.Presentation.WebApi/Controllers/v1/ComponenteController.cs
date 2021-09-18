using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Componentes.Queries.GetAllComponentes;

namespace WillyNet.SGP.Presentation.WebApi.Controllers.v1
{

    public class ComponenteController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllComponentesParameters filters)
        {
            return Ok(await Mediator.Send(
                    new GetAllComponentesQuery
                    {
                        PageNumber = filters.PageNumber,
                        PageSize = filters.PageSize,
                        Nombre = filters.Nombre
                    }
                ));
        }
    }
}
