using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.DTOs;
using WillyNet.SGP.Core.Application.DTOs.Users;
using WillyNet.SGP.Core.Application.Features.Estados.Commands.CreateEstado;
using WillyNet.SGP.Core.Application.Features.Iniciativas.Commands.CreateIniciativa;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateEstadoCommand, Estado>();
            CreateMap<CreateIniciativaCommand, Iniciativa>();
            #endregion

            #region Dtos
            CreateMap<Estado, EstadoDto>();
            CreateMap<Area, AreaDto>();
            CreateMap<Componente, ComponenteDto>();
            CreateMap<Flujo, FlujoDto>();
            CreateMap<Iniciativa, IniciativaDto>();
            CreateMap<Modulo, ModuloDto>();
            CreateMap<UserApp, UserAppDto>();
            CreateMap<Archivo, ArchivoDto>();
            #endregion
        }
    }
}
