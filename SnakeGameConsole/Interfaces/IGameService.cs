﻿using SnakeGameConsole.Models;

namespace SnakeGameConsole.Interfaces
{
    public interface IGameService
    {
        Game Start();
        void Tick(Game game);
        void Render(Game game);
        void AcceptInput(Game game);
        bool IsInPlay(Game game);
        void GameOver(Game game);
    }
}