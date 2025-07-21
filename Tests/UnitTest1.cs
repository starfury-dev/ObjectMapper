using ObjectMapper;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void MapCatEntityToCatDto_MapsAllPropertiesCorrectly()
    {
        var catEntity1 = new CatEntity(1, "Whiskers", "Siamese", new DateTime(2020, 1, 1), "Brown", true);
        var catDto = Mapper.Map<CatEntity, CatDto>(catEntity1);

        Assert.NotNull(catDto);
        Assert.IsType<CatDto>(catDto);
        Assert.Equal(catEntity1.Id, catDto.Id);
        Assert.Equal(catEntity1.Name, catDto.Name);
        Assert.Equal(catEntity1.Breed, catDto.Breed);
        Assert.Equal(catEntity1.BirthDate, catDto.BirthDate);
        Assert.Equal(catEntity1.Color, catDto.Color);
        Assert.Equal(catEntity1.IsIndoor, catDto.IsIndoor);
    }

    [Fact]
    public void MapCatEntityWithFoodToCatDto_MapsFoodPropertiesCorrectly()
    {
        var catEntity1 = new CatEntity(1, "Whiskers", "Siamese", new DateTime(2020, 1, 1), "Brown", true)
        {
            Food = new CatFood()
            {
                FoodType = "Dry",
                Quantity = 100
            }
        };
        var catDto = Mapper.Map<CatEntity, CatDto>(catEntity1);

        Assert.NotNull(catDto);
        Assert.IsType<CatDto>(catDto);
        Assert.Equal(catEntity1.Id, catDto.Id);
        Assert.NotNull(catDto.Food);
        Assert.Equal(catEntity1.Food.FoodType, catDto.Food.FoodType);
        Assert.Equal(catEntity1.Food.Quantity, catDto.Food.Quantity);
    }

    [Fact]
    public void MapCatEntityWithTeethToCatDto_MapsTeethPropertyCorrectly()
    {
        var catEntity1 = new CatEntity(1, "Whiskers", "Siamese", new DateTime(2020, 1, 1), "Brown", true)
        {
            Teeth = 30
        };
        var catDto = Mapper.Map<CatEntity, CatDto>(catEntity1);
        Assert.NotNull(catDto);
        Assert.IsType<CatDto>(catDto);
        Assert.Equal(catEntity1.Teeth, catDto.NoofTeeth);
    }
}