// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;

namespace TinyCsvParser.Tokenizer.RFC4180
{
    public class RFC4180Tokenizer : ITokenizer
    {
        private Reader reader;

        public RFC4180Tokenizer(Options options)
        {
            this.reader = new Reader(options);
        }

        public string[] Tokenize(string input)
        {
            IList<Reader.Token> tokenized_input;

            using (var stringReader = new StringReader(input))
            {
                tokenized_input = reader.ReadTokens(stringReader);
            }

            var tokenized_output = new List<string>(tokenized_input.Count);

            foreach (var token in tokenized_input)
            {
                tokenized_output.Add(token.Content);
            }

            return tokenized_output.ToArray();
        }

        public override string ToString()
        {
            return $"RFC4180Tokenizer (Reader = {reader})";
        }
    }
}
