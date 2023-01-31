using BlazorFluentUI;
using Portal.Cache;
using Portal.Data;
using Portal.Data.Models;
using Portal.Exceptions.ExceptionHandling;
using Portal.Services.ModelServices.Invoices;
using Portal.Services.ModelServices.Plays;
using Portal.Services.Reports;

namespace Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddBlazorFluentUI();

            builder.Services.AddSingleton<ZoneTreeCache<Play>>();

            builder.Services.Configure<PortalDatabaseSettings>(builder.Configuration.GetSection("PortalDatabaseSettings"));
            builder.Services.AddScoped<InvoiceModelService>();
            builder.Services.AddScoped<PlayModelService>();
            builder.Services.AddScoped<PlanModelService>();
            builder.Services.AddScoped<ReportService>();
            //builder.Services.AddSingleton<IActorBridge, AkkaService>();
            //builder.Services.AddHostedService(sp => (AkkaService)sp.GetRequiredService<IActorBridge>());

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.MapControllers();

            app.Run();
        }
    }
}