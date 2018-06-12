using IntegrationEvents.Models;
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
                });
            For<HttpContext>().Use(() => HttpContext.Current);
        }
    }
}