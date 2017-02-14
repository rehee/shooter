using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Hubs
{
    public interface IHubService
    {
        IHubProcessUnit GetUnit();
    }
}
