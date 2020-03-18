// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TinyCsvParser.Tokenizer.RegularExpressions
{
    public abstract class RegularExpressionTokenizer : ITokenizer
    {
        public abstract Regex Regexp { get; }

        public string[] Tokenize(string input)
        {
            var matches = Regexp.Matches(input);
            var tokenized_output = new List<string>(matches.Count);

            foreach (Match match in matches)
            {
                tokenized_output.Add(match.Value);
            }

            return tokenized_output.ToArray();
        }

        public override string ToString()
        {
            return $"Regexp = {Regexp}";
        }
    }
}