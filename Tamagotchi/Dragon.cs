namespace Tamagotchi
{
    public class Dragon
    {
        public string Name { get; set; } = string.Empty;
        
        public double Age { get; set; }

        public AgeGroup AgeGroup
        {
            get
            {
                if (Age <= 10)
                    return AgeGroup.Baby;

                else if (Age <= 20)
                    return AgeGroup.Teen;

                else if (Age <= 60)
                    return AgeGroup.Adult;

                else
                    return AgeGroup.Senior;
            }
        }

        public bool IsAlive { get; set; } = true;
        
        public int Feedometer { get; set; } = 33;
        
        public int Happiness { get; set; } = 50;

        public int LifeIntervalTime { get; set; }

        public int FeedingIncrement { get; set; }

        public int PettingIncrement { get; set; }
    }
}