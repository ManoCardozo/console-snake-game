using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SnakeGameConsole.Interfaces;
using SnakeGameConsole.Services;

namespace SnakeGameConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = DependencyInjection.Setup();
            var gameService = serviceProvider.GetService<IGameService>();

            var game = gameService.Setup();

            while (gameService.IsInPlay(game))
            {
                gameService.Tick(game);
                await Task.Delay(50);
            }           
        }
    }
}
