using IntegrationEvents.Models;
using MediatR;
using StructureMap;
using StructureMap.Graph;
using System.Web;

namespace IntegrationEvents.DependencyResolution
{
    public class DefaultRegistry : Registry {
        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                    scan.AddAllTypesOf(typeof(IRequestHandler<,>));
                    scan.AddAllTypesOf(typeof(INotificationHandler<>));
                });
            For<HttpContext>().Use(() => HttpContext.Current);
            For<IMediator>().Use<Mediator>();
            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);

        }
    }
}