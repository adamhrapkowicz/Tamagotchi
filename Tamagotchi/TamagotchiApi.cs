using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class TamagotchiApi
    {
        private readonly LifeCycle _lifeCycle;
        private readonly LifeCycleManager _lifeCycleManager;

        public TamagotchiApi(LifeCycle lifeCycle, LifeCycleManager lifeCycleManager)
        {
            _lifeCycle = lifeCycle;
            _lifeCycleManager = lifeCycleManager;
        }

        public Guid StartGame(string name)
        {
            return _lifeCycleManager.CreateDragon(name);
        }

        public FeedDragonResponse FeedDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseFeedometer(dragonId);
        }

        public PetDragonResponse PetDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseHappiness(dragonId);
        }

        public Dragon GetGameStatus(Guid dragonId) 
        {
            return _lifeCycleManager.GetDragonById(dragonId);
        }
    }
}
