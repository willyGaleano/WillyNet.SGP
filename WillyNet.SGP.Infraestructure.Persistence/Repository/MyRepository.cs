using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Infraestructure.Persistence.Contexts;

namespace WillyNet.SGP.Infraestructure.Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly DbSGPContext _dbSGPContext;
        public MyRepositoryAsync(DbSGPContext dbSGPContext) : base(dbSGPContext)
        {
            _dbSGPContext = dbSGPContext;
        }        
    }
}
