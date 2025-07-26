namespace Tests;

public static class CatConverters
{
    public static int ConvertToAge(DateTime birthDate)
    {
        return DateTime.Today.Year - birthDate.Year;
    }

    public static CatFood ConvertToCatFood(CatFood catfood)
    {
        return new CatFood
        {
            FoodType = catfood.FoodType,
            Quantity = catfood.Quantity
        };
	}
}
