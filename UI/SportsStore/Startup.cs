using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders.Testing;
using SportsStore.DAL.Context;
using SportsStore.Infrastructure.AutoMapper;
using SportsStore.Services;
using SportsStore.Services.Data;

namespace SportsStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration Configuration) => _Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSportStoreServices();
            services.AddDirectoryBrowser();

            services.AddDbContext<SportStoreDB>(opt =>
            {
                opt.UseSqlServer(_Configuration.GetConnectionString("Default"));
                opt.EnableSensitiveDataLogging();
            });

            services.AddAutoMapper(opt =>
                {
                    opt.AddProfile<ProductMap>();
                },
                typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SportStoreDBInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseFileServer(new FileServerOptions
                {
                    EnableDefaultFiles = true,
                    EnableDirectoryBrowsing = true,
                    RequestPath = "/files"
                });
            }
            else
            {
                app.UseDefaultFiles();
                app.UseStaticFiles();
            }

            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
           
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
