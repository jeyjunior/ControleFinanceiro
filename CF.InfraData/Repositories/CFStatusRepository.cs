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
    public class CFStatusRepository : Repository<CFStatus>, ICFStatusRepository
    {
        public CFStatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<CFStatus> ObterStatus()
        {
            var statusCollection = new List<CFStatus>() 
            {
                new CFStatus { PK_CFStatus = 1, Status = "Pago" },
                new CFStatus { PK_CFStatus = 2, Status = "Previsto" },
                new CFStatus { PK_CFStatus = 3, Status = "Vencido" },
            };

            return statusCollection;
        }
    }
}
