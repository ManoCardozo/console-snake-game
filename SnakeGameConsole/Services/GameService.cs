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

            var game = new Game
            {
                InPlay = true,
                Score = 0,
                NextFoodUpdate = DateTime.MinValue,
                Boundary = new BoardBoundary
                {
                    Left = 0,
                    Right = 78,
                    Top = 1,
                    Bottom = 38
                },
                Snake = new Snake(new ScreenPosition
                {
                    Left = 10,
                    Top = 10
                })
            };

            Console.SetWindowSize(game.Boundary.Width, game.Boundary.Height);
            Console.SetBufferSize(game.Boundary.Width, game.Boundary.Height);

            return game;
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
            scene.Append("Score: " + game.Score);
            scene.Append(Environment.NewLine);

            var topBuffer = game.Boundary.Top;
            var leftBuffer = game.Boundary.Left;
            var height = game.Boundary.Bottom;
            var width = game.Boundary.Right;

            for (int i = topBuffer; i < game.Boundary.Height - 1; i++)
            {
                for (int j = 0; j < game.Boundary.Width - 1; j++)
                {
                    if (i <= topBuffer || i >= height || j <= leftBuffer || j >= width)
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

                //if (i != game.Boundary.Height)
                {
                    scene.Append(Environment.NewLine);
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
            /*game.Boundary.Left++;
            game.Boundary.Right--;
            game.Boundary.Top++;
            game.Boundary.Bottom--;*/
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
