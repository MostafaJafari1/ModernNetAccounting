namespace BuildingBlocks.Domain.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> _enumerations =
        CreateEnumerations();

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public int Value { get; protected init; }
    public string Name { get; protected init; } = string.Empty;

    public static IReadOnlyCollection<TEnum> GetValues() =>
        _enumerations.Values.ToList();

    public static TEnum? FromValue(int value) =>
        _enumerations.TryGetValue(value, out TEnum? enumeration)
            ? enumeration
            : null;

    public static TEnum? FromName(string name) =>
        _enumerations.Values
            .SingleOrDefault(e => e.Name == name);

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;
        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj) =>
        obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        return enumerationType
            .GetFields(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.DeclaredOnly)
            .Where(f => enumerationType.IsAssignableFrom(f.FieldType))
            .Select(f => (TEnum)f.GetValue(default)!)
            .ToDictionary(e => e.Value);
    }
}