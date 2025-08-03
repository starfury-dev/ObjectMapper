namespace ObjectMapper.Attributes
{
    /// <summary>
    /// Maps a property or class from a source property using a specified converter method.
    /// The converter method must be static and belong to the specified converter type.
    /// The source property name is used to identify the property in the source object that should be
    /// used for mapping.
    /// The converter method will be called with the value of the source property to convert it to
    /// the target property type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class MapFromUsingAttribute : Attribute
    {
        public string SourcePropertyName { get; }
        public Type ConverterType { get; }
        public string ConverterMethodName { get; }

        public MapFromUsingAttribute(Type converterType, string converterMethodName, string sourcePropertyName)
        {
            ConverterType = converterType ?? throw new ArgumentNullException(nameof(converterType));
            ConverterMethodName = converterMethodName ?? throw new ArgumentNullException(nameof(converterMethodName));
            SourcePropertyName = sourcePropertyName ?? throw new ArgumentNullException(nameof(sourcePropertyName));
        }
    }
}
