using System.Web.Mvc;
using AlloyTechEpi10.Business.Rendering;
using AlloyTechEpi10.Models.ViewModels;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using StructureMap;
using StructureMap.Web.Pipeline;

namespace AlloyTechEpi10.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(ConfigureContainer);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
        }

        private static void ConfigureContainer(ConfigurationExpression container)
        {
            //Swap out the default ContentRenderer for our custom
            container.For<IContentRenderer>().Use<ErrorHandlingContentRenderer>();
            container.For<ContentAreaRenderer>().Use<AlloyContentAreaRenderer>();

            //Implementations for custom interfaces can be registered here.

            container.For<LayoutModel>().Use<LayoutModel>().SetLifecycleTo<HttpContextLifecycle>();
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
