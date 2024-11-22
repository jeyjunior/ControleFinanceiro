using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFTipoDePagamento
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_CFTipoDePagamento { get; set; }
        [Obrigatorio, TamanhoString(100)]
        public string Pagamento { get; set; }
        [Obrigatorio]
        public bool Ativo {  get; set; }
        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
