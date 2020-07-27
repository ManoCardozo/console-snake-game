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

            Console.SetWindowSize(60, 25);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

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
                Left = 0,
                Top = 0
            };
        }

        private static void DrawScreen()
        {
            StringBuilder sceneBuilder = new StringBuilder();
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {

                }
            }

            //This causes screen to blink
            Console.Clear();

            Console.SetCursorPosition(Console.WindowWidth - 3, Console.WindowHeight - 1);
            Console.Write(_score);

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var point in points)
            {
                Console.SetCursorPosition(point.Left, point.Top);
                Console.Write('*');
            }

            Console.ForegroundColor = ConsoleColor.Red;
            if (_foodPosition != null)
            {
                Console.SetCursorPosition(_foodPosition.Left, _foodPosition.Top);
                Console.Write('X');
            }
        }

        private static void CleanUp()
        {
            while (points.Count() > _length)
            {
                points.Remove(points.First());
            }
        }

        private static DateTime nextUpdate = DateTime.MinValue;
        private static ScreenPosition _foodPosition = null;
        private static Random _random = new Random();
        private static bool UpdateGame()
        {
            if (DateTime.Now < nextUpdate)
            {
                return false;
            }

            if (_foodPosition == null)
            {
                _foodPosition = new ScreenPosition
                {
                    Left = _random.Next(Console.WindowWidth),
                    Top = _random.Next(Console.WindowHeight)
                };
            }

            if (_lastKey.HasValue)
            {
                Move(_lastKey.Value);
            }

            nextUpdate = DateTime.Now.AddMilliseconds(200 / (_score + 1));

            return true;
        }
    }
}
