using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Areas.Queries.GetAllAreas;

namespace WillyNet.SGP.Presentation.WebApi.Controllers.v1
{

    public class AreaController : BaseApiController
    {
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAreasParameters filters)
        {
            return Ok(await Mediator.Send(
                    new GetAllAreasQuery
                    {
                        PageNumber = filters.PageNumber,
                        PageSize = filters.PageSize,
                        Nombre = filters.Nombre
                    }
                ));
        }
    }
}
