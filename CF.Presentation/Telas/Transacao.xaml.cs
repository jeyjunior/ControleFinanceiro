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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CF.Presentation.Telas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Transacao : Page
    {
        public Transacao()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cartaoResumoFinanceiro.AtualizarInformacoesIniciais(new Domain.DTO.ResumoFinanceiroDTO
            {
                TipoOperacaoFinanceira = Domain.Enumerador.eTipoOperacaoFinanceira.Entrada,
                Titulo = "Entrada",
                ValorPago =287.99m,
                ValorPendente = 2350.44m
            });

            cartaoResumoFinanceiroSimples.AtualizarSaldo(new Domain.DTO.ResumoFinanceiroSaldoDTO { SaldoTotal = 256.44m });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cartaoResumoFinanceiro.AtualizarInformacoesIniciais(new Domain.DTO.ResumoFinanceiroDTO
            {
                TipoOperacaoFinanceira = Domain.Enumerador.eTipoOperacaoFinanceira.Saida,
                Titulo = "Saída",
                ValorPago = 15287.99m,
                ValorPendente = 350.44m
            });

            cartaoResumoFinanceiroSimples.AtualizarSaldo(new Domain.DTO.ResumoFinanceiroSaldoDTO { SaldoTotal = -956.56m });
        }
    }
}
