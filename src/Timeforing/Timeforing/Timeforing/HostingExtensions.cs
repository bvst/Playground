using Microsoft.EntityFrameworkCore;
using Timeforing.Components;
using Timeforing.DbAccess;
using Timeforing.Services;

namespace Timeforing;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("TimeforingDb"));
        
        builder.Services.AddSingleton<CsvExportService>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();
        
        app.MapGet("/export/csv", async (AppDbContext db, CsvExportService csv) =>
        {
            var entries = await db.TimeEntries.ToListAsync();
            var file = csv.ExportToCsv(entries);

            return Results.File(file, "text/csv", $"timer-{DateTime.Now:yyyyMMdd}.csv");
        });


        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        
        return app;
    }
}