
using Countries__Cities.Core.Repository;
using Countries__Cities.Core.Service;
using Countries__Cities.Core.UnitOfWorks;
using Countries__Cities.Repository.Concrete;
using Countries__Cities.Repository.Repository;
using Countries__Cities.Repository.UnitOfWorks;
using Countries__Cities.Service.Mapping;
using Countries__Cities.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
using Autofac;
using Countries__Cities.Caching.ServicesCaching;

namespace Countries__CitiesAPI.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();

            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterType<CountryServiceWithCaching>().As<ICountryService>();
            builder.RegisterType<CityServiceWithCaching>().As<ICityService>();
       
        }
    }
}
