using Accounting.Core.Application.Journals.Commands.CreateJournal;
using Accounting.Core.Contracts.Journals;
using Accounting.Core.Domain.Journals.Events;
using Accounting.Infrastructure.Data.EventSourcing.Write.Journals;
using Asp.Versioning;
using JasperFx;
using JasperFx.Events;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;
using Marten;
using Wolverine;
using Wolverine.Marten;
using BuildingBlocks.Integration.Marten.Extensions;
using Accounting.Infrastructure.Data.EventSourcing.Write;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJournalRepository, JournalRepository>();

var accountingSettings = builder.Configuration
    .GetSection("Accounting")
    .Get<AccountingMartenSettings>() ?? new AccountingMartenSettings();

//Marten EventSorucing Database Configuration
builder.Services
    .AddMartenDefaults(
        builder.Configuration.GetConnectionString("Default")!,
        options => AccountingMartenConfig.Configure(options, accountingSettings))
    .IntegrateWithWolverine()
    .AddAsyncDaemon(DaemonMode.Solo);

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(CreateJournalCommandHandler).Assembly);
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();