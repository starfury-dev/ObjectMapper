namespace ObjectMapper.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
public class MapFromAttribute : Attribute
{
    public string SourcePropertyName { get; }
    public MapFromAttribute(string sourcePropertyName)
    {
        SourcePropertyName = sourcePropertyName;
    }
}
