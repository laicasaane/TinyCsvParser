using System;
using TinyCsvParser.TypeConverter;

namespace TinyCsvParser.Mapping
{
    public class CsvCollectionPropertyMapping<TEntity, TProperty> : ICsvPropertyMapping<TEntity, ReadOnlySpan<string>>
        where TEntity : class, new()
    {
        private readonly string propertyName;
        private readonly IArrayTypeConverter<TProperty> propertyConverter;
        private readonly Action<TEntity, TProperty> propertySetter;
        private readonly Func<TEntity, TProperty> propertyGetter;

        public CsvCollectionPropertyMapping(Func<TEntity, TProperty> propertyGetter, Action<TEntity, TProperty> propertySetter, IArrayTypeConverter<TProperty> typeConverter, string propertyName = null)
        {
            this.propertyGetter = propertyGetter;
            this.propertySetter = propertySetter;
            this.propertyConverter = typeConverter;
            this.propertyName = string.IsNullOrEmpty(propertyName) ? string.Empty : propertyName;
        }

        public bool TryMapValue(TEntity entity, ReadOnlySpan<string> value)
        {
            if (!propertyConverter.TryConvert(value, out var convertedValue))
            {
                return false;
            }

            propertySetter(entity, convertedValue);

            return true;
        }

        public override string ToString()
        {
            return $"CsvPropertyMapping (PropertyName = {propertyName}, Converter = {propertyConverter})";
        }
    }
}
