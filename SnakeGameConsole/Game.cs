using SnakeGameConsole.Exceptions;
using System;
using System.Text;

namespace SnakeGameConsole
{
    public class Game
    {
        public int Score { get; set; }
        private ConsoleKeyInfo? LastKey { get; set; }
        private DateTime NextFoodUpdate { get; set; }
        private ScreenPosition FoodPosition { get; set; }
        public bool InPlay { get; set; }

        private readonly Snake snake;

        public Game(Snake snake)
        {
            InPlay = true;
            Score = 0;
            NextFoodUpdate = DateTime.MinValue;

            this.snake = snake;
        }

        public void Tick()
        {
            AcceptInput();
            AddFood();
            snake.EnsureSize();

            if (LastKey != null)
            {
                try
                {
                    snake.Move(LastKey.Value);
                }
                catch (Exception ex) when (ex is GoneOutOfBoundsScreenException || ex is TailCrashException)
                {
                    GameOver();
                }
            }

            if (CanEat(snake.CurrentPosition))
            {
                snake.Eat();
                FoodPosition = null;
                Score++;
            }

            Render();
        }

        private void AcceptInput()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            LastKey = Console.ReadKey(true);
        }

        private bool CanEat(ScreenPosition currentPosition)
        {
            if (FoodPosition == null) return false;

            return FoodPosition.Left == currentPosition.Left && FoodPosition.Top == currentPosition.Top;
        }

        private void GameOver()
        {
            InPlay = false;
            Console.Clear();
            Console.WriteLine("Game over.");
            Console.ReadLine();
        }

        private void Render()
        {
            Console.SetCursorPosition(0, 0);
            //Console.Write("Score: " + Score);
            //Console.SetCursorPosition(0, 1);

            var scene = new StringBuilder();
            for (int i = 0; i < Console.WindowHeight - 1; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if (FoodPosition?.Left == j && FoodPosition?.Top == i)
                    {
                        scene.Append("X");
                    }
                    else if (snake.IsOnPosition(j, i))
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

        private void AddFood()
        {
            if (FoodPosition != null) return;
            if (DateTime.Now < NextFoodUpdate) return;

            var random = new Random();
            FoodPosition = new ScreenPosition
            {
                Left = random.Next(Console.WindowWidth),
                Top = random.Next(Console.WindowHeight)
            };


        }
    }
}
