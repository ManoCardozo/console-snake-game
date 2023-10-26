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
            var mainMenuService = serviceProvider.GetService<IMainMenuService>();
            var gameService = serviceProvider.GetService<IGameService>();
            var game = gameService.Start();

            mainMenuService.ShowMenu(game);

            while (gameService.IsInPlay(game))
            {
                gameService.Tick(game);
                await Task.Delay(50);
            }           
        }
    }
}
