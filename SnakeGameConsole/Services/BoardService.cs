using System;
using SnakeGameConsole.Models;
using SnakeGameConsole.Interfaces;

namespace SnakeGameConsole.Services
{
    public class BoardService : IBoardService
    {
        public void AddFood(Game game)
        {
            if (game.FoodPosition != null) return;
            if (DateTime.Now < game.NextFoodUpdate) return;

            var random = new Random();
            game.FoodPosition = new ScreenPosition
            {
                Left = random.Next(1, game.Settings.ScreenWidth - 1),
                Top = random.Next(1, game.Settings.ScreenHeight - 1)
            };
        }
    }
}
