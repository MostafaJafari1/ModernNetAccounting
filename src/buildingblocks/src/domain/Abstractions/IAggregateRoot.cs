namespace BuildingBlocks.Domain.Abstractions;

public interface IAggregateRoot : IEntity
{
    // دریافت تمام رویدادهای دامنه ثبت شده
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    // پاک کردن رویدادهای دامنه پس از انتشار
    void ClearDomainEvents();
}
