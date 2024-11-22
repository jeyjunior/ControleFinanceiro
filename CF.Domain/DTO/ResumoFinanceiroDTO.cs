using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.UW.Core.Validador;
using System.Threading.Tasks;
using CF.Domain.Enumerador;

namespace CF.Domain.DTO
{
    public class ResumoFinanceiroDTO
    {
        public eTipoOperacaoFinanceira TipoOperacaoFinanceira { get; set; }
        public string Titulo { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorPendente { get; set; }

        public ValidarResultado Validacao { get; set; } = new ValidarResultado();
    }

    public class ResumoFinanceiroSaldoDTO
    {
        public decimal SaldoRealizado { get; set; }
        public decimal SaldoPendente { get; set; }
        public decimal SaldoTotal { get; set; }
    }
}
