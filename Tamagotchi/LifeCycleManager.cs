using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        public void ProgressLife(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer--;
            dragon.Happiness--;
        }
    }
}
