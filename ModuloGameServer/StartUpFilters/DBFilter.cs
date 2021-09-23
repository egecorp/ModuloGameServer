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
            TempLog.Log("Configure: 13");

            return app =>
            {
                TempLog.Log("Configure: 17");
                try
                {
                    using (IServiceScope scope = app.ApplicationServices.CreateScope())
                    {
                        TempLog.Log("Configure: 22");
                        var dBSetup = scope.ServiceProvider.GetService<IModuloGameDBSetupService>();
                        TempLog.Log("Configure: 24");
                        dBSetup.Setup();
                        TempLog.Log("Configure: 26");
                        next(app);
                    }
                }
                catch (Exception e)
                {
                    TempLog.Log(e.Message + " " + e.StackTrace);
                    throw;
                }
            };

        }

    }
}
