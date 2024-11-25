using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFTerceiro
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_CFTerceiro { get; set; }
        [Obrigatorio, TamanhoString(100)]
        public string Nome { get; set; }

        [Relacionamento("CFCategoria", "PK_CFCategoria")]
        public int? FK_CFCategoria { get; set; }

        [TamanhoString(250)]
        public string Observacoes { get; set; }

        [Editavel(false)]
        public CFCategoria CFCategoria { get; set; }

        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
