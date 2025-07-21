using ObjectMapper.Attributes;

namespace Tests;

public class CatDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Breed { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string Color { get; set; } = default!;
    public bool IsIndoor { get; set; }
    public CatFood Food { get; set; } = default!;
    public CatDto()
    {

    }

    [MapFrom(nameof(CatEntity.Teeth))]
    public int NoofTeeth { get; set; }
}
