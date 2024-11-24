using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CF.Domain.Entities;
using JJ.UW.Core.Extensoes;
using CF.Domain.Interfaces;
using CF.Application;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using CF.Domain.Utilitarios;
using CF.Domain.Enumerador;


namespace CF.Presentation.Componentes
{
    public sealed partial class GridSaida : UserControl
    {
        #region Interfaces

        #endregion
        #region Propriedades
        private ObservableCollection<object> linhas { get; set; } = new ObservableCollection<object>();
        private CancellationTokenSource cancellationToken;
        #endregion

        #region Construtor
        public GridSaida()
        {
            this.InitializeComponent();


            lvwConteudo.ItemsSource = linhas;
        }
        #endregion

        #region Eventos
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AtualizarLarguraColunas();
        }
        #endregion

        #region Metodos
        private string ObterStatus(int fk_CFStatus)
        {
            if (fk_CFStatus <= 0)
                return "";

            //var status = cFStatusRepository.ObterStatus().FirstOrDefault(s => s.PK_CFStatus == fk_CFStatus);

            //if (status != null)
            //    return status.Status.ObterValorOuPadrao("").Trim();

            return "";
        }
        
        private string ObterTipoPagamento(int fk_CFTipoDePagamento)
        {
            if (fk_CFTipoDePagamento <= 0)
                return "";

            //var tipoPagamento = cFTipoDePagamentoRepository.ObterTipoDePagamentoInicial().Where(t => t.i == fK_CFTipoDePagamento).FirstOrDefault();

            //if (tipoPagamento != null)
            //    return tipoPagamento.Pagamento.ObterValorOuPadrao("").Trim();
            return fk_CFTipoDePagamento.ToString();
            //return "";
        }
        
        private string ObterTipoOperacao(int fk_CFTipoOperacao)
        {
            if (fk_CFTipoOperacao <= 0)
                return "";

            //var tipoOperacao = cFTipoOperacaoRepository.ObterTipoOperacao().FirstOrDefault(t => t.PK_CFTipoOperacao == fk_CFTipoOperacao);

            //if (tipoOperacao != null)
            //    return tipoOperacao.Nome.ObterValorOuPadrao("").Trim();

            return "";
        }

        private string ObterCategoria(CFCategoria cFCategoria)
        {
            if (cFCategoria == null)
                return "";

            return cFCategoria.Categoria.ObterValorOuPadrao("").Trim();
        }

        public void BindGrid(IEnumerable<CFRegistroFinanceiro> cFRegistroFinanceiro)
        {
            linhas.Clear();
            var ret = cFRegistroFinanceiro.Select(i => new
            {
                FrameInicCorFrameInicio = Cor.ObterCorHexadecimal(eCores.Rosa),
                FrameInicCorFrameMeio = Cor.ObterCorHexadecimal(eCores.Roxo),
                FrameInicCorFrameFim = Cor.ObterCorHexadecimal(eCores.Laranja),
                //PK_CFRegistroFinanceiro = i.PK_CFRegistroFinanceiro,
                //PK_CFTipoTransacaoFinanceira = i.FK_CFTipoTransacaoFinanceira,
                //TipoTransacaoFinanceira = Cor.ObterCor(Domain.Enumerador.eCores.Laranja),
                //Descricao = "Teste",
                //Categoria = "Teste 2",
                //Vencimento = i.DataVencimento.ToString("d"),
                //Pagamento = i.DataPagamento.ObterValorOuPadrao(""),
                //Status = i.FK_CFStatus,
                //Valor = i.Valor.ToString("C"),
            })
            .ToList();

            foreach (var item in ret)
                linhas.Add(item);
        }

        private void gConteudo_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var grid = sender as Grid;

            if (grid == null)
                return;

            for (int i = 0; i < gCabecalho.ColumnDefinitions.Count; i++)
                grid.ColumnDefinitions[i].Width = new GridLength(gCabecalho.ColumnDefinitions[i].ActualWidth);
        }
        
        private void AtualizarLarguraColunas()
        {
            if (gCabecalho.ColumnDefinitions.Count == 0)
                return;

            var largurasColunas = gCabecalho.ColumnDefinitions
                .Select(cd => cd.ActualWidth)
                .ToArray();

            for (int i = 0; i < lvwConteudo.Items.Count; i++)
            {
                if (lvwConteudo.ContainerFromIndex(i) is ListViewItem item && item.ContentTemplateRoot is Grid grid)
                {
                    for (int j = 0; j < largurasColunas.Length; j++)
                        grid.ColumnDefinitions[j].Width = new GridLength(largurasColunas[j]);
                }
            }
        }
        #endregion

        private void frameTipoTransacao_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var frame = (Frame)sender;

            frame.Background = Cor.ObterCor(eCores.Roxo);
        }
    }
}
