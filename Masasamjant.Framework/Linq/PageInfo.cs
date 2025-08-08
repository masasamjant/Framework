using Masasamjant.Serialization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents information of page of data.
    /// </summary>
    [TypeConverter(typeof(PageInfoConverter))]
    public sealed class PageInfo : IJsonSerializable
    {
        private int totalCount = -1;

        /// <summary>
        /// Initializes new instance of the <see cref="PageInfo"/> class.
        /// </summary>
        /// <param name="index">The 0-based page index.</param>
        /// <param name="size">The page size.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="index"/> is less than 0.
        /// -or-
        /// If value of <paramref name="size"/> is less than 0.
        /// </exception>
        public PageInfo(int index, int size)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "The index must be greater than or equal to 0.");

            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), size, "The size must be greater than or equal to 0.");

            Index = index;
            Size = size;
        }

        /// <summary>
        /// Initializes default instance of the <see cref="PageInfo"/> class.
        /// </summary>
        public PageInfo()
        { }

        /// <summary>
        /// Gets the 0-based page index.
        /// </summary>
        [JsonInclude]
        public int Index { get; internal set; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        [JsonInclude]
        public int Size { get; internal set; }

        /// <summary>
        /// Gets or sets the total count of items. Initial value is less than 0 to indicate that total count has not been set.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If value set is less than 0.</exception>
        [JsonInclude]
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(totalCount), value, "Total count must be greater than or equal to 0.");
                totalCount = value;
            }
        }

        /// <summary>
        /// Gets the total count of pages.
        /// </summary>
        [JsonIgnore]
        public int PageCount
        {
            get
            {
                if (Size == 0 || TotalCount == 0)
                    return 0;

                return Convert.ToInt32(Math.Round((double)TotalCount / Size, MidpointRounding.ToPositiveInfinity));
            }
        }

        /// <summary>
        /// Gets the <see cref="PageInfo"/> of the next page.
        /// </summary>
        /// <returns>A <see cref="PageInfo"/> of the next page.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="Index"/> of current page is <see cref="int.MaxValue"/> meaning maximum page is reached.</exception>
        public PageInfo Next()
        {
            if (Index == int.MaxValue)
                throw new InvalidOperationException("The maximum page has been reached.");

            return new PageInfo(Index + 1, Size);
        }

        /// <summary>
        /// Gets the <see cref="PageInfo"/> of the previous page or current instance, 
        /// if current <see cref="Index"/> is 0. 
        /// </summary>
        /// <returns>A <see cref="PageInfo"/> of the next page or current instance, if current <see cref="Index"/> is 0.</returns>
        public PageInfo Previous()
        {
            if (Index == 0)
                return this;

            return new PageInfo(Index - 1, Size);
        }
    }
}
