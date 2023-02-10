using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        public string IncreaseFeedometer(Dragon dragon)
        {
            dragon.Feedometer += 50;

            var dragonsmessage = "That was yummy!";

            return dragonsmessage;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            dragon.Happiness += 50;

            var dragonsmessage = "I love you!";
            
            return dragonsmessage;
        }

        public void ProgressLifeSettings(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer--;
            dragon.Happiness--;
        }
    }
}
