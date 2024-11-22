using CF.Domain.Enumerador;
using CF.Domain.Utilitarios;
using JJ.UW.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CF.Presentation.Componentes
{
    public sealed partial class SeletorMesAno : UserControl
    {
        #region Propriedades
        private Item _mesAtual;
        private Item _anoAtual;
        private Item _mesSelecionado;
        private Item _anoSelecionado;
        private EventHandler dataSelecionada;
        #endregion

        public SeletorMesAno()
        {
            this.InitializeComponent();
        }

        #region Eventos
        private void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CarregarComboBox();
            SelecionarMesEAnoAtual();
            ExibirDataSelecionada();
        }
        private void lbMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mesSelecionado = (Item)((ListBox)sender).SelectedItem;
        }
        private void lbAno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _anoSelecionado = (Item)((ListBox)sender).SelectedItem;
        }
        private void btnSelecionarMes_Click(object sender, RoutedEventArgs e)
        {
            popupMes.IsOpen = !popupMes.IsOpen;

            lbMes.SelectedItem = _mesAtual;
        }
        private void btnSelecionarAno_Click(object sender, RoutedEventArgs e)
        {
            popupAno.IsOpen = !popupAno.IsOpen;

            lbAno.SelectedItem = _anoAtual;
        }
        private void popupAno_Opened(object sender, object e)
        {
            lbAno.ScrollIntoView(_anoAtual);
            btnSelecionarAno.Background = Cor.ObterCor(eCores.Cinza9);

        }
        private void popupAno_Closed(object sender, object e)
        {
            lbAno.ScrollIntoView(lbAno.Items.Cast<Item>().FirstOrDefault());
            btnSelecionarAno.Background = Cor.ObterCor(eCores.Nenhuma);
        }
        private void popupMes_Opened(object sender, object e)
        {
            lbMes.ScrollIntoView(_mesAtual);
            btnSelecionarMes.Background = Cor.ObterCor(eCores.Cinza9);
        }
        private void popupMes_Closed(object sender, object e)
        {
            lbMes.ScrollIntoView(lbMes.Items.Cast<Item>().FirstOrDefault());
            btnSelecionarMes.Background = Cor.ObterCor(eCores.Nenhuma);
        }
        private void btnConfirmarMes_Click(object sender, RoutedEventArgs e)
        {
            AtualizarMesAnoAtual();
            ExecutarEventoPersonalizado();

            FecharPopupMes();
        }
        private void btnConfirmarAno_Click(object sender, RoutedEventArgs e)
        {
            AtualizarMesAnoAtual();
            ExecutarEventoPersonalizado();

            FecharPopupAno();
        }
        #endregion

        #region Metodos
        private void CarregarComboBox()
        {
            CarregarCboMes();
            CarregarCboAno();
        }
        private void CarregarCboMes()
        {
            var cultureInfo = new CultureInfo("pt-BR");
            var monthNames = cultureInfo.DateTimeFormat.MonthNames;

            for (int i = 0; i < 12; i++)
            {
                var item = new Item
                {
                    Id = i + 1,
                    Nome = monthNames[i]
                };

                item.Nome = char.ToUpper(item.Nome[0], cultureInfo) + item.Nome.Substring(1);

                lbMes.Items.Add(item);
            }

            lbMes.DisplayMemberPath = "Nome";
            lbMes.HorizontalAlignment = HorizontalAlignment.Center;
        }
        private void CarregarCboAno()
        {
            for (int i = 2024; i < 2025; i++)
            {
                var item = new Item { Id = i, Nome = i.ToString() };

                lbAno.Items.Add(item);
            }

            lbAno.DisplayMemberPath = "Nome";
            lbAno.HorizontalAlignment = HorizontalAlignment.Center;
        }
        private void SelecionarMesEAnoAtual()
        {
            _mesAtual = lbMes.Items.Cast<Item>().FirstOrDefault(i => i.Id == DateTime.Now.Month);
            if (_mesSelecionado != null)
                lbMes.SelectedItem = _mesAtual;

            _anoAtual = lbAno.Items.Cast<Item>().FirstOrDefault(i => i.Id == DateTime.Now.Year);

            if (_anoSelecionado != null)
                lbAno.SelectedItem = _anoAtual;

            AtualizarMesAnoAtual();
        }
        private void AtualizarMesAnoAtual()
        {
            if (_mesSelecionado != null)
                _mesAtual = _mesSelecionado;

            if (_anoSelecionado != null)
                _anoAtual = _anoSelecionado;

            _mesSelecionado = null;
            _anoSelecionado = null;
        }
        private void FecharPopupMes()
        {
            ExibirDataSelecionada();
            popupMes.IsOpen = false;
        }
        private void FecharPopupAno()
        {
            ExibirDataSelecionada();
            popupAno.IsOpen = false;
        }
        private void ExibirDataSelecionada()
        {
            btnSelecionarMes.Content = _mesAtual.Nome.ToString();
            btnSelecionarAno.Content = _anoAtual.Nome.ToString();
        }
        private void ExecutarEventoPersonalizado()
        {
            if (dataSelecionada == null)
                return;

            if (popupMes.IsOpen && popupAno.IsOpen)
                return;

            AtualizarMesAnoAtual();
            dataSelecionada.Invoke(new DateTime(_anoAtual.Id, _mesAtual.Id, 1), null);
        }
        #endregion

        #region Metodos Publico
        public void ObterDataSelecionada(EventHandler handler)
        {
            dataSelecionada = handler;
        }
        public DateTime ObterDataSelecionada()
        {
            return new DateTime(_anoAtual.Id, _mesAtual.Id, 1);
        }
        #endregion

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
