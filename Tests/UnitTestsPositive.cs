using StarFuryDev.ObjectMapper;

namespace StarFuryDev.ObjectMapper.Tests;

public class UnitTestsPositive
{
	[Fact]
	public void MapMovieLists_DefaultMapping()
	{
		var myMovieCollection = new MyMovieCollection
		{
			Movies = ["The Lord of the Rings", "The Matrix", "Interstellar"]
		};
		var favouriteMovies = Mapper.Map<MyMovieCollection, FavouriteMovies>(myMovieCollection);
		Assert.NotNull(favouriteMovies);
		Assert.IsType<FavouriteMovies>(favouriteMovies);
		Assert.Equal(myMovieCollection.Movies, favouriteMovies.Movies);

	}

	[Fact]
	public void MapMoviesDictionary_DefaultMapping()
	{
		var myMovieCollection = new MyMovieCollection
		{
			MoviesDictionary = new Dictionary<int, string>
			{
				{ 1, "The Lord of the Rings" },
				{ 2, "The Matrix" },
				{ 3, "Interstellar" }
			}
		};
		var favouriteMovies = Mapper.Map<MyMovieCollection, FavouriteMovies>(myMovieCollection);
		Assert.NotNull(favouriteMovies);
		Assert.IsType<FavouriteMovies>(favouriteMovies);
		Assert.Equal(myMovieCollection.MoviesDictionary, favouriteMovies.MoviesDictionary);
	}

	#region MapDotNetValueTypesWithDefaultMapping

	[Fact]
	public void MapValueTypes_DefaultMapping()
	{
		var source = new ValueTypesSource
		{
			BoolProp = true,
			ByteProp = 1,
			SByteProp = -1,
			CharProp = 'A',
			DecimalProp = 10.5m,
			DoubleProp = 20.5,
			FloatProp = 30.5f,
			IntProp = 100,
			UIntProp = 200,
			NintProp = -300,
			UuintProp = 400,
			LongProp = -5000,
			UlongProp = 6000,
			ShortProp = -700,
			UshortProp = 800
		};
		var destination = Mapper.Map<ValueTypesSource, ValueTypesDestination>(source);
		Assert.NotNull(destination);
		Assert.IsType<ValueTypesDestination>(destination);
		Assert.Equal(source.BoolProp, destination.BoolProp);
		Assert.Equal(source.ByteProp, destination.ByteProp);
		Assert.Equal(source.SByteProp, destination.SByteProp);
		Assert.Equal(source.CharProp, destination.CharProp);
		Assert.Equal(source.DecimalProp, destination.DecimalProp);
		Assert.Equal(source.DoubleProp, destination.DoubleProp);
		Assert.Equal(source.FloatProp, destination.FloatProp);
		Assert.Equal(source.IntProp, destination.IntProp);
		Assert.Equal(source.UIntProp, destination.UIntProp);
		Assert.Equal(source.NintProp, destination.NintProp);
		Assert.Equal(source.UuintProp, destination.UuintProp);
		Assert.Equal(source.LongProp, destination.LongProp);
		Assert.Equal(source.UlongProp, destination.UlongProp);
		Assert.Equal(source.ShortProp, destination.ShortProp);
		Assert.Equal(source.UshortProp, destination.UshortProp);
	}

	#endregion

	#region Test MappingConditions

	[Fact]
	public void MapSourceWithNoProperties()
	{
		var source = new SourceWithNoProperties();
		var destination = Mapper.Map<SourceWithNoProperties, DestinationForNoProperties>(source);
		Assert.NotNull(destination);
		Assert.Null(destination.MyProperty);
	}

	[Fact]
	public void MapSourceWithPrivateSetter()
	{
		var source = new SourceWithNoProperties();
		var destination = Mapper.Map<SourceWithNoProperties, DestinationWithReadOnlyProperty>(source);
		Assert.NotNull(destination);
		Assert.Equal("ReadOnly", destination.MyProperty);
	}

	[Fact]
	public void MapSourceToDestinationWithMapIgnoreAttributeOnProperty()
	{
		var source = new SourceForMapIgnore();
		var destination = Mapper.Map<SourceForMapIgnore, DestinatonWithMapIgnore>(source);
		Assert.NotNull(destination);
		Assert.Equal("Original text", destination.MyProperty);
	}

	[Fact]
	public void MapSourceToDestinationWithMapUsingAttributeOnProperty()
	{
		var source = new Source1() { Text1 = "Earth", Text2 = "Moon" };
		var destination = Mapper.Map<Source1, Destination1>(source);
		Assert.NotNull(destination);
		Assert.Equal($"{source.Text1} - {source.Text2} - from ToText3 method", destination.Text3);
		Assert.Equal($"{source.Text1} - {source.Text2} - from default convert method", destination.Text4);
	}

	#endregion
}