﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;

namespace TinyCsvParser
{
    public interface ICsvParser<TEntity>
    {
        IEnumerable<CsvMappingResult<TEntity>> Parse(IEnumerable<Row> csvData);
    }
}
