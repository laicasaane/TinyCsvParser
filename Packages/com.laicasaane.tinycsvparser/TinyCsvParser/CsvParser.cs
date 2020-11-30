﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
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

        public ParallelQuery<CsvMappingResult<TEntity>> Parse(IEnumerable<Row> csvData)
        {
            if (csvData == null)
            {
                throw new ArgumentNullException(nameof(csvData));
            }

            var query = csvData
                .Skip(options.SkipHeader ? 1 : 0)
                .AsParallel();

            // If you want to get the same order as in the CSV file, this option needs to be set:
            if (options.KeepOrder)
            {
                query = query.AsOrdered();
            }

            query = query
                .WithDegreeOfParallelism(options.DegreeOfParallelism)
                .Where(row => !string.IsNullOrWhiteSpace(row.Data));

            // Ignore lines that start with a comment character:
            if (!string.IsNullOrWhiteSpace(options.CommentCharacter))
            {
                query = query.Where(line => !line.Data.StartsWith(options.CommentCharacter));
            }

            return query
                .Select(line => new TokenizedRow(line.Index, options.Tokenizer.Tokenize(line.Data)))
                .Where(Validate)
                .Select(fields => mapping.Map(fields));
        }

        private bool Validate(TokenizedRow row)
        {
            if (options.SkipEmptyRow)
            {
                foreach (var token in row.Tokens)
                {
                    if (!string.IsNullOrWhiteSpace(token))
                        return true;
                }

                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"CsvParser (Options = {options}, Mapping = {mapping})";
        }
    }
}