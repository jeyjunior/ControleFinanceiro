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
using JJ.UW.Core.Extensoes;
using JJ.UW.Core.Enumerador;
using JJ.UW.Core.Utilidades;
using CF.Domain.DTO;
using CF.Domain.Utilitarios;
using CF.Domain.Enumerador;

namespace CF.Presentation.Componentes.Cartao
{
    public sealed partial class ResumoFinanceiro : UserControl
    {
        public ResumoFinanceiro()
        {
            this.InitializeComponent();
        }

        private void AtualizarOperacao(eIconesGlyph icone, Brush cor)
        {
            ficonTriangulo.AtualizarIcone(icone, cor);

            progresso.Foreground = cor;
            txbValorPago.Foreground = cor;
        }

        private void AtualizarProgresso(double min, decimal max, decimal valor)
        {
            progresso.Minimum = min;
            progresso.Maximum = Convert.ToDouble(max);
            progresso.Value = Convert.ToDouble(valor);
        }

        private void AtualizarPorcentagem(decimal porcentagem)
        {
            txbPorcentagem.Text = porcentagem.ToString("N0") + "%";
        }

        #region Metodos Publico
        public void AtualizarInformacoesIniciais(ResumoFinanceiroDTO resumoFinanceiro)
        {
            if (resumoFinanceiro == null)
                return;

            switch (resumoFinanceiro.TipoOperacaoFinanceira)
            {
                case eTipoOperacaoFinanceira.Todas:     break;
                case eTipoOperacaoFinanceira.Entrada:   AtualizarOperacao(eIconesGlyph.TrianguloCima, Cor.ObterCor(eCores.Verde1)); break;
                case eTipoOperacaoFinanceira.Saida:     AtualizarOperacao(eIconesGlyph.TrianguloBaixo, Cor.ObterCor(eCores.Vermelho1)); break;
            }

            txbTitulo.Text = resumoFinanceiro.Titulo.ObterValorOuPadrao("").Trim();

            AtualizarValores(resumoFinanceiro.ValorPago, resumoFinanceiro.ValorPendente);
        }

        public void AtualizarValores(decimal valorPago, decimal valorPendente)
        {
            txbValorPago.Text = valorPago.ToString("N2");
            txbValorPendente.Text = "/ " + valorPendente.ToString("N2");

            decimal total = valorPago + valorPendente;
            decimal porcentagem = (valorPago / total) * 100;

            AtualizarProgresso(0, total, valorPago);
            AtualizarPorcentagem(porcentagem);
        }
        #endregion
    }
}
