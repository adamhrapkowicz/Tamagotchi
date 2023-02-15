namespace Tamagotchi
{
    internal class ConsoleManager : IConsoleManager
    {
        public string? GetDragonNameFromUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Your dragon has just been born! Please name it!");

            Console.ResetColor();

            var inputName = Console.ReadLine();

            return inputName;
        }

        public void WriteGameStatus(Dragon dragon)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Age: {dragon.Age:N2}            Happiness: {dragon.Happiness:D3}           Feedometer: {dragon.Feedometer:D3}      {dragon.AgeGroup} ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("To feed press 1, to pet press 2.");

            Console.ResetColor();
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 3);
        }

        public string GetCareInstructionsFromUser()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("                                                                           ");

            var careInstructionsFromUser = Console.ReadLine();

            Console.WriteLine("                                                                           ");
            Console.WriteLine("                                                                           ");

            Console.SetCursorPosition(0, 4);

            return careInstructionsFromUser;
        }

        public void WriteDragonsMessage(string dragonsMessage)
        {
            Console.WriteLine(dragonsMessage);
        }

        public void WriteDeclarationOfDeath(Dragon dragon)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{dragon.Name} has just died!");
        }
    }
}