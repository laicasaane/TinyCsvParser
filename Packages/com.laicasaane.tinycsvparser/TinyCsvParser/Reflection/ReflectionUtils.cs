// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace TinyCsvParser.Reflection
{
    public static class ReflectionUtils
    {
        public static bool IsEnum(Type type)
        {
#if NETSTANDARD1_3
            return typeof(Enum).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());

#else
            return typeof(Enum).IsAssignableFrom(type);
#endif
        }
    }
}
