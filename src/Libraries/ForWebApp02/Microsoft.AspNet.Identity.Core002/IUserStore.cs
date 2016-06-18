using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserStore : IDisposable
    {
        void Create(IdentityUser user);
        void Update(IdentityUser user);
        void Delete(IdentityUser user);
        IdentityUser FindById(string userId);
        IdentityUser FindByName(string userName);

        bool CheckPassword(IdentityUser user, string password);
    }
}
