# Unity AOT Compatibility #

Because [IL2CPP does not have the capability to share generics with value type arguments yet](https://forum.unity.com/threads/are-c-expression-trees-or-ilgenerator-allowed-on-ios.489498/#post-3665089), System.Linq.Expressions has been removed from `MapProperty` method and replaced with `System.Action` as a property setter.

Therefore, the mapping code (*see [Basic Usage](https://github.com/laicasaane/TinyCsvParser/#basic-usage) section*) should be re-written:

```csharp
private class CsvPersonMapping : CsvMapping<Person>
{
    public CsvPersonMapping()
        : base()
    {
        MapProperty<string>(0, (x, v) => x.FirstName = v);
        MapProperty<string>(1, (x, v) => x.LastName = v);
        MapProperty<DateTime>(2, (x, v) => x.BirthDate = v);
    }
}
```

Or with another `System.Func` parameter as a property setter:

```csharp
private class CsvPersonMapping : CsvMapping<Person>
{
    public CsvPersonMapping()
        : base()
    {
        MapProperty(0, x => x.FirstName, (x, v) => x.FirstName = v);
        MapProperty(1, x => x.LastName, (x, v) => x.LastName = v);
        MapProperty(2, x => x.BirthDate, (x, v) => x.BirthDate = v);
    }
}
```

# TinyCsvParser #

[TinyCsvParser]: https://github.com/bytefish/TinyCsvParser
[MIT License]: https://opensource.org/licenses/MIT

[TinyCsvParser] is a .NET library to parse CSV data in an easy and *fun way*, while offering very high performance and a very clean API. It is highly configurable to provide maximum flexibility.

To get started quickly, follow the [Quickstart](http://bytefish.github.io/TinyCsvParser/sections/quickstart.html).

[TinyCsvParser] supports .NET Core.

## Installing TinyCsvParser ##

You can use [NuGet](https://www.nuget.org) to install [TinyCsvParser]. Run the following command
in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console).

```
PM> Install-Package TinyCsvParser
```

## Documentation and Changelog ##

[TinyCsvParser] comes with an official documentation and changelog:

* [http://bytefish.github.io/TinyCsvParser/](http://bytefish.github.io/TinyCsvParser/)

## Basic Usage ##

This is only an example for the most common use of TinyCsvParser. For more detailed information on custom formats and more advanced use-cases,
please consult the full [User Guide](http://bytefish.github.io/TinyCsvParser/sections/userguide.html) of the official documentation.

Imagine we have list of Persons in a CSV file ``persons.csv`` with their first name, last name and birthdate.

```
FirstName;LastName;BirthDate
Philipp;Wagner;1986/05/12
Max;Musterman;2014/01/02
```

The corresponding domain model in our system might look like this.

```csharp
private class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
}
```

When using [TinyCsvParser] you have to define the mapping between the columns in the CSV data and the property in you domain model.

```csharp
private class CsvPersonMapping : CsvMapping<Person>
{
    public CsvPersonMapping()
        : base()
    {
        MapProperty(0, x => x.FirstName);
        MapProperty(1, x => x.LastName);
        MapProperty(2, x => x.BirthDate);
    }
}
```

And then we can use the mapping to parse the CSV data with a ``CsvParser``.

```csharp
namespace TinyCsvParser.Test
{
    [TestFixture]
    public class TinyCsvParserTest
    {
        [Test]
        public void TinyCsvTest()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            CsvPersonMapping csvMapper = new CsvPersonMapping();
            CsvParser<Person> csvParser = new CsvParser<Person>(csvParserOptions, csvMapper);

            var result = csvParser
                .ReadFromFile(@"persons.csv", Encoding.ASCII)
                .ToList();

            Assert.AreEqual(2, result.Count);

            Assert.IsTrue(result.All(x => x.IsValid));

            Assert.AreEqual("Philipp", result[0].Result.FirstName);
            Assert.AreEqual("Wagner", result[0].Result.LastName);

            Assert.AreEqual(1986, result[0].Result.BirthDate.Year);
            Assert.AreEqual(5, result[0].Result.BirthDate.Month);
            Assert.AreEqual(12, result[0].Result.BirthDate.Day);

            Assert.AreEqual("Max", result[1].Result.FirstName);
            Assert.AreEqual("Mustermann", result[1].Result.LastName);

            Assert.AreEqual(2014, result[1].Result.BirthDate.Year);
            Assert.AreEqual(1, result[1].Result.BirthDate.Month);
            Assert.AreEqual(1, result[1].Result.BirthDate.Day);
        }
    }
}
```

## License ##

The library is released under terms of the [MIT License]:

* [https://github.com/bytefish/TinyCsvParser](https://github.com/bytefish/TinyCsvParser)