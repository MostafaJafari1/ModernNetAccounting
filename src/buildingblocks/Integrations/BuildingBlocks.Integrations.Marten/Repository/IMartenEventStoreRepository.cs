using JasperFx.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integrations.Marten.Repository;

// مسئولیت: فقط عملیات پایه‌ای read/write روی event stream
public interface IMartenEventStoreRepository<TAggregate> where TAggregate : class
{
    // ثبت رویدادها - نسخه با Expected Version برای Optimistic Concurrency
    Task AppendEventsAsync(
        Guid streamId,
        IEnumerable<object> events,
        long? expectedVersion = null,
        CancellationToken cancellationToken = default);

    // بازسازی aggregate تا یک نسخه خاص، null یعنی آخرین نسخه
    Task<TAggregate?> GetByIdAsync(
        Guid streamId,
        long? version = null,
        CancellationToken cancellationToken = default);

    // متادیتای stream بدون بازسازی کامل aggregate
    Task<StreamState?> GetStreamStateAsync(
        Guid streamId,
        CancellationToken cancellationToken = default);

    // Soft delete - ثبت رویداد آرشیو، نه تغییر مستقیم stream
    Task ArchiveAsync(
        Guid streamId,
        CancellationToken cancellationToken = default);
}
