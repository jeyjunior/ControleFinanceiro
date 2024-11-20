using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validation;

namespace CF.Domain.Entities
{
    public class CFTipoTransacaoFinanceira
    {
        [ChavePrimaria, Obrigatorio]
        public short PK_CFTipoTransacaoFinanceira { get; set; }
        [Obrigatorio, TamanhoString(10)]
        public string Transaco { get; set; }
        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
