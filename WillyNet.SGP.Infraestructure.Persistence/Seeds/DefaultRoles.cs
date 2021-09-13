using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Enums;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Infraestructure.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
