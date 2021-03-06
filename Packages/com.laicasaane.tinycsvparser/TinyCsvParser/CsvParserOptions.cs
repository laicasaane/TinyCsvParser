﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using TinyCsvParser.Tokenizer;

namespace TinyCsvParser
{
    public class CsvParserOptions
    {
        public readonly ITokenizer Tokenizer;

        public readonly bool SkipHeader;

        public readonly bool SkipEmptyRow;

        public readonly string CommentCharacter;

        /// <summary>
        /// The degree of parallelism to use in a query. The value must be from 1 to <see cref="Environment"/>.<see cref="Environment.ProcessorCount"/> (inclusive).
        /// </summary>
        public readonly int DegreeOfParallelism;

        public readonly bool KeepOrder;

        public CsvParserOptions(bool skipHeader, char fieldsSeparator)
            : this(skipHeader, new QuotedStringTokenizer(fieldsSeparator))
        {
        }

        public CsvParserOptions(bool skipHeader, bool skipEmptyRow, char fieldsSeparator)
            : this(skipHeader, skipEmptyRow, new QuotedStringTokenizer(fieldsSeparator))
        {
        }

        public CsvParserOptions(bool skipHeader, char fieldsSeparator, int degreeOfParallelism, bool keepOrder)
            : this(skipHeader, string.Empty, new QuotedStringTokenizer(fieldsSeparator), degreeOfParallelism, keepOrder)
        {
        }

        public CsvParserOptions(bool skipHeader, bool skipEmptyRow, char fieldsSeparator, int degreeOfParallelism, bool keepOrder)
            : this(skipHeader, skipEmptyRow, string.Empty, new QuotedStringTokenizer(fieldsSeparator), degreeOfParallelism, keepOrder)
        {
        }

        public CsvParserOptions(bool skipHeader, ITokenizer tokenizer)
            : this(skipHeader, string.Empty, tokenizer)
        {
        }

        public CsvParserOptions(bool skipHeader, bool skipEmptyRow, ITokenizer tokenizer)
            : this(skipHeader, skipEmptyRow, string.Empty, tokenizer)
        {
        }

        public CsvParserOptions(bool skipHeader, string commentCharacter, ITokenizer tokenizer)
            : this(skipHeader, commentCharacter, tokenizer, Environment.ProcessorCount, true)
        {
        }

        public CsvParserOptions(bool skipHeader, bool skipEmptyRow, string commentCharacter, ITokenizer tokenizer)
            : this(skipHeader, skipEmptyRow, commentCharacter, tokenizer, Environment.ProcessorCount, true)
        {
        }

        public CsvParserOptions(bool skipHeader, string commentCharacter, ITokenizer tokenizer, int degreeOfParallelism, bool keepOrder)
            : this(skipHeader, false, commentCharacter, tokenizer, degreeOfParallelism, keepOrder)
        {
        }

        public CsvParserOptions(bool skipHeader, bool skipEmptyRow, string commentCharacter, ITokenizer tokenizer, int degreeOfParallelism, bool keepOrder)
        {
            SkipHeader = skipHeader;
            SkipEmptyRow = skipEmptyRow;
            CommentCharacter = commentCharacter;
            Tokenizer = tokenizer;
            DegreeOfParallelism = Math.Min(Math.Max(1, degreeOfParallelism), Environment.ProcessorCount);
            KeepOrder = keepOrder;
        }

        public override string ToString()
        {
            return $"CsvParserOptions (Tokenizer = {Tokenizer}, SkipHeader = {SkipHeader}, DegreeOfParallelism = {DegreeOfParallelism}, KeepOrder = {KeepOrder}, CommentCharacter = {CommentCharacter}, SkipEmptyRow = {SkipEmptyRow})";
        }
    }
}
