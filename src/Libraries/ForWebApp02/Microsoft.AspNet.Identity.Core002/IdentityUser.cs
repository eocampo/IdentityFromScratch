using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class IdentityUser 
    {
        public IdentityUser() {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}
