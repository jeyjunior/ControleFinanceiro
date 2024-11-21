using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using CF.Domain.Enums;
using Windows.UI.Xaml.Controls;

namespace CF.Domain.Utilitarios
{
    public static class Imagem
    {
        public static ImageSource Obter(eIconesSVG eIcones)
        {
            return new SvgImageSource { UriSource = new Uri($"ms-appx:///CF.Domain/Recursos/Icones/{eIcones.ToString()}.svg"), };
        }

        public static BitmapIcon Obter(eIconesPNG eIcones) 
        {
            return new BitmapIcon() { UriSource = new Uri($"ms-appx:///CF.Domain/Recursos/Icones/{eIcones.ToString()}.png") };
        }

        public static FontIcon Obter(eIconesGlyph eIcones, eCores cores)
        {
            return new FontIcon() { Glyph = eIcones.ToString(), }; 
        }
    }
}
