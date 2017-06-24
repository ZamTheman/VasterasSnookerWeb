using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VästeråsSnooker.DB;

namespace VästeråsSnooker.Installers
{
    public class DAInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().Pick().WithServiceDefaultInterfaces().If(c => c.Name.EndsWith("PlayerDA")));
            container.Register(Classes.FromThisAssembly().Pick().WithServiceDefaultInterfaces().If(c => c.Name.EndsWith("GameDA")));    
        }
    }
}