using FinancialReporting.Core.Application.Journals.EventHandlers;
using JasperFx.CodeGeneration.Model;
using Wolverine;
using Wolverine.Configuration;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Host.UseWolverine(opts =>
{
    opts.Durability.KeepAfterMessageHandling = TimeSpan.FromDays(7);
    //opts.Policies.AutoApplyTransactions();
    //opts.Policies.UseDurableLocalQueues();
    //opts.Policies.UseDurableInboxOnAllListeners();
    //opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

    // ۱. اتصال به کافکا
    //opts.UseKafka("localhost:9092");
    opts.UseKafka("localhost:9092,localhost:9095,localhost:9096");

    // ۲. تنظیمات گوش دادن با Consumer Group و Outbox
    opts.ListenToKafkaTopic("journal-entry-events")
        .ConfigureConsumer(config =>
        {
            // تنظیم نام کانسیومر گروپ
            config.GroupId = "accounting-service-group";
            config.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            config.EnableAutoCommit = false;
            // شروع از ابتدای لاگ در صورت عدم وجود آفست قبلی
        })
        // فعال‌سازی Inbox/Outbox برای پیام‌های دریافتی (تضمین Exactly-once processing)
        .UseDurableInbox()
        .ProcessInline();

    // ۳. سیاست‌های ری‌ترا (Retry Policies) اختصاصی برای خطاها
    opts.Policies.OnException<Exception>()
        .RetryTimes(3); // ۳ بار تلاش مجدد

    // اگر اسمبلی هندلرها متفاوت است
    opts.Discovery.IncludeAssembly(typeof(JournalPostedIntegrationHandler).Assembly);
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
