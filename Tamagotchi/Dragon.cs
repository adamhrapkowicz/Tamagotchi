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
            Console.WriteLine($"Your dragon has just been born! Please name it!");
            var inputName = Console.ReadLine();
            if (inputName != null && inputName != "")
            {
                Name = inputName;
            }
            else
            {
                Happiness -= 1;
            }
        }
    }
}