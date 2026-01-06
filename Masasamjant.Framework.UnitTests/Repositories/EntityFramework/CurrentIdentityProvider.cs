using Masasamjant.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Masasamjant.Repositories.EntityFramework
{
    public class CurrentIdentityProvider : ICurrentIdentityProvider
    {
        public IIdentity? GetCurrentIdentity()
        {
            return null;
        }
    }
}
