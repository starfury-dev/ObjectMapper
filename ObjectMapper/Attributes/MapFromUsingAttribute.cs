namespace StarFuryDev.ObjectMapper.Attributes
{
    /// <summary>
    /// Maps a property or class from a source property using a specified converter method.
    /// The converter method must be static and belong to the specified converter type.
    /// A custom converter method or a default Convert method name is used to identify the converter method.
    /// The converter method will be called with the value of the source property to convert it to
    /// the target property type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class MapFromUsingAttribute : Attribute
    {
        public Type ConverterType { get; }
        public string? ConverterMethodName { get; }

        public MapFromUsingAttribute(Type converterType, string converterMethodName)
        {
            ConverterType = converterType ?? throw new ArgumentNullException(nameof(converterType));
            ConverterMethodName = converterMethodName ?? throw new ArgumentNullException(nameof(converterMethodName));
        }

        public MapFromUsingAttribute(Type converterType)
        {
			ConverterType = converterType ?? throw new ArgumentNullException(nameof(converterType));
		}
	}
}
