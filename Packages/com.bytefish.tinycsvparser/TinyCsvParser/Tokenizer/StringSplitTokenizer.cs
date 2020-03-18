// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace TinyCsvParser.Tokenizer
{
    public class StringSplitTokenizer : ITokenizer
    {
        public readonly char[] FieldsSeparator;
        public readonly bool TrimValues;

        public StringSplitTokenizer(char[] fieldsSeparator, bool trimValues)
        {
            FieldsSeparator = fieldsSeparator;
            TrimValues = trimValues;
        }

        public string[] Tokenize(string input)
        {
            var tokenized_input = input.Split(FieldsSeparator);

            if (TrimValues)
            {
                var tokenized_output = new List<string>();

                foreach (var token in tokenized_input)
                {
                    tokenized_output.Add(token.Trim());
                }

                return tokenized_output.ToArray();
            }

            return tokenized_input;
        }

        public override string ToString()
        {
            return $"StringSplitTokenizer (FieldsSeparator = {FieldsSeparator}, TrimValues = {TrimValues})";
        }
    }
}