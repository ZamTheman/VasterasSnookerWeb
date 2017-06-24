using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VästeråsSnooker.BL;

namespace VästeråsSnooker.Installers
{
    public class BLInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().Pick().WithServiceDefaultInterfaces().If(c => c.Name.EndsWith("PlayerRepository")));
            container.Register(Classes.FromThisAssembly().Pick().WithServiceDefaultInterfaces().If(c => c.Name.EndsWith("GamesRepository")));
        }
    }
}