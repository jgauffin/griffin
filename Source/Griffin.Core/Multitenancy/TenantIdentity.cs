using System.Collections.Generic;
using System.Security.Principal;

namespace Griffin.Core.Multitenancy
{
    /// <summary>
    /// Currently logged in user.
    /// </summary>
    public class TenantIdentity : IIdentity
    {
        private readonly IEnumerable<string> _roles;

        public TenantIdentity(ITenantUser user, IEnumerable<string> roles)
        {
            _roles = roles;
            User = user;
        }

        public ITenantUser User { get; private set; }

        public IEnumerable<string> Roles
        {
            get { return _roles; }
        }

        #region IIdentity Members

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name
        {
            get { return User.FullName; }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return "Any"; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        #endregion
    }
}