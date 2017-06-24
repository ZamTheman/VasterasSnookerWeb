using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using VästeråsSnooker.Installers;
using Castle.Facilities.TypedFactory;

namespace VästeråsSnooker.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;
        
        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer();

            container.AddFacility<TypedFactoryFacility>();

            container.Install(
                new ModelsInstaller(), // No Dependencies
                new DAInstaller(), // Depends on Models
                new HelpersInstallers(),
                new BLInstaller(), // Depends on DataAccess and Helpers
                new ViewModelsInstallers(), // Depends on Models
                new ControllersInstaller() // Depends on alot
                );
            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}