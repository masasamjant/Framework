using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents a lock for an object instance, allowing only one instance of a specified type to hold the lock at a time.
    /// </summary>
    /// <typeparam name="T">The type of the object instance to be locked.</typeparam>
    internal class ObjectInstanceLock<T> where T : class
    {
        /// <summary>
        /// Gets a value indicating whether the lock is currently held by an instance of type <typeparamref name="T"/>.
        /// </summary>
        [MemberNotNullWhen(true, nameof(Owner))]
        public bool IsLocked { get; private set; }

        private T? Owner { get; set; }

        /// <summary>
        /// Locks this instance for the specified instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="instance">The instance of type <typeparamref name="T"/> to obtain the lock.</param>
        /// <exception cref="InvalidOperationException">If another instance has already obtained the lock.</exception>
        public void Lock(T instance)
        {
            if (IsLocked)
            {
                if (!ReferenceEquals(Owner, instance))
                    throw new InvalidOperationException("Another instance has already obtained the lock.");

                return;
            }

            IsLocked = true;
            Owner = instance;
        }

        /// <summary>
        /// Releases the lock held by the specified instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="instance">The instance of type <typeparamref name="T"/> to release the lock.</param>
        /// <exception cref="InvalidOperationException">If the instance attempting to release the lock does not own the lock.</exception>
        public void Release(T instance)
        {
            if (IsLocked)
            {
                if (ReferenceEquals(Owner, instance))
                {
                    IsLocked = false;
                    Owner = null;
                }
                else
                    throw new InvalidOperationException("The instance attempting to release the lock does not own the lock.");
            }
        }

        /// <summary>
        /// Check if specified instance of type <typeparamref name="T"/> is the owner of the lock.
        /// </summary>
        /// <param name="instance">The instance of type <typeparamref name="T"/> to check.</param>
        /// <returns><c>true</c> if locked and the specified instance is the owner of the lock; otherwise, <c>false</c>.</returns>
        public bool IsLockOwner(T instance)
        {
            if (IsLocked)
            {
                return ReferenceEquals(Owner, instance);
            }

            return false;
        }
    }
}
