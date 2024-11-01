﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
#nullable enable

#if FEATURE_SPANFORMATTABLE

namespace ICU4N.Text
{
    internal ref partial struct ValueStringBuilder
    {
        internal void AppendSpanFormattable<T>(T value, string? format = null, IFormatProvider? provider = null) where T : ISpanFormattable
        {
            if (value.TryFormat(_chars.Slice(_pos), out int charsWritten, format, provider))
            {
                _pos += charsWritten;
                UpdateMaxLength();
            }
            else
            {
                Append(value.ToString(format, provider));
            }
        }

        internal void InsertSpanFormattable<T>(int index, T value, string? format = null, IFormatProvider? provider = null) where T : ISpanFormattable
        {
            Span<char> buffer = stackalloc char[CharStackBufferSize];
            if (value.TryFormat(buffer, out int charsWritten, format, provider))
            {
                Insert(index, buffer.Slice(0, charsWritten));
            }
            else
            {
                Insert(index, value.ToString(format, provider));
            }
        }
    }
}

#endif
