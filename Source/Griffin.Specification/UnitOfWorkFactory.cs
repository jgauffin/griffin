using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin
{
    /// <summary>
    /// Factory used to create pseudo unit of work items.
    /// </summary>
    /// <remarks>
    /// The items created by this factory isn't pure unit of work items since
    /// you cannot attach anything it it. Instead it's up to each implementing class
    /// to keep track of all items to save etc. 
    /// </remarks>
    public static class UnitOfWorkFactory
    {
    }
}
