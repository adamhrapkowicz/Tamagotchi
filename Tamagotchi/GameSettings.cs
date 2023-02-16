using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    internal sealed class GameSettings
    {
        public int Feedometer { get; set; } 

        public int Happiness { get; set; }

        public int MinValueOfFeedometer { get; set; }
        
        public int MinValueOfHappiness { get; set; }
        
        public double MaxAge { get; set; }

        public double GameStatusTimerInterval { get; set; }
        
        public double LifeProgressTimerInterval { get; set; }

    }
}
