using System;

namespace Griffin.Repository
{
    /// <summary>
    /// Data layer transaction
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Save everything to the database
        /// </summary>
        void Commit();

        /// <summary>
        /// Cancel actions
        /// </summary>
        /// <remarks>
        /// Will be called by dispose if Commit have not been called.
        /// </remarks>
        void Rollback();

        /// <summary>
        /// Everything have been saved successfully.
        /// </summary>
        event EventHandler Committed;

        /// <summary>
        /// Nothing was saved.
        /// </summary>
        event EventHandler RolledBack;
    }
}