using System;

namespace SnakeGameConsole.Models
{
    public class Game
    {
        public Game()
        {
            InPlay = true;
            Score = 0;
            NextFoodUpdate = DateTime.MinValue;
            Settings = new BoardSettings
            {
                Width = Console.WindowWidth,
                Height = Console.WindowHeight
            };

            this.Snake = new Snake(new ScreenPosition
            {
                Left = 2,
                Top = 2
            });
        }

        public int Score { get; set; }
        public ConsoleKeyInfo? LastKey { get; set; }
        public DateTime NextFoodUpdate { get; set; }
        public ScreenPosition FoodPosition { get; set; }
        public Snake Snake { get; set; }
        public BoardSettings Settings { get; set; }
        public bool InPlay { get; set; }
    }
}
