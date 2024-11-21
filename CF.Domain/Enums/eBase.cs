using JJ.UW.Core.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Domain.Enums
{
    public enum eTipoOperacaoFinanceira
    {
        Todas = 0,
        Entrada = 1,
        Saida = 2,
    }

    public enum eIconesSVG
    {
        
    }

    public enum eIconesPNG
    {
        
    }

    public enum eIconesGlyph
    {
        [CodigoGlyph("1F4CA")]
        Dashboard,

        [CodigoGlyph("1F4B5")]
        Transacao,

        [CodigoGlyph("25B2")]
        TrianguloCima,
        [CodigoGlyph("25B6")]
        TrianguloDireita,
        [CodigoGlyph("25BC")]
        TrianguloBaixo,
        [CodigoGlyph("25C0")]
        TrianguloEsquerda,
        
        [CodigoGlyph("F784")]
        Circulo,
        [CodigoGlyph("F78D")]
        Quadrado,
        [CodigoGlyph("F798")]
        Losangulo,

        [CodigoGlyph("F7A4")]
        Adicionar,
        [CodigoGlyph("F7AB")]
        Fechar,
    }

    public enum eCores
    {
        Nenhuma,
        Branco,
        Preto,
        Verde1,
        Verde2,
        Verde3,
        Verde4,
        Azul1,
        Azul2,
        Azul3,
        Azul4,
        Vermelho1,
        Vermelho2,
        Vermelho3,
        Vermelho4,
        Amarelo,
        Laranja,
        Roxo,
        Rosa,
        Violeta,
        Cinza1,
        Cinza2,
        Cinza3,
        Cinza4,
        Cinza5,
        Cinza6,
        Cinza7,
        Cinza8,
        Cinza9,
        Cinza10,
        Cinza11,
        Cinza12,
    }
}
