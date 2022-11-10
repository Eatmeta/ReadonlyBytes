using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private readonly byte[] _array;
        internal readonly int Length;
        private readonly int _hashCode;

        public ReadonlyBytes(params byte[] item)
        {
            _array = item ?? throw new ArgumentNullException();
            _hashCode = CalculateHashCode();
            Length = item.Length;
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return _array[index];
            }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
                yield return _array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            if (_array.Length == 0)
                return "[]";
            var builder = new StringBuilder();
            builder.Append("[");
            for (var i = 0; i < _array.Length; i++)
            {
                builder.Append(_array[i]);
                builder.Append(i == _array.Length - 1 ? "]" : ", ");
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != this.GetType()) return false;
            return obj.GetHashCode() == _hashCode;
        }

        public sealed override int GetHashCode()
        {
            return _hashCode;
        }

        private int CalculateHashCode()
        {
            unchecked
            {
                var hash = 1677711;
                foreach (var item in _array)
                    hash = hash * 2166131 + item.GetHashCode();

                return hash;
            }
        }
    }
}