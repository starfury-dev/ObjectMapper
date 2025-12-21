using StarFuryDev.ObjectMapper.Attributes;

namespace StarFuryDev.ObjectMapper.Tests;

internal class SourceWithNoProperties
{
}

[MapFrom(nameof(SourceWithNoProperties))]
internal class DestinationForNoProperties
{
	public string MyProperty { get; set; } = default!;
}

[MapFrom(nameof(SourceWithNoProperties))]
internal class DestinationWithReadOnlyProperty
{
	public string MyProperty { get; } = "ReadOnly";
}

internal class SourceForMapIgnore
{
	public string MyProperty { get; set; } = "Some text";
}

[MapFrom(nameof(SourceForMapIgnore))]
internal class DestinatonWithMapIgnore
{
	[MapIgnore]
	public string MyProperty { get; set; } = "Original text";
}