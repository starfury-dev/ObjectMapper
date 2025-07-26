namespace Tests;

public static class CatConverters
{
    public static int ConvertToAge(DateTime birthDate)
    {
        return DateTime.Today.Year - birthDate.Year;
    }
}
