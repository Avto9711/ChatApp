using ChatApp.Api.IoC;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ChatApp.Api.Startup;

namespace ChatApp.Api.Config
{
    public static class StructureMapConfig
    {
        public static IServiceProvider ConfigureIoC(this IServiceCollection services, Action func = null)
        {
            IContainer container = DependencyService.Container;
            container.Configure(config =>
            {
                config.Populate(services);

            });
            func?.Invoke();
            return container.GetInstance<IServiceProvider>();
        }
    }

    public static class DependencyService
    {
        private static Container _container;
        public static Container Container
        {
            get
            {
                if (_container == null)
                {
                    Registry registry = new Registry();
                    _container = new Container(registry);
                }
                return _container;
            }
        }
        public static object GetInstanceOf(Type type) => Container.GetInstance(type);
        public static T GetInstanceOf<T>() => (T)GetInstanceOf(typeof(T));
        public static bool CanResolve(Type type) => Container.TryGetInstance(type) != null;
    }
}
