namespace SnakeGameConsole.Models
{
    public class GameSettings
    {
        public int TopBuffer { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int BoardWidth => ScreenWidth - 2;
        public int BoardHeight => ScreenHeight - TopBuffer - 1;
    }
}
