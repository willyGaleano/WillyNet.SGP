using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WillyNet.SGP.Core.Application.Interfaces;

namespace WillyNet.SGP.Presentation.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;       
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            //_httpContextAccessor = httpContextAccessor;
            //UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }
        public string UserId { get; }
    }
}
