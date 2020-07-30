using System;
using System.Linq;
using System.Collections.Generic;
using SnakeGameConsole.Exceptions;

namespace SnakeGameConsole
{
    public class Snake
    {
        private int BodyLength { get; set; }
        private List<ScreenPosition> Body { get; set; }
        private ScreenPosition InitialPosition { get; set; }
        public ScreenPosition CurrentPosition { get; set; }

        public Snake(ScreenPosition initialPosition)
        {
            Body = new List<ScreenPosition>();
            BodyLength = 3;
            InitialPosition = initialPosition;
            CurrentPosition = new ScreenPosition { Left = 0, Top = 0 };
        }

        public void Move(ConsoleKeyInfo key)
        {
            if (Body.Any())
            {
                var lastPoint = Body.Last();
                CurrentPosition = new ScreenPosition
                {
                    Left = lastPoint.Left,
                    Top = lastPoint.Top
                };
            }
            else
            {
                CurrentPosition = new ScreenPosition
                {
                    Left = InitialPosition.Left,
                    Top = InitialPosition.Top
                };
            }

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    CurrentPosition.Left--;
                    break;
                case ConsoleKey.RightArrow:
                    CurrentPosition.Left++;
                    break;
                case ConsoleKey.UpArrow:
                    CurrentPosition.Top--;
                    break;
                case ConsoleKey.DownArrow:
                    CurrentPosition.Top++;
                    break;
            }

            if (HasGoneOffScreen())
            {
                throw new GoneOutOfBoundsScreenException();
            }

            if (HasTailCrashed())
            {
                throw new TailCrashException();
            }

            Body.Add(CurrentPosition);
        }

        public void Eat()
        {
            BodyLength++;
        }

        public void EnsureSize()
        {
            while (Body.Count() > BodyLength)
            {
                Body.Remove(Body.First());
            }
        }

        private bool HasTailCrashed()
        {
            return Body.Any(p => p.Left == CurrentPosition.Left && p.Top == CurrentPosition.Top);
        }

        private bool HasGoneOffScreen()
        {
            return CurrentPosition.Top < 0
                || CurrentPosition.Top > Console.WindowHeight
                || CurrentPosition.Left < 0
                || CurrentPosition.Left > Console.WindowWidth;
        }

        public bool IsOnPosition(int left, int top)
        {
            return Body.Any(p => p.Left == left && p.Top == top);
        }
    }
}
