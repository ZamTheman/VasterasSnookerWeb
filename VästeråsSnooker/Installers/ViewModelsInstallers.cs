using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VästeråsSnooker.Models;

namespace VästeråsSnooker.Installers
{
    public class ViewModelsInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .Pick().WithServiceDefaultInterfaces()
                    .If(c => c.Name.EndsWith("PlayerDetailsViewModel"))
                    .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                    .Pick().WithServiceDefaultInterfaces()
                    .If(c => c.Name.EndsWith("ViewPlayersViewModel"))
                    .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                    .Pick().WithServiceDefaultInterfaces()
                    .If(c => c.Name.EndsWith("GameViewModel"))
                    .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                    .Pick().WithServiceDefaultInterfaces()
                    .If(c => c.Name.EndsWith("GameListViewModel"))
                    .LifestyleTransient());
        }
    }
}