using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Data.Interfaces;
using CF.Domain.Entities;
using CF.Domain.Interfaces;

namespace CF.InfraData.Repositories
{
    public class CFRegistroFinanceiroRepository : Repository<CFRegistroFinanceiro>, ICFRegistroFinanceiroRepository
    {
        public CFRegistroFinanceiroRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
