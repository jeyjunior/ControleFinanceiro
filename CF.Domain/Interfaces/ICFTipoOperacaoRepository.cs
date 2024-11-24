using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Interfaces;
using CF.Domain.Entities;


namespace CF.Domain.Interfaces
{
    public interface ICFTipoOperacaoRepository : IRepository<CFTipoOperacao>
    {
        IEnumerable<CFTipoOperacao> ObterTipoOperacaoInicial();
    }
}
