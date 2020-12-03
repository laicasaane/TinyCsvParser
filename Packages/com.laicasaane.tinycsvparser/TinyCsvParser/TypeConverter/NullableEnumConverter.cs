// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace TinyCsvParser.TypeConverter
{
    public class NullableEnumConverter<TTargetType> : NullableInnerConverter<TTargetType>
        where TTargetType : struct, Enum
    {
        public NullableEnumConverter()
            : base(new EnumConverter<TTargetType>())
        {
        }

        public NullableEnumConverter(bool ignoreCase)
            : base(new EnumConverter<TTargetType>(ignoreCase))
        {
        }
    }
}