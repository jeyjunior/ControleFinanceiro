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
        
        public DateTime? DataPagamento { get; set; }
        
        [Obrigatorio]
        public DateTime DataVencimento { get; set; }
        
        [Obrigatorio, TamanhoDecimal(18,2)]
        public decimal Valor { get; set; }

        [Obrigatorio, Relacionamento("CFStatus", "PK_CFStatus")]
        public int FK_CFStatus { get; set; }

        [Obrigatorio, Relacionamento("CFTerceiro", "PK_CFTerceiro")]
        public int FK_CFTerceiro { get; set; }
        
        [Obrigatorio, Relacionamento("CFTipoDePagamento", "PK_CFTipoDePagamento")]
        public int FK_CFTipoDePagamento { get; set; }

        [Obrigatorio, Relacionamento("CFTipoTransacaoFinanceira", "PK_CFTipoTransacaoFinanceira")]
        public int FK_CFTipoTransacaoFinanceira { get; set; }
        
        [Obrigatorio, Relacionamento("CFTipoOperacao", "PK_CFTipoOperacao")]
        public int FK_CFTipoOperacao { get; set; }

        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
