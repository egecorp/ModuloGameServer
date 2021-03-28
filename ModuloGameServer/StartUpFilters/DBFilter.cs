using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ModuloGameServer.Contracts;
using System;

namespace ModuloGameServer
{
    public class ModuloGameDBStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                using (IServiceScope scope = app.ApplicationServices.CreateScope())
                {
                    var dBSetup = scope.ServiceProvider.GetService<IModuloGameDBSetupService>();
                    dBSetup.Setup();
                    next(app);
                }

            };

        }

    }
}
