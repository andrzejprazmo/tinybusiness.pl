using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using SEE.TinyBusinessMarket.BackEnd.BLL.Service;
using SEE.Framework.Core.Abstract;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Factory;
using Newtonsoft.Json.Serialization;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Payu;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Mail;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Template;
using System.Runtime.Loader;
using System.Reflection;
using System.IO;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Pdf;
using SEE.TinyBusinessMarket.BackEnd.Infrastructure.File;
using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

namespace SEE.TinyBusinessMarket.FrontEnd.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            Log.Logger.Information("Start application.");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add HttpContext
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Add anti forgery
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            // Add framework services.
            services
                .AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver
                    = new DefaultContractResolver());

            services.AddOptions();
            services.Configure<ApplicationConfiguration>(Configuration.GetSection(nameof(ApplicationConfiguration)));

            // Add logger
            services.AddTransient(typeof(ILog<>), typeof(Log<>));

            // Add application services
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DataContext")));

            // Factories
            services.AddTransient<IQuery, Query>()
                .AddTransient<IStore, Store>()
                .AddTransient<ITransaction, Transaction>();

            // Infrastructure
            services.AddTransient<IPayuManager, PayuManager>()
                .AddTransient<IMailSender, MailSender>()
                .AddTransient<ITemplateRepository, TemplateRepository>()
                .AddTransient<IPdfCreator, PdfCreator>()
                .AddTransient<IFileRepository, FileRepository>();

            // Services
            services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ICustomerService, CustomerService>()
                .AddTransient<IPaymentService, PaymentService>()
                .AddTransient<IDashboardService, DashboardService>()
                .AddTransient<IMailService, MailService>()
                .AddTransient<IInvoiceService, InvoiceService>()
                .AddTransient<IDownloadService, DownloadService>()
                .AddTransient<IProductService, ProductService>()
                ;

            // Authentication
            services
                .AddAuthentication(IdentityConsts.IdentityInstanceClientCookieName)
                .AddCookie(IdentityConsts.IdentityInstanceClientCookieName, cookie => 
                {
                    cookie.LoginPath = new PathString("/Admin/Login/");
                    cookie.AccessDeniedPath = new PathString("/Error/Forbidden/");
                });

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug().AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl-PL"),
            });


        }
    }
}

