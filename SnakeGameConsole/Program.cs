using System.Threading.Tasks;
using SnakeGameConsole.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SnakeGameConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = DependencyInjection.Setup();
            var gameService = serviceProvider.GetService<IGameService>();

            gameService.ShowMainMenu();

            var game = gameService.Start();

            while (gameService.IsInPlay(game))
            {
                gameService.Tick(game);
                await Task.Delay(50);
            }           
        }
    }
}
