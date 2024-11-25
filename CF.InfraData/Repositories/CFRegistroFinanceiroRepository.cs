using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Data.Interfaces;
using CF.Domain.Entities;
using CF.Domain.Interfaces;
using Dapper;
using JJ.UW.Core.Extensoes;

namespace CF.InfraData.Repositories
{
    public class CFRegistroFinanceiroRepository : Repository<CFRegistroFinanceiro>, ICFRegistroFinanceiroRepository
    {
        public CFRegistroFinanceiroRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<CFRegistroFinanceiro> ObterRegistrosComDetalhes(CFRegistroFinanceiro_Request cFRegistroFinanceiro_Request)
        {
            var parametro = new
            {
                Ano = cFRegistroFinanceiro_Request.DataVencimento.Year.ToString(),
                Mes = cFRegistroFinanceiro_Request.DataVencimento.Month.ToString("00"),
                FK_CFTipoOperacaoFinanceira = (int)cFRegistroFinanceiro_Request.TipoOperacaoFinanceira,
            };

            var sql = "" +
            "SELECT CFRegistroFinanceiro.*,\n" +
            "		CFTerceiro.*,\n" +
            "		CFCategoria.*\n" +
            "FROM   CFRegistroFinanceiro\n" +
            "LEFT   JOIN    CFTerceiro\n" +
            "       ON      CFTerceiro.PK_CFTerceiro = CFRegistroFinanceiro.FK_CFTerceiro\n" +
            "LEFT   JOIN    CFCategoria\n" +
            "       ON      CFCategoria.PK_CFCategoria = CFTerceiro.FK_CFCategoria\n";

            string where = "" +
                "WHERE  STRFTIME('%Y', CFRegistroFinanceiro.DataVencimento) = @Ano\n" +
                "       AND     STRFTIME('%m', CFRegistroFinanceiro.DataVencimento) = @Mes\n";

            if (cFRegistroFinanceiro_Request.TipoOperacaoFinanceira != Domain.Enumerador.eTipoOperacaoFinanceira.Todas)
                where += "  AND FK_CFTipoOperacaoFinanceira = @FK_CFTipoOperacaoFinanceira\n";

            sql += where;

            var ret = unitOfWork.Connection.Query<CFRegistroFinanceiro, CFTerceiro, CFCategoria, CFRegistroFinanceiro >(
                sql: sql.ToSQL(),
                param: parametro,
                map: (cFRegistroFinanceiro, cFTerceiro, cFTipoCategoria) =>
                {
                    cFRegistroFinanceiro.CFTerceiro = cFTerceiro;
                    cFRegistroFinanceiro.CFTerceiro.CFCategoria = cFTipoCategoria;
                    return cFRegistroFinanceiro;
                },
                splitOn: "PK_CFRegistroFinanceiro,PK_CFTerceiro,PK_CFCategoria")
                .ToList();

            return ret;
        }
    }
}
