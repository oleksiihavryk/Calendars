namespace Calendars.Resources.Shared.Extensions;
/// <summary>
///     Extensions for all class implementations.
/// </summary>
public static class SharedExtensions
{
    public static void ShallowUpdateProperties<T>(
        this T updatedEntity, 
        T sampleEntity,
        params string[]? exceptNameOfProperties)
        where T : class
    {
        var entityType = typeof(T);
        var properties = entityType
            .GetProperties()
            .Where(p =>
                p is { CanWrite: true, CanRead: true } &&
                exceptNameOfProperties?
                    .FirstOrDefault(propName => propName == p.Name) is null);

        foreach (var p in properties) 
            p.SetValue(updatedEntity, p.GetValue(sampleEntity));
    }
}