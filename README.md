# StarFuryDev.ObjectMapper

[![NuGet](https://img.shields.io/nuget/v/StarFuryDev.ObjectMapper.svg)](https://www.nuget.org/packages/StarFuryDev.ObjectMapper/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A simple and lightweighta attribute based object mapping library for .NET, built as a hobby project to make object-to-object mapping easier.

## Why This Library?

I created this library as a personal learning project and wanted to share it with the community. While there are other great mapping libraries out there, I built this to:

- Learn more about reflection and attributes in C#
- Explore different approaches to object mapping
- Share something useful with the .NET community

Feel free to use it or just peek at the code!

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package StarFuryDev.ObjectMapper
```

Or via Package Manager Console:

```powershell
Install-Package StarFuryDev.ObjectMapper
```

## Quick Start

```csharp
using StarFuryDev.ObjectMapper;

// Source data
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

// Destination
[MapFrom(nameof(Person))]
public class Profile
{
    // default mapping - source property name = destination property name
    public string FirstName { get; set; }

    // Map from using a custom converter
    [MapFromUsing(typeof(CustomConverters), nameof(CustomConverters.ConvertToFullName))]
    public string FullName { get; set; }
    
    // Map from using a different property 
    [MapFrom(nameof(Source.Age))]
    public int MyAge { get; set; }

    // Ignore when mapping
    [MapIgnore]
    prop string Address { get; set; }
}

public static class CustomConverters
{
    public static string ConvertToFullName(string firstName, string lastName)
    {
        return $"{firstName} {lastname}";
    }
}

// Simple mapping
var person = new Person { FirstName = "John", LastName = "Smith" Age = 30 };
var profile = Mapper.Map<Person, Profile>(person);
```

## Features

- ✨ Simple, Attribute based mapping
- 🔧 Property name matching (case-sensitive)
- 📦 Lightweight with no dependencies
- 🎯 Convention-based (default name to name) mapping
- 🔄 List<> and Dictionary<,> mapping support
- ⚙️ Custom mapping converter support
- 🎯 Null source properties ignored as default

## Usage Examples

### Basic Mapping

```csharp
var mapper = new ObjectMapper();

var person = new Person { FirstName = "Jane", LastName = "Doe" };
var personDto = mapper.Map<Person, PersonDto>(person);
```

### Custom Property Mapping

```csharp
mapper.CreateMap<Source, Destination>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == "Active"));
```

### Mapping Collections

```csharp
var sourceList = new List<Source> 
{ 
    new Source { Name = "Alice" },
    new Source { Name = "Bob" }
};

var destinationList = mapper.Map<List<Source>, List<Destination>>(sourceList);
```

### Ignore Properties

```csharp
mapper.CreateMap<Source, Destination>()
    .ForMember(dest => dest.InternalId, opt => opt.Ignore());
```

## Configuration Options

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.CaseSensitive = false; // Match properties case-insensitively
    cfg.AllowNullValues = true; // Allow mapping null values
    cfg.CreateMap<Source, Destination>();
});

var mapper = new ObjectMapper(config);
```

## Limitations

Since this is a hobby project, there are some limitations:

- Not as feature-rich as AutoMapper or Mapster

## Contributing

This is a hobby project, but contributions are welcome! If you find a bug or have an idea:

1. Open an issue to discuss it
3. Create a feature branch
4. Submit a pull request

No contribution is too small - typo fixes, documentation improvements, and feature suggestions are all appreciated!

## Building from Source

```bash
# Clone the repository
git clone https://github.com/starfury-dev/ObjectMapper-.git

# Restore dependencies
dotnet restore

# Build
dotnet build

# Run tests
dotnet test
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Inspired by other great mapping libraries in the .NET ecosystem
- Thanks to the .NET community for feedback and support
- Built with ❤️ as a learning project

## Contact

- GitHub: [@yourusername](https://github.com/starfury-dev)
- Issues: [GitHub Issues](https://github.com/starfury-dev/ObjectMapper/issues)

---

⭐ If you find this project useful, consider giving it a star on GitHub!