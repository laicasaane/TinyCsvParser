using System;
using System.Collections.Generic;
using System.Linq;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;

namespace TinyCsvParser.Advanced
{
    public class AdvancedCsvParser<TEntity> : ICsvParser<TEntity>
    {
        private readonly AdvancedCsvParserOptions options;
        private readonly ICsvMapping<TEntity> mapping;

        public AdvancedCsvParser(AdvancedCsvParserOptions options, ICsvMapping<TEntity> mapping)
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
                .Where(CanSelectRow)
                .AsParallel();

            if (options.KeepOrder)
                query = query.AsOrdered();

            query = query
                .WithDegreeOfParallelism(options.DegreeOfParallelism)
                .Where(row => !string.IsNullOrWhiteSpace(row.Data));

            if (!string.IsNullOrWhiteSpace(options.RowComment))
            {
                query = query.Where(line => !line.Data.StartsWith(options.RowComment));
            }

            var newQuery = query
                .Select(line => new TokenizedRow(line.Index, options.Tokenizer.Tokenize(line.Data)))
                .Where(CanSelectRow);

            if (options.RowAsColumn)
                newQuery = RowAsColumn(newQuery);

            return newQuery.Select(fields => mapping.Map(fields));
        }

        private ParallelQuery<TokenizedRow> RowAsColumn(ParallelQuery<TokenizedRow> query)
        {
            var columns = query.ToArray();

            if (columns.Length <= 0)
                return query;

            var rows = new List<TokenizedRow>();
            var rowTokens = new List<string>();
            var columnLength = columns[0].Tokens.Length;
            var checkComment = !string.IsNullOrWhiteSpace(options.ColumnComment);

            for (var i = 0; i < columnLength; i++)
            {
                rowTokens.Clear();

                for (var k = 0; k < columns.Length; k++)
                {
                    var tokens = columns[k].Tokens;

                    if (i >= tokens.Length)
                    {
                        rowTokens.Add(string.Empty);
                        continue;
                    }

                    if (k == 0 && checkComment && tokens[i].StartsWith(options.ColumnComment))
                        break;

                    rowTokens.Add(tokens[i]);
                }

                if (rowTokens.Count > 0)
                    rows.Add(new TokenizedRow(i, rowTokens.ToArray()));
            }

            var newQuery = rows
                .Where(CanSelectColumn)
                .AsParallel();

            if (options.KeepOrder)
                newQuery = newQuery.AsOrdered();

            newQuery = newQuery
                .WithDegreeOfParallelism(options.DegreeOfParallelism);

            return newQuery;
        }

        private bool CanSelectRow(Row row)
            => options.RowRange.Contains(row.Index);

        private bool CanSelectRow(TokenizedRow row)
        {
            if (options.SkipEmptyRow)
            {
                for (var i = 0; i < row.Tokens.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(row.Tokens[i]))
                        return true;
                }

                return false;
            }

            return true;
        }

        private bool CanSelectColumn(TokenizedRow row)
        {
            if (!options.ColumnRange.Contains(row.Index))
                return false;

            if (options.SkipEmptyColumn)
            {
                for (var i = 0; i < row.Tokens.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(row.Tokens[i]))
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