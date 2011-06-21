using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Griffin.Core.Multitenancy
{
    class TenantPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets tenant that has a logged in user.
        /// </summary>
        public ITenant Tenant { get; private set; }

        private readonly TenantIdentity _identity;

        public TenantPrincipal(ITenant tenant, TenantIdentity identity)
        {
            Tenant = tenant;
            _identity = identity;
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            return _identity.Roles.Any(r => r == role);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity
        {
            get { return _identity; }
        }
    }
}
