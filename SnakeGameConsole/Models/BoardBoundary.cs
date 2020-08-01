using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGameConsole.Models
{
    public class BoardBoundary
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }

        public int Width => Right + 2;
        public int Height => Bottom + 2;
    }
}
