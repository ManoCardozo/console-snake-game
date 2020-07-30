using System;
using System.Threading.Tasks;

namespace SnakeGameConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TODO: Game over screen
            //TODO: Menu screen, start, easy or hard etc

            //Console.SetWindowSize(60, 25);
            Console.CursorVisible = false;
            //Console.ForegroundColor = ConsoleColor.Green;

            var pos = new ScreenPosition
            {
                Left = 2,
                Top = 2
            };

            var snake = new Snake(pos);
            var game = new Game(snake);

            while (game.InPlay)
            {
                game.Tick();
                //await Task.Delay(50);
            }
        }
    }
}
