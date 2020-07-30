using System;

namespace SnakeGameConsole.Exceptions
{
    public class TailCrashException : Exception
    {
        public TailCrashException() { }

        public TailCrashException(string message) : base(message) { }

        public TailCrashException(string message, Exception inner) : base(message, inner) { }
    }
}
