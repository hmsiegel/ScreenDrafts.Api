using System.Reflection;

namespace ScreenDrafts.Api.Domain.Primitives;
public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Lazy<Dictionary<int, TEnum>> _enumerationDictionary
        = new(() => CreateEnumerationDictionary(typeof(TEnum)));

    protected Enumeration(int id, string? name)
        : this()
    {
        Id = id;
        Name = name;
    }

    protected Enumeration() => Name = string.Empty;

    public int Id { get; protected init; }

    public string? Name { get; protected init; }

    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum> a, Enumeration<TEnum> b) => !(a == b);
    public static IReadOnlyCollection<TEnum> GetValues() => _enumerationDictionary.Value.Values.ToList();
    public static TEnum? FromId(int id) => _enumerationDictionary.Value.TryGetValue(id, out TEnum? enumeration) ? enumeration : null;
    public static TEnum? FromName(string name) => _enumerationDictionary.Value.Values.SingleOrDefault(x => x.Name == name);
    public static bool Contains(int id)
    {
        return _enumerationDictionary.Value.ContainsKey(id);
    }

    public virtual bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && other.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return obj is Enumeration<TEnum> otherValue && otherValue.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 37;
    }

    private static Dictionary<int, TEnum> CreateEnumerationDictionary(Type enumType) => GetFieldsForType(enumType).ToDictionary(t => t.Id);

    private static IEnumerable<TEnum> GetFieldsForType(Type enumType) =>
        enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => enumType.IsAssignableFrom(fi.FieldType))
            .Select(fi => (TEnum)fi.GetValue(default)!);
}
