using SnakeGameConsole.Services;
using SnakeGameConsole.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SnakeGameConsole
{
    public class DependencyInjection
    {
        public static ServiceProvider Setup() =>
            new ServiceCollection()
            .AddSingleton<IGameService, GameService>()
            .AddSingleton<ISnakeService, SnakeService>()
            .AddSingleton<IBoardService, BoardService>()
            .BuildServiceProvider();
    }
}
