using CF.Application;
using CF.Domain.Entities;
using CF.Domain.Interfaces;
using CF.Presentation.Telas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CF.Presentation
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        public static List<CFStatus> Status { get; private set; }
        public static List<CFTipoOperacao> TipoOperacao { get; private set; }
        public static List<CFTipoTransacaoFinanceira> TipoTransacaoFinanceiras { get; private set; }

        public static CultureInfo CulturaSistema = Thread.CurrentThread.CurrentCulture;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            DefinirCulturaSistema();
        }


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Windows.UI.Xaml.Window.Current.Content as Frame;


            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (Bootstrap.Iniciar())
                    {
                        rootFrame.Navigate(typeof(PopupBaseDados), e.Arguments);
                    }
                    else
                    {
                        IniciarSistemaPrincipal();
                    }
                }
                Window.Current.Activate();
            }
        }

        public static void IniciarSistemaPrincipal()
        {
            var rootFrame = (Frame)Window.Current.Content;

            rootFrame.Navigate(typeof(MainPage));

            Bootstrap.Iniciar();
            CarregarColecoes();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private static void CarregarColecoes()
        {
            try
            {
                var cfStatusRepository = Bootstrap.Container.GetInstance<ICFStatusRepository>();
                var cfTipoOperacaoRepository = Bootstrap.Container.GetInstance<ICFTipoOperacaoRepository>();
                var cfTIpoTransacaoFinanceiraRepository = Bootstrap.Container.GetInstance<ICFTipoTransacaoFinanceiraRepository>();

                Status = cfStatusRepository.ObterLista().ToList();
                TipoOperacao = cfTipoOperacaoRepository.ObterLista().ToList();
                TipoTransacaoFinanceiras = cfTIpoTransacaoFinanceiraRepository.ObterLista().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao carregar informações iniciais.");
            }
        }

        private static void DefinirCulturaSistema()
        {
            CulturaSistema = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = CulturaSistema;
            CultureInfo.DefaultThreadCurrentUICulture = CulturaSistema;

            Thread.CurrentThread.CurrentCulture = CulturaSistema;
            Thread.CurrentThread.CurrentUICulture = CulturaSistema;
        }
    }
}
