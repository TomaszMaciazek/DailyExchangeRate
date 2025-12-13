using DailyExchangeRate.Application;
using DailyExchangeRate.Infrastructure;
using DailyExchangeRate.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;


var builder = Host.CreateApplicationBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

// Configure Serilog from configuration
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Use Serilog as the host logger
builder.Services.AddSerilog(logger);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWorkerApplication(builder.Configuration);

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("NbpRatesImportJob");

    q.AddJob<NbpRatesImportJob>(opts =>
        opts.WithIdentity(jobKey)
            .StoreDurably());

    // Trigger the job to run on a schedule specified in configuration
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("NbpRatesImportJobWithCron")
        .WithCronSchedule(builder.Configuration["NbpRatesImportJobCron"]!));

    // Trigger the job to run once at startup
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("NbpRatesImportJobStartup")
        .StartNow()
        .WithSimpleSchedule(x => x.WithRepeatCount(0)));
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

try
{
    Log.Information("Starting host");

    var host = builder.Build();

    using var scope = host.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ExchangeRateDbContext>();
    db.Database.Migrate();

    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
