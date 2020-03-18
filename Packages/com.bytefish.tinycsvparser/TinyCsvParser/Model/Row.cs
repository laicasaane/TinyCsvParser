// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace TinyCsvParser.Model
{
    public class Row
    {
        public readonly int Index;

        public readonly string Data;

        public Row(int index, string data)
        {
            Index = index;
            Data = data;
        }

        public readonly struct Comparer : IComparer<Row>
        {
            public int Compare(Row x, Row y)
            {
                if (x == null)
                {
                    if (y == null)
                        return 0;

                    return -1;
                }

                return x.Index.CompareTo(y.Index);
            }
        }
    }
}
