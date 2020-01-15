﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;
using TinyCsvParser.TypeConverter;

namespace TinyCsvParser.Test.Mapping
{
    public class CsvPermissiveMapping<TEntity> : ICsvMapping<TEntity>
        where TEntity : class, new()
    {
        private class IndexToPropertyMapping
        {
            public int ColumnIndex { get; set; }

            public ICsvPropertyMapping<TEntity, string> PropertyMapping { get; set; }

            public override string ToString()
            {
                return $"IndexToPropertyMapping (ColumnIndex = {ColumnIndex}, PropertyMapping = {PropertyMapping}";
            }
        }

        private readonly ITypeConverterProvider typeConverterProvider;
        private readonly List<IndexToPropertyMapping> csvIndexPropertyMappings;

        protected CsvPermissiveMapping()
            : this(new TypeConverterProvider())
        {
        }

        protected CsvPermissiveMapping(ITypeConverterProvider typeConverterProvider)
        {
            this.typeConverterProvider = typeConverterProvider;
            this.csvIndexPropertyMappings = new List<IndexToPropertyMapping>();
        }

        protected CsvPropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Action<TEntity, TProperty> propertySetter, string propertyName = null)
        {
            return MapProperty(columnIndex, propertySetter, typeConverterProvider.Resolve<TProperty>(), propertyName);
        }

        protected CsvPropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Action<TEntity, TProperty> propertySetter, ITypeConverter<TProperty> typeConverter, string propertyName = null)
        {
            if (csvIndexPropertyMappings.Any(x => x.ColumnIndex == columnIndex))
            {
                throw new InvalidOperationException($"Duplicate mapping for column index {columnIndex}");
            }

            var propertyMapping = new CsvPropertyMapping<TEntity, TProperty>(null, propertySetter, typeConverter, propertyName);

            AddPropertyMapping(columnIndex, propertyMapping);

            return propertyMapping;
        }

        protected CsvPropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Func<TEntity, TProperty> propertyGetter, Action<TEntity, TProperty> propertySetter, string propertyName = null)
        {
            return MapProperty(columnIndex, propertyGetter, propertySetter, typeConverterProvider.Resolve<TProperty>(), propertyName);
        }

        protected CsvPropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Func<TEntity, TProperty> propertyGetter, Action<TEntity, TProperty> propertySetter, ITypeConverter<TProperty> typeConverter, string propertyName = null)
        {
            if (csvIndexPropertyMappings.Any(x => x.ColumnIndex == columnIndex))
            {
                throw new InvalidOperationException($"Duplicate mapping for column index {columnIndex}");
            }

            var propertyMapping = new CsvPropertyMapping<TEntity, TProperty>(propertyGetter, propertySetter, typeConverter, propertyName);

            AddPropertyMapping(columnIndex, propertyMapping);

            return propertyMapping;
        }

        private void AddPropertyMapping<TProperty>(int columnIndex, CsvPropertyMapping<TEntity, TProperty> propertyMapping)
        {
            var indexToPropertyMapping = new IndexToPropertyMapping
            {
                ColumnIndex = columnIndex,
                PropertyMapping = propertyMapping
            };

            csvIndexPropertyMappings.Add(indexToPropertyMapping);
        }

        public CsvMappingResult<TEntity> Map(TokenizedRow values)
        {
            TEntity entity = new TEntity();

            // Iterate over Index Mappings:
            for (int pos = 0; pos < csvIndexPropertyMappings.Count; pos++)
            {
                var indexToPropertyMapping = csvIndexPropertyMappings[pos];

                var columnIndex = indexToPropertyMapping.ColumnIndex;

                if (columnIndex >= values.Tokens.Length)
                {
                    continue;
                }

                var value = values.Tokens[columnIndex];

                if (!indexToPropertyMapping.PropertyMapping.TryMapValue(entity, value))
                {
                    return new CsvMappingResult<TEntity>
                    {
                        RowIndex = values.Index,
                        Error = new CsvMappingError
                        {
                            ColumnIndex = columnIndex,
                            Value = $"Column {columnIndex} with Value '{value}' cannot be converted",
                            UnmappedRow = string.Join("|", values.Tokens)
                        }
                    };
                }
            }

            return new CsvMappingResult<TEntity>
            {
                RowIndex = values.Index,
                Result = entity
            };
        }

        public override string ToString()
        {
            var csvPropertyMappingsString = string.Join(", ", csvIndexPropertyMappings.Select(x => x.ToString()));

            return $"CsvMissingValuesMapping (TypeConverterProvider = {typeConverterProvider}, Mappings = {csvPropertyMappingsString})";
        }
    }

    [TestFixture]
    public class CsvPermissiveMappingTest
    {
        private class SampleEntity
        {
            public string Value1 { get; set; }

            public string Value2 { get; set; }

            public string Value3 { get; set; }
        }

        private class PermissiveSampleEntityMapper : CsvPermissiveMapping<SampleEntity>
        {
            public PermissiveSampleEntityMapper()
            {
                MapProperty(0, x => x.Value1, (x, v) => x.Value1 = v);
                MapProperty(1, x => x.Value2, (x, v) => x.Value2 = v);
                MapProperty(2, x => x.Value3, (x, v) => x.Value3 = v);
            }
        }

        [Test]
        public void ExecuteWithMissingValuesTest()
        {
            CsvParserOptions options = new CsvParserOptions(true, ';');
            CsvParser<SampleEntity> customCsvParser = new CsvParser<SampleEntity>(options, new PermissiveSampleEntityMapper());

            var stringBuilder = new StringBuilder()
                .AppendLine("FirstName;LastName;BirthDate")
                .AppendLine("1;2;3")
                .AppendLine("4");

            var csvReaderOptions = new CsvReaderOptions(new [] { Environment.NewLine });

            var result = customCsvParser
                .ReadFromString(csvReaderOptions, stringBuilder.ToString())
                .ToList();

            Assert.AreEqual(2, result.Count);

            Assert.IsTrue(result.All(x => x.IsValid));

            Assert.AreEqual("1",result[0].Result.Value1);
            Assert.AreEqual("2", result[0].Result.Value2);
            Assert.AreEqual("3", result[0].Result.Value3);

            Assert.AreEqual("4", result[1].Result.Value1);
            Assert.AreEqual(null, result[1].Result.Value2);
            Assert.AreEqual(null, result[1].Result.Value3);
        }
    }
}
