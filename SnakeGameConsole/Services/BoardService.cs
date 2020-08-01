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
                Left = random.Next(game.Boundary.Left + 1, game.Boundary.Right - 1),
                Top = random.Next(game.Boundary.Top + 1, game.Boundary.Bottom - 1)
            };
        }
    }
}
