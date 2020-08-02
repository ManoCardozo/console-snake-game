using System;
using System.Text;
using SnakeGameConsole.Models;
using SnakeGameConsole.Interfaces;

namespace SnakeGameConsole.Services
{
    public class MainMenuService : IMainMenuService
    {

        public void ShowMenu(Game game)
        {
            var scene = BuildScene(game);
            Console.Write(scene);
            Console.ReadKey();
        }

        private string BuildScene(Game game)
        {
            var scene = new StringBuilder();

            for (int i = 0; i < game.Boundary.Height - 1; i++)
            {
                for (int j = 0; j < game.Boundary.Width - 1; j++)
                {
                    if (i == 0)
                    {
                        scene.Append("#");
                    }
                    else if (i == 3 && j == 0)
                    {
                        scene.Append("SNAKE GAME");
                    }
                    else if (i == 5 && j == 0)
                    {
                        scene.Append("Press any key to continue...");
                    }

                }

                scene.Append(Environment.NewLine);
            }

            return scene.ToString();
        }
    }
}
