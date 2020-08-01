using System;
using System.Linq;
using SnakeGameConsole.Models;
using SnakeGameConsole.Interfaces;
using SnakeGameConsole.Exceptions;

namespace SnakeGameConsole.Services
{
    public class SnakeService : ISnakeService
    {
        public void Move(Game game)
        {
            var snake = game.Snake;
            var key = game.LastKey.Value;

            if (snake.Body.Any())
            {
                var lastPoint = snake.Body.Last();
                snake.CurrentPosition = new ScreenPosition
                {
                    Left = lastPoint.Left,
                    Top = lastPoint.Top
                };
            }
            else
            {
                snake.CurrentPosition = new ScreenPosition
                {
                    Left = snake.InitialPosition.Left,
                    Top = snake.InitialPosition.Top
                };
            }

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    snake.CurrentPosition.Left--;
                    break;
                case ConsoleKey.RightArrow:
                    snake.CurrentPosition.Left++;
                    break;
                case ConsoleKey.UpArrow:
                    snake.CurrentPosition.Top--;
                    break;
                case ConsoleKey.DownArrow:
                    snake.CurrentPosition.Top++;
                    break;
            }

            if (HasGoneOffScreen(game))
            {
                throw new OutOfBoardBoundaryException();
            }

            if (HasTailCrashed(snake))
            {
                throw new TailCrashException();
            }

            snake.Body.Add(snake.CurrentPosition);
        }

        public void Eat(Snake snake)
        {
            snake.BodyLength++;
        }

        public bool CanEat(Game game)
        {
            var snake = game.Snake;

            if (game.FoodPosition == null) return false;

            return game.FoodPosition.Left == snake.CurrentPosition.Left && game.FoodPosition.Top == snake.CurrentPosition.Top;
        }

        public void EnsureSize(Snake snake)
        {
            while (snake.Body.Count() > snake.BodyLength)
            {
                snake.Body.Remove(snake.Body.First());
            }
        }

        public bool HasGoneOffScreen(Game game)
        {
            var position = game.Snake.CurrentPosition;
            var settings = game.Settings;

            return position.Top < 0
                || position.Top > settings.Height
                || position.Left < 0
                || position.Left > settings.Width;
        }

        public bool HasTailCrashed(Snake snake)
        {
            return snake.Body.Any(p => p.Left == snake.CurrentPosition.Left && p.Top == snake.CurrentPosition.Top);
        }

        public bool IsOnPosition(Snake snake, ScreenPosition position)
        {
            return snake.Body.Any(p => p.Left == position.Left && p.Top == position.Top);
        }
    }
}
