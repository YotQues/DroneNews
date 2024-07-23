using CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DroneNews.CommandHandlers
{
    public static class CommandHandlerHelper
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            Type iCommandHandler = typeof(CommandHandlerAttribute);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.IsDefined(iCommandHandler, false));

            foreach (var type in types)
            {
                services.AddScoped(type, type);
            }
            return services;
        }
    }
}
