using SnakeGameConsole.Models;

namespace SnakeGameConsole.Interfaces
{
    public interface IGameService
    {
        void AcceptInput(Game game);
        void GameOver(Game game);
        bool IsInPlay(Game game);
        void Render(Game game);
        Game Start();
        void Tick(Game game);
    }
}