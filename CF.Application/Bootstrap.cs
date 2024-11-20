using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using CF.InfraData;

namespace CF.Application
{
    public class Bootstrap
    {
        public static Container Container;

        public static void Iniciar()
        {
            Container = new Container();
            Container.Options.DefaultLifestyle = Lifestyle.Scoped;

            BootstrapInfraData.Iniciar(Container);

            Container.Verify();
        }
    }
}
