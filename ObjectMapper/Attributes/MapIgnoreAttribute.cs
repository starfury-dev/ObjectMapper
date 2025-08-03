namespace ObjectMapper.Attributes;

/// <summary>
/// Ignores the property or class during mapping.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class MapIgnoreAttribute : Attribute;