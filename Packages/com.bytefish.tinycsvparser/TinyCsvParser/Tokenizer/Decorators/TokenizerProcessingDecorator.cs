using System;
using System.Collections.Generic;

namespace TinyCsvParser.Tokenizer.Decorators
{
    public class TokenizerProcessingDecorator : ITokenizer
    {
        private readonly ITokenizer tokenizer;

        public class Preprocessor
        {
            public readonly Func<string, string> Processor;

            public Preprocessor()
                : this(x => x) { }

            public Preprocessor(Func<string, string> preprocessor)
            {
                Processor = preprocessor;
            }
        }

        public class Postprocessor
        {
            public readonly Func<string, string> Processor;

            public Postprocessor()
                : this(x => x) { }

            public Postprocessor(Func<string, string> preprocessor)
            {
                Processor = preprocessor;
            }
        }

        private readonly Preprocessor preprocessor;
        private readonly Postprocessor postprocessor;

        public TokenizerProcessingDecorator(ITokenizer tokenizer, Preprocessor preprocessor)
            : this(tokenizer, preprocessor, new Postprocessor())
        {
        }

        public TokenizerProcessingDecorator(ITokenizer tokenizer, Postprocessor postprocessor)
            : this(tokenizer, new Preprocessor(), postprocessor)
        {
        }

        public TokenizerProcessingDecorator(ITokenizer tokenizer, Preprocessor preprocessor, Postprocessor postprocessor)
        {
            this.tokenizer = tokenizer;

            this.preprocessor = preprocessor;
            this.postprocessor = postprocessor;
        }

        public string[] Tokenize(string input)
        {
            var preprocessed_input = preprocessor.Processor(input);

            var tokenized_input = tokenizer.Tokenize(preprocessed_input);
            var tokenized_output = new List<string>();

            foreach (var token in tokenized_input)
            {
                tokenized_output.Add(postprocessor.Processor(token));
            }

            return tokenized_output.ToArray();
        }

        public override string ToString()
        {
            return $"TokenizerProcessingDecorator (Preprocessor = {preprocessor}, Postprocessor = {postprocessor})";
        }
    }
}
