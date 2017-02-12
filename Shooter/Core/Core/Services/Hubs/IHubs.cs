using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Hubs
{
    public interface IHubs
    {
        void ConnectHub();
        void Command(List<string> Command);
    }
}
