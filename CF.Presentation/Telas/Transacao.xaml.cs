using CF.Application;
using CF.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CF.Presentation.Telas
{
    public sealed partial class Transacao : Page
    {
        private readonly ICFRegistroFinanceiroRepository cFRegistroFinanceiroRepository;
        public Transacao()
        {
            this.InitializeComponent();

            cFRegistroFinanceiroRepository = Bootstrap.Container.GetInstance<ICFRegistroFinanceiroRepository>();
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
            var ret = cFRegistroFinanceiroRepository.ObterLista().ToList().Take(10);
            gridSaida.BindGrid(ret);

            cartaoResumoFinanceiro.AtualizarInformacoesIniciais(new Domain.DTO.ResumoFinanceiroDTO
            {
                TipoOperacaoFinanceira = Domain.Enumerador.eTipoOperacaoFinanceira.Saida,
                Titulo = "Saída",
                ValorPago = 15287.99m,
                ValorPendente = 350.44m
            });

            cartaoResumoFinanceiroSimples.AtualizarSaldo(new Domain.DTO.ResumoFinanceiroSaldoDTO { SaldoTotal = -956.56m });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            //string toastXmlString = "<toast><visual><binding template='ToastGeneric'><text>Exemplo de Notificação</text><text>Mensagem de exemplo</text></binding></visual></toast>";
            //XmlDocument toastXml = new XmlDocument();
            //toastXml.LoadXml(toastXmlString);

            //ToastNotification toast = new ToastNotification(toastXml);
            //ToastNotificationManager.CreateToastNotifier().Show(toast);

            //MessageDialog dialog = new MessageDialog("Essa é uma mensagem de alerta.", "Alerta");
            //dialog.Commands.Add(new UICommand("OK"));
            //dialog.ShowAsync();
        }
    }
}
