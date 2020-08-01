using SnakeGameConsole.Models;

namespace SnakeGameConsole.Interfaces
{
    public interface ISnakeService
    {
        bool CanEat(Game game);
        void Eat(Snake snake);
        void EnsureSize(Snake snake);
        bool HasGoneOffScreen(Game game);
        bool HasTailCrashed(Snake snake);
        bool IsOnPosition(Snake snake, ScreenPosition position);
        void Move(Game game);
    }
}