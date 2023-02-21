namespace Tamagotchi
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
                if (Age <= 2)
                    return AgeGroup.Baby;

                else if (Age <= 10)
                    return AgeGroup.Child;

                else if (Age <= 20)
                    return AgeGroup.Teen;

                else if (Age <= 60)
                    return AgeGroup.Adult;

                else
                    return AgeGroup.Senior;
            }
        }

        public bool IsAlive { get; set; } = true;
        
        public int Feedometer { get; set; }
        
        public int Happiness { get; set; } 
    }
}