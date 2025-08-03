using ObjectMapper.Attributes;

namespace Tests;

[MapFrom(nameof(CatEntity))]
public class CatDtoWithMapIgnore
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Breed { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string Color { get; set; } = default!;
    public bool IsIndoor { get; set; }
    
    [MapIgnore]
    public CatFood? Food { get; set; } = default!;
    
    public CatDtoWithMapIgnore()
    {

    }
}