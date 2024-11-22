using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFStatus
    {
        [ChavePrimaria, Obrigatorio]
        public short PK_CFStatus { get; set; }
        [Obrigatorio, TamanhoString(15)]
        public string Status { get; set; }
        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
