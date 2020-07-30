using System;

namespace SnakeGameConsole.Exceptions
{
    public class GoneOutOfBoundsScreenException : Exception
    {
        public GoneOutOfBoundsScreenException() { }

        public GoneOutOfBoundsScreenException(string message) : base(message) { }

        public GoneOutOfBoundsScreenException(string message, Exception inner) : base(message, inner) { }
    }
}
