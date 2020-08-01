using System;
using System.Text;
using SnakeGameConsole.Models;
using SnakeGameConsole.Interfaces;
using SnakeGameConsole.Exceptions;

namespace SnakeGameConsole.Services
{
    public class GameService : IGameService
    {
        private readonly ISnakeService snakeService;
        private readonly IBoardService boardService;

        public GameService(
            ISnakeService snakeService, 
            IBoardService boardService)
        {
            this.snakeService = snakeService;
            this.boardService = boardService;
        }

        public Game Setup()
        {
            //TODO: Game over screen
            //TODO: Menu screen, start, easy or hard etc

            Console.CursorVisible = false;
            //Console.SetWindowSize(GameConstants.ScreenWidth, GameConstants.ScreenHeight);
            //Console.ForegroundColor = ConsoleColor.Yellow;

            return new Game
            {
                InPlay = true,
                Score = 0,
                NextFoodUpdate = DateTime.MinValue,
                Settings = new BoardSettings
                {
                    Width = Console.WindowWidth,
                    Height = Console.WindowHeight
                },
                Snake = new Snake(new ScreenPosition
                {
                    Left = 2,
                    Top = 2
                })
            };
        }

        public void Tick(Game game)
        {
            var snake = game.Snake;

            AcceptInput(game);
            boardService.AddFood(game);
            snakeService.EnsureSize(snake);

            if (game.LastKey != null)
            {
                try
                {
                    snakeService.Move(game);
                }
                catch (Exception ex) when (ex is OutOfBoardBoundaryException || ex is TailCrashException)
                {
                    GameOver(game);
                }
            }

            if (snakeService.CanEat(game))
            {
                snakeService.Eat(snake);
                game.FoodPosition = null;
                game.Score++;
            }

            Render(game);
        }

        public void Render(Game game)
        {
            Console.SetCursorPosition(0, 0);
            var scene = new StringBuilder();
            Console.WriteLine("Score: " + game.Score);
            Console.SetCursorPosition(0, 1);

            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if (i == 1 || i == Console.WindowHeight - 2)
                    {
                        scene.Append("#");
                    }
                    else if (j == 0 || j == Console.WindowWidth - 2)
                    {
                        scene.Append("#");
                    }
                    else if (game.FoodPosition?.Left == j && game.FoodPosition?.Top == i)
                    {
                        scene.Append("X");
                    }
                    else if (snakeService.IsOnPosition(game.Snake, new ScreenPosition { Left = j, Top = i }))
                    {
                        scene.Append("*");
                    }
                    else
                    {
                        scene.Append(" ");
                    }
                }
            }
            Console.Write(scene);
        }

        public void AcceptInput(Game game)
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            game.LastKey = Console.ReadKey(true);
        }

        public bool IsInPlay(Game game)
        {
            return game.InPlay;
        }

        public void GameOver(Game game)
        {
            game.InPlay = false;
            Console.Clear();
            Console.WriteLine("Game over.");
            Console.ReadLine();
        }
    }
}
