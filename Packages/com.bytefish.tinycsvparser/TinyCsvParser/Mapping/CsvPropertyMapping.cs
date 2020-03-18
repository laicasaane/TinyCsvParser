// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using TinyCsvParser.TypeConverter;

namespace TinyCsvParser.Mapping
{
    public class CsvPropertyMapping<TEntity, TProperty> : ICsvPropertyMapping<TEntity, string>
        where TEntity : class, new()
    {
        private readonly string propertyName;
        private readonly ITypeConverter<TProperty> propertyConverter;
        private readonly Action<TEntity, TProperty> propertySetter;
        private readonly Func<TEntity, TProperty> propertyGetter;

        public CsvPropertyMapping(Func<TEntity, TProperty> propertyGetter, Action<TEntity, TProperty> propertySetter, ITypeConverter<TProperty> typeConverter, string propertyName = null)
        {
            this.propertyGetter = propertyGetter;
            this.propertySetter = propertySetter;
            this.propertyConverter = typeConverter;
            this.propertyName = string.IsNullOrEmpty(propertyName) ? string.Empty : propertyName;
        }

        public bool TryMapValue(TEntity entity, string value)
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