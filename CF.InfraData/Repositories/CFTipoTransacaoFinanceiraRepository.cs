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
    public class CFTipoTransacaoFinanceiraRepository : Repository<CFTipoTransacaoFinanceira>, ICFTipoTransacaoFinanceiraRepository
    {
        public CFTipoTransacaoFinanceiraRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<CFTipoTransacaoFinanceira> ObterTipoTransacaoFinanceira()
        {
            var tipoTransacaoFinanceiroCollection = new List<CFTipoTransacaoFinanceira>()
            {
                new CFTipoTransacaoFinanceira { PK_CFTipoTransacaoFinanceira = 1, Transaco = "Entrada"},
                new CFTipoTransacaoFinanceira { PK_CFTipoTransacaoFinanceira = 2, Transaco = "Saída"},
            };

            return tipoTransacaoFinanceiroCollection;
        }
    }
}
