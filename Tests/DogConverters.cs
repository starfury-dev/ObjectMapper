namespace Tests;

internal class DogConverters
{
	public static string Convert(DogEntity dog)
	{
		return $"Cool dog {dog.Name}";
	}
}
