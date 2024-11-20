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
    public class CFTipoOperacaoRepository : Repository<CFTipoOperacao>, ICFTipoOperacaoRepository
    {
        public CFTipoOperacaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<CFTipoOperacao> ObterTipoOperacao()
        {
            var tipoOperacaoCollection = new List<CFTipoOperacao>()
            {
                new CFTipoOperacao { PK_CFTipoOperacao = 1, Nome = "Fixa", Ativo = true},
                new CFTipoOperacao { PK_CFTipoOperacao = 2, Nome = "Variável", Ativo = true},
                new CFTipoOperacao { PK_CFTipoOperacao = 3, Nome = "Extra", Ativo = true},
            };

            return tipoOperacaoCollection;
        }
    }
}
