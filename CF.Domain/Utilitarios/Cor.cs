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
    }
}
