using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IRole
    {
        string Id { get; }        
        string Name { get; set; }
    }
}
