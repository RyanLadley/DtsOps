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
using dtso.auth.Logic.Interfaces;
using dtso.auth.Logic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using dtso.auth.Settings;
using dtso.core.Utilities;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

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
                .AddJsonFile("authsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"authsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AuthSettings>(Configuration);

            services.AddCors();
            services.AddMvc();

            //Add Support for JWT Tokens
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = _getTokenValidationParams(Configuration);
            });

            //Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();
            
            //Core
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IInvoiceManager, InvoiceManager>();
            services.AddScoped<VendorManager>();
            services.AddScoped<MaterialManager>();
            services.AddScoped<TicketManager>();
            services.AddScoped<TransferManager>();
            services.AddScoped<WordDocumentHandle>();

            //Auth
            services.AddScoped<IPasswordManager, PasswordManager>();
            services.AddScoped<TokenManager>();
            services.AddScoped<IUserRegistrar, UserRegistrar>();

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

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"StaticDocuments")),
                RequestPath = new PathString("/Documents"),
                EnableDirectoryBrowsing = true
            });

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }

        /// <summary>
        /// Uses the configuration to get the token settings to congifure the jwt validation 
        /// </summary>
        /// <param name="config"></param>
        private TokenValidationParameters _getTokenValidationParams(IConfigurationRoot config)
        {
            var tokenSettings = TokenSettings.parseFromConfig(config);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = tokenSettings.SigningKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = tokenSettings.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = tokenSettings.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                //Authentication Roles
                RoleClaimType = "Permissions"
            };

            return tokenValidationParameters;
        }
    }
}
