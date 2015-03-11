using LightInject;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Repositories;

namespace Ltht.TechTest.Ioc
{
    public class ContainerFactory
    {
        public static IServiceContainer Create()
        {
            var container = new ServiceContainer();
            container.Register<IDbContextProvider, DbContextProvider>(new PerContainerLifetime());
            container.Register<IColourRepository, ColourRepository>();
            container.Register<IPersonRepository, PersonRepository>();
            return container;
        }
    }
}