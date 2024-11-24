using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using CF.Domain.Enumerador;

namespace CF.Domain.Utilitarios
{
    public static class Cor
    {
        public static Brush ObterCor(eCores eCores)
        {
            Brush brush = null;

            try
            {
                brush = (Brush)Application.Current.Resources[eCores.ToString()];
            }
            catch 
            {
                brush = (Brush)Application.Current.Resources[eCores.Branco.ToString()];
            }

            return brush;
        }

        public static string ObterCorHexadecimal(eCores eCores)
        {
            string corHex = "#FFFFFF";

            try
            {
                var brush = ObterCor(eCores);

                if (brush is SolidColorBrush solidColorBrush)
                {
                    var color = solidColorBrush.Color;
                    corHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
            }
            catch
            {
                corHex = "#FFFFFF";
            }

            return corHex;
        }
    }
}
