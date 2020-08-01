using System;

namespace SnakeGameConsole.Exceptions
{
    public class OutOfBoardBoundaryException : Exception
    {
        public OutOfBoardBoundaryException() { }

        public OutOfBoardBoundaryException(string message) : base(message) { }

        public OutOfBoardBoundaryException(string message, Exception inner) : base(message, inner) { }
    }
}
