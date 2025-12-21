namespace StarFuryDev.ObjectMapper.Attributes;

/// <summary>
/// Maps a property or class from a source property.
/// The source property name is used to identify the property in the source object that should be
/// used for mapping.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class MapFromAttribute : Attribute
{
    public string SourcePropertyName { get; }
    public MapFromAttribute(string sourcePropertyName)
    {
        SourcePropertyName = sourcePropertyName;
    }
}
