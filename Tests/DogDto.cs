using ObjectMapper.Attributes;

namespace Tests;

[MapFrom(nameof(DogEntity))]
internal class DogDto
{
	[MapFromUsing(typeof(DogConverters))]
	public string Name { get; set; } = default!;
}
