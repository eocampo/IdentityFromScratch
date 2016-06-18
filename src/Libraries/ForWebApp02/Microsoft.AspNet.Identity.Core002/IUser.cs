using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUser
    {
        string Id { get; }        
        string UserName { get; set; }
    }
}
