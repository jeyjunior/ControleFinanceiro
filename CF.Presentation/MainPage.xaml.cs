
using CF.Domain.Utilitarios;
using CF.Presentation.Views;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CF.Presentation
{
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        private Button btnSelecionado = null;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            Dashboard_Click(btnDashboard, null);
        }

        private void Dashboard_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Dashboard));

            AtualizarBotaoSelecionado((Button)sender);
        }

        private void Transacao_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Transacao));

            AtualizarBotaoSelecionado((Button)sender);
        }

        private void AtualizarBotaoSelecionado(Button btn)
        {
            if (btn == null)
                return;

            if(btnSelecionado != null)
                btnSelecionado.Background = Cor.ObterCor(Domain.Enums.eCores.Nenhuma);

            btnSelecionado = btn;

            btnSelecionado.Background = Cor.ObterCor(Domain.Enums.eCores.Cinza9);
        }
    }
}
