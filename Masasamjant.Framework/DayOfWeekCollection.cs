using Masasamjant.Collections.Abstractions;
using System.Collections;

namespace Masasamjant
{
    /// <summary>
    /// Represents collection of <see cref="DayOfWeek"/> values. 
    /// Collection does not allow duplicate values and can be set to read-only state.
    /// </summary>
    public sealed class DayOfWeekCollection : ICollection<DayOfWeek>, ISupportReadOnly
    {
        private readonly HashSet<DayOfWeek> weekDays;

        /// <summary>
        /// Initializes new default instance of the <see cref="DayOfWeekCollection"/> class.
        /// </summary>
        public DayOfWeekCollection()
        {
            weekDays = new HashSet<DayOfWeek>();
        }

        /// <summary>
        /// Initializes new instance of the <see cref="DayOfWeekCollection"/> class with the specified collection of days of the week.
        /// </summary>
        /// <param name="weekDays">The initial day of week to include. Duplicate values are added once.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="weekDays"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="weekDays"/> contains not defined value.</exception>
        public DayOfWeekCollection(IEnumerable<DayOfWeek> weekDays)
            : this()
        {
            if (weekDays == null)
                throw new ArgumentNullException(nameof(weekDays));
            
            foreach (var day in weekDays)
            {
                if (!Enum.IsDefined(day))
                    throw new ArgumentException("One or more values are not defined.", nameof(weekDays));

                this.weekDays.Add(day);
            }
        }

        /// <summary>
        /// Gets if or not collection is in read-only state.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Gets the count of days of the week in the collection.
        /// </summary>
        public int Count
        {
            get { return weekDays.Count; }
        }

        /// <summary>
        /// Creates new <see cref="DayOfWeekCollection"/> instance containing all days of the week.
        /// </summary>
        /// <returns>A <see cref="DayOfWeekCollection"/> with all days of the week.</returns>
        public static DayOfWeekCollection CreateAll()
        {
            var collection = new DayOfWeekCollection();
            
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                collection.Add(day);
            }
            
            return collection;
        }

        /// <summary>
        /// Add specified day of the week to the collection. 
        /// If the value is already in the collection, it is not added again.
        /// </summary>
        /// <param name="weekDay">The day of week to add.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="weekDay"/> is not defined.</exception>
        /// <exception cref="InvalidOperationException">If collection is in read-only state.</exception>
        public void Add(DayOfWeek weekDay)
        {
            CheckReadOnly();

            if (!Enum.IsDefined(weekDay))
                throw new ArgumentException("The value is not defined.", nameof(weekDay));
            
            weekDays.Add(weekDay);
        }

        /// <summary>
        /// Removes all days of the week from the collection. 
        /// After this operation, collection will be empty.
        /// </summary>
        /// <exception cref="InvalidOperationException">If collection is in read-only state.</exception>
        public void Clear()
        {
            CheckReadOnly();
            weekDays.Clear();
        }

        /// <summary>
        /// Check if collection contains specified day of the week.
        /// </summary>
        /// <param name="weekDay">The day of week.</param>
        /// <returns><c>true</c> if collection contains <paramref name="weekDay"/>; <c>false</c> otherwise.</returns>
        public bool Contains(DayOfWeek weekDay)
        {
            return weekDays.Contains(weekDay);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The array to copy items.</param>
        /// <param name="arrayIndex">The array index to start copy.</param>
        public void CopyTo(DayOfWeek[] array, int arrayIndex)
        {
            weekDays.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A enumerator to iterate collection.</returns>
        public IEnumerator<DayOfWeek> GetEnumerator()
        {
            return weekDays.GetEnumerator();
        }

        /// <summary>
        /// Remove specified day of week from the collection if it exists.
        /// </summary>
        /// <param name="weekDay">The day of week to remove.</param>
        /// <returns><c>true</c> if <paramref name="weekDay"/> exists and removed; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If collection is in read-only state.</exception>
        public bool Remove(DayOfWeek weekDay)
        {
            CheckReadOnly();

            if (!Enum.IsDefined(weekDay))
                return false;

            return weekDays.Remove(weekDay);
        }

        /// <summary>
        /// Sets collection to read-only state. After this method is called, collection cannot be modified.
        /// </summary>
        public void SetReadOnly()
        {
            IsReadOnly = true;
        }

        private void CheckReadOnly()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Collection is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
