using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JJ.UW.Core.Utilidades;
using JJ.UW.Core.Enumerador;
using JJ.UW.Core.Extensoes;
using CF.Domain.DTO;
using CF.Domain.Enumerador;
using CF.Domain.Utilitarios;

namespace CF.Presentation.Componentes
{
    public sealed partial class ResumoFinanceiroSimples : UserControl
    {
        #region Construtor
        public ResumoFinanceiroSimples()
        {
            this.InitializeComponent();
        }
        #endregion
        
        #region Metodos
        private void AtualizarOperacao(eIconesGlyph icone, Brush cor)
        {
            ficonTriangulo.AtualizarIcone(icone, cor);
            txbValor.Foreground = cor;
        }
        #endregion

        #region Metodos Publico
        public void AtualizarSaldo(ResumoFinanceiroSaldoDTO saldo)
        {
            txbValor.Text = saldo.SaldoTotal.ToString("N2");

            if (saldo.SaldoTotal >= 0)
            {
                AtualizarOperacao(eIconesGlyph.TrianguloCima, Cor.ObterCor(eCores.Verde1));
            }
            else
            {
                AtualizarOperacao(eIconesGlyph.TrianguloBaixo, Cor.ObterCor(eCores.Vermelho1));
            }
        }
        #endregion
    }
}
