using System.Reflection;
using ObjectMapper.Attributes;

namespace ObjectMapper;

public static class Mapper
{
	/// <summary>
	/// Maps an object of type T1 to an object of type T2.
	/// This method uses reflection to map properties from the source object to the target object.
	/// </summary>
	/// <typeparam name="T1">Source type</typeparam>
	/// <typeparam name="T2">Target type</typeparam>
	/// <param name="source">Source object</param>
	/// <returns>Mapped target object</returns>
    public static T2 Map<T1, T2>(T1 source) where T2 : class, new()
	{
		if (source is null)
		{
			throw new ArgumentNullException(nameof(source), $"{nameof(source)} cannot be null");
		}

		var targetType = typeof(T2);
		_ = targetType.GetCustomAttribute<MapFromAttribute>()
			?? throw new InvalidOperationException($"Target type {targetType.Name} must have a MapFrom attribute.");

		var target = MapObject<T1, T2>(source);

		return target;
	}

	/// <summary>
	/// Maps an object of type T1 to an object of type T2.
	/// This method uses reflection to map properties from the source object to the target object.
	/// </summary>
	/// <typeparam name="T1">Source type</typeparam>
	/// <typeparam name="T2">Target type</typeparam>
	/// <param name="source">Source object</param>
	/// <returns>Mapped target object</returns>
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
			// Check for MapFromUsingAttribute
			var mapFromUsingAttr = prop.GetCustomAttribute<MapFromUsingAttribute>();
			if (mapFromUsingAttr is not null)
			{
				var sourcePropNameForUsing = mapFromUsingAttr.SourcePropertyName ?? prop.Name;
				if (prop.CanWrite && sourcePropertiesDict.TryGetValue(sourcePropNameForUsing, out var sourcePropForUsing) && sourcePropForUsing.CanRead)
				{
					var value = sourcePropForUsing.GetValue(source);
					if (value is null)
					{
						prop.SetValue(target, null);
						continue;
					}
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
			var mapFromAttr = prop.GetCustomAttribute<MapFromAttribute>();

			// Create source prop name based on MapFromAttribute or default to property name
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
			else
			{
				// basic property mapping for primitive types and strings
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
				}
			}
		}
		return target;
	}
}
