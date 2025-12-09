namespace Tests;

public static class CatConverters
{
	public static int ConvertToAge(CatEntity catEntity)
	{
		return DateTime.Today.Year - catEntity.BirthDate.Year;
	}

	public static CatFood ConvertToCatFood(CatEntity catEntity)
    {
        return new CatFood
        {
            FoodType = catEntity.Food.FoodType,
            Quantity = catEntity.Food.Quantity
        };
	}
}
