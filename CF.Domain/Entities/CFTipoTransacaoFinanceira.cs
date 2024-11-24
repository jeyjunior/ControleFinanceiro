using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFTipoTransacaoFinanceira
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_CFTipoTransacaoFinanceira { get; set; }
        [Obrigatorio, TamanhoString(10)]
        public string Transacao { get; set; }
        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
