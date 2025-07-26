using System.Reflection;
using ObjectMapper.Attributes;

namespace ObjectMapper;

public static class Mapper
{
    public static T2 Map<T1, T2>(T1 source) where T2 : class, new()
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source), $"{nameof(source)} cannot be null");
        }

		var target = MapObject<T1, T2>(source);
        
        return target;
    }

    private static T2 MapObject<T1, T2>(T1 source) where T2 : class, new()
    {
		var sourceType = typeof(T1);
		var targetType = typeof(T2);

		var target = new T2();

		var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		var sourcePropertiesDict = sourceProperties.ToDictionary(p => p.Name);

		foreach (var prop in targetProperties)
		{
			var mapFromAttr = prop.GetCustomAttribute<MapFromAttribute>();
			var sourcePropName = mapFromAttr?.SourcePropertyName ?? prop.Name;

			// Check if prop is a class (excluding string and value types)
			if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
			{
				if (prop.CanWrite && sourcePropertiesDict.TryGetValue(sourcePropName, out var sourceObjectProp) && sourceObjectProp.CanRead)
				{
					var sourceValue = sourceObjectProp.GetValue(source);
					if (sourceValue is not null)
					{
						// Use MakeGenericMethod to call MapObject with correct types
						var mapObjectMethod = typeof(Mapper).GetMethod("MapObject", BindingFlags.NonPublic | BindingFlags.Static);
						if (mapObjectMethod is not null)
						{
							var genericMapObject = mapObjectMethod.MakeGenericMethod(sourceObjectProp.PropertyType, prop.PropertyType);
							var convertedObject = genericMapObject.Invoke(null, [sourceValue]);
							prop.SetValue(target, convertedObject);
						}
					}
					else
					{
						prop.SetValue(target, null);
					}
				}
				continue;
			}

			// Check for MapFromUsingAttribute
			var mapFromUsingAttr = prop.GetCustomAttribute<MapFromUsingAttribute>();
			if (mapFromUsingAttr is not null)
			{
				var sourcePropNameForUsing = mapFromUsingAttr.SourcePropertyName ?? prop.Name;
				if (prop.CanWrite && sourcePropertiesDict.TryGetValue(sourcePropNameForUsing, out var sourcePropForUsing) && sourcePropForUsing.CanRead)
				{
					var value = sourcePropForUsing.GetValue(source);
					var converterMethod = mapFromUsingAttr.ConverterType.GetMethod(mapFromUsingAttr.ConverterMethodName, BindingFlags.Public | BindingFlags.Static);
					if (converterMethod is not null)
					{
						var convertedValue = converterMethod.Invoke(null, [value]);
						prop.SetValue(target, convertedValue);
					}
				}
				continue;
			}


			// Check for MapFromAttribute
			if (prop.CanWrite && sourcePropertiesDict.TryGetValue(sourcePropName, out var sourceProp) && sourceProp.CanRead)
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
		return target;
	}
}
