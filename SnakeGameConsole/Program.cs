using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGameConsole
{
    class Program
    {
        private static int _length = 3;
        private static int _score = 0;
        private static List<ScreenPosition> points = new List<ScreenPosition>();
        private static ConsoleKeyInfo? _lastKey = null;
        private static bool _inPlay = true;

        static void Main(string[] args)
        {
            //TODO: Game over screen
            //TODO: Menu screen, start, easy or hard etc

            //Console.SetWindowSize(60, 25);
            Console.CursorVisible = false;
            //Console.ForegroundColor = ConsoleColor.Green;

            var game = new Game();
            //game.Initialize();

            DrawScreen();
            while (_inPlay)
            {
                if (AcceptInput() || UpdateGame())
                {
                    DrawScreen();
                }
                Thread.Sleep(30);
            }
        }

        private static bool AcceptInput()
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }

            _lastKey = Console.ReadKey(true);

            return true;
        }

        private static void Move(ConsoleKeyInfo key)
        {
            ScreenPosition currentPosition;

            if (points.Any())
            {
                var lastPoint = points.Last();
                currentPosition = new ScreenPosition
                {
                    Left = lastPoint.Left,
                    Top = lastPoint.Top
                };
            }
            else
            {
                currentPosition = GetStartPosition();
            }

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentPosition.Left--;
                    break;
                case ConsoleKey.RightArrow:
                    currentPosition.Left++;
                    break;
                case ConsoleKey.UpArrow:
                    currentPosition.Top--;
                    break;
                case ConsoleKey.DownArrow:
                    currentPosition.Top++;
                    break;
            }

            if (OffScreen(currentPosition) || SnakeCrash(currentPosition))
            {
                GameOver();
            }

            if (HasFood(currentPosition))
            {
                Eat();
            }

            points.Add(currentPosition);
            CleanUp();
        }

        private static void Eat()
        {
            _length++;
            _score++;
            _foodPosition = null;
            nextUpdate = DateTime.Now.AddMilliseconds(2000 / (_score + 1));
        }

        private static bool HasFood(ScreenPosition currentPosition)
        {
            if (_foodPosition == null) return false;

            return _foodPosition.Left == currentPosition.Left && _foodPosition.Top == currentPosition.Top;
        }

        private static bool SnakeCrash(ScreenPosition currentPosition)
        {
            return points.Any(p => p.Left == currentPosition.Left && p.Top == currentPosition.Top);
        }

        private static bool OffScreen(ScreenPosition currentPosition)
        {
            return currentPosition.Top < 0
                || currentPosition.Top > Console.WindowHeight
                || currentPosition.Left < 0
                || currentPosition.Left > Console.WindowWidth;
        }

        private static void GameOver()
        {
            _inPlay = false;
            Console.Clear();
            Console.WriteLine("Game over.");
            Console.ReadLine();
        }

        private static ScreenPosition GetStartPosition()
        {
            return new ScreenPosition
            {
                Left = 1,
                Top = 2
            };
        }

        private static void DrawScreen()
        {
            Console.SetCursorPosition(0, 0);

            var scene = new StringBuilder();
            for (int i = 0; i < Console.WindowHeight - 1; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if (_foodPosition?.Left == j && _foodPosition?.Top == i)
                    {
                        scene.Append("X");
                    }
                    else if (points.Any(p => p.Left == j && p.Top == i))
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

        private static void CleanUp()
        {
            while (points.Count() > _length)
            {
                points.Remove(points.First());
            }
        }
        
        private static bool UpdateGame()
        {
            AddFood();

            if (_lastKey.HasValue)
            {
                Move(_lastKey.Value);
            }

            return true;
        }

        private static DateTime nextUpdate = DateTime.MinValue;
        private static ScreenPosition _foodPosition = null;
        private static Random _random = new Random();
        private static void AddFood()
        {
            if (_foodPosition == null)
            {
                if (DateTime.Now > nextUpdate)
                {
                    _foodPosition = new ScreenPosition
                    {
                        Left = _random.Next(Console.WindowWidth),
                        Top = _random.Next(Console.WindowHeight)
                    };
                }
            }
        }
    }
}
