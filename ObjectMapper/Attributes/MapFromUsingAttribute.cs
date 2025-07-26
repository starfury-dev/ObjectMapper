namespace ObjectMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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
