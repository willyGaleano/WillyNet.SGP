using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.DTOs;
using WillyNet.SGP.Core.Application.DTOs.Users;

namespace WillyNet.SGP.Core.Application.DTOs
{
    public class IniciativaDto
    {
        public int IniId { get; set; }
        public string IniNomb { get; set; }
        public string IniDescrip { get; set; }
        public bool IniPriori { get; set; }
        public string IniCodi { get; set; }
        public ComponenteDto Componente { get; set; }
        public AreaDto Area { get; set; }
        public UserAppDto UserAppCrea { get; set; }
        public UserAppDto UserAppSolic { get; set; }
        public List<ArchivoDto> Archivos { get; set; }        
    }
}
