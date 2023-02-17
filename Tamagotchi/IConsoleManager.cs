namespace Tamagotchi
{
    internal interface IConsoleManager
    {
        void WriteDragonsMessage(string dragonsMessage);

        string? GetCareInstructionsFromUser();
        
        string? GetDragonNameFromUser();
        
        void WriteDeclarationOfDeath(Dragon dragon);
        
        void WriteGameStatus(Dragon dragon);
    }
}