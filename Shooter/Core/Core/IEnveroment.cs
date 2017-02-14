using Core.Logic.Controllers;
using Core.Services.Games;
using Core.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public interface IEnveroment
    {
        void SetGameService(IGameService gameService);

        IHubProcessUnit NewIHubProcessUnit();
        IPlayerController NewIPlayerController();
    }
}
