﻿namespace Tamagotchi
{
    internal interface IConsoleManager
    {
        void DragonsMessage(string dragonsmessage);

        string GetCareInstructionsFromUser();
        
        string? GetDragonNameFromUser();
        
        void PrintDeclarationOfDeath(Dragon dragon);
        
        void WriteGameStatus(Dragon dragon);
    }
}