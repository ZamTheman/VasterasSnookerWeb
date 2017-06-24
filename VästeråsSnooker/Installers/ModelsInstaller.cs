using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.Installers
{
    public class ModelsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                .Pick().WithServiceDefaultInterfaces()
                .If(c => c.Name.EndsWith("Player"))
                .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                .Pick().WithServiceDefaultInterfaces()
                .If(c => c.Name.EndsWith("Game"))
                .LifestyleTransient());

        }
    }
}