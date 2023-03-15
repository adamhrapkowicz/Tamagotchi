using Microsoft.Extensions.Options;
using System.Timers;
using Tamagotchi.Contracts;
using Tamagotchi.Controllers;
using TamagotchiData.Models;

namespace Tamagotchi.TamagotchiConsoleUi
{
    public class ConsoleManager : IHostedService
    {
        private static readonly System.Timers.Timer GameStatusTimer = new();

        private readonly TamagotchiApiController _tamagotchiApi;
        private readonly GameSettings _gameSettings;
        private readonly DragonMessages _dragonMessages;

        private Guid _dragonId;
        private string? _dragonName;

        public ConsoleManager(
            TamagotchiApiController tamagotchiApi, IOptions<GameSettings> gameSettings, IOptions<DragonMessages> dragonMessages)
        {
            _tamagotchiApi = tamagotchiApi;
            _gameSettings = gameSettings.Value;
            _dragonMessages = dragonMessages.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartConsoleGameWithNewDragon();
            ScheduleGameStatusTimer();
            LetUserCareForDragon();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            GameStatusTimer.Stop();
            return Task.CompletedTask;
        }

        private void ScheduleGameStatusTimer()
        {
            GameStatusTimer.Interval = _gameSettings.GameStatusTimerInterval;
            GameStatusTimer.Elapsed += DisplayGameStatus;
            GameStatusTimer.Start();
        }

        private void StartConsoleGameWithNewDragon()
        {
            _dragonName = GetDragonNameFromUser();
            _dragonId = _tamagotchiApi.StartGame(_dragonName);
        }

        private void EndGameAtDeathOfTheDragon()
        {
            StopAsync(CancellationToken.None).Wait();
            WriteDeclarationOfDeath();
            Environment.Exit(0);
        }

        private void DisplayGameStatus(object? sender, ElapsedEventArgs e)
        {
            var dragon = _tamagotchiApi.GetGameStatus(_dragonId);

            if (!dragon.IsAlive)
            {
                GameStatusTimer.Dispose();
                EndGameAtDeathOfTheDragon();
            }
            else
            {
                WriteGameStatus(dragon);
            }
        }

        private void LetUserCareForDragon()
        {
            var dragon = _tamagotchiApi.GetGameStatus(_dragonId);

            while (dragon.IsAlive)
            {
                var careInstructionsFromUser = GetCareInstructionsFromUser();
                ImplementUserInstructions(careInstructionsFromUser);
            }
        }

        private void ImplementUserInstructions(string? careInstructionsFromUser)
        {
            var dragonsMessage = careInstructionsFromUser switch
            {
                "1" => FeedDragon(),
                "2" => PetDragon(),
                _ => _dragonMessages.WrongKey
            };

            Console.WriteLine(dragonsMessage);
        }

        private string PetDragon()
        {
            var response = _tamagotchiApi.PetDragon(_dragonId);

            string dragonsMessage;

            if (response.Success)
            {
                dragonsMessage = _dragonMessages.PettingSuccess;
            }
            else if (response.Reason == PettingFailureReason.Overpetted)
            {
                dragonsMessage = _dragonMessages.Overpetting;
            }
            else
            {
                dragonsMessage = _dragonMessages.AlreadyDead;
            }

            return dragonsMessage;
        }

        private string FeedDragon()
        {
            var response = _tamagotchiApi.FeedDragon(_dragonId);
            string dragonsMessage;

            if (response.Success)
            {
                dragonsMessage = _dragonMessages.FeedingSuccess;
            }
            else if (response.Reason == FeedingFailureReason.Full)
            {
                dragonsMessage = _dragonMessages.Overfeeding;
            }
            else
            {
                dragonsMessage = _dragonMessages.AlreadyDead;
            }

            return dragonsMessage;
        }

        private void WriteDeclarationOfDeath()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{_dragonName} has just died!");
        }

        private static string GetDragonNameFromUser()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Your dragon has just been born! Please name it!");

                Console.ResetColor();

                var inputName = Console.ReadLine();

                if (!string.IsNullOrEmpty(inputName))
                    return inputName;
            }
        }

        private static void WriteGameStatus(Dragon dragon)
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

        private static string? GetCareInstructionsFromUser()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("                                                                           ");

            var careInstructionsFromUser = Console.ReadLine();

            Console.WriteLine("                                                                           ");
            Console.WriteLine("                                                                           ");

            Console.SetCursorPosition(0, 4);

            return careInstructionsFromUser;
        }
    }
}