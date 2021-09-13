using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Infraestructure.Persistence.Contexts;

namespace WillyNet.SGP.Infraestructure.Persistence.Services
{
    public class TransactionDb : ITransactionDb
    {
        public IDbContextTransaction DbContextTransaction { get; }
        public TransactionDb(DbSGPContext context)
        {
            DbContextTransaction = context.Database.BeginTransaction();
        }
    }
}
