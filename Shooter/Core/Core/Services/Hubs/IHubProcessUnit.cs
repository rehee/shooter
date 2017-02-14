using Core.Logic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Hubs
{
    public interface IHubProcessUnit
    {
        bool UnitActive { get; set; }
        void SetPlayerControl(string PlayerId);
        IPlayerController player { get; }
        void DoCommand(List<string> command);
        string PushMessage();
        void LeftRoom();
    }
}
