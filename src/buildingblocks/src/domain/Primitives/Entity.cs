using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.Rules;

namespace BuildingBlocks.Domain.Primitives;

public abstract class Entity : IEntity
{
    protected Entity() { }

    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Entity Id cannot be empty.", nameof(id));

        Id = id;
    }

    public Guid Id { get; private set; }

    protected void CheckRule(IBusinessRule rule) => Check.Rule(rule);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;
        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
}