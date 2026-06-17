using Accounting.Core.Application.Journals.Commands.CreateJournal;
using Accounting.Core.Contracts.Journals;
using Accounting.Core.Domain.Journals.Events;
using Accounting.Core.RequestResponse.Journals.Commands;
using Accounting.Infrastructure.Data.EventSourcing.Write;
using Accounting.Infrastructure.Data.EventSourcing.Write.Journals;
using Asp.Versioning;
using BuildingBlocks.API.Extensions;
using BuildingBlocks.API.Infrastructure;
using BuildingBlocks.Integration.Marten.Extensions;
using FluentValidation;
using JasperFx;
using JasperFx.Events;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;
using Marten;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Marten;

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

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssemblyContaining<CreateJournalCommandValidator>();

builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();

    opts.Discovery.IncludeAssembly(typeof(CreateJournalCommandHandler).Assembly);
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApiVersioningConfiguration();

var app = builder.Build();

app.UseExceptionHandler(options => { });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();