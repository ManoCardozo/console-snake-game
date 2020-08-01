using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGameConsole.Models
{
    public class DefaultFood : IFood
    {
        public string Representation => "X";

        public void Apply(Game game)
        {
            
        }
    }
}
