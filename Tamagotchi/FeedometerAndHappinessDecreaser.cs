using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class FeedometerAndHappinessDecreaser
    {
        public void DecreaseFeedometerAndHappines(Dragon dragon) 
        {
           while (true) 
            {
                dragon.Feedometer--;
                dragon.Happiness--;
                Thread.Sleep(60000);
            }
        }
    }
}
