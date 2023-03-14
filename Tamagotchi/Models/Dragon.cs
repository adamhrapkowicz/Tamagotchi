namespace Tamagotchi.Models
{
    public class Dragon
    {
        public Guid DragonId { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Age { get; set; }

        public AgeGroup AgeGroup
        {
            get
            {
                return Age switch
                {
                    <= 2 => AgeGroup.Baby,
                    <= 10 => AgeGroup.Child,
                    <= 20 => AgeGroup.Teen,
                    <= 60 => AgeGroup.Adult,
                    _ => AgeGroup.Senior
                };
            }
        }

        public bool IsAlive { get; set; } = true;

        public int Feedometer { get; set; }

        public int Happiness { get; set; }
    }
}