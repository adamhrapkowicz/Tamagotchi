using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public sealed class DragonMessages
    {
        public string FeedingSuccess { get; set; } = string.Empty;

        public string Overfeeding { get; set; } = string.Empty;
        
        public string PettingSuccess { get; set; } = string.Empty;
        
        public string Overpetting { get; set; } = string.Empty;
        
        public string WrongKey { get; set; } = string.Empty;
    }
}
