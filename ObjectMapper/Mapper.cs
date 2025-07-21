using System.Reflection;

namespace ObjectMapper;

public static class Mapper
{
    public static T2 Map<T1, T2>(T1 source) where T2 : class, new()
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source), $"{nameof(source)} cannot be null");
        }

        var sourceType = typeof(T1);
        var targetType = typeof(T2);

        var target = new T2();

        var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var sourcePropertiesDict = sourceProperties.ToDictionary(p => p.Name);

        foreach (var prop in targetProperties)
        {
            if (prop.CanWrite)
            {
                if (sourcePropertiesDict.TryGetValue(prop.Name, out var sourceProp) && sourceProp.CanRead)
                {
                    var value = sourceProp.GetValue(source);
                    if (value is not null && prop.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        prop.SetValue(target, value);
                    }
                    else if (value is null && !prop.PropertyType.IsValueType)
                    {
                        prop.SetValue(target, null);
                    }
                    // Optionally, handle type conversion or ignore incompatible types
                }
            }
        }
        return target;
    }
}
