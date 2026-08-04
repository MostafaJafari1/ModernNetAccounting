using FinancialReporting.Core.Application.Journals.EventHandlers;
using FinancialReporting.Core.Contracts.Journals;
using FinancialReporting.Infrastructure.Data.NoSql.Repositories;
using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Model;
using MongoDB.Driver;
using Wolverine;
using Wolverine.Configuration;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;
using JasperFx.CodeGeneration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ۱. ثبت IMongoClient به صورت Singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb")
        ?? "mongodb://localhost:27017";
    return new MongoClient(connectionString);
});

// ۲. ثبت IMongoDatabase به صورت Singleton (تغییر از Scoped به Singleton)
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("FinancialReportingDb");
});

// ۳. ثبت Repository
builder.Services.AddScoped<IGeneralLedgerWriteRepository, GeneralLedgerWriteRepository>();

builder.Host.UseWolverine(opts =>
{
    opts.Durability.KeepAfterMessageHandling = TimeSpan.FromDays(7);

    // اجازه دادن به Wolverine برای استفاده از Service Location در صورت لزوم
    opts.ServiceLocationPolicy = ServiceLocationPolicy.AlwaysAllowed;
    // ۱. اتصال به کافکا
    opts.UseKafka("localhost:9092,localhost:9095,localhost:9096");

    // ۲. تنظیمات گوش دادن با Consumer Group و Outbox
    opts.ListenToKafkaTopic("journal-entry-events")
        .ConfigureConsumer(config =>
        {
            config.GroupId = "accounting-service-group";
            config.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            config.EnableAutoCommit = false;
        })
        .UseDurableInbox()
        .ProcessInline();

    // ۳. سیاست‌های ری‌ترا (Retry Policies)
    opts.Policies.OnException<Exception>()
        .RetryTimes(3);

    // اسکن اسمبلی هندلرها
    opts.Discovery.IncludeAssembly(typeof(JournalPostedIntegrationHandler).Assembly);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();