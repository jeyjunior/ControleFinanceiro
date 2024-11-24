using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Atributos;
using JJ.UW.Core.Validador;

namespace CF.Domain.Entities
{
    public class CFCategoria
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_CFCategoria { get; set; }
        [Obrigatorio, TamanhoString(100)]
        public string Categoria { get; set; }
        [Editavel(false)]
        public ValidarResultado Validar { get; set; } = new ValidarResultado();
    }
}
