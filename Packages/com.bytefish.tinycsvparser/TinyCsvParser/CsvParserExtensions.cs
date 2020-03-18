// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;

namespace TinyCsvParser
{
    public static class CsvParserExtensions
    {
        public static IEnumerable<CsvMappingResult<TEntity>> ReadFromFile<TEntity>(this CsvParser<TEntity> csvParser, string fileName, Encoding encoding)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var lines = File.ReadLines(fileName, encoding);
            var rows = new List<Row>();

            var index = 0;

            foreach (var line in lines)
            {
                rows.Add(new Row(index, line));
                index += 1;
            }

            return csvParser.Parse(rows);
        }

        public static IEnumerable<CsvMappingResult<TEntity>> ReadFromString<TEntity>(this CsvParser<TEntity> csvParser, CsvReaderOptions csvReaderOptions, string csvData)
        {
            var lines = csvData.Split(csvReaderOptions.NewLine, StringSplitOptions.None);
            var rows = new List<Row>();

            var index = 0;

            foreach (var line in lines)
            {
                rows.Add(new Row(index, line));
                index += 1;
            }

            return csvParser.Parse(rows);
        }

        private static IEnumerable<string> ReadLinesFromStream(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks = false, int bufferSize = 1024, bool leaveOpen = false)
        {
            using (var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        public static IEnumerable<CsvMappingResult<TEntity>> ReadFromStream<TEntity>(this CsvParser<TEntity> csvParser, Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks = false, int bufferSize = 1024, bool leaveOpen = false)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var lines = ReadLinesFromStream(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
            var rows = new List<Row>();

            var index = 0;

            foreach (var line in lines)
            {
                rows.Add(new Row(index, line));
                index += 1;
            }

            return csvParser.Parse(rows);
        }
    }
}
