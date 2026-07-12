using Accounting.Core.Contracts.Journals.IntegrationEvents;
using Accounting.Core.Domain.Journals;
using System.Collections;
using Wolverine.Configuration;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IJournalRepository, JournalRepository>();

var accountingSettings = builder.Configuration
    .GetSection("Accounting")
    .Get<AccountingMartenSettings>() ?? new AccountingMartenSettings();

//Postgress DB SQL
builder.AddAccountingInfrastructure(builder.Configuration.GetConnectionString("Default")!);

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
    opts.Durability.KeepAfterMessageHandling = TimeSpan.FromDays(7);
    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();
    opts.Policies.UseDurableInboxOnAllListeners();
    opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

    #region Kafka Integration
    opts.UseKafka("localhost:9092,localhost:9095,localhost:9096");

    KafkaSubscriberConfiguration kafkaSubscriberConfiguration = opts.PublishMessage<JournalEntryPostedIntegrationEvent>()
      .ToKafkaTopic("journal-entry-events"); 
    #endregion

    opts.Policies.OnException<Exception>()
          .RetryWithCooldown(
            TimeSpan.FromSeconds(5), // تلاش اول پس از ۵ ثانیه
            TimeSpan.FromSeconds(5) // تلاش دوم پس از ۵ ثانیه دیگر
        );


    opts.ServiceLocationPolicy = ServiceLocationPolicy.AlwaysAllowed;

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