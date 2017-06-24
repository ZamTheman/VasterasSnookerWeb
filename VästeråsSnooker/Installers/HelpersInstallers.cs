using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Data;
using System.Data.SqlClient;
using VästeråsSnooker.Helpers;

namespace VästeråsSnooker.Installers
{
    public class HelpersInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                .Pick().WithServiceDefaultInterfaces()
                .If(c => c.Name.EndsWith("ConfigurationReader"))
                .LifestyleTransient());
        }
    }
}