using System;
using TinyCsvParser.Tokenizer;

namespace TinyCsvParser.Advanced
{
    public partial class AdvancedCsvParserOptions
    {
        public readonly ITokenizer Tokenizer;

        public readonly string RowComment;

        public readonly string ColumnComment;

        public readonly Range RowRange;

        public readonly Range ColumnRange;

        public readonly bool SkipEmptyRow;

        public readonly bool SkipEmptyColumn;

        /// <summary>
        /// Indicates whether rows should be parsed as columns and vice versa.
        /// </summary>
        public readonly bool RowAsColumn;

        public readonly bool KeepOrder;

        /// <summary>
        /// The degree of parallelism to use in a query. The value must be from 1 to <see cref="Environment"/>.<see cref="Environment.ProcessorCount"/> (inclusive).
        /// </summary>
        public readonly int DegreeOfParallelism;

        public AdvancedCsvParserOptions(char fieldsSeparator, string comment = null, Range? rowRange = null, Range? columnRange = null, bool skipEmptyRow = false, bool skipEmptyColumn = false, bool rowAsColumn = false, bool keepOrder = true, int? degreeOfParallelism = null)
            : this(fieldsSeparator, comment, comment, rowRange, columnRange, skipEmptyRow, skipEmptyColumn, rowAsColumn, keepOrder, degreeOfParallelism)
        {
        }

        public AdvancedCsvParserOptions(char fieldsSeparator, string rowComment = null, string columnComment = null, Range? rowRange = null, Range? columnRange = null, bool skipEmptyRow = false, bool skipEmptyColumn = false, bool rowAsColumn = false, bool keepOrder = true, int? degreeOfParallelism = null)
            : this(new QuotedStringTokenizer(fieldsSeparator), rowComment, columnComment, rowRange, columnRange, skipEmptyRow, skipEmptyColumn, rowAsColumn, keepOrder, degreeOfParallelism)
        {
        }

        public AdvancedCsvParserOptions(ITokenizer tokenizer, string comment = null, Range? rowRange = null, Range? columnRange = null, bool skipEmptyRow = false, bool skipEmptyColumn = false, bool rowAsColumn = false, bool keepOrder = true, int? degreeOfParallelism = null)
            : this(tokenizer, comment, comment, rowRange, columnRange, skipEmptyRow, skipEmptyColumn, rowAsColumn, keepOrder, degreeOfParallelism)
        {
        }

        public AdvancedCsvParserOptions(ITokenizer tokenizer, string rowComment = null, string columnComment = null, Range? rowRange = null, Range? columnRange = null, bool skipEmptyRow = false, bool skipEmptyColumn = false, bool rowAsColumn = false, bool keepOrder = true, int? degreeOfParallelism = null)
        {
            Tokenizer = tokenizer;
            RowComment = rowComment;
            ColumnComment = columnComment;
            RowRange = rowRange ?? new Range(0);
            ColumnRange = columnRange ?? new Range(0);
            SkipEmptyRow = skipEmptyRow;
            SkipEmptyColumn = skipEmptyColumn;
            RowAsColumn = rowAsColumn;
            KeepOrder = keepOrder;
            DegreeOfParallelism = Math.Min(Math.Max(1, degreeOfParallelism ?? Environment.ProcessorCount), Environment.ProcessorCount);
        }
    }
}