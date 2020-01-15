using TinyCsvParser.Benchmark.Model;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace TinyCsvParser.Benchmark.Mapper
{
    public class LocalWeatherDataMapper : CsvMapping<LocalWeatherData>
    {
        public LocalWeatherDataMapper()
        {
            MapProperty(0, x => x.WBAN, (x, v) => x.WBAN = v);
            MapProperty(1, x => x.Date, (x, v) => x.Date = v, new DateTimeConverter("yyyyMMdd"));
            MapProperty(2, x => x.Time, (x, v) => x.Time = v, new TimeSpanConverter("hhmm"));
            MapProperty(4, x => x.SkyCondition, (x, v) => x.SkyCondition = v);
            MapProperty(12, x => x.DryBulbCelsius, (x, v) => x.DryBulbCelsius = v);
            MapProperty(24, x => x.WindSpeed, (x, v) => x.WindSpeed = v);
            MapProperty(30, x => x.StationPressure, (x, v) => x.StationPressure = v);
        }
    }
}
