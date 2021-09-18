using Ardalis.Specification;
using System.Linq;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Specifications.FlujoSpecification
{
    public class PagedFlujoSpecification : Specification<Flujo>
    {
        public PagedFlujoSpecification(int pageNumber, int pageSize, string nombEstado, string nombModulo,
            string NombreIniciativa, string IniCodi, int EstadoId)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            if (!string.IsNullOrEmpty(nombEstado) && !string.IsNullOrEmpty(nombModulo))
                Query.Where(x => x.FlujoActivo == true 
                                && x.Estado.EstadNomb.ToUpper() == nombEstado.ToUpper()
                                && x.Modulo.ModuNomb.ToUpper() == nombModulo.ToUpper());
            else
                Query.Where(x => x.FlujoActivo == true);

            Query.Include(x => x.Estado);
            Query.Include(x => x.Modulo);
            Query.Include(x => x.Iniciativa).ThenInclude(x => x.Area);
            Query.Include(x => x.Iniciativa).ThenInclude(x => x.UserAppCrea);
            Query.Include(x => x.Iniciativa).ThenInclude(x => x.UserAppSolic);
            Query.Include(x => x.Iniciativa).ThenInclude(x => x.Componente);
            Query.Include(x => x.Iniciativa)
                .ThenInclude(x => x.Archivos);

            if (!string.IsNullOrEmpty(NombreIniciativa))
                Query.Search(x => x.Iniciativa.IniNomb, "%" + NombreIniciativa + "%");                    
            
            if (!string.IsNullOrEmpty(IniCodi))
                Query.Search(x => x.Iniciativa.IniCodi, "%" + IniCodi + "%");
            if (EstadoId > 0)
                Query.Where(x => x.Estado.EstadId == EstadoId);
        }
    }
}
