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

        public IEnumerable<CFTipoTransacaoFinanceira> ObterTipoTransacaoFinanceiraInicial()
        {
            var tipoTransacaoFinanceiroCollection = new List<CFTipoTransacaoFinanceira>()
            {
                new CFTipoTransacaoFinanceira { PK_CFTipoTransacaoFinanceira = 1, Transacao = "Entrada"},
                new CFTipoTransacaoFinanceira { PK_CFTipoTransacaoFinanceira = 2, Transacao = "Saída"},
            };

            return tipoTransacaoFinanceiroCollection;
        }
    }
}
