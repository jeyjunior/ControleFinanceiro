using CF.Application;
using CF.Application.Services;
using JJ.UW.Core.Enumerador;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CF.Presentation.Telas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopupBaseDados : Page
    {
        public PopupBaseDados()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppService.DefinirConexao(eConexao.MySql);
            App.IniciarSistemaPrincipal();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppService.DefinirConexao(eConexao.SQLite);
            App.IniciarSistemaPrincipal();
        }
    }
}
