using StarFuryDev.ObjectMapper.Attributes;

namespace StarFuryDev.ObjectMapper.Tests;

public class MyMovieCollection
{
	public List<string> Movies { get; set; } = [];
	public Dictionary<int, string> MoviesDictionary { get; set; } = [];
}

[MapFrom(nameof(MyMovieCollection))]
public class FavouriteMovies
{
	public List<string> Movies { get; set; } = [];

	public Dictionary<int, string> MoviesDictionary { get; set; } = [];
}