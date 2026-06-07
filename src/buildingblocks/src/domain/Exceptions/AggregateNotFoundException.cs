namespace BuildingBlocks.Domain.Exceptions;
public class AggregateNotFoundException : DomainException
{
    public AggregateNotFoundException(Type aggregateType, object id)
        : base($"{aggregateType.Name} with ID '{id}' was not found.")
    {
    }
}