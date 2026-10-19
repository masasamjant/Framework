using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant
{
    public ref struct CharSpanSegmentEnumerator
    {
        private CharSpanRangeEnumerator rangeEnumerator;
        
        public CharSpanSegmentEnumerator(ReadOnlySpan<char> characters, char separator)
        {
            this.rangeEnumerator = new CharSpanRangeEnumerator(characters, separator);
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public bool MoveNext()
        {
            if (rangeEnumerator.MoveNext())
            {
                var range = rangeEnumerator.Current;
                Current = range.Start.Equals(range.End) ? ReadOnlySpan<char>.Empty : rangeEnumerator.Characters[range];
                return true;
            }
            else
            {
                Current = ReadOnlySpan<char>.Empty;
                return false;
            }
        }

    }
}
