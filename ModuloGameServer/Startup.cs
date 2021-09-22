using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModuloGameServer.Contracts;
using ModuloGameServer.Models;

namespace ModuloGameServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            string ConnectionString = Configuration.GetConnectionString("ModuloGameDBContext");


            services.AddSqlServer(ConnectionString);
            services.AddScoped<IModuloGameDBSetupService, ModuloGameDBSetupService>();
            services.AddScoped<IModuloGameBotService, BotService.BotService>();
            
            services.AddControllersWithViews();

            services.AddCors(o => o.AddPolicy("ApiPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddTransient<IStartupFilter, ModuloGameDBStartupFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (true || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("ApiPolicy");

            app.UseAuthorization();


            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<Startup>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "API/{controller}/{action}");

                endpoints.MapControllerRoute(
                    name: "testbot",
                    pattern: "Bot/{controller=Bot}");
                

                endpoints.MapGet(@".well-known/pki-validation/0367EA235D6369120E51F1DA3B74C1D8.txt", async context =>
                {
                    await context.Response.WriteAsync(@"D6D4805252C5F63150FC381B3EAD9C7F441F7F26D5CBAC0C0C5B04DA21149512
comodoca.com
46d3517eb12e1ab");
                });
                
                endpoints.MapGet(@".well-known/pki-validation/D5250CBBE7E445A3A868BDD9FD2ABDC0.txt", async context =>
                {
                    await context.Response.WriteAsync(@"10044330A70A7760F5186361F0793651A097D3201C9DEDE25EA23E1BCD5177B7
comodoca.com
64aa6b2a1d6c06c");
                });
                

                endpoints.MapGet(@"az", async context =>
                {
                    await context.Response.WriteAsync(@"az mthfcker");
                });



            });
        }
    }
}
