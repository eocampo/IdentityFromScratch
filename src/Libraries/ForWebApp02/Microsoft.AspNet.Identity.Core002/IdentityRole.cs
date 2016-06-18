using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class IdentityRole : IRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
