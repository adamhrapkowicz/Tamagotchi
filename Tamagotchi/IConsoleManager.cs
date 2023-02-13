namespace Tamagotchi
{
    internal interface IConsoleManager
    {
        void DragonsMessage(string dragonsMessage);

        string GetCareInstructionsFromUser();
        
        string? GetDragonNameFromUser();
        
        void WriteDeclarationOfDeath(Dragon dragon);
        
        void WriteGameStatus(Dragon dragon);
    }
}