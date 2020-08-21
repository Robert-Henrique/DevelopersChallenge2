using Application;
using Domain.Repositories;
using Domain.Transactions;
using Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;

namespace WebApp
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
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionReader, TransactionReader>();
            services.AddScoped<IExtractManager, ExtractManager>();

            services.AddControllersWithViews().AddNToastNotifyToastr(DefaultOptionsToastr(), DefaultNToastNotifyOptions());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseNToastNotify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Transaction}/{action=Index}/{id?}");
            });
        }

        private static NToastNotifyOption DefaultNToastNotifyOptions()
        {
            return new NToastNotifyOption
            {
                DefaultSuccessMessage = string.Empty,
                DefaultSuccessTitle = string.Empty,
                DefaultAlertMessage = string.Empty,
                DefaultAlertTitle = string.Empty,
                DefaultErrorMessage = string.Empty,
                DefaultErrorTitle = string.Empty,
                DefaultInfoMessage = string.Empty,
                DefaultInfoTitle = string.Empty,
                DefaultWarningMessage = string.Empty,
                DefaultWarningTitle = string.Empty
            };
        }

        private static ToastrOptions DefaultOptionsToastr()
        {
            return new ToastrOptions
            {
                CloseButton = false,
                Debug = false,
                NewestOnTop = false,
                ProgressBar = false,
                PositionClass = "toast-top-right",
                PreventDuplicates = false,
                ShowDuration = 300,
                HideDuration = 1000,
                TimeOut = 5000,
                ExtendedTimeOut = 1000,
                ShowEasing = "swing",
                HideEasing = "linear",
                ShowMethod = "fadeIn",
                HideMethod = "fadeOut"
            };
        }
    }
}
