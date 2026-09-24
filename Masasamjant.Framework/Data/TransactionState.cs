namespace Masasamjant.Data
{
    /// <summary>
    /// Defines states of a transaction.
    /// </summary>
    public enum TransactionState : int
    {
        /// <summary>
        /// The transaction is uncommitted. 
        /// Changes made to the data are not yet finalized and can still be rolled back.
        /// </summary>
        Uncommitted = 0,
        
        /// <summary>
        /// The transaction is committed. 
        /// Changes made to the data are finalized and cannot be rolled back.
        /// </summary>
        Committed = 1,
        
        /// <summary>
        /// The transaction is rolled back. 
        /// Changes made to the data are discarded and cannot be committed.
        /// </summary>  
        RolledBack = 2      
    }
}
