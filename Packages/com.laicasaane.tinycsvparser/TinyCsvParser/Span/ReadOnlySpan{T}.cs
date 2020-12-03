using System;
using System.Collections;
using System.Collections.Generic;

namespace TinyCsvParser
{
    public readonly struct ReadOnlySpan<T> : IEquatable<ReadOnlySpan<T>>
    {
        public int Length => this.length;

        public bool IsEmpty => 0 >= (uint)this.length;

        public ref readonly T this[int index]
        {
            get
            {
                if (index >= (uint)this.length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return ref this.array[this.start + index];
            }
        }

        private readonly T[] array;
        private readonly int length;
        private readonly int start;

        public ReadOnlySpan(T[] array)
        {
            if (array == null)
            {
                this = default;
                return;
            }

            this.array = array;
            this.start = 0;
            this.length = this.array.Length;
        }

        public ReadOnlySpan(T[] array, int start)
        {
            if (array == null)
            {
                if (start != 0)
                    throw new ArgumentOutOfRangeException();

                this = default;
                return;
            }

            this.array = array;

#if TARGET_64BIT
	        if ((ulong)(uint)start > (ulong)(uint)array.Length)
	            throw new ArgumentOutOfRangeException();
#else
            if ((uint)start > (uint)array.Length)
                throw new ArgumentOutOfRangeException();
#endif

            this.start = start;
            this.length = array.Length - start;
        }

        public ReadOnlySpan(T[] array, int start, int length)
        {
            if (array == null)
            {
                if (start != 0 || length != 0)
                    throw new ArgumentOutOfRangeException();

                this = default;
                return;
            }

            this.array = array;

#if TARGET_64BIT
	        if ((ulong)(uint)start + (ulong)(uint)length > (ulong)(uint)array.Length)
	            throw new ArgumentOutOfRangeException();
#else
            if ((uint)start > (uint)array.Length || (uint)length > (uint)(array.Length - start))
                throw new ArgumentOutOfRangeException();
#endif

            this.start = start;
            this.length = length;
        }

        public ReadOnlySpan<T> Slice(int index)
        {
            if ((uint)index > (uint)this.length)
                throw new ArgumentOutOfRangeException(nameof(index));

            return new ReadOnlySpan<T>(this.array, this.start + index, this.length - index);
        }

        public ReadOnlySpan<T> Slice(int index, int length)
        {
            if ((uint)index > (uint)this.length || (uint)length > (uint)(this.length - index))
                throw new ArgumentOutOfRangeException();

            return new ReadOnlySpan<T>(this.array, this.start + index, length);
        }

        public T[] ToArray()
        {
            if (0 >= (uint)this.length)
                return Array.Empty<T>();

            var array = new T[this.length];
            Array.Copy(this.array, this.start, array, 0, this.length);
            return array;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public bool Equals(ReadOnlySpan<T> other)
            => this.array == other.array && this.length == other.length && this.start == other.start;

        public bool Equals(in ReadOnlySpan<T> other)
            => this.array == other.array && this.length == other.length && this.start == other.start;

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

        [Obsolete("Equals(object) on ReadOnlySpan will always throw an exception. Use Equals(ReadOnlySpan), or Equals(in ReadOnlySpan), or == instead.")]
        public override bool Equals(object obj)
            => throw new NotSupportedException("Equals(object) on ReadOnlySpan is not supported. Use Equals(ReadOnlySpan), or Equals(in ReadOnlySpan), or == instead.");

        [Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
        public override int GetHashCode()
            => throw new NotSupportedException("GetHashCode() on ReadOnlySpan is not supported.");

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        private static readonly T[] _empty = Array.Empty<T>();

        public static ReadOnlySpan<T> Empty => default;

        public static implicit operator ReadOnlySpan<T>(T[] array)
            => new ReadOnlySpan<T>(array);

        public static bool operator ==(in ReadOnlySpan<T> lhs, in ReadOnlySpan<T> rhs)
            => lhs.array == rhs.array && lhs.length == rhs.length && lhs.start == rhs.start;

        public static bool operator !=(in ReadOnlySpan<T> lhs, in ReadOnlySpan<T> rhs)
            => lhs.array != rhs.array || lhs.length != rhs.length || lhs.start != rhs.start;

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadOnlySpan<T> span)
            {
                this.source = span.array ?? _empty;

                if (0 >= (uint)span.length)
                {
                    this.start = 0;
                    this.end = 0;
                    this.current = 0;
                }
                else
                {
                    this.start = span.start;
                    this.end = span.start + span.length;
                    this.current = this.start - 1;
                }
            }

            public bool MoveNext()
            {
                if (this.current < this.end)
                {
                    this.current++;
                    return (this.current < this.end);
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < this.start)
                        throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");

                    if (this.current >= this.end)
                        throw new InvalidOperationException("Enumeration already finished.");

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = this.start - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}