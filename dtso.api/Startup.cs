using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using dtso.data.Context;
using Microsoft.EntityFrameworkCore;
using dtso.data.Repositories;
using dtso.data.Repositories.Interfaces;
using dtso.core.Managers;
using dtso.core.Managers.Interfaces;
using dtso.api.Utilities;

namespace dtso.api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            //Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            //Core
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IInvoiceManager, InvoiceManager>();
            services.AddScoped<VendorManager>();
            services.AddScoped<MaterialManager>();
            services.AddScoped<TicketManager>();

            //Api
            services.AddTransient<ResponseGenerator>();

            services.AddDbContext<MainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
