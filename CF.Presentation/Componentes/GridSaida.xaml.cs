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
using JJ.UW.Core.DTOs;
using JJ.UW.Core.Utilidades;
using JJ.UW.Core.Enumerador;


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
        private string TratarDescricao(CFTerceiro cFTerceiro)
        {
            if (cFTerceiro == null)
                return "";

            return cFTerceiro.Nome.ObterValorOuPadrao("").Trim();
        }

        private string TratarCategoria(CFTerceiro cFTerceiro)
        {
            if (cFTerceiro == null)
                return "";

            if (cFTerceiro.CFCategoria == null)
                return "";

            return cFTerceiro.CFCategoria.Categoria.ObterValorOuPadrao("").Trim();
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
                PK_CFRegistroFinanceiro = i.PK_CFRegistroFinanceiro,
                PK_CFTipoTransacaoFinanceira = i.FK_CFTipoTransacaoFinanceira,
                Descricao = TratarDescricao(i.CFTerceiro),
                Categoria = TratarCategoria(i.CFTerceiro),
                Vencimento = i.DataVencimento.ToString("d"),
                Pagamento = i.DataPagamento.ObterValorOuPadrao(""),
                Status = i.FK_CFStatus,
                Valor = i.Valor.ToString("C"),
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


        private void txbCellValorCabecalho_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var txb = (TextBlock)sender;

            var dataContext = txb.DataContext;

            if (dataContext == null)
                return;

            var item = dataContext as dynamic;

            if (item == null)
                return;

            var pkTipoTransacaoFinanceira = item.PK_CFTipoTransacaoFinanceira;

            if (pkTipoTransacaoFinanceira == null)
                return;

            txb.Foreground = (pkTipoTransacaoFinanceira == 1) ? Cor.ObterCor(eCores.Verde2) : Cor.ObterCor(eCores.Vermelho2);
        }

        private void ficoStatus_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var fontIcon = (FontIcon)sender;

            if (fontIcon == null)
                return;

            var status = Convert.ToInt32(fontIcon.Tag);

            switch (status)
            {
                case 1: 
                    fontIcon.Glyph = Imagem.ObterGlyph(eIconesGlyph.Check).Glyph;
                    fontIcon.Foreground = Cor.ObterCor(eCores.Verde1);
                break;
                case 2: 
                    fontIcon.Glyph = Imagem.ObterGlyph(eIconesGlyph.Relogio).Glyph;
                    fontIcon.Foreground = Cor.ObterCor(eCores.Amarelo);
                break;
                case 3: 
                    fontIcon.Glyph = Imagem.ObterGlyph(eIconesGlyph.Relogio).Glyph;
                    fontIcon.Foreground = Cor.ObterCor(eCores.Vermelho2);
                    break;
                default: break;
            }
        }
    }
}
