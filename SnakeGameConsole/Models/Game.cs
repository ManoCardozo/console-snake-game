using System;

namespace SnakeGameConsole.Models
{
    public class Game
    {
        public int Score { get; set; }
        public ConsoleKeyInfo? LastKey { get; set; }
        public DateTime NextFoodUpdate { get; set; }
        public ScreenPosition FoodPosition { get; set; }
        public Snake Snake { get; set; }
        public GameSettings Settings { get; set; }
        public bool InPlay { get; set; }
    }
}
