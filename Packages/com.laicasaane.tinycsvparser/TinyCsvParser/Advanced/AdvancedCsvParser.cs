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

            if (options.KeepOrder || options.RowAsColumn)
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
            var rows = query.ToArray();

            if (rows.Length <= 0)
                return query;

            var columns = new List<TokenizedRow>();
            var colTokens = new List<string>();
            var columnLength = rows[0].Tokens.Length;
            var checkComment = !string.IsNullOrWhiteSpace(options.ColumnComment);

            for (var i = 0; i < columnLength; i++)
            {
                colTokens.Clear();

                for (var k = 0; k < rows.Length; k++)
                {
                    var index = rows[k].Index;
                    var tokens = rows[k].Tokens;

                    if (i >= tokens.Length)
                    {
                        colTokens.Add(string.Empty);
                        continue;
                    }

                    if (index == 0 && checkComment && tokens[i].StartsWith(options.ColumnComment))
                    {
                        colTokens.Clear();
                        break;
                    }

                    if (!options.RowRange.Contains(k))
                        continue;

                    colTokens.Add(tokens[i]);
                }

                if (colTokens.Count > 0)
                    columns.Add(new TokenizedRow(i, colTokens.ToArray()));
            }

            var newQuery = columns
                .Where(CanSelectColumn)
                .AsParallel();

            if (options.KeepOrder)
                newQuery = newQuery.AsOrdered();

            newQuery = newQuery
                .WithDegreeOfParallelism(options.DegreeOfParallelism);

            return newQuery;
        }

        private bool CanSelectRow(Row row)
            => options.RowAsColumn || options.RowRange.Contains(row.Index);

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