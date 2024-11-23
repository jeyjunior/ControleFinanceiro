using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.System;
using JJ.UW.Core.DTOs;
using CF.Domain.Enumerador;
using CF.Domain.Utilitarios;

namespace CF.Presentation.Componentes
{
    public sealed partial class SeletorMesAno : UserControl
    {
        #region Propriedades
        private List<Item> meses = new List<Item>();
        private List<Item> anos = new List<Item>();
        private Item mesSelecionado = null;
        private Item anoSelecionado = null;

        private int indiceMesAtual = 0;
        private int indiceAnoAtual = 0;

        private bool popupMesFocado = false;
        #endregion

        #region Construtor
        public SeletorMesAno()
        {
            this.InitializeComponent();

            CarregarMeses();
            CarregarAnos();
            CarregarMesAnoAtual();

            AtualizarConteudoDosBotoesMesNoPopup();
            AtualizarConteudoDosBotoesAnoNoPopup();

            AtualizarBotaoMesAtual();
            AtualizarBotaoAnoAtual();
        }
        #endregion

        #region Eventos
        private void btnMes_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Right)
            {
                btnAno.Focus(FocusState.Keyboard);
                popupMes.IsOpen = false;
            }
        }

        private void btnMesMeio_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!popupMes.IsOpen || !popupMesFocado)
                return;

            if (e.Key == VirtualKey.Up)
            {
                SelecionarMes(btnMesCima);
            }

            else if (e.Key == VirtualKey.Down)
            {
                SelecionarMes(btnMesBaixo);
            }

            AtualizarConteudoDosBotoesMesNoPopup();
        }

        private void btnAno_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Left)
            {
                btnMes.Focus(FocusState.Keyboard);
                popupAno.IsOpen = false;
            }
        }

        private void btnAnoMeio_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!popupAno.IsOpen || popupMesFocado)
                return;

            if (e.Key == VirtualKey.Up)
            {
                SelecionarAno(btnAnoCima);
            }

            else if (e.Key == VirtualKey.Down)
            {
                SelecionarAno(btnAnoBaixo);
            }

            AtualizarConteudoDosBotoesAnoNoPopup();
        }

        private void popupMes_Opened(object sender, object e)
        {
            AtualizarFocoPopup();
        }

        private void popupAno_Opened(object sender, object e)
        {
            AtualizarFocoPopup();
        }

        private void btnMes_Click(object sender, RoutedEventArgs e)
        {
            AtualizarConteudoDosBotoesMesNoPopup();

            canvasMes.Visibility = Visibility.Visible;
            popupMes.IsOpen = true;

            popupMesFocado = true;
            btnMesMeio.Focus(FocusState.Keyboard);
        }

        private void btnAno_Click(object sender, RoutedEventArgs e)
        {
            AtualizarConteudoDosBotoesAnoNoPopup();

            canvasAno.Visibility = Visibility.Visible;
            popupAno.IsOpen = true;

            popupMesFocado = false;
            btnAnoMeio.Focus(FocusState.Keyboard);
        }

        private void btnSelecinarMes_Click(object sender, RoutedEventArgs e)
        {
            SelecionarMes(sender);

            if (((Button)sender).FocusState == FocusState.Keyboard && ((Button)sender).Name == btnMesMeio.Name)
                btnMes.Focus(FocusState.Keyboard);

            canvasMes.Visibility = Visibility.Collapsed;
            popupMes.IsOpen = false;
        }

        private void btnSelecinarAno_Click(object sender, RoutedEventArgs e)
        {
            SelecionarAno(sender);

            if (((Button)sender).FocusState == FocusState.Keyboard && ((Button)sender).Name == btnAnoMeio.Name)
                btnAno.Focus(FocusState.Keyboard);

            canvasAno.Visibility = Visibility.Collapsed;
            popupAno.IsOpen = false;
        }

        private void btnSelecionarMes_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var pointerEventArgs = e.GetCurrentPoint(sender as UIElement);

            if (pointerEventArgs.Properties.MouseWheelDelta > 0)
            {
                indiceMesAtual = (indiceMesAtual > 0) ? indiceMesAtual - 1 : meses.Count - 1;
            }
            else
            {
                indiceMesAtual = (indiceMesAtual < meses.Count - 1) ? indiceMesAtual + 1 : 0;
            }

            AtualizarConteudoDosBotoesMesNoPopup();
        }

        private void btnSelecionarAno_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var pointerEventArgs = e.GetCurrentPoint(sender as UIElement);

            if (pointerEventArgs.Properties.MouseWheelDelta > 0)
            {
                indiceAnoAtual = (indiceAnoAtual > 0) ? indiceAnoAtual - 1 : anos.Count - 1;
            }
            else
            {
                indiceAnoAtual = (indiceAnoAtual < anos.Count - 1) ? indiceAnoAtual + 1 : 0;
            }

            AtualizarConteudoDosBotoesAnoNoPopup();
        }
        #endregion

        #region Metodos
        private void AtualizarFocoPopup()
        {
            if (popupMesFocado)
            {
                gridMes.BorderBrush = Cor.ObterCor(eCores.Branco);
                gridAno.BorderBrush = Cor.ObterCor(eCores.Cinza5);
            }
            else
            {
                gridMes.BorderBrush = Cor.ObterCor(eCores.Cinza5);
                gridAno.BorderBrush = Cor.ObterCor(eCores.Branco);
            }
        }

        private void AtualizarBotaoMesAtual()
        {
            btnMes.Content = mesSelecionado.Nome;
        }

        private void AtualizarBotaoAnoAtual()
        {
            btnAno.Content = anoSelecionado.Nome;
        }

        private void SelecionarAno(object sender)
        {
            anoSelecionado = anos.FirstOrDefault(i => i.Id == (int)((Button)sender).Tag);
            indiceAnoAtual = anos.FindIndex(m => m.Id == anoSelecionado.Id);

            if (indiceAnoAtual < 0)
                indiceAnoAtual = 10;

            AtualizarBotaoAnoAtual();
        }

        private void SelecionarMes(object sender)
        {
            mesSelecionado = meses.FirstOrDefault(i => i.Id == (int)((Button)sender).Tag);
            indiceMesAtual = meses.FindIndex(m => m.Id == mesSelecionado.Id);

            if (indiceMesAtual < 0)
                indiceMesAtual = 11;

            AtualizarBotaoMesAtual();
        }

        private void CarregarMesAnoAtual()
        {
            mesSelecionado = meses.FirstOrDefault(i => i.Id == DateTime.Now.Month);
            indiceMesAtual = meses.IndexOf(mesSelecionado);

            anoSelecionado = anos.FirstOrDefault(i => Convert.ToInt32(i.Nome) == DateTime.Now.Year);
            indiceAnoAtual = anos.IndexOf(anoSelecionado);
        }

        private void CarregarMeses()
        {
            var culturaInfo = new CultureInfo("pt-BR");
            var nomesMes = culturaInfo.DateTimeFormat.MonthNames;

            for (int i = 0; i < 12; i++)
            {
                var item = new Item
                {
                    Id = i + 1,
                    Nome = culturaInfo.TextInfo.ToTitleCase(nomesMes[i].ToLower()),
                };

                meses.Add(item);
            }
        }

        private void CarregarAnos()
        {
            int anoInicial = 2020;

            for (int i = 0; i <= 10; i++)
            {
                var item = new Item
                {
                    Id = i + 1,
                    Nome = anoInicial.ToString(),
                };

                anoInicial++;

                anos.Add(item);
            }
        }

        private void AtualizarConteudoDosBotoesMesNoPopup()
        {
            popupMesFocado = true;
            AtualizarFocoPopup();

            int indiceSup = 0;

            indiceSup = (indiceMesAtual - 1 >= 0) ? indiceMesAtual - 1 : 11;

            btnMesCima.Content = meses[indiceSup].Nome;
            btnMesCima.Tag = meses[indiceSup].Id;

            btnMesMeio.Content = meses[indiceMesAtual].Nome;
            btnMesMeio.Tag = meses[indiceMesAtual].Id;

            indiceSup = (indiceMesAtual + 1 <= 11) ? indiceMesAtual + 1 : 0;

            btnMesBaixo.Content = meses[indiceSup].Nome;
            btnMesBaixo.Tag = meses[indiceSup].Id;
        }

        private void AtualizarConteudoDosBotoesAnoNoPopup()
        {
            popupMesFocado = false;
            AtualizarFocoPopup();

            int indiceSup = 0;

            indiceSup = (indiceAnoAtual - 1 >= 0) ? indiceAnoAtual - 1 : 10;

            btnAnoCima.Content = anos[indiceSup].Nome;
            btnAnoCima.Tag = anos[indiceSup].Id;

            btnAnoMeio.Content = anos[indiceAnoAtual].Nome;
            btnAnoMeio.Tag = anos[indiceAnoAtual].Id;

            indiceSup = (indiceAnoAtual + 1 <= 10) ? indiceAnoAtual + 1 : 0;

            btnAnoBaixo.Content = anos[indiceSup].Nome;
            btnAnoBaixo.Tag = anos[indiceSup].Id;
        }
        #endregion

        #region Metodos Publico
        public DateTime ObterDataSelecionada()
        {
            return new DateTime(Convert.ToInt32(anoSelecionado.Nome), mesSelecionado.Id, 1);
        }
        #endregion
    }
}
