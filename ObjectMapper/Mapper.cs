using StarFuryDev.ObjectMapper.Attributes;
using System.Reflection;

namespace StarFuryDev.ObjectMapper;

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
		var mapFromAttr = targetType.GetCustomAttribute<MapFromAttribute>()
			?? throw new InvalidOperationException($"Target type {targetType.Name} must have a MapFrom attribute.");

		var mapIgnoreAttr = targetType.GetCustomAttribute<MapIgnoreAttribute>();
		if (mapIgnoreAttr is not null)
		{
			throw new InvalidOperationException($"Target type {targetType.Name} is marked with MapIgnoreAttribute, mapping is not allowed.");
		}

		if (mapFromAttr.SourcePropertyName != source.GetType().Name)
		{
			throw new InvalidOperationException($"Source type {source.GetType().Name} does not match the expected source type {mapFromAttr.SourcePropertyName}.");
		}

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

		foreach (var targetProp in targetProperties)
		{
			if (!targetProp.CanWrite)
				continue;

			// Check for MapIgnoreAttribute
			var mapIgnoreAttr = targetProp.GetCustomAttribute<MapIgnoreAttribute>();
			if (mapIgnoreAttr is not null)
			{
				// If the property is marked with MapIgnoreAttribute, skip mapping for this property
				continue;
			}

			// Check for MapFromUsingAttribute
			var mapFromUsingAttr = targetProp.GetCustomAttribute<MapFromUsingAttribute>();
			if (mapFromUsingAttr is not null)
			{
				var converterMethod = mapFromUsingAttr.ConverterType.GetMethod(mapFromUsingAttr.ConverterMethodName ?? Constants.DefaultConvertMethodName, BindingFlags.Public | BindingFlags.Static);
				if (converterMethod is not null)
				{
					var convertedValue = converterMethod.Invoke(null, [source]);
					targetProp.SetValue(target, convertedValue);
				}
				continue;
			}

			// Check for MapFromAttribute
			var mapFromAttr = targetProp.GetCustomAttribute<MapFromAttribute>();

			// Create source prop name based on MapFromAttribute or default to property name
			var sourcePropName = mapFromAttr?.SourcePropertyName ?? targetProp.Name;
			if (!(sourcePropertiesDict.TryGetValue(sourcePropName, out var sourceObjectProp) && sourceObjectProp.CanRead))
				continue;

			var sourceValue = sourceObjectProp.GetValue(source);
			if (sourceValue is null)
				continue;

			// check if prop is a List<> or Dictionary<,>
			if (targetProp.PropertyType.IsGenericType)
			{
				Type genericTypeDef = targetProp.PropertyType.GetGenericTypeDefinition();
				Type[] genericArgs = targetProp.PropertyType.GetGenericArguments();
				if (genericTypeDef == typeof(List<>))
				{
					Type itemType = genericArgs[0];
					Type listType = typeof(List<>).MakeGenericType(itemType);
					var copiedList = Activator.CreateInstance(listType, sourceValue);
					targetProp.SetValue(target, copiedList);
				}
				else if (genericTypeDef == typeof(Dictionary<,>))
				{
					Type keyType = genericArgs[0];
					Type valueType = genericArgs[1];
					Type dictType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
					var copiedDict = Activator.CreateInstance(dictType, sourceValue);
					targetProp.SetValue(target, copiedDict);
				}
				continue;
			}
			// Check if prop is a class (excluding string and value types)
			else if (targetProp.PropertyType.IsClass && targetProp.PropertyType != typeof(string))
			{
				// Use MakeGenericMethod to call MapObject with correct types
				var mapObjectMethod = typeof(Mapper).GetMethod("MapObject", BindingFlags.NonPublic | BindingFlags.Static);
				if (mapObjectMethod is not null)
				{
					var genericMapObject = mapObjectMethod.MakeGenericMethod(sourceObjectProp.PropertyType, targetProp.PropertyType);
					var convertedObject = genericMapObject.Invoke(null, [sourceValue]);
					targetProp.SetValue(target, convertedObject);
				}
				continue;
			}
			else
			{
				// basic property mapping for primitive types and strings
				if (targetProp.PropertyType.IsAssignableFrom(sourceObjectProp.PropertyType))
				{
					targetProp.SetValue(target, sourceValue);
				}
			}
		}
		return target;
	}
}
