using System.Collections.Generic;

namespace SnakeGameConsole.Models
{
    public class Snake
    {
        public Snake(ScreenPosition initialPosition)
        {
            Body = new List<ScreenPosition>();
            BodyLength = 3;
            InitialPosition = initialPosition;
            CurrentPosition = new ScreenPosition { Left = 0, Top = 0 };
        }

        public int BodyLength { get; set; }
        public List<ScreenPosition> Body { get; set; }
        public ScreenPosition InitialPosition { get; set; }
        public ScreenPosition CurrentPosition { get; set; }
    }
}
