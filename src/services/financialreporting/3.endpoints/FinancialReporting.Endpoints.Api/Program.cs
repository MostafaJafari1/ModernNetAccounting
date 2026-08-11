using FinancialReporting.Core.Application.Journals.EventHandlers;
using FinancialReporting.Core.Contracts.Journals;
using FinancialReporting.Endpoints.Api.Options;
using FinancialReporting.Infrastructure.Data.NoSql.Repositories;
using JasperFx.CodeGeneration.Model;
using MongoDB.Driver;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

// Register MongoClient
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["MongoSettings:ConnectionString"];
    return new MongoClient(connectionString);
});

// Register IMongoDatabase
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoSettings:DatabaseName"];
    return client.GetDatabase(databaseName);
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