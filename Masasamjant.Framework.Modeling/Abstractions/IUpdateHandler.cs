using System.Security.Principal;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that wants to be notified when updated in non-volatile memory like database or file. 
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Invoked when model instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing update.</param>
        void OnUpdate(IIdentity? identity);
    }
}
