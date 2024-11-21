using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using CF.Domain.Enums;

namespace CF.Domain.Utilitarios
{
    public static class Cor
    {
        public static Brush ObterCor(eCores eCores)
        {
            return (Brush)Application.Current.Resources[eCores.ToString()];
        }
    }
}
