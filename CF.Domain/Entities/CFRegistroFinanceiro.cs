using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFRegistroFinanceiro
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_CFRegistroFinanceiro { get; set; }
        [Obrigatorio, TamanhoString(150)]
        public string Nome { get; set; }
        public DateTime? DataPagamento { get; set; }
        [Obrigatorio]
        public DateTime DataVencimento { get; set; }
        [Obrigatorio, TamanhoDecimal(18,2)]
        public decimal Valor { get; set; }

        [TamanhoString(250)]
        public string Observacao { get; set; }
        [Obrigatorio]
        public short FK_CFTipoTransacaoFinanceira { get; set; }
        [Obrigatorio]
        public short FK_CFTipoOperacao { get; set; }
        [Obrigatorio]
        public short FK_CFStatus { get; set; }
        [Obrigatorio, Relacionamento("CFTipoDePagamento", "PK_CFTipoDePagamento")]
        public int FK_CFTipoDePagamento { get; set; }
        [Relacionamento("CFCategoria", "PK_CFCategoria")]
        public int FK_CFCategoria { get; set; }
        [Relacionamento("CFTerceiro", "PK_CFTerceiro")]
        public int FK_CFTerceiro { get; set; }

        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
