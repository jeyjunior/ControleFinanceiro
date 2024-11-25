using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CF.InfraData;
using JJ.UW.Core.Enumerador;

namespace CF.Application.Services
{
    public static class AppService
    {
        // Configurações
        public static void DefinirConexao(eConexao conexao)
        {
            BootstrapInfraData.DefinirConexaoAtiva(conexao);
        }
    }
}
