using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Iniciativas.Commands.CreateIniciativa;

namespace WillyNet.SGP.Presentation.WebApi.Controllers.v1
{
    
    public class IniciativaController : BaseApiController
    {
        [HttpPost("CreateIniciativa")]
        public async Task<IActionResult> Create([FromBody] CreateIniciativaCommand command)
        {
            return Ok( await Mediator.Send(command));
        }
    }
}
