namespace Tamagotchi
{
    public class Dragon
    {
        public string Name { get; set; } = string.Empty;
        
        public int Age { get; set; } = 0;
        
        public bool IsAlive { get; set; } = true;
        
        public int Feedometer { get; set; } = 3;
        
        public int Happiness { get; set; } = 5;

        public void BirthOfTheDragon()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Your dragon has just been born! Please name it!");

            Console.ResetColor();
        
            var inputName = Console.ReadLine();
            
            if (inputName != null && inputName != "")
            {
                Name = inputName;
            }
            else
            {
                Happiness -= 1;
            }

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("To feed press 1, to pet press 2.");
            Console.WriteLine($"Value of Happiness is {Happiness} and value of Feedometer is {Feedometer}.");

            Console.ResetColor();
        }

        public void DeathOfTheDragon() 
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{Name} has just died!");
        }
    }
}