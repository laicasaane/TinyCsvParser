using System;

namespace TinyCsvParser.Advanced
{
    public partial class AdvancedCsvParserOptions
    {
        public readonly struct Range
        {
            public int Start { get; }

            public int End { get; }

            public Range(int start) : this(start, -1)
            {
            }

            public Range(int start, int end)
            {
                Start = Math.Max(start, 0);
                End = end;
            }

            public bool ValidateLength()
                => End < 0 || End > Start;

            public bool Contains(int index)
                => (index >= Start) && (End < 0 || index <= End);

            public bool PassEnd(int index)
                => End >= 0 && index > End;

            public override string ToString()
                => $"({Start}, {End})";

            public static implicit operator Range(int start)
                => new Range(start);

            public static implicit operator Range(in (int start, int end) range)
                => new Range(range.start, range.end);
        }
    }
}