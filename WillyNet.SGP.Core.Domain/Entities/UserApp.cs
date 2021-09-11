using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WillyNet.SGP.Core.Domain.Entities
{
    public class UserApp : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Iniciativa> IniciativasUserCrea { get; set; }
        public ICollection<Iniciativa> IniciativasUserSolic { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
