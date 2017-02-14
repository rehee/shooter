using Core;
using Core.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Logic.Controllers;
using Core.Services.Games;

namespace Enveroments
{
    public class GameEnveroment: IEnveroment
    {
        private Type IHubProcessUnitType;
        private Type IPlayerControllerType;
        private IGameService gameService;

        public GameEnveroment(Type IHubProcessUnitType, Type IPlayerControllerType)
        {
            this.IHubProcessUnitType = IHubProcessUnitType;
            this.IPlayerControllerType = IPlayerControllerType;
        }

        public void SetGameService(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IHubProcessUnit NewIHubProcessUnit()
        {
            return (IHubProcessUnit)G.CreateGeneralType(IHubProcessUnitType, NewIPlayerController());
        }
        public IPlayerController NewIPlayerController()
        {
            return (IPlayerController)G.CreateGeneralType(IPlayerControllerType, gameService);
        }

        
    }
}
