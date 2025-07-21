namespace Tests;

public class CatEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public DateTime BirthDate { get; set; }
    public string Color { get; set; }
    public bool IsIndoor { get; set; }
    public CatEntity(int id, string name, string breed, DateTime birthDate, string color, bool isIndoor)
    {
        Id = id;
        Name = name;
        Breed = breed;
        BirthDate = birthDate;
        Color = color;
        IsIndoor = isIndoor;
    }

    public CatFood Food { get; set; } = default!;

    public int Teeth { get; set; }
}

public class CatFood
{
    public string FoodType { get; set; } = default!;
    public int Quantity { get; set; }
}
