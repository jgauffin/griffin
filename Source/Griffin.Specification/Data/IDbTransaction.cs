/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;

namespace Griffin.Data
{
    /// <summary>
    /// Database transaction.
    /// </summary>
    /// <remarks>
    /// Wrapper around ADO.NET transactions to be able to do stuff when the transaction either is committed or rolled back.
    /// </remarks>
    public interface IDbTransaction : IDisposable
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