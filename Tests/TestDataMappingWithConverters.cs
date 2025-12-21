using StarFuryDev.ObjectMapper.Attributes;

namespace StarFuryDev.ObjectMapper.Tests;

internal class Source1
{
	public string Text1 { get; set; } = default!;
	public string Text2 { get; set; } = default!;
}

[MapFrom(nameof(Source1))]
internal class Destination1
{
	[MapFromUsing(typeof(ConvertersForTestData), nameof(ConvertersForTestData.ToText3))]
	public string Text3 { get; set; } = default!;

	[MapFromUsing(typeof(ConvertersForTestData))]
	public string Text4 { get; set; } = default!;
}

internal static class ConvertersForTestData
{
	public static string ToText3(Source1 source)
	{
		return $"{source.Text1} - {source.Text2} - from ToText3 method";
	}

	public static string Convert(Source1 source)
	{
		return $"{source.Text1} - {source.Text2} - from default convert method";
	}
}