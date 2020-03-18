// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;

namespace TinyCsvParser
{
    public class CsvParser<TEntity> : ICsvParser<TEntity>
    {
        private readonly CsvParserOptions options;
        private readonly ICsvMapping<TEntity> mapping;

        public CsvParser(CsvParserOptions options, ICsvMapping<TEntity> mapping)
        {
            this.options = options;
            this.mapping = mapping;
        }

        public IEnumerable<CsvMappingResult<TEntity>> Parse(IEnumerable<Row> csvData)
        {
            if (csvData == null)
            {
                throw new ArgumentNullException(nameof(csvData));
            }

            var rows = new List<Row>(csvData);

            if (options.SkipHeader)
                rows.RemoveAt(0);

            // If you want to get the same order as in the CSV file, this option needs to be set:
            if (options.KeepOrder)
                rows.Sort(new Row.Comparer());

            for (var i = rows.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(rows[i].Data))
                    rows.RemoveAt(i);
            }

            // Ignore Lines, that start with a comment character:
            if (!string.IsNullOrWhiteSpace(options.CommentCharacter))
            {
                for (var i = rows.Count - 1; i >= 0; i--)
                {
                    if (rows[i].Data.StartsWith(options.CommentCharacter))
                        rows.RemoveAt(i);
                }
            }

            var tokenizedRows = new List<TokenizedRow>();

            foreach (var row in rows)
            {
                tokenizedRows.Add(new TokenizedRow(row.Index, options.Tokenizer.Tokenize(row.Data)));
            }

            var csvMappingResults = new List<CsvMappingResult<TEntity>>();

            foreach (var row in tokenizedRows)
            {
                csvMappingResults.Add(mapping.Map(row));
            }

            return csvMappingResults;
        }

        public override string ToString()
        {
            return $"CsvParser (Options = {options}, Mapping = {mapping})";
        }
    }
}
