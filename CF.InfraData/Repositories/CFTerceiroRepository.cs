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
    public class CFTerceiroRepository : Repository<CFTerceiro>, ICFTerceiroRepository
    {
        public CFTerceiroRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
