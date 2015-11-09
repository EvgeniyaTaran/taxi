using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Entities;

namespace Taxi.DataAccess
{
    public class EntityContext: IdentityDbContext<WebUser>
    {
    }
}
